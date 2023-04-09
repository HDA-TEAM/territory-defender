using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillTypeEnum{
    ActiveSkill,
    InsticSkill
}

public class NightBorneSkill : MonoBehaviour  //Client
{ 
    #region variables
        public SkillTypeEnum skillType;       
        private ISkillable iSkillable;
	#endregion

    public GameObject _prefabHellfire;
    public GameObject _prefabBurneSkill;

    //public HellFire _hellfire;
    [SerializeField] protected Player _target;

    [SerializeField] private Animator _animatorInsticSkill;
    [SerializeField] private Animator _animatorActiceSkill;
    
    public bool IsDeloyInsticSkill { get; private set; }
    public bool IsDeloyActiveSkill { get; private set; }

    [SerializeField] private SpriteRenderer _characterRenderer, _skillRenderer;
    [SerializeField] private Vector2 _pointerPosition { get; set; }

    public Transform circleOrigin;
    public float radius;

    private float delayActiveSkill = 2f;
    private bool attackBlocked;
    
    private static Context context = new Context();

    public void ResetIsDeloySkill()
    {
        IsDeloyActiveSkill = false;
        IsDeloyInsticSkill = false;
    }
    
    public void SetTarget(Player target){  
        //_hellfire._target = target;  
        _target = target; 
        IsDeloyActiveSkill = true;
    }

    void Update()
    {       
        if (!IsDeloyActiveSkill)
            return;

        
        if (_target != null && BossMelee._isBossEmployActiveSkill == true)
        {       
            setSurface();           
            if (attackBlocked) 
                return;

            attackBlocked = true;
            _animatorActiceSkill.SetTrigger("Attack");
                       
            context.DeloySkill(_prefabHellfire ,this.transform, _target);

            _animatorActiceSkill.SetTrigger("Attack");

            StartCoroutine(DelayAttack()); 
            UnityEngine.Object.Destroy(this.gameObject, 1f);      
        }  

        else if (_target != null && BossMelee._isBossEmployInsticSkill == true)
        {                   
            if (attackBlocked) 
                return;

            attackBlocked = true;
            //_animatorActiceSkill.SetBool("isDeployment", false);
            _animatorInsticSkill.SetBool("Attack", true);

            context.DeloySkill(_prefabBurneSkill,this.transform, _target);   
            
            _animatorInsticSkill.SetBool("Attack", false);
            UnityEngine.Object.Destroy(this.gameObject, 0.4f);      
        }            
    }

    public void SetTypeSkill(SkillTypeEnum _skillTypeEnum){
        skillType = _skillTypeEnum;
        //Debug.Log(skillType);
        #region Strategy
		switch(skillType){		
			case SkillTypeEnum.ActiveSkill:
				iSkillable = gameObject.AddComponent<ActiveSkill>();
                context.SetContext(iSkillable);  
				break;
				
			case SkillTypeEnum.InsticSkill:
				iSkillable = gameObject.AddComponent<InsticSkill> ();  
                context.SetContext(iSkillable);  
                       
				break;
				
			default:
				//do something
				break;
		}
		#endregion
    }

    private void setSurface()
    {
        Vector2 direction = (_pointerPosition - (Vector2)transform.position).normalized;
            transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }

        else if (direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;

        if(transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            _skillRenderer.sortingOrder = _characterRenderer.sortingOrder - 1;

        }else{
            _skillRenderer.sortingOrder = _characterRenderer.sortingOrder + 1; 
        }  
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            Debug.Log(collider.name);
        }
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delayActiveSkill);
        attackBlocked = false;
    }
}

public class Context
{
    private ISkillable _iSkillable;

    // Constructor
    public void SetContext(ISkillable iSkillable)
    {
        this._iSkillable = iSkillable;
    }

    public void DeloySkill(GameObject prefabs ,Transform transform, Player target){
        _iSkillable.FetchSkill(prefabs ,transform, target);
    }

}

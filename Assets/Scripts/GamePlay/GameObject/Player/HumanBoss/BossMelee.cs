using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Jobs;

public class BossMelee : Human 
{
    [SerializeField] private NightBorneSkill _skill;
    
    private Vector2 _pointerInput, _movementInput;

    public static bool _isBossEmploySkill;
    public static bool _isBossEmployInsticSkill;
    public static bool _isBossEmployActiveSkill;

    public static float delay = 2f;
    private bool attackBlocked;

    private float minHealth = 0;
    private float healthToDesploySkill = 500;

    new void Update()
    {     
        base.Update();
        
        if(_health <= minHealth){
            _isBossEmployActiveSkill = false;
            if (attackBlocked) 
                return;

            attackBlocked = true;

            _isBossEmploySkill = true; 
            _isBossEmployInsticSkill = true;
            this._animator.SetBool("isAttack",false);

            EmploySkill();

            // _isBossEmployInsticSkill = false;
            // _isBossEmploySkill = false;
             
        }
        else if((_health <= healthToDesploySkill && _health > minHealth) && _target != null)
        {
            if (attackBlocked) 
                return;

            Debug.Log("boss turn skill");
            attackBlocked = true;
                       
            _isBossEmploySkill = true; 
            _isBossEmployActiveSkill = true;

            this._animator.SetBool("isAttack",false);
            EmploySkill();   
        } 
    }

    protected void EmploySkill()
    {      
        if(_isBossEmploySkill)
        {   
            NightBorneSkill nbSkill = Instantiate(_skill, transform.position, Quaternion.identity);
            nbSkill.SetTarget(this._target); //boss target   
            if (_isBossEmployActiveSkill){
                // nbSkill.game
                nbSkill.SetTypeSkill(SkillTypeEnum.ActiveSkill);
            }
            else if(_isBossEmployInsticSkill){
                nbSkill.SetTypeSkill(SkillTypeEnum.InsticSkill);
            }
                     
            StartCoroutine(DelayAttack()); 
            Destroy(nbSkill.gameObject, 1f);
        }            
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
}

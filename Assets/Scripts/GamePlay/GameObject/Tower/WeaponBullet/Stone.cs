using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : WeaponBullet
{
    // Start is called before the first frame update
    [SerializeField]
    private float _rangeExplore;
    private bool _isExplored = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
    override protected void Rotate(){
        // do nothing
    }
    public override void Hitting()
    {
        

        if(_isExplored == false){
            DameByExplored();
        }

        _isExplored = true;
        _animator.SetBool("isExplored",_isExplored);
        Destroy(this.gameObject,0.4f);
    }

    private void DameByExplored(){
        GameObject[] instanceList = GameObject.FindGameObjectsWithTag("Human");
        foreach (GameObject item in instanceList)
        {
            Human human = item.GetComponent<Human>();
            if(Vector2.Distance(human.transform.position,transform.position) < _rangeExplore){
                human._health -= _dame;
            }
            if(human._health <= 0f){
                human.SelfDestroy();
            }
        }
    }
}

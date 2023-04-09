using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellFire : MonoBehaviour
{ 
    [SerializeField] private float _rangeExplore;
    
    public Player _target;

    private float _dame = 100f;
    private float _movingSpeed = 4f;

    protected float _angle;

    protected bool _isTargetExist = false;
    void Start()
    {
    }
    void Update()
    {
        if (_target != null)
        {
            _isTargetExist = true;
            Employing();
        }   
    }

    public void Employing()
    {
        
        // if (_isTargetExist != false)
        // {
        //     Debug.Log("Hell fire attack");
            //Moving();
            DameByExplored();
        //}
        
        //Destroy(this.gameObject, 1f);
    }

    void Moving(){
        Vector3 line = _target.transform.position - transform.position;
        // Debug.Log(transform.up);
        _angle = Vector3.SignedAngle(transform.up,line,transform.forward);
        Rotate();

        if(Vector2.Distance(transform.position,_target.transform.position) < 0.5f){
            DameByExplored();
        }
        transform.position = Vector2.Lerp(transform.position,_target.transform.position,_movingSpeed * Time.deltaTime);
        _movingSpeed += _movingSpeed * 0.01f;
    }

    virtual protected void Rotate(){
        transform.Rotate(0f,0f,_angle); 
    }
    
    private void DameByExplored()
    {
        GameObject[] instanceList = GameObject.FindGameObjectsWithTag("Evil");
        foreach (GameObject item in instanceList)
        {
            Evil evil = item.GetComponent<Evil>();
            if(Vector2.Distance(evil.transform.position, transform.position) < _rangeExplore)
            {
                evil.HealthReduce(_dame);
            }
            if(evil._health <= 0f)
            {
                evil.SelfDestroy();
            }
        }
    }
}

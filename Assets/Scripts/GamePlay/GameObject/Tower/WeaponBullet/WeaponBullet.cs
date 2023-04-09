using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBullet : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField] private AudioSource FireAudio;

    [SerializeField] protected Human _target;
    [SerializeField] protected float _movingSpeed;
    [SerializeField] protected Animator _animator;

    protected float _dame;

    protected float _angle;

    protected bool _isTargetExist = false;

    void Start()
    {
        _animator.SetFloat("movingSpeed", _movingSpeed);
    }

    // Update is called once per frame
    protected void Update()
    {
        SelfDestroy();
        if (_target != null)
        {
            _isTargetExist = true;
            Moving();
        }
    }
    void Moving(){

        Vector3 line = _target.transform.position - transform.position;
        _angle = Vector3.SignedAngle(transform.up,line,transform.forward);
        Rotate();

        if(Vector2.Distance(transform.position,_target.transform.position) < 0.5f){
            Hitting();
        }
        transform.position = Vector2.Lerp(transform.position,_target.transform.position,_movingSpeed * Time.deltaTime);
        _movingSpeed += _movingSpeed * 0.01f;
    }

    virtual protected void Rotate(){
        transform.Rotate(0f,0f,_angle); 
    }
    public virtual void Hitting(){
        _target.HealthReduce(_dame);
        Destroy(this.gameObject);
    }
    protected void SelfDestroy(){
        if((_isTargetExist == true && _target == null) || (_target != null && _target.gameObject.activeSelf == false)){
            Destroy(this.gameObject);
        }
    }
    public void SetTarget(Human human){
        _target = human;
    }
    public void SetDamage(float damage){
        _dame = damage;
    }

}

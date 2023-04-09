using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TowerWeapon : Tower
{
    [SerializeField] protected List<Human> _humans;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected AudioSource _audioSound;
    [SerializeField] protected AudioClip _attackSound;
    [SerializeField] protected RangeTower _rangeTower;

    protected bool _isAttack = false;
    protected float _waitTime;
    protected float _waitShotAnimation;
    protected float _delayPerShot;
    protected float _angle;
    protected float _timeToAttack;
   

    protected const float TIME_ATTACK = 0.4f / 1;
    protected const float WAIT_SHOT_ANIMATION = TIME_ATTACK   / (2f * 1);

    protected new void Start()
    {
        base.Start();
        SetTimeAttack();
        _rangeTower._humans = _humans;
    }

    protected void SetTimeAttack(){
        _delayPerShot = _attackSpeed /  1;

        _timeToAttack = TIME_ATTACK;

        _waitShotAnimation = _timeToAttack / 2f;

        _waitTime = 0f;
    }
    // Update is called once per frame
    protected new void Update() {
        base.Update();
        _rangeTower._range = _attackRange;
        AttackControler();    
    }

    // Atack controller : start
    protected void AttackControler(){
        if(_humans.Count != 0){
            if(_humans[0] != null && _rangeTower.IsTargetInRange()){
                if(_isAttack == true){
                    _timeToAttack-=Time.deltaTime;
                }
                if(_timeToAttack <= 0){
                    _isAttack = false;
                    _animator.SetBool("isAttack",_isAttack);
                    _timeToAttack = TIME_ATTACK;
                }

                _waitTime -= Time.deltaTime;

                if (_waitTime <= 0f)
                {
                    if(_humans[0]._health >= 0){
                        if(_isAttack == false){
                            _isAttack = true;
                            _animator.SetBool("isAttack",_isAttack);
                        }

                        Rotate();
                        _waitShotAnimation -= Time.deltaTime;
                        if(_waitShotAnimation <= 0 ){
                            Shoot();
                            _waitShotAnimation = WAIT_SHOT_ANIMATION;
                            _waitTime = _delayPerShot - WAIT_SHOT_ANIMATION;
                        }
                    }
                }

            }
            else{
                _humans.RemoveAt(0);
            }
        }
        else{
            _isAttack = false;
            _animator.SetBool("isAttack",_isAttack);
            _waitTime = _delayPerShot;
        }
    }
     // Atack controller : end

    protected virtual void Shoot(){
        // do something
    }

    protected virtual void Rotate(){
        Vector3 line = _humans[0].transform.position - transform.position;
        _angle = Vector3.SignedAngle(transform.up,line,transform.forward);
        transform.Rotate(0f,0f,_angle); 
    }


}

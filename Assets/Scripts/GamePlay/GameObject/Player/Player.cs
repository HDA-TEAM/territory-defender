using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction{
    Idle = 0,
    Attack = 1,
    Walk = 2
}
public class Player : MonoBehaviour
{   
    [Header("Player atribute")]
    [SerializeField] public int _playerId;
    [SerializeField] public float _rangeAttack;
    [SerializeField] public float _rangeDetecting;
    [SerializeField] public float _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _attackSpeed;
    [SerializeField] protected float _dame;

    [SerializeField] protected Animator _animator;
    [SerializeField] protected HealthBar _healthBar;
    [SerializeField] protected SpriteRenderer _spriteRenderer;

    [SerializeField] protected RangeWeapon _weapon;
    [SerializeField] protected GameObject _hurtBlood;
    [SerializeField] protected GameObject _dieSmoke;
    [SerializeField] protected PlayerWeapon _playerWeapon;
    

    [Header("Control atribute")]
    public int _unitType;
    public bool _isDie = false;
    public Player _target;

    protected float _attackAnimationTime = 0.85f;
    protected float _idleAnimationTime = 0.85f;
    protected float _walkAnimationTime = 0.85f;
    protected float _waitingTime = 0f;
    protected int _playerAction = 0;


    protected Vector2 _targetPositon;
    protected Vector2 _lastPosition;

    protected const float TIME_ATTACK = 0.4f / 1;
    protected const float WAIT_SHOT_ANIMATION = TIME_ATTACK   / (2f * 1);

    public int _maxMeleeCompetitor;
    public int _meleeCompetitorCounter = 0;
    protected int _playerSide;

    protected void Start()
    {

        SetUpPlayerSide();

        SetUpUnitType();

        _healthBar = GetComponent<HealthBar>();
        // _animator.speed = 1.2f;
    }
    public void SetTarget(Player target){
        _target = target;
    }

    protected void Update(){
        // Attack or Moving to target
        if(HealthControl() == false ){
            SelfDestroy();
        }
        else{
            Rotate();
            if(IsTargetActive()){
                AttackTargetProcess();
            }
            // Moving default or listen tower troop control following assemble point
            else{
                _animator.SetBool("isAttack",false);
                if(transform.hasChanged){
                    transform.hasChanged = false;
                    _animator.SetFloat("movingSpeed",0.5f);
                }
                else{
                    _animator.SetFloat("movingSpeed",-0.5f);
                }
                
                MoveDefault();
            }
        }
        
    }
    bool IsTargetActive(){
        
        if(_target == null || _target.gameObject.activeSelf == false ){
            _target = null;
            return false;
        }
        return true;
    }
    bool HealthControl(){
        if(_healthBar.CurrentHealth > _health && _health > 0){
            CreateHurtObject();
        }
        _healthBar.CurrentHealth = _health;
        if(_health <= 0){
            return false;
        }
        return true;
    }
    void AttackTargetProcess(){
        if(Vector2.Distance(this.transform.position,_target.transform.position) > _rangeAttack){
            _animator.SetBool("isAttack",false);
            MoveToTarget();
        }
        else{
            
            AttackAnimator();
        }
    }

    #region Set up on start
    void SetUpUnitType(){
        if(_weapon != null){
            _unitType = (int)UnitType.Range;
        }
        else{
            _unitType = (int)UnitType.Melee;
        }
    }

    void SetUpPlayerSide(){
        if(gameObject.tag == "Enemy"){
            _playerSide = (int)PlayerSide.Enemy;
        }
        else if(gameObject.tag == "Ally"){
            _playerSide = (int)PlayerSide.Ally;
        }
    }


    #endregion Set up for start

    protected void MoveToTarget(){
        _animator.SetFloat("movingSpeed",0.5f);
        transform.position = Vector2.MoveTowards(transform.position,_target.transform.position,Time.deltaTime * _speed);
    }
 
    virtual protected void MoveDefault(){

    }
  

    #region Acttack control 

    protected void AttackAnimator(){
        _animator.SetFloat("movingSpeed",-0.5f);

        _waitingTime -= Time.deltaTime;
        if(_waitingTime <= _attackAnimationTime + _idleAnimationTime && _waitingTime > _idleAnimationTime){
            if(_playerAction != (int)PlayerAction.Attack){
                _playerAction = (int)PlayerAction.Attack;
                _animator.SetBool("isAttack",true);
                
            }
        }
        else if(_waitingTime <= _idleAnimationTime && _waitingTime > 0){
            if(_playerAction != (int)PlayerAction.Idle){
                Attack();
                _playerAction = (int)PlayerAction.Idle;
                _animator.SetBool("isAttack",false);
            }
        }
        else{
            _waitingTime = _attackAnimationTime + _idleAnimationTime;
        }
    }
  
    void Attack(){
        _target.HealthReduce(_dame);
    }
    
    
    #endregion Attack control

    public void HealthReduce(float healthReduce){
        _health -= healthReduce;
    }

   
    virtual public void SelfDestroy(){
        CreateDieObject();
        ResetPlayer();
    }       

    void CreateDieObject(){
        GameObject instance = Instantiate(_dieSmoke,transform.position,Quaternion.identity);
        Destroy(instance,0.25f);
    }
    void CreateHurtObject(){
        GameObject instance = Instantiate(_hurtBlood,this.transform.position,Quaternion.identity);
        Destroy(instance,0.25f);
    }
    
    protected void Rotate(){
        if(transform.position.x > _lastPosition.x){
            _spriteRenderer.flipX = false;
        }
        else if(transform.position.x < _lastPosition.x){
            _spriteRenderer.flipX = true;
        }
        _lastPosition = this.transform.position;
    }
    private void ResetPlayer(){
        _target = null;
        _health = _healthBar._maxHealth;;
    }
 

}

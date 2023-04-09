using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerTroop : Tower
{
    [SerializeField] private int _numberEvils;
    [SerializeField] private Evil _prefab;
    [SerializeField] private List<Evil> _evils = new List<Evil>();
    
    [SerializeField] private AssemblePoint _assemblePoint;
    [SerializeField] protected RangeTower _rangeTower;
    private Vector2 _currentAssemblePoint;
    public float _timeRevive = 5f;
    public bool _settingAssemblePoint = false;
   
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        InitEvil();
        SetEvilAssemblePoint();
        _rangeTower._range = _attackRange;
    }
    void SetEvilAssemblePoint(){
        for (int i = 0; i < _evils.Count; i++)
        {
            _evils[i]._assemblePoint =  new Vector2(
                _assemblePoint.transform.position.x + Random.value * 2,
                _assemblePoint.transform.position.y + Random.value * 2
            );
        }
    }
  
    void InitEvil(){
        
        for (int i = 0; i < _numberEvils; i++)
        {
            _evils.Add(Instantiate(_prefab));
            _evils[i].transform.position = transform.position;
        }
    }
    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
        _rangeTower._range = _attackRange;
        _assemblePoint._rangeToFire = _attackRange;
        if(_currentAssemblePoint != new Vector2(_assemblePoint.transform.position.x,_assemblePoint.transform.position.y)){
            _currentAssemblePoint =_assemblePoint.transform.position;
            SetEvilAssemblePoint();
        }
        RunReviveProcess();
    }


    void RunReviveProcess(){
        foreach (var item in _evils)
        {
            if(item._isDie == true){
                item._timeReviveCounter -= Time.deltaTime;
            }
            if(item._timeReviveCounter <= 0){
                SetupAfterRevive(item);
            }
        }
    }
  
    void SetupAfterRevive(Evil evil){
        
        evil.gameObject.SetActive(true);
        evil._isDie = false;
        evil._timeReviveCounter = _timeRevive;
        evil.transform.position = transform.position;
        BattleControler.AddEvil(evil);
    }
    
}

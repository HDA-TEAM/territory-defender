using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Player
{   
    [SerializeField] protected LineRenderer _route;
    [SerializeField] private int _earningCoin;
    private int _currentPositionIndex;
    private int _lengthOfPath;
    new void Start() {
        base.Start();
        _maxMeleeCompetitor = 1; 
       _currentPositionIndex = 0;
        _lengthOfPath = _route.positionCount; 
        BattleControler.AddHuman(this);
    }
    public void SetRoute(LineRenderer route){
        _route = route;
        transform.position = _route.GetPosition(0);
    }

    override protected void MoveDefault(){
        if(Vector2.Distance(transform.position,_route.GetPosition(_currentPositionIndex)) < 0.1f ){
            _currentPositionIndex += 1;
            ArrivedDestination();
        }
        transform.position = Vector2.MoveTowards(transform.position,_route.GetPosition(_currentPositionIndex),Time.deltaTime * _speed);
    }
    // void SetMoveDefaultPosition(int index){
    //     _currentPositionIndex = index;
    //     transform.position = _route.GetPosition(_currentPositionIndex);
    // }
    void ArrivedDestination(){
        if(_currentPositionIndex == _lengthOfPath){
            _currentPositionIndex = 0;
            transform.position = _route.GetPosition(_currentPositionIndex);
            ObjectPooler.ReturnToPool(gameObject);
            GameControl.ReduceHealth();
        }
    }

    

    override public void SelfDestroy(){
        base.SelfDestroy();
        // Put back to pool
        ObjectPooler.ReturnToPool(gameObject);

        _currentPositionIndex = 0; // reset position
        //Earning Coin
        GameControl.IncreaseCoin(_earningCoin);
    }       
    
}

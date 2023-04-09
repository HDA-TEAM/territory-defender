using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil : Player
{   
    public Vector2 _assemblePoint {set;get;}
    public float _timeReviveCounter;

    new void Start() {
        base.Start();
        _maxMeleeCompetitor = 1;
        BattleControler.AddEvil(this);
    }
    override protected void MoveDefault(){
        transform.position = Vector2.MoveTowards(transform.position,_assemblePoint,Time.deltaTime * _speed);
    }

    override public void SelfDestroy(){
        base.SelfDestroy();

        _isDie = true;
        _target = null;
        gameObject.SetActive(false);
    }       
}

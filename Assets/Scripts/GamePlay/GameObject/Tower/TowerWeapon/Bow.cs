using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : TowerWeapon
{
    [SerializeField] private Arrow _prefab;
    new void Start(){
        base.Start();
    }

    new void Update() { 
        base.Update(); 
    }

    protected override void Rotate(){
        base.Rotate();
    }
    protected override void Shoot(){
        Arrow arrow = Instantiate(_prefab,transform.position,Quaternion.identity);
        arrow.SetTarget(_humans[0]);
        arrow.SetDamage(_attackDame);
        _audioSound.PlayOneShot(_attackSound);
    }

    
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : TowerWeapon
{
     [SerializeField] private Stone _prefab;
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
        Stone stone = Instantiate(_prefab,transform.position,Quaternion.identity);
        stone.SetTarget(_humans[0]);
        stone.SetDamage(_attackDame);
        _audioSound.PlayOneShot(_attackSound);
    }

    
    
}
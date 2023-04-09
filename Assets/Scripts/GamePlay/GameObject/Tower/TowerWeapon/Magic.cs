using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : TowerWeapon
{
    [SerializeField] private Bolt _prefab;
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
        Bolt bolt = Instantiate(_prefab,transform.position,Quaternion.identity);
        bolt.SetTarget(_humans[0]);
        bolt.SetDamage(_attackDame);
        _audioSound.PlayOneShot(_attackSound);
    }
}

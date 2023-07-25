using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializeField]
    private GameObject bulletPooling;
    public GameObject ObjectBulletPooling () =>  bulletPooling;
}

using GamePlay.Scripts.Tower;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public enum UnitId
{
    Ally = 100,
    Enemy = 200,
    EnemyShieldMan = 201,
    Arrow = 1000,
    WaterBomb = 1001
}


[Serializable]
public struct PoolingComposite
{
    public int initNumber;
    [FormerlySerializedAs("poolingType")]
    public UnitId _objectType;
    public GameObject prefab;
}

public class PoolingController : SingletonBase<PoolingController>
{
    [SerializeField] private List<UnitPooling> poolingList;
    private Dictionary<UnitId, PoolingBase> dictPooling = new Dictionary<UnitId, PoolingBase>();

    public override void Awake()
    {
        base.Awake();
        SetUp();
    }
    private void SetUp()
    {
        foreach (var pooling in poolingList)
        {
            PoolingComposite poolingComposite = pooling.UnitPool;
            pooling.InitPoolWithParam(poolingComposite.initNumber, poolingComposite.prefab, pooling.gameObject);
            dictPooling.Add(poolingComposite._objectType, pooling);
        }
    }
    public PoolingBase GetPooling(UnitId unitId)
    {
        dictPooling.TryGetValue(unitId, out PoolingBase poolingBase);
        return poolingBase;
    }
    public GameObject SpawnObject(UnitId objectType, Vector3 position = new Vector3())
    {
        GameObject go = GetPooling(objectType).GetInstance();
        go.SetActive(transform);
        go.transform.position = position;
        return go;
    }
}

using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitId
{
    AllyWarrior = 100,
    EnemyShieldMan = 200,
    
    EnemyShieldMans = 201,
    
    //Hero
    TrungTrac = 400,
    Tower = 300,

    //Projectile
    None = 0,
    Arrow = 1000,
    WaterBomb = 1001
}
public enum UnitSideId
{
    Ally = 1,
    Enemy = 2,
    Tower = 3,
}

public class PoolingController : SingletonBase<PoolingController>
{
    [SerializedDictionary("TowerId", "TowerType")]
    [SerializeField] private SerializedDictionary<UnitId,GameObject> _dictPoolingPrefab = new SerializedDictionary<UnitId, GameObject>();
    [SerializeField] private UnitPooling _poolingPrefab;
    [SerializeField] private List<UnitPooling> poolingList;
    private Dictionary<UnitId, PoolingBase> dictPooling = new Dictionary<UnitId, PoolingBase>();

    public override void Awake()
    {
        base.Awake();
        SetUp();
    }
    private void SetUp()
    {
        // foreach (var pooling in poolingList)
        // {
        //     PoolingComposite poolingComposite = pooling.UnitPool;
        //     pooling.InitPoolWithParam(poolingComposite.initNumber, poolingComposite.prefab, pooling.gameObject);
        //     dictPooling.Add(poolingComposite._objectType, pooling);
        // }
    }
    private PoolingBase GetPooling(UnitId objectType)
    {
        dictPooling.TryGetValue(objectType, out PoolingBase poolingBase);
        return poolingBase;
    }
    public GameObject SpawnObject(UnitId objectType, Vector3 position = new Vector3())
    {
        if (!IsPoolExist(objectType))
        {
            CreateNewPool(objectType);
        }
        GameObject go = GetPooling(objectType).GetInstance();
        go.SetActive(transform);
        go.transform.position = new Vector3(position.x,position.y,0);
        return go;
    }
    private bool IsPoolExist(UnitId objectType)
    {
        return dictPooling.ContainsKey(objectType);
    }
    private void CreateNewPool(UnitId objectType)
    {
        _dictPoolingPrefab.TryGetValue(objectType, out GameObject prefab);
        UnitPooling unitPooling = Instantiate(_poolingPrefab);
        unitPooling.name = prefab.name + "Pooling";
        unitPooling.transform.SetParent(transform);
        unitPooling.InitPoolWithParam(3,prefab, unitPooling.gameObject);
        dictPooling.Add(objectType, unitPooling);
    }
}

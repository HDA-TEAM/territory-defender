using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitSideId
{
    Ally = 1,
    Enemy = 2,
    Tower = 3,
    Hero = 4,
}

public static class UnitId
{
    public enum Ally
    {
        Warrior = 100,
    }

    public enum Enemy
    {
        ShieldMan = 200,
    }

    public enum Hero
    {
        TrungTrac = 400,
    }

    public enum Tower
    {
        WarriorTower = 300,
    }
    public enum Projectile
    {
        None = 1000,
        Arrow = 1001,
        WaterBomb = 1002,
    }
}

public class PoolingController : SingletonBase<PoolingController>
{
    [SerializedDictionary("UnitId", "UnitPrefab")]
    [SerializeField] private SerializedDictionary<string,GameObject> _dictPoolingPrefab = new SerializedDictionary<string, GameObject>();
    [SerializeField] private UnitPooling _poolingPrefab;
    private readonly Dictionary<string, PoolingBase> _dictPooling = new Dictionary<string, PoolingBase>();

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
    private PoolingBase GetPooling(string objectType)
    {
        _dictPooling.TryGetValue(objectType, out PoolingBase poolingBase);
        return poolingBase;
    }
    public GameObject SpawnObject(string objectType, Vector3 position = new Vector3())
    {
        Debug.Log("objectType " + objectType);
        if (!IsPoolExist(objectType))
        {
            CreateNewPool(objectType);
        }
        GameObject go = GetPooling(objectType).GetInstance();
        go.SetActive(transform);
        go.transform.position = new Vector3(position.x,position.y,0);
        return go;
    }
    private bool IsPoolExist(string objectType)
    {
        return _dictPooling.ContainsKey(objectType);
    }
    private void CreateNewPool(string objectType)
    {
        _dictPoolingPrefab.TryGetValue(objectType, out GameObject prefab);
        UnitPooling unitPooling = Instantiate(_poolingPrefab);
        unitPooling.name = prefab.name + "Pooling";
        unitPooling.transform.SetParent(transform);
        unitPooling.InitPoolWithParam(3,prefab, unitPooling.gameObject);
        _dictPooling.Add(objectType, unitPooling);
    }
}

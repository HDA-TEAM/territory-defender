using AYellowpaper.SerializedCollections;
using GamePlay.Scripts.GamePlay;
using System.Collections.Generic;
using UnityEngine;

public static class UnitId
{
    public enum BaseId
    {
        Ally = 1,
        Enemy = 2,
        Hero = 3,
        Tower = 4,
    }

    public static bool IsUnitInBaseId(int unitId, BaseId baseId) => unitId == (int)baseId;

    public enum Ally
    {
        Warrior = 100,
    }

    public enum Enemy
    {
        ShieldMan = 200,
        ArcherMan = 210,
        AssassinMan = 220,
    }

    public enum Hero
    {
        TrungTrac = 400,
        TrungNhi = 410,
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

public class PoolingController : GamePlaySingletonBase<PoolingController>
{
    [SerializedDictionary("UnitId", "UnitPrefab")]
    [SerializeField] private SerializedDictionary<string,GameObject> _dictPoolingPrefab = new SerializedDictionary<string, GameObject>();
    [SerializeField] private UnitPooling _poolingPrefab;
    private readonly Dictionary<string, PoolingBase> _dictPooling = new Dictionary<string, PoolingBase>();

    private void OnRestart()
    {
        foreach (var pooling in _dictPooling)
        {
            PoolingBase removingPool = pooling.Value;
            Destroy(removingPool.gameObject);    
        }
        _dictPooling.Clear();
        Destroy(gameObject);
    }
    private PoolingBase GetPooling(string objectType)
    {
        _dictPooling.TryGetValue(objectType, out PoolingBase poolingBase);
        return poolingBase;
    }
    public GameObject SpawnObject(string objectType, Vector3 position = new Vector3())
    {
        if (!IsPoolExist(objectType))
            CreateNewPool(objectType);
        
        GameObject go = GetPooling(objectType).GetInstance();
        go.SetActive(true);
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
    public void ReturnPool(GameObject gameObject,UnitId.BaseId sideId)
    {
        gameObject.SetActive(false);
        if (sideId == UnitId.BaseId.Enemy)
        {
            InGameStateController.Instance.CheckingStageSuccess();
        }
    }
    public override void SetUpNewGame()
    {
        
    }
    public override void ResetGame()
    {
        OnRestart();
    }
}

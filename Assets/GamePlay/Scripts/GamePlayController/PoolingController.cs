using AYellowpaper.SerializedCollections;
using Common.Scripts.Pooler;
using CustomInspector;
using GamePlay.Scripts.GamePlay;
using GamePlay.Scripts.Pooling;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.GamePlayController
{
    public struct OnSpawnObjectPayload
    {
        public bool ActiveAtSpawning;
        public string ObjectType;
        public Vector3 InitPosition;
        public Action<GameObject> OnSpawned;
    }
    public struct OnReturnObjectToPoolPayload
    {
        public GameObject GameObject;
        public string ObjectType;
    }
    
    public class PoolingController : GamePlayMainFlowBase
    {
        [Button("OnBuildPools")]
        [SerializeField] private UnitPoolBuilding _unitPoolBuilding;
        [SerializedDictionary("UnitId", "UnitPrefab")]
        [SerializeField] private SerializedDictionary<string, GameObject> _dictPoolingPrefab;
        [SerializeField] private UnitPooling _poolingPrefab;
        private readonly Dictionary<string, PoolingBase> _dictPooling = new Dictionary<string, PoolingBase>();

        protected override void Awake()
        {
            base.Awake();
            Messenger.Default.Subscribe<OnSpawnObjectPayload>(OnSpawnObject);
            Messenger.Default.Subscribe<OnReturnObjectToPoolPayload>(OnReturnPool);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            Messenger.Default.Unsubscribe<OnSpawnObjectPayload>(OnSpawnObject);
            Messenger.Default.Unsubscribe<OnReturnObjectToPoolPayload>(OnReturnPool);
        }
        private void OnRestart()
        {
            // foreach (var pooling in _dictPooling)
            // {
            //     PoolingBase removingPool = pooling.Value;
            //     Destroy(removingPool.gameObject);
            // }
            // _dictPooling.Clear();
            // Destroy(gameObject);
        }
        private PoolingBase GetPooling(string objectType)
        {
            _dictPooling.TryGetValue(objectType, out PoolingBase poolingBase);
            return poolingBase;
        }
        private void OnSpawnObject(OnSpawnObjectPayload onSpawnObjectPayload)
        {
            if (!IsPoolExist(onSpawnObjectPayload.ObjectType))
                CreateNewPool(onSpawnObjectPayload.ObjectType);

            GameObject go = GetPooling(onSpawnObjectPayload.ObjectType).GetInstance();
            go.SetActive(onSpawnObjectPayload.ActiveAtSpawning);
            go.transform.position = new Vector3(onSpawnObjectPayload.InitPosition.x, onSpawnObjectPayload.InitPosition.y, 0);
            onSpawnObjectPayload.OnSpawned?.Invoke(go);
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
            unitPooling.InitPoolWithParam(3, prefab, unitPooling.gameObject);
            _dictPooling.Add(objectType, unitPooling);
        }
        private void OnReturnPool(OnReturnObjectToPoolPayload objectToPoolPayload)
        {
            objectToPoolPayload.GameObject.SetActive(false);
        }

#if UNITY_EDITOR
        private void OnBuildPools()
        {
            _dictPoolingPrefab = _unitPoolBuilding.BuildPoolingDictionary();
        }
#endif
        protected override void OnSetupNewGame(SetUpNewGamePayload setUpNewGamePayload)
        {
        }
        protected override void OnResetGame(ResetGamePayload resetGamePayload)
        {
            OnRestart();
        }
    }
}

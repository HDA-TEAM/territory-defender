using Common.Scripts;
using Common.Scripts.Utilities;
using Cysharp.Threading.Tasks;
using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.GamePlayController;
using GamePlay.Scripts.Tower.TowerKIT;
using SuperMaxim.Messaging;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Character.TowerBehaviour
{
    public class TroopTowerBehaviour : TowerBehaviourBase
    {
        // Default 3 units
        private const int MaxAllyCount = 3;
        private const float MinPerUnitDistance = 0.5f;
        [SerializeField] private UnitId.Ally _unitSpawnId;
        [SerializeField] private float _cooldownReviveUnit;
        [SerializeField] private List<UnitBase> _allyUnits = new List<UnitBase>();
        [SerializeField] private float _campingRange;
        [SerializeField] private Vector3 _campingPos;
        private Vector3 _parentPos;
        protected override void StatsUpdate()
        {
            StatsHandlerComponent stats = _unitBaseParent.UnitStatsHandlerComp();
            _cooldownReviveUnit = stats.GetCurrentStatValue(StatId.TimeToRevive);
            _campingRange = stats.GetCurrentStatValue(StatId.CampingRange);
        }
        private void OnEnable()
        {
            StatsUpdate();
        }
        public override void ShowTool()
        {
            _towerKit.TowerRangingHandler().SetShowRanging(false);
        }
        public override void Setup(TowerKit towerKit)
        {
            _towerKit = towerKit;
            
            float rangeVal= _unitBaseParent.UnitStatsHandlerComp().GetCurrentStatValue(StatId.CampingRange);
            towerKit.TowerRangingHandler().SetUp(rangeVal);
            
            _parentPos = _towerKit.transform.position;
            for (int i = 0; i < MaxAllyCount; i++)
                Messenger.Default.Publish(new OnSpawnObjectPayload
                {
                    ActiveAtSpawning = false,
                    ObjectType = _unitSpawnId.ToString(),
                    OnSpawned = SpawnSingleUnit,
                });
            
            Messenger.Default.Publish(new OnGetNearestPosFromRoutePayload
            {
                PosInput = _parentPos,
                OnCalculateSuccess = SetCampingPlace,
            });
        }

        /// Spawning new object from pool and set on revive for it
        private void SpawnSingleUnit(GameObject objectSpawned)
        {
            UnitBase unitBase = objectSpawned.GetComponent<UnitBase>();
            unitBase.UnitStatsHandlerComp().ReplaceBaseStats(_unitBaseParent.UnitStatsHandlerComp().GetBaseStats());
            unitBase.UnitReviveHandlerComp().SetupRevive(OnWaitingToRevive);
            unitBase.OnUpdateStats?.Invoke();
            objectSpawned.transform.position = _towerKit.transform.position;
            objectSpawned.SetActive(true);
            _allyUnits.Add(unitBase);
            SetCampingPlaceCorner();
        }
        private void OnDisable()
        {
            // When tower is remove, all unit which tower control should clear
            foreach (UnitBase unitBase in _allyUnits)
            {
                if (!unitBase || !unitBase.gameObject)
                    continue;
                // remove callback for reviving first
                unitBase.UnitReviveHandlerComp().OnRemoveRevive();
                // Return to pool
                Messenger.Default.Publish(new OnReturnObjectToPoolPayload
                {
                    GameObject = unitBase.gameObject,
                });
            }

        }
        private async void OnWaitingToRevive(UnitBase unitBase)
        {
            // remove old unit and returning it to the pool
            _allyUnits.Remove(unitBase);
            
            await UniTask.Delay(TimeSpan.FromSeconds(_cooldownReviveUnit));
            // Prevent spawning if tower is destroy
            if (!_towerKit.IsExistTower())
                return;
            // Spawning new unit from pool
            Messenger.Default.Publish(new OnSpawnObjectPayload
            {
                ActiveAtSpawning = false,
                ObjectType = _unitSpawnId.ToString(),
                OnSpawned = SpawnSingleUnit,
            });
        }
        
        public void SetCampingPlace(Vector3 newCampingPos)
        {
            // Get current tower pos on the map
            Vector3 parentPos = _towerKit.transform.position;
            if (VectorUtility.Distance2dOfTwoPos(newCampingPos, parentPos) > _campingRange)
                return;

            _campingPos = newCampingPos;

            SetCampingPlaceCorner();
        }
        private void SetCampingPlaceCorner()
        {
            // Set camping pos for each unit
            for (int i = 0; i < _allyUnits.Count; i++)
            {
                Vector3 curUnitCampingPlace = GetCampingPlaceOffset(MaxAllyCount, i, _campingPos);
                // Moving to camping pos
                _allyUnits[i].UserActionController().SetMovingPosition(curUnitCampingPlace);
            }
        }
        private Vector3 GetCampingPlaceOffset(int maxNumber, int index, Vector3 startPos)
        {
            float startDegree = -90; // Ensure first unit will be create at bottom middle place
            float curDegree = (360f * index / maxNumber) + startDegree;
            float curRadian = curDegree * Mathf.Deg2Rad;
            return startPos + new Vector3(MinPerUnitDistance * Mathf.Cos(curRadian), MinPerUnitDistance * Mathf.Sin(curRadian), 0f);
        }
    }
}

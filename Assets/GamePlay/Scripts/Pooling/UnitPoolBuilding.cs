using AYellowpaper.SerializedCollections;
using Common.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Pooling
{
    [Serializable]
    public class UnitPoolBuilding
    {
        [SerializeField] private UnitPoolBuildingListItem<UnitId.Tower,GameObject> _towers;
        [SerializeField] private UnitPoolBuildingListItem<UnitId.Ally,GameObject> _allies;
        [SerializeField] private UnitPoolBuildingListItem<UnitId.Enemy,GameObject> _enemies;
        [SerializeField] private UnitPoolBuildingListItem<UnitId.Hero,GameObject> _heroes;
        [SerializeField] private UnitPoolBuildingListItem<UnitId.Projectile,GameObject> _projectiles;

        public SerializedDictionary<string, GameObject> BuildPoolingDictionary()
        {
            SerializedDictionary<string, GameObject> poolingDict = new SerializedDictionary<string, GameObject>();
            _towers.BuildPoolingDictionary(ref poolingDict);
            _allies.BuildPoolingDictionary(ref poolingDict);
            _enemies.BuildPoolingDictionary(ref poolingDict);
            _heroes.BuildPoolingDictionary(ref poolingDict);
            _projectiles.BuildPoolingDictionary(ref poolingDict);
            return poolingDict;
        }
        [Serializable]
        public struct UnitPoolBuildingComposite<TKey,TVal>
        {
            public TKey Key;
            public TVal Value;
        }
        [Serializable]
        public class UnitPoolBuildingListItem<TKey,TVal>
        {
            [SerializeField] private List<UnitPoolBuildingComposite<TKey,TVal>> _units;
            public void BuildPoolingDictionary(ref SerializedDictionary<string, GameObject> poolingDict)
            {
                foreach (var unit in _units)
                {
                    poolingDict.Add(unit.Key.ToString(),unit.Value as GameObject);
                }
            }
        }
    }
}

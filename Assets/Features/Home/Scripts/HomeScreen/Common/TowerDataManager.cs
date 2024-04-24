using System.Collections.Generic;
using Features.MasteryPage.Scripts.Tower;
using UnityEngine;
using UnityEngine.Serialization;

public class TowerDataManager : SingletonBase<TowerDataManager>
{
      [FormerlySerializedAs("_commonTowerConfig")]
      [Header("Data"), Space(12)]
      [SerializeField] private TowerRuneDataConfig _towerRuneDataConfig;
      public List<TowerComposite> TowerComposites { get; private set; }

      protected override void Awake()
      {
            base.Awake();
            LoadTowerData();
      }
      private void LoadTowerData()
      {
            if (TowerComposites == null)
                  TowerComposites = new List<TowerComposite>();

            else TowerComposites.Clear();
            
            if (_towerRuneDataConfig == null)
                  return;
            
            List<TowerDataConfig> listTowerData = _towerRuneDataConfig.GetAllTowerData();
            foreach (var towerDataSo in listTowerData)
            {
                  TowerComposites.Add(
                        new TowerComposite
                        {
                              TowerId = towerDataSo.GetTowerId(),
                              RuneLevels = towerDataSo.GetAllRuneDatLevels(),
                        }
                  );
            }
      }
}


using Common.Scripts;
using Common.Scripts.Data.DataConfig;
using System.Collections.Generic;
using UnityEngine;

public class TowerDataManager : SingletonBase<TowerDataManager>
{
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


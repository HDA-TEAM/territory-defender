using System.Collections.Generic;
using UnityEngine;

public class TowerDataManager : SingletonBase<TowerDataManager>
{
      [Header("Data"), Space(12)]
      [SerializeField] private CommonTowerConfig _commonTowerConfig;
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
            
            if (_commonTowerConfig == null)
                  return;
            
            List<CommonTowerSO> listTowerData = _commonTowerConfig.GetAllTowerData();
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


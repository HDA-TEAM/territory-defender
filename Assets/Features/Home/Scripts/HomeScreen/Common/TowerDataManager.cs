using Common.Scripts;
using Common.Scripts.Data.DataConfig;
using Features.MasteryPage.Scripts.Tower;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Home.Scripts.HomeScreen.Common
{
      public class TowerDataManager : SingletonBase<TowerDataManager>
      {
            [FormerlySerializedAs("_towerRuneDataConfig")]
            [Header("Data"), Space(12)]
            [SerializeField] private TowerRuneDataAsset _towerRuneDataAsset;
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
            
                  if (_towerRuneDataAsset == null)
                        return;
            
                  List<TowerDataConfig> listTowerData = _towerRuneDataAsset.GetAllTowerDataConfig();
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
}


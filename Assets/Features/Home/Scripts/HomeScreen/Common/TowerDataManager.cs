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
            [FormerlySerializedAs("_towerRuneDataAsset")]
            [FormerlySerializedAs("_towerRuneDataConfig")]
            [Header("Data"), Space(12)]
            [SerializeField] private TowerDataAsset _towerDataAsset;
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
            
                  if (_towerDataAsset == null)
                        return;
            
                  List<TowerDataConfig> listTowerData = _towerDataAsset.GetAllTowerDataConfig();
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


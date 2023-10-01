using GamePlay.Scripts.Tower;
using System.Collections.Generic;
using UnityEngine;

namespace Features.ADMIN
{
    public class AdminGameplayManager : MonoBehaviour
    {
        private int _currentStageId;
        [SerializeField] AdminTowerKitManager adminTowerKitManager;
        [SerializeField] List<OldStageConfig> _stages;
        public void ChangeStage()
        {
            LoadMap();
            LoadTowerKits();
            LoadRoutes();
            LoadCreepWaves();
        }
        void LoadMap()
        {
            
        }
        void LoadTowerKits()
        {
        }
        void LoadRoutes()
        {
            
        }
        void LoadCreepWaves()
        {
            
        }
    }
}

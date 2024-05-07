using Common.Loading.Scripts;
using CustomInspector;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlay;
using GamePlay.Scripts.Tower.TowerKIT;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.GamePlayController
{
    public class TowerKitSetController : GamePlaySingletonBase<TowerKitSetController>
    {
        [Button("SaveToConfig")]
        [Button("LoadFromConfig")]
        [SerializeField] private StageId _currentStageId;

        [SerializeField] private TowerKitSetConfig _towerKitSetConfig;
        [SerializeField] private List<TowerKit> _currentTowerKits = new List<TowerKit>();
        [SerializeField] private List<GameObject> _currentObjectTowers = new List<GameObject>();
        public TowerKit CurrentSelectedKit;
        private TowerKit _preSelectedKit;
        private Action _onSelected;

        protected override void Awake()
        {
            base.Awake();
            SetUpData();
        }
        public void SaveToConfig()
        {
            List<Vector3> places = new List<Vector3>();

            foreach (GameObject towerObj in _currentObjectTowers)
            {
                // Check if this Kit available to save
                if (towerObj.activeSelf)
                    places.Add(towerObj.transform.position);
            }

            _towerKitSetConfig.SaveToConfig(places, _currentStageId);
        
        }

        private void LoadFromConfig()
        {
            var places = _towerKitSetConfig.LoadFromConfig(_currentStageId);

            for (int i = 0; i < _currentObjectTowers.Count; i++)
            {
                // If current kit exist on map > total places count in config
                if (i >= places.Count)
                {
                    _currentObjectTowers[i].SetActive(false);
                    continue;
                }

                // Check if this Kit available to load
                if (!_currentObjectTowers[i].activeSelf)
                    _currentObjectTowers[i].SetActive(true);

                // Save position of kit
                // Value of Z always zero
                _currentObjectTowers[i].transform.position = new Vector3(
                    places[i].x,
                    places[i].y,
                    0);
            }
        }
        private void SetUpData()
        {
            // Setup callback when selected
            foreach (TowerKit kit in _currentTowerKits)
            {
                kit.Setup(SetCurrentSelectedKit);
            }
        }
        private void SetCurrentSelectedKit(TowerKit towerKit)
        {
            if (_preSelectedKit != null)
            {
                _preSelectedKit.OnCancelMenu();
            }
            CurrentSelectedKit = towerKit;
            _preSelectedKit = CurrentSelectedKit;
        }
        public override void SetUpNewGame(StartStageComposite startStageComposite)
        {
            _currentStageId = startStageComposite.StageId;
            LoadFromConfig();
        }
        public override void ResetGame()
        {
        }
    }
}

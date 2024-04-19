using Common.Loading.Scripts;
using CustomInspector;
using GamePlay.Scripts.Data;
using GamePlay.Scripts.GamePlay;
using GamePlay.Scripts.Tower.TowerKIT;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public TowerKit CurrentSelectedKit;
        private TowerKit _preSelectedKit;
        private Action _onSelected;

        private void Reset() => _currentTowerKits = GetComponentsInChildren<TowerKit>().ToList();
        protected override void Awake()
        {
            base.Awake();
            SetUpData();
        }
        public void SaveToConfig()
        {
            List<Vector3> places = new List<Vector3>();

            foreach (TowerKit tk in _currentTowerKits)
            {
                // Check if this Kit available to save
                if (tk.gameObject.activeSelf)
                    places.Add(tk.gameObject.transform.position);
            }

            _towerKitSetConfig.SaveToConfig(places, _currentStageId);
        
        }

        private void LoadFromConfig()
        {
            var places = _towerKitSetConfig.LoadFromConfig(_currentStageId);

            for (int i = 0; i < _currentTowerKits.Count; i++)
            {
                // If current kit exist on map > total places count in config
                if (i >= places.Count)
                {
                    _currentTowerKits[i].gameObject.SetActive(false);
                    continue;
                }

                // Check if this Kit available to load
                if (!_currentTowerKits[i].gameObject.activeSelf)
                    _currentTowerKits[i].gameObject.SetActive(true);

                // Save position of kit
                // Value of Z always zero
                _currentTowerKits[i].transform.position = new Vector3(
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

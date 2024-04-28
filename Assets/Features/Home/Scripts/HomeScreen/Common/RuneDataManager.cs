
using System.Collections.Generic;
using Features.MasteryPage.Scripts.Rune;
using Features.MasteryPage.Scripts.Tower;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Home.Scripts.HomeScreen.Common
{
    public class RuneDataManager : SingletonBase<RuneDataManager>
    {
        [Header("Data"), Space(12)]
        [SerializeField] private RuneDataAsset _runeDataAsset;
        [SerializeField] private TowerRuneDataConfig _towerRuneDataConfig;

        public List<TowerHasRuneComposite> TowerRuneComposites { get; private set; }
        public TowerRuneDataConfig TowerRuneDataConfig => _towerRuneDataConfig;
        public RuneDataAsset RuneDataAsset => _runeDataAsset;
        
        private List<RuneComposite> _runeComposites;
        
        protected override void Awake()
        {
            base.Awake();
            GetTowerRuneData();
        }

        public void GetTowerRuneData()
        {
            //Check and setup for Towers which had the rune
            if (TowerRuneComposites == null)
                TowerRuneComposites = new List<TowerHasRuneComposite>();
            else TowerRuneComposites.Clear();
            
            //Check and setup Runes
            if (_runeComposites == null)
                _runeComposites = new List<RuneComposite>();
            else _runeComposites.Clear();
            
            if (_runeDataAsset == null)
                return;
            
            // Load Rune data into each Tower
            List<RuneDataConfig> listRuneSos = _runeDataAsset.GetAllRuneData();
            List<TowerDataConfig> listTowerDataAsset = _towerRuneDataConfig.GetAllTowerData();
            TowerDataModel loadedTowerData = _towerRuneDataConfig.GetTowerDataAsset();
            
            foreach (var towerSo in listTowerDataAsset)
            {
                _runeComposites = new List<RuneComposite>();

                // Include default data for each rune
                foreach (var runeSo in listRuneSos)
                {
                    _runeComposites.Add(new RuneComposite
                    {
                        RuneId = runeSo.GetRuneId(),
                        Name = runeSo._name,
                        Level = 0,
                        MaxLevel = runeSo._maxLevel,
                        AvatarSelected = runeSo._avatarSelected,
                        AvatarStarted = runeSo._avatarStarted,
                        Effects = runeSo._effects,
                    });
                }

                if (loadedTowerData.TowerList != null) // Check if json is new created or null
                {
                    // Find the corresponding TowerSoSaver in loadedTowerData
                    int towerSoSaverIndex = loadedTowerData.TowerList.FindIndex(t => t.TowerId == towerSo.GetTowerId());
                    if (towerSoSaverIndex != -1)
                    {
                        TowerSoSaver towerSoSaver = loadedTowerData.TowerList[towerSoSaverIndex];
                        foreach (var runeLevel in towerSoSaver.RuneLevels)
                        {
                            int runeCompositeIndex = _runeComposites.FindIndex(rc => rc.RuneId == runeLevel.RuneId);
                            if (runeCompositeIndex != -1)
                            {
                                RuneComposite temp = _runeComposites[runeCompositeIndex];
                                temp.Level = runeLevel.Level;
                                _runeComposites[runeCompositeIndex] = temp;
                            }
                        }
                    }
                }
                
                TowerRuneComposites.Add(new TowerHasRuneComposite
                {
                    TowerId = towerSo.GetTowerId(),
                    RuneComposite = _runeComposites
                });
            }
        }
    }
}

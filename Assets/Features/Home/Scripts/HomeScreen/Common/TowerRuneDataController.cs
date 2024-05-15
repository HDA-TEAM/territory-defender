using System.Collections.Generic;
using System.Linq;
using Features.MasteryPage.Scripts.Rune;
using Features.MasteryPage.Scripts.Tower;
using UnityEngine;

namespace Features.Home.Scripts.HomeScreen.Common
{
    public class TowerRuneDataController : MonoBehaviour
    {
        [Header("Data"), Space(12)] [SerializeField]
        private RuneDataAsset _runeDataAsset;

        [SerializeField] private TowerRuneDataAsset _towerRuneDataAsset;

        private ITowerRune _currentTowerRune;
        public void SetStrategy(ITowerRune strategy)
        {
            _currentTowerRune = strategy;
        }

        public void ExecuteStrategy()
        {
            _currentTowerRune?.TowerRuneExecute(this);
        }
        public static List<TowerRuneComposite> TowerRuneComposites { get; private set; }
        public TowerRuneDataAsset TowerRuneDataAsset => _towerRuneDataAsset;
        public RuneDataAsset RuneDataAsset => _runeDataAsset;

        private List<RuneComposite> _runeComposites;

        

        public void InitializeTowerRuneData()
        {
            Debug.Log("Initializing Tower Rune Data");
            
            //Check and setup for Towers which had the rune
            if (TowerRuneComposites == null)
                TowerRuneComposites = new List<TowerRuneComposite>();
            else TowerRuneComposites.Clear();

            //Check and setup Runes
            if (_runeComposites == null)
                _runeComposites = new List<RuneComposite>();
            else _runeComposites.Clear();

            // if (_runeDataAsset == null)
            //     return;

            // Load Rune data into each Tower
            List<RuneDataConfig> listRuneSos = _runeDataAsset.GetAllRuneData();
            //_towerRuneDataAsset.UpdateTowerDataConfig();
            List<TowerDataConfig> towerDataConfigs = _towerRuneDataAsset.GetAllTowerDataConfig();
            //List<TowerDataSaver> loadedTowerData = _towerRuneDataAsset.UpdateTowerDataConfig();

            // TODO: A lot of bug and that logic need to be changed all
            // Use Strategy to N state:
            // maybe 3 state of TowerRune: InitTowerRune, UpgradeTowerRune, ResetTowerRune

            int i = 0;
            foreach (var towerSo in towerDataConfigs)
            {
                Debug.Log(i++ + "..?????");
                // if (towerSo._runeLevels == null)
                // {
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
                    //}
                }


                // if (loadedTowerData != null) // Check if json is new created or null
                // {
                //     // Find the corresponding TowerSoSaver in loadedTowerData
                //     int towerSoSaverIndex = loadedTowerData.FindIndex(t => t.TowerId == towerSo.GetTowerId());
                //     if (towerSoSaverIndex != -1)
                //     {
                //         TowerDataSaver towerDataSaver = loadedTowerData[towerSoSaverIndex];
                //         foreach (var runeLevel in towerDataSaver.RuneLevels)
                //         {
                //             int runeCompositeIndex = _runeComposites.FindIndex(rc => rc.RuneId == runeLevel.RuneId);
                //             if (runeCompositeIndex != -1)
                //             {
                //                 RuneComposite temp = _runeComposites[runeCompositeIndex];
                //                 temp.Level = runeLevel.Level;
                //                 _runeComposites[runeCompositeIndex] = temp;
                //             }
                //         }
                //     }
                // }

                TowerRuneComposites.Add(new TowerRuneComposite
                {
                    TowerId = towerSo.GetTowerId(),
                    RuneComposite = _runeComposites
                });
            }
        }
        
        public void UpgradeTowerRuneData(){}
        public void ResetTowerRuneData(){}
    }
}

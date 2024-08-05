using System.Collections.Generic;
using Common.Scripts;
using Common.Scripts.Data;
using Common.Scripts.Data.DataAsset;
using Common.Scripts.Data.DataConfig;
using Features.MasteryPage.Scripts.Rune;
using Features.MasteryPage.Scripts.Tower;
using UnityEngine;

namespace Features.Home.Scripts.HomeScreen.Common
{
    public class TowerRuneDataController : MonoBehaviour
    {
        [Header("Data"), Space(12)]
        [SerializeField] private TowerDataAsset _towerDataAsset;
        [SerializeField] private RuneDataConfig _runeDataConfig;
        [SerializeField] private InventoryDataAsset _inventoryDataAsset;
        public List<TowerRuneComposite> TowerRuneComposites { get; private set; }
        private List<RuneComposite> _runeComposites;

        private ITowerRune _currentStrategy;

        public void SetStrategy(ITowerRune strategy)
        {
            _currentStrategy = strategy;
        }

        public void ExecuteStrategy()
        {
            _currentStrategy?.Execute(this);
        }

        public void InitializeTowerRuneData()
        {
            //Check and setup for Towers which had the rune
            if (TowerRuneComposites == null)
                TowerRuneComposites = new List<TowerRuneComposite>();
            else TowerRuneComposites.Clear();

            //Check and setup Runes
            if (_runeComposites == null)
                _runeComposites = new List<RuneComposite>();
            else _runeComposites.Clear();

            // Get data from local data
            InitializeTowerWithLocalRunes(_towerDataAsset.TowerRuneDataList);
        }
        private void InitializeTowerWithLocalRunes(List<TowerRuneData> towerRuneDataList)
        {
            // Create a list to store the indices of runes that need to be modified
            // Iterate over the local tower's rune data
            foreach (TowerRuneData singleTowerRuneData in towerRuneDataList)
            {
                List<RuneComposite> runeComposites = new List<RuneComposite>();
                foreach (RuneData runeData in singleTowerRuneData.RuneLevels)
                {
                    RuneDataSo runeSo = _runeDataConfig.GetConfigByKey(runeData.RuneId);

                    runeComposites.Add(new RuneComposite
                    {
                        RuneId = runeSo.GetRuneId(),
                        Name = runeSo._name,
                        Description = runeSo.Description,
                        Level = runeData.Level,
                        MaxLevel = runeSo._maxLevel,
                        AvatarSelected = runeSo._avatarSelected,
                        AvatarStarted = runeSo._avatarStarted,
                        PowerUnits = runeSo.PowerUnits,
                        Effects = runeSo._effects,
                    });
                }
                // Update TowerRuneComposites
                TowerRuneComposites.Add(new TowerRuneComposite
                {
                    TowerId = singleTowerRuneData.TowerId,
                    RuneComposite = runeComposites
                });
            }

        }
        public void UpgradeTowerRuneData(UnitId.Tower towerId, RuneComposite runeComposite)
        {
            _towerDataAsset.CheckAndUpgradeTowerRuneLevel(towerId,runeComposite.RuneId,runeComposite.MaxLevel);
            InitializeTowerWithLocalRunes(_towerDataAsset.TowerRuneDataList);
        }
        public void ResetTowerRuneData(UnitId.Tower towerId)
        {
            _inventoryDataAsset.TryChangeInventoryData(InventoryType.TalentPoint, _towerDataAsset.GetReturnStar(towerId));
            _towerDataAsset.ResetSpecificTowerRuneData(towerId);
        }
        }
    }

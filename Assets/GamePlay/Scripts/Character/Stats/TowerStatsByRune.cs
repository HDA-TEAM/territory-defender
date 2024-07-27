using Common.Scripts;
using Common.Scripts.Data.DataAsset;
using Common.Scripts.Data.DataConfig;
using UnityEngine;

namespace GamePlay.Scripts.Character.Stats
{
    [CreateAssetMenu(fileName = "TowerStatsByRuneConfig", menuName = "ScriptableObject/TowerStatsByRuneConfig")]
    public class TowerStatsByRune : Stats
    {
        [SerializeField] private UnitId.Tower _towerId;
        [SerializeField] private TowerDataAsset _towerDataAsset;
        [SerializeField] private RuneDataConfig _runeDataConfig;
        public override float GetStat(StatId statId)
        {
            RuneData runeData = _towerDataAsset.GetTowerRuneDataLevel(_towerId, statId);
            if (runeData.RuneId != RuneId.None && base.GetStat(statId) != 0f)
            {
                float originVal = base.GetStat(statId);
                float buffUnit = _runeDataConfig.GetConfigByKey(runeData.RuneId).GetPowerByLevel(runeData.Level);
                return originVal + buffUnit * originVal;
            }
            return base.GetStat(statId);

        }   
    }
}

using GamePlay.Scripts.Character.Stats;
using GamePlay.Scripts.Menu.UnitInformationPanel;
using System;
using UnityEngine;

public class StatsHandlerComponent : UnitBaseComponent
{
    private Action _onSynStat;
    private BuffHandler _buffHandler;
    [SerializeField] private Stats _baseStats;
    private readonly ShowStatInformationSelector _statInformationSelector = new ShowStatInformationSelector();
    #region Access
    public BuffHandler BuffHandler
    {
        get => _buffHandler ?? new BuffHandler(SynData);
    }
    public Stats GetBaseStats() => _baseStats;
    public ShowStatsInformationComposite GetShowStatsInformation()
    {
        ShowStatsInformationComposite statsInformationComposite = _statInformationSelector.GetShowUnitInformation(_unitBaseParent.UnitSide).GetShowStatsInformation(_baseStats);
        statsInformationComposite.UnitBase = _unitBaseParent;
        return statsInformationComposite;
    }
    public void ReplaceBaseStats(Stats stats)
    {
        _baseStats = stats;
    }
    #endregion

    protected override void Start()
    {
        base.Start();
        _buffHandler = new BuffHandler(SynData);
    }
    private void SynData() => _unitBaseParent.OnUpdateBuffs?.Invoke();

    public float GetCurrentStatValue(StatId statId)
    {
        if (_baseStats.IsStatExist(statId))
        {
            var originVal = _baseStats.GetStat(statId);
            // Check buff
            if (_buffHandler != null &&  _buffHandler.IsExistBuffOrDeBuff())
            {
                return _buffHandler.GetValueApplyBuff(statId, originVal);
            }
            return originVal;
        }
        return 0f;
    }
}

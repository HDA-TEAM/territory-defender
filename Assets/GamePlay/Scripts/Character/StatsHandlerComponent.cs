using System;
using UnityEngine;

public class StatsHandlerComponent : UnitBaseComponent
{
    private Action _onSynStat;
    private BuffHandler _buffHandler;
    [SerializeField] private Stats _baseStats;

    #region Access
    public BuffHandler BuffHandler() => _buffHandler;
    public Stats GetBaseStats() => _baseStats;
    #endregion

    private void Start()
    {
        _buffHandler = new BuffHandler(SynData);
    }
    private void SynData()
    {
        _unitBaseParent.OnUpdateStats.Invoke();
    }

    public float GetCurrentStatValue(StatId statId)
    {
        if (_baseStats.IsStatExist(statId))
        {
            // Check buff
            if (_buffHandler.IsExistBuffOrDeBuff())
            {
                return _buffHandler.GetValueApplyBuff(statId, _baseStats.GetStat(statId));
            }
        }
        return 0f;
    }
}

using UnityEngine;

public class StatsHandlerComponent : UnitBaseComponent
{
    private BuffHandler _buffHandler = new BuffHandler();
    [SerializeField] private Stats _baseStats;
    private void Start()
    {
        _buffHandler = new BuffHandler();
    }
    public void SynData()
    {
        _unitBaseParent.OnUpdateStats.Invoke();
    }
    public Stats GetBaseStats() => _baseStats;
    public float GetCurrentStatValue(StatId statId)
    {
        if (_baseStats.IsStatExist(statId))
        {
            // Check buff
            if (_buffHandler.IsExistBuffOrDeBuff())
            {
                _baseStats.GetStat(statId);
            }
        }
        return 0f;
    }
}

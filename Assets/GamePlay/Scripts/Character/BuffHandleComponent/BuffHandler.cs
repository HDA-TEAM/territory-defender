
using System;
using System.Collections.Generic;
using UnityEngine;

public enum BuffId
{
    Attribute = 1,
    
}

public abstract class UnitBuffBase
{
    public BuffId BuffId;
}

public class AttributeBuff : UnitBuffBase
{
    public readonly float BuffPercent;
    public readonly List<StatId> StatsBuff;
    public AttributeBuff(List<StatId> statIds, float buffPercent)
    {
        BuffId = BuffId.Attribute;
        BuffPercent = buffPercent;
        StatsBuff = statIds;
    }
}
public class BuffHandler
{
    public BuffHandler(Action synStat)
    {
        _onSynStat = synStat;
    }
    private AttributeBuff _attributeBuff;
    private readonly Action _onSynStat;
    public bool IsExistBuffOrDeBuff() => _attributeBuff != null;
    public float GetValueApplyBuff(StatId statId, float originVal)
    {
        // Check this stat have been buffed
        if (_attributeBuff != null && _attributeBuff.StatsBuff.Contains(statId))
        {
            return originVal + (originVal * _attributeBuff.BuffPercent / 100f);
        } 
        return originVal;
    }
    public void AddAttributeBuff(AttributeBuff attributeBuff)
    {
        _attributeBuff = attributeBuff;
        _onSynStat?.Invoke();
    }
    public void RemoveAttributeBuff()
    {
        _attributeBuff = null;
        _onSynStat?.Invoke();
    }
}

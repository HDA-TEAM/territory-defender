
using System.Collections.Generic;

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
    public List<StatId> StatsBuff;
    public AttributeBuff(List<StatId> statIds, float buffPercent)
    {
        BuffId = BuffId.Attribute;
        BuffPercent = buffPercent;
        StatsBuff = statIds;
    }
}
public class BuffHandler
{
    private AttributeBuff _attributeBuff;
    public bool IsExistBuffOrDeBuff() => _attributeBuff != null;
    public float GetValueApplyBuff(StatId statId, float originVal)
    {
        // Check this stat have been buffed
        if (_attributeBuff != null && _attributeBuff.StatsBuff.Contains(statId))
        {
            return originVal + (originVal * _attributeBuff.BuffPercent / 100);
        } 
        return originVal;
    }
    public void AddAttributeBuff(AttributeBuff attributeBuff)
    {
        _attributeBuff = attributeBuff;
    }
    public void RemoveAttributeBuff()
    {
        _attributeBuff = null;
    }
}

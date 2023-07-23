using UnityEngine;

public class WeaponDataAsset : ScriptableObject
{
        
}

public interface WeaponLineRoute
{
    void ApplyLineRoute();
}
public class ArcRouteLine : WeaponLineRoute
{
    public void ApplyLineRoute()
    {
        
    }
}
public class StraightRouteLine : WeaponLineRoute
{
    public void ApplyLineRoute()
    {
        
    }
}

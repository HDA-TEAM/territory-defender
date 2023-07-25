using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections.Generic;
using UnityEngine;


public enum BulletType{
    Arrow,
}

[Serializable]
public struct BulletComponent
{
    public BulletType BulletType;
    public GameObject prefab;
}
[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObject/Stage/BulletDataAsset")]
public class BulletDataAsset : ScriptableObject
{
    [SerializeField] private List<BulletComponent> bulletComponent = new List<BulletComponent>();
    public Tweener GetLineRoute(Vector2 posSpawn, BulletType bulletType,UnitBase target)
    {
        GameObject bullet = Instantiate(bulletComponent.Find((i) => i.BulletType == bulletType).prefab);
        
        var v = GameObject.Find("BulletPooling");
        bullet.transform.SetParent(v.transform);
        bullet.transform.position = posSpawn;
        return new StraightRouteLine().ApplyLineRoute(bullet, target);
    }
}

public interface WeaponLineRoute
{
    Tweener ApplyLineRoute(GameObject curWeapon,UnitBase target);
}
// public class ArcRouteLine : WeaponLineRoute
// {
//     public Tween ApplyLineRoute(UnitBase curWeapon,UnitBase target)
//     {
//         return new DOTween();
//     }
// }
public class StraightRouteLine : WeaponLineRoute
{
    public Tweener ApplyLineRoute(GameObject curWeapon,UnitBase target)
    {
        return curWeapon.transform.DOMove(target.transform.position, 0.25f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                target.GetComponent<HealthComp>().PlayHurting(10);
                curWeapon.SetActive(false);
            });
    }
}

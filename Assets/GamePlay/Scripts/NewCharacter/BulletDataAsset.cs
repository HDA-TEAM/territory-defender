using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections.Generic;
using UnityEngine;


public enum BulletType{
    Arrow,
}

// [Serializable]
// public struct BulletComponent
// {
//     public BulletType BulletType;
//     public GameObject prefab;
// }

[CreateAssetMenu(fileName = "BulletDataAsset", menuName = "ScriptableObject/Stage/BulletDataAsset")]
public class BulletDataAsset : ScriptableObject
{
    // [SerializeField] private List<BulletComponent> bulletComponent = new List<BulletComponent>();
    // Get route between cur pos to target 
    [SerializeField] private AnimationCurve bowAttackCurve;
    public void GetLineRoute(Vector2 posSpawn, BulletType bulletType,UnitBase target)
    {
        var bullet = PoolingManager.Instance.SpawnObject(PoolingTypeEnum.ArrowBullet,posSpawn);
        new ProjectileTrajectoryRouteLine().ApplyLineRoute(bullet, target, bowAttackCurve);
    }
}

public interface WeaponLineRoute
{
    void ApplyLineRoute(GameObject curWeapon,UnitBase target, AnimationCurve customCurve);
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
    public void ApplyLineRoute(GameObject curWeapon, UnitBase target, AnimationCurve customCurve)
    {
        // curWeapon.transform.DOMove(target.transform.position, 0.25f).SetEase(Ease.Linear)
        //     .OnComplete(() =>
        //     {
        //         target.GetComponent<HealthComp>().PlayHurting(10);
        //         curWeapon.SetActive(false);
        //     });
        // transform.DOPath(new Vector3[] { pathPoints[0].position, pathPoints[1].position, pathPoints[2].position, pathPoints[3].position },
        //         duration, PathType.CubicBezier)
        //     .SetOptions(false)
        //     .SetEase(Ease.Linear)
        //     .OnComplete(OnMovementComplete);
        // return curWeapon.transform.DOPath(
        //         new Vector3[]
        //         {
        //             target.transform.position,
        //             curWeapon.transform.position + Vector3.up * 10,
        //             (curWeapon.transform.position + target.transform.position) / 2 + Vector3.up * 10,
        //         },
        //         0.1f, PathType.CubicBezier)
        //     .SetOptions(false)
        //     .SetEase(Ease.Linear)
        //     .OnComplete(
        //         () =>
        //         {
        //             target.GetComponent<HealthComp>().PlayHurting(10);
        //             curWeapon.SetActive(false);
        //         });
    }
}

public class ProjectileTrajectoryRouteLine : WeaponLineRoute
{
    public void ApplyLineRoute(GameObject curWeapon, UnitBase target, AnimationCurve customCurve)
    {
        float t = 0f;
        Vector3 prevBulletPos = curWeapon.transform.position;
        DOTween.To(() => t, x => t = x, 1f, 0.75f)
            .SetEase(customCurve)
            .OnUpdate(() =>
            {
                // Calculate the position of the arrow based on the Bezier curve equation.
                Vector3 newPosition = CalculateBezierPoint(
                    t,
                    curWeapon.transform.position,
                    curWeapon.transform.position + Vector3.up / 3f ,
                    target.transform.position
                    
                );
                
                // Update the arrow's position.
                curWeapon.transform.position = newPosition;
                
                // Update the arrow's angle
                float zAngle = VectorUtility.GetZAngleOfTwoPoint(prevBulletPos, newPosition);
                curWeapon.transform.rotation = Quaternion.Euler(0f,0f, zAngle);
                    
                 //Update previous pos of bullet
                prevBulletPos = newPosition;
                
            })
            .OnComplete(
                () =>
                {
                    target.GetComponent<HealthComp>().PlayHurting(10);
                    curWeapon.SetActive(false);
                });
    }
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // Bezier curve equation: B(t) = (1-t)^2 * P0 + 2 * (1-t) * t * P1 + t^2 * P2
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0; // (1-t)^2 * P0
        p += 2f * u * t * p1; // 2 * (1-t) * t * P1
        p += tt * p2; // t^2 * P2
        return p;
    }
}

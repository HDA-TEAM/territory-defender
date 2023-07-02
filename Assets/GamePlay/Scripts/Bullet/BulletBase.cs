using GamePlay.Scripts.Tower;
using System;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 0.5f;
    [SerializeField] private float dame;
    [SerializeField] private GameObject target;
    private bool isSetup = false;
    public void SetUp(GameObject target, float dame)
    {
        isSetup = true;
        this.target = target;
    }
    private void Update()
    {
        if (isSetup)
        {
            if (target != null && target.activeSelf)
            {
               gameObject.transform.position = VectorUtility.Vector2MovingAToB(this.transform.position,target.transform.position,movementSpeed);
               CheckBulletHitTarget();
            }
            else
            {
                BulletDestroy();
            }
        }
    }
    private void CheckBulletHitTarget()
    {
        if (VectorUtility.IsTwoPointReached(this.transform.position, target.transform.position))
        {
            //todo
            // reduce target health
            BulletDestroy();
        }
    }
    private void BulletDestroy()
    {
        isSetup = false;
        Destroy(gameObject);
    }
}

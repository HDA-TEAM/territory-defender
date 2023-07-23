using GamePlay.Scripts.Tower;
using System;
using UnityEngine;

public class Testing : MonoBehaviour
{
        [SerializeField] private GameObject a;
        [SerializeField] private GameObject b;
        private float distanceAandB;
        private void Update()
        {
                distanceAandB = GameObjectUtility.Distance2dOfTwoGameObject(a, b);
        }
}


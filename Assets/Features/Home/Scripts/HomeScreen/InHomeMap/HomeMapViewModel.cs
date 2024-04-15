using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Home.Scripts.HomeScreen.InHomeMap
{
    public class HomeMapViewModel : MonoBehaviour
    {
        public static HomeMapViewModel Instance { get; private set; }

        [SerializeField] private GameObject _imgLightCol;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void MoveLightColTo(Vector3 position)
        {
            Vector3 adjustedPosition = new Vector3(position.x, position.y - 1, position.z);
            _imgLightCol.transform.position = adjustedPosition;
        }
    }
}

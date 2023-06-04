using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Scripts.Tower
{
    [Serializable] public struct TowerCanBuild
    {
        public Button Button;
        public GameObject GameObject;
    }

    public class TowerController : MonoBehaviour
    {
        public List<TowerCanBuild> towersCanBuild;
        void Start()
        {
            foreach (var towerCanBuild in towersCanBuild)
            {
                towerCanBuild.Button.onClick.AddListener(delegate { BuildTower(towerCanBuild); });
            }
        }
        private void BuildTower(TowerCanBuild towerCanBuild)
        {
            GameObject go = Instantiate(towerCanBuild.GameObject);
            go.transform.position = this.transform.position;
        }
    }
}

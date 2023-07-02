using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Tower
{
    [Serializable] public struct TowerCanBuild
    {
        public ButtonBuildTower button;
        public GameObject gameObject;
    }

    public class TowerKIT: MonoBehaviour
    {
        [SerializeField] private List<TowerCanBuild> towersCanBuild;
        Vector2 _place;
        public Vector2 Place() => _place;
        void Start()
        {
            ResetCheckWantToBuild();
        }
        
        void ResetCheckWantToBuild()
        {
            foreach (var towerCanBuild in towersCanBuild)
            {
                towerCanBuild.button.ResetToDefault();
                towerCanBuild.button.buttonBuild.onClick.AddListener(delegate { CheckWantToBuild(towerCanBuild); });
            }
        }
        private void CheckWantToBuild(TowerCanBuild towerCanBuild)
        {
            if (towerCanBuild.button.IsAccepted() == true)
            {
                BuildTower(towerCanBuild);
                ResetCheckWantToBuild();
            }
            else
            {
                ResetCheckWantToBuild();
                towerCanBuild.button.OnHandleAccepted();
            }
        }
        private void BuildTower(TowerCanBuild towerCanBuild)
        {
            GameObject tower = Instantiate(towerCanBuild.gameObject);
            var towerScript = tower.GetComponent<TowerBase>();
            towerScript.Build(this);
            this.gameObject.SetActive(false);
            // reduce coin
        }
        public void ResetTowerKitStatus()
        {
            this.gameObject.SetActive(true);
        }
    }
}

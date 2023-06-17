using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Scripts.Tower
{
    public class TowerKITManager : MonoBehaviour
    {
        
        [SerializeField] TowerKIT towerKitPrefab;
        public List<TowerKIT> TowerKits;
        public KeyCode screenShotButton;
        void Update()
        {
            // if (Input.GetKeyDown(screenShotButton))
            // {
            //     ScreenCapture.CaptureScreenshot("screenshot.png");
            //     Debug.Log("A screenshot was taken!");
            // }
        }
        public void RemoveTowerKit()
        {
            // int lastTowerKitIndex = TowerKits.Count - 1;
            // TowerKits.RemoveAt(lastTowerKitIndex);
            foreach (TowerKIT tk in TowerKits)
            {
                if (tk.gameObject.activeSelf == true)
                {
                    tk.gameObject.SetActive(false);
                    return;
                }
            }
        }
        public void CreateTowerKit()
        {
            foreach (TowerKIT tk in TowerKits)
            {
                if (tk.gameObject.activeSelf == false)
                {
                    tk.gameObject.SetActive(true);
                    return;
                }
            }
            // TowerKIT newTowerKit = Instantiate(towerKitPrefab,transform,false);
            // TowerKits.Add(newTowerKit);
            // newTowerKit.gameObject.transform.SetParent(this.transform);
        }
    }
}

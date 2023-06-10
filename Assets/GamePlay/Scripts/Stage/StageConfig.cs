using GamePlay.Scripts.Tower;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageConfig_", menuName = "ScriptableObject/Stage/StageConfig")]
public class StageConfig : ScriptableObject
{
    public int stageId;
    [SerializeField] private List<Vector2> TowerKitLocation = new List<Vector2>();
    public int CountNumberTowerKit()
    {
        return TowerKitLocation.Count;
    }
    public void SaveTowerKITsPosition(List<TowerKIT> towerKITs)
    {
        TowerKitLocation.Clear();
        foreach (TowerKIT tk in towerKITs)
        {
            if (tk.gameObject.activeSelf == true)
            {
                TowerKitLocation.Add(tk.gameObject.transform.position);
            }
        }
    }
    public void LoadTowerKITsPosition(List<TowerKIT> towerKITs)
    {
        for (int i = 0; i < this.TowerKitLocation.Count; i++)
        {
            if (towerKITs[i].gameObject.activeSelf == false)
            {
                towerKITs[i].gameObject.SetActive(true);
            }
            towerKITs[i].gameObject.transform.position = this.TowerKitLocation[i];
        }
    }
}

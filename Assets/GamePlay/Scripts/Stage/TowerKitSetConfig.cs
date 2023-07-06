using GamePlay.Scripts.Tower;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerKitSet_", menuName = "ScriptableObject/Stage/TowerKitSetConfig")]
public class TowerKitSetConfig : ScriptableObject
{
    [SerializeField] private List<Vector2> towerKitLocation = new List<Vector2>();
    public int CountNumberTowerKit()
    {
        return towerKitLocation.Count;
    }
    public void SaveTowerKitPositionToOs(List<TowerKitManager> towerKITs)
    {
        towerKitLocation.Clear();
        foreach (TowerKitManager tk in towerKITs)
        {
            if (tk.gameObject.activeSelf == true)
            {
                towerKitLocation.Add(tk.gameObject.transform.position);
            }
        }
    }
    public void LoadTowerKitsPositionFromOs(List<TowerKitManager> towerKITs)
    {
        for (int i = 0; i < this.towerKitLocation.Count; i++)
        {
            if (towerKITs[i].gameObject.activeSelf == false)
            {
                towerKITs[i].gameObject.SetActive(true);
            }
            towerKITs[i].gameObject.transform.position = this.towerKitLocation[i];
        }
    }
}

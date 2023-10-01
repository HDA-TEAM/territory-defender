using GamePlay.Scripts.Tower;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TowerKitSetConfig_", menuName = "ScriptableObject/Database/Stage/TowerKitSetConfig")]
public class TowerKitSetConfig : ScriptableObject
{
    [FormerlySerializedAs("towerKitLocation")]
    [SerializeField] private List<Vector3> _towerKitLocation = new List<Vector3>();
    public void SaveTowerKitPositionToConfig(List<TowerKit> towerKITs)
    {
        // Clear config
        _towerKitLocation.Clear();
        
        foreach (TowerKit tk in towerKITs)
        {
            // Check if this Kit available to save
            if (tk.gameObject.activeSelf)
                _towerKitLocation.Add(tk.gameObject.transform.position );
        }
    }
    public void LoadTowerKitsPositionFromConfig(List<TowerKit> towerKITs)
    {
        for (int i = 0; i < this._towerKitLocation.Count; i++)
        {
            // Check if this Kit available to save
            if (!towerKITs[i].gameObject.activeSelf)
                towerKITs[i].gameObject.SetActive(true);
                
            // Save position of kit
            towerKITs[i].transform.position =  new Vector3(
                _towerKitLocation[i].x,
                _towerKitLocation[i].y,
                _towerKitLocation[i].z);
        }
    }
}

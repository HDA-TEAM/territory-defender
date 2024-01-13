
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "CommonTowerDataAsset", menuName = "ScriptableObject/DataAsset/CommonTowerDataAsset")]
public class CommonTowerDataAsset : ScriptableObject
{
    [SerializedDictionary("TowerId", "CommonTowerSO")] [SerializeField]
    private SerializedDictionary<TowerId, CommonTowerSO> _towerTypeDict = new SerializedDictionary<TowerId, CommonTowerSO>();
    
    private TowerId _towerId;
    public CommonTowerSO GetTowerType(TowerId towerId)
    {
        _towerTypeDict.TryGetValue(towerId, out CommonTowerSO tower);
        if (!tower)
        {
            Debug.LogError("Tower type not exist in dictionary");
            return _towerTypeDict[0];
        }
        return tower;
    }

    public List<CommonTowerSO> GetAllTowerData()
    {
        return _towerTypeDict.Values.ToList();
    }

    public void UpdateTowerData(TowerId towerId, RuneComposite runeComposite)
    {
        _towerTypeDict.TryGetValue(towerId, out CommonTowerSO curTower);
        Debug.Log("curTower" + curTower._towerId);
        if (!curTower)
        {
            Debug.LogError("Tower type not exist in dictionary");
        }
        else
        {
            RuneLevel runeLevel = new RuneLevel(runeComposite.RuneId, runeComposite.Level);
            int index = curTower.RuneLevels.FindIndex(r => r._runeId == runeComposite.RuneId);
            if (index != -1)
            {
                // RuneId exists, update the rune
                curTower.UpdateRune(index);
            }
            else
            {
                // RuneId does not exist, add a new rune
                curTower.AddRune(runeLevel);
            }
        }
    }

    public void ShowTowerRuneData(TowerId towerId)
    {
        _towerTypeDict.TryGetValue(towerId, out CommonTowerSO curTower);
        Debug.Log(curTower.RuneLevels.Count + " - ????????");
    }
}


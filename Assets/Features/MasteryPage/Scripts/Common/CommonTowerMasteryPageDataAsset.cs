
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "CommonTowerMasteryPageDataAsset", menuName = "ScriptableObject/DataAsset/CommonTowerMasteryPageDataAsset")]
public class CommonTowerMasteryPageDataAsset : ScriptableObject
{
    [SerializedDictionary("TowerId", "RuneData")] [SerializeField]
    private SerializedDictionary<TowerId, MasteryPageDataAsset> 
        _towerMasteryPageDict = new SerializedDictionary<TowerId, MasteryPageDataAsset>();
    
    #region MasteryPage access
    //public float Get
        
    #endregion
}


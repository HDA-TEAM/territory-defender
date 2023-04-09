using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuidingPlaceController : MonoBehaviour
{
    /// <summary>
    /// 1) Mapping tower building places for each stage
    /// 2) Wirting tower building places of a stage down xml file
    /// 3) Control towerBuildingKit action ( active,deactive)
    /// </summary>

    [SerializeField] private GameObject _buildingPlaces;
    [SerializeField] private GameObject _towerBuildingPlacePrefab;
    private static GameObject towerBuildingPlacePrefab;
    public List<GameObject> buildingPlaceList = new List<GameObject>();
    public static List<GameObject> _buildingPlaceList = new List<GameObject>();

    void Start(){
        towerBuildingPlacePrefab = _towerBuildingPlacePrefab;
        _buildingPlaceList = buildingPlaceList;
    }
    public static void ActiveInstance(Transform transform){
        GameObject tmp = null;
        float distance = float.MaxValue;
        foreach (var item in _buildingPlaceList)
        {
            float curDistance = Vector2.Distance(transform.position, item.transform.position);
                if(curDistance <= distance){
                    distance = curDistance;
                    tmp = item;
                }
        }
        if(tmp != null){
            tmp.gameObject.SetActive(true);
        }
    }

    public static void returnToBool(GameObject buildingPlace){
        buildingPlace.gameObject.SetActive(false);
    }

    public static void MappingTowerPlaceData(int stageId){
        _buildingPlaceList.Clear();
        StageData tmpStageData = XMLControler._stageDataList.FindStageData(stageId);
        foreach (var place in tmpStageData.towerPlaceList)
        {
            GameObject towerBuilding = Instantiate(towerBuildingPlacePrefab);
            towerBuilding.transform.position = new Vector3(
                    place.x,
                    place.y,
                    0f
                );
            _buildingPlaceList.Add(towerBuilding);
        }
    }
    public static void WriteDownTowerPlaceSetForStage(int stageId){
        StageData tmpStageData = XMLControler._stageDataList.FindStageData(stageId);
        tmpStageData.towerPlaceList.Clear();
        for (int i = 0; i < _buildingPlaceList.Count; i++)
        {
            
            TowerPlaceData tmpTL = new TowerPlaceData();
            tmpTL.towerPlaceId = i;
            tmpTL.x = _buildingPlaceList[i].transform.position.x;
            tmpTL.y = _buildingPlaceList[i].transform.position.y;
            tmpStageData.towerPlaceList.Add(tmpTL);
        }

        XMLControler.WriteDownXML<StageListData>("StageWrite.xml",XMLControler._stageDataList);
    }
  
}

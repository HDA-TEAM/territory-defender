using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RouteSet : MonoBehaviour
{
    /// <summary>
    /// 1) Collecting XML data to render gate and route 
    /// 2) Writing down gate and route to XML
    /// 3) Finding nearst point on specific route
    /// </summary>

    [SerializeField] private  List<LineRenderer> _gates;
    [SerializeField] private  SpriteRenderer sr;
    
    private static List<LineRenderer> gates;

    private void MappingGateData(int stageId){
        _gates = new List<LineRenderer>();
        StageData tmpStageData = XMLControler._stageDataList.FindStageData(stageId);
        foreach (var gate in tmpStageData.gateList)
        {
            LineRenderer tmpLr = new GameObject().AddComponent<LineRenderer>() as LineRenderer;
            tmpLr.transform.name = "Gate" + gate.gateId;
            tmpLr.transform.parent = gameObject.transform;
            List<PointData> tmpPointList = gate.pointList;
            tmpLr.positionCount = tmpPointList.Count;
            for (int i = 0; i < tmpPointList.Count; i++)
            {
                Vector3 tmpV3 = new Vector3(
                    tmpPointList[i].x,
                    tmpPointList[i].y,
                    0
                );
                tmpLr.SetPosition(i,tmpV3);
            }
            _gates.Add(tmpLr);
        }
    }
    public static void WriteDownGateSetForStage(int stageId){
        StageData tmpStageData = XMLControler._stageDataList.FindStageData(stageId);
        tmpStageData.gateList.Clear();
        for (int i = 0; i < gates.Count; i++)
        {
            LineRenderer tmpLr = gates[i];
            List<PointData> tmpPointList = new List<PointData>();
            for (int j = 0; j < tmpLr.positionCount; j++)
            {
                Vector3 tmpV3 = tmpLr.GetPosition(j);
                PointData tmpPoint = new PointData{};
                tmpPoint.x = tmpV3.x;
                tmpPoint.y = tmpV3.y;
                tmpPointList.Add(tmpPoint);
            }
            tmpStageData.gateList.Add(new GateData());
            tmpStageData.gateList[i].pointList = tmpPointList;
            tmpStageData.gateList[i].gateId = i;
        }
        XMLControler.WriteDownXML<StageListData>("StageWrite.xml",XMLControler._stageDataList);
    }
   
    public List<LineRenderer> GetGatesOfStage(int stageId){
        MappingGateData(stageId);
        float worldScreenHeight = Camera.main.orthographicSize * 2;

        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
       
        transform.localScale = new Vector3(
            worldScreenWidth  / (sr.sprite.bounds.size.x * sr.transform.localScale.x * transform.localScale.x),
            worldScreenHeight / (sr.sprite.bounds.size.y * sr.transform.localScale.y * transform.localScale.y), 1);
        foreach (var item in _gates)
        {
            for (int i = 0; i < item.positionCount; i++)
            {
                Vector3 tmp = item.GetPosition(i);
                tmp.x = tmp.x * transform.localScale.x;
         
                tmp.y = tmp.y * transform.localScale.y;
                item.SetPosition(i,tmp);
            }
        }
        gates = _gates;
        return _gates;
    }
    public static Vector2 FindNearestPoint(Vector2 point){
        Vector2 nearstPoint = new Vector2(0f,0f);
        float dis = float.MaxValue;
        foreach (var item in gates)
        {
            for (int i = 0; i < item.positionCount; i++)
            {
                float tmpDis = Vector2.Distance(item.GetPosition(i),point);
                if(tmpDis< dis){
                    dis = tmpDis;
                    nearstPoint = item.GetPosition(i);;
                }
            }
        }
        return nearstPoint;
    }
}

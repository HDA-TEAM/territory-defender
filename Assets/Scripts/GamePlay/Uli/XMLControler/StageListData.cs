using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Data class for XMLSerialize 
/// </summary>

[XmlRoot(ElementName = "StageList")]
public class StageListData{
    [XmlElement(ElementName = "Stage")]
    public List<StageData> stageListData;
    public  StageData FindStageData(int stageId){
        foreach (var item in stageListData)
        {
            if(item.stageId == stageId){
                return item;
            }
        }
        return null;
    }
}
[XmlRoot(ElementName = "Stage")]
public class StageData{
    [XmlAttribute(AttributeName = "stageId")]
    public int stageId;

    [XmlArray(ElementName = "UnitList")]
    [XmlArrayItem(ElementName = "Unit")]
    public List<UnitData> unitList;

    [XmlArray(ElementName = "GateList")]
    [XmlArrayItem(ElementName = "Gate")]
    public List<GateData> gateList;

    [XmlArray(ElementName = "TowerPlaceList")]
    [XmlArrayItem(ElementName = "TowerPlace")]
    public List<TowerPlaceData> towerPlaceList;

    [XmlArray(ElementName = "WaveList")]
    [XmlArrayItem(ElementName = "Wave")]
    public List<WaveData> waveList;
}

public class UnitData{
    [XmlAttribute(AttributeName = "unitId")]
    public int unitId;
}

public class GateData{
    [XmlAttribute(AttributeName = "gateId")]
    public int gateId;

    [XmlArray(ElementName = "PointList")]
    [XmlArrayItem(ElementName = "Point")]
    public List<PointData> pointList;
}

public class PointData{
    [XmlElement(ElementName = "X")]
    public float x;
    [XmlElement(ElementName = "Y")]
    public float y;
}


public class TowerPlaceData{
    [XmlAttribute(AttributeName = "towerPlaceId")]
    public int towerPlaceId;
    [XmlElement(ElementName = "X")]
    public float x;
    [XmlElement(ElementName = "Y")]
    public float y;
}

public class WaveData{
    [XmlArray(ElementName = "SubwaveList")]
    [XmlArrayItem(ElementName = "Subwave")]
    public List<SubwaveData> subwaveList;
    public int totalTimeWave;
    public float GetTotalTimeWave( float spawnTime){
        float maxTime = 0f;
        foreach (var subwave in subwaveList)
        {
            float tmpVal = subwave.appearTime * subwave.number;
            if(tmpVal > maxTime){
                maxTime = tmpVal;
            }
        }
        return maxTime;
    }
}


public class SubwaveData{
    [XmlAttribute(AttributeName = "subwaveId")]
    public int subwaveId;
    [XmlElement(ElementName = "AppearTime")]
    public float appearTime;
    [XmlElement(ElementName = "GateId")]
    public int gateId;
    [XmlElement(ElementName = "UnitID")]
    public int unitID;
    [XmlElement(ElementName = "Number")]
    public int number;
}


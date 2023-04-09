using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;


/// <summary>
/// Data class for XMLSerialize 
/// </summary>

[XmlRoot(ElementName = "TowerList")]
public class TowerListData{
    [XmlElement(ElementName = "Tower")]
    public List<TowerData> towerDataList;
    public TowerData FindTowerData(int towerId, int towerLevel){
        foreach (var item in towerDataList)
        {
            if(item.id == towerId && item.level == towerLevel){
                return item;
            }
        }
        return null;
    }
}
[XmlRoot(ElementName = "Tower")]
public class TowerData{
    [XmlAttribute(AttributeName = "id")]
    public int id;
    [XmlElement(ElementName = "level")]
    public int level;
    [XmlElement(ElementName = "name")]
    public string name;
    [XmlElement(ElementName = "attackDame")]
    public float attackDame;
    [XmlElement(ElementName = "health")]
    public float health;
    [XmlElement(ElementName = "attackSpeed")]
    public float attackSpeed;
    [XmlElement(ElementName = "attackRange")]
    public float attackRange;
    [XmlElement(ElementName = "price")]
    public int price;
}
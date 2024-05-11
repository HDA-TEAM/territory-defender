using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Datas.DataAsset
{
    public enum InventoryType
    {
        Star =  1,
        TalentPoint = 2,
        GoldenCoin = 3,
        SliverCoin = 4,
    }
    [Serializable]
    public struct InventoryData
    {
        public InventoryType InventoryType;
        public int Amount;
    }
    
    [Serializable]
    public struct InventoryDataModel : IDefaultDataModel
    {
        public List<InventoryData> ListInventoryData;
        public bool IsEmpty()
        {
            return false;
        }
        public void SetDefault()
        {
            ListInventoryData = new List<InventoryData>();
        }
    }
    
    [CreateAssetMenu(fileName = "InventoryDataAsset", menuName = "ScriptableObject/DataAsset/InventoryDataAsset")]
    public class InventoryDataAsset : BaseDataAsset<InventoryDataModel>
    {
        //[SerializedDictionary("StageId", "StageDataSO")] [SerializeField]
        //private SerializedDictionary<InventoryType, int>
            //_inventoryDataDict = new SerializedDictionary<InventoryType, int>();

        public List<InventoryData> InventoryDatas;
        public List<InventoryData> GetAllStageData()
        {
            return InventoryDatas;
        }
    }
}

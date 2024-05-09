using System;
using System.Collections.Generic;
using GamePlay.Scripts.Data;
using UnityEngine;

namespace Common.Scripts.Datas.DataAsset
{
    [Serializable]
    public struct CurrentInventory
    {
        public int Star;
    }
    
    [Serializable]
    public struct InventoryDataModel : IDefaultDataModel
    {
        public List<CurrentInventory> ListInventory;
        public bool IsEmpty()
        {
            return false;
        }
        public void SetDefault()
        {
            ListInventory = new List<CurrentInventory>();
        }
    }
    
    [CreateAssetMenu(fileName = "InventoryDataAsset", menuName = "ScriptableObject/Database/Stage/InventoryDataAsset")]
    public class InventoryDataAsset : BaseDataAsset<InventoryDataModel>
    {
        
    }
}

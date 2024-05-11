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

        #region MyRegion
        private Action<int> _onAmountChange;
        
        public void RegisterAmountChange(Action<int> action) => _onAmountChange += action;
        public void UnRegisterAmountChange(Action<int> action) => _onAmountChange -= action;
        public void NotifyAmountChange() => _onAmountChange?.Invoke(this.Amount);
        #endregion
        
        public void TryChangeAmount(int amount)
        {
            this.Amount += amount;
        }
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
        public List<InventoryData> InventoryDatas;
        
        public void AmountDataChange(InventoryType type, int amountChange)
        {
            for (int i = 0; i < InventoryDatas.Count; i++)
            {
                if (InventoryDatas[i].InventoryType == type)
                {
                    InventoryData updatedInventory = InventoryDatas[i];
                    updatedInventory.Amount += amountChange;
                    InventoryDatas[i] = updatedInventory; // Reassign the modified struct back to the list

                    // If there's a callback registered, call it
                    updatedInventory.NotifyAmountChange();
                    break;
                }
            }
        }
        public InventoryData GetInventoryDataByType(InventoryType type)
        {
            return InventoryDatas.Find(data => data.InventoryType == type);
        }
        public List<InventoryData> GetAllStageData()
        {
            return InventoryDatas;
        }
    }
}

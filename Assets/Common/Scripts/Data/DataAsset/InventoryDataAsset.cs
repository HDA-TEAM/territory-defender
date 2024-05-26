using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Scripts.Data.DataAsset
{
    public enum InventoryType
    {
        TotalStar =  1, // Total Star of all Stage are conquered
        TalentPoint = 2, // Point use to upgrade Rune
        GoldenCoin = 3, // Can be placed when purchase
        SliverCoin = 4, // Can be placed after complete each Stage
    }
    [Serializable]
    public struct InventoryData
    {
        public InventoryType InventoryType;
        public int Amount;

        #region Callback
        private Action<int> _onAmountChange;
        
        // TODO: check event inside struct, maybe it not run when create new struct
        public void RegisterAmountChange(Action<int> action) => _onAmountChange += action;
        public void UnRegisterAmountChange(Action<int> action) => _onAmountChange -= action;
        public void NotifyAmountChange() => _onAmountChange?.Invoke(this.Amount);
        #endregion
        
        public void TryChangeAmount(int amount)
        {
            Amount += amount;
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
            ListInventoryData = new List<InventoryData>
            {
                new InventoryData
                {
                    InventoryType = InventoryType.TotalStar,
                    Amount = 0,
                },
                new InventoryData
                {
                    InventoryType = InventoryType.TalentPoint,
                    Amount = 0,
                },
                new InventoryData
                {
                    InventoryType = InventoryType.GoldenCoin,
                    Amount = 0,
                },
                new InventoryData
                {
                    InventoryType = InventoryType.SliverCoin,
                    Amount = 0,
                },
            };
        }
    }
    
    [CreateAssetMenu(fileName = "InventoryDataAsset", menuName = "ScriptableObject/DataAsset/InventoryDataAsset")]
    public class InventoryDataAsset : LocalDataAsset<InventoryDataModel>
    {
        public List<InventoryData> InventoryDatas
        {
            get
            {
                return  _model.ListInventoryData ??= new List<InventoryData>();
            }
        }
        
        public void TryChangeInventoryData(InventoryType type, int amountChange)
        {
            for (int i = 0; i < InventoryDatas.Count; i++)
            {
                if (InventoryDatas[i].InventoryType != type)
                    continue;
                InventoryData updatedInventory = InventoryDatas[i];
                updatedInventory.Amount += amountChange;
                InventoryDatas[i] = updatedInventory; // Reassign the modified struct back to the list

                // If there's a callback registered, call it
                updatedInventory.NotifyAmountChange();
                break;
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

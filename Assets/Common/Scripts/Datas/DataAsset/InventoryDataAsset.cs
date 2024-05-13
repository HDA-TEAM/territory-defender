using System;
using System.Collections.Generic;
using GamePlay.Scripts.Data;
using UnityEngine;

namespace Common.Scripts.Datas.DataAsset
{
    public enum InventoryType
    {
        TotalStar   = 1, // Total Star of all Stage are conquered
        CurrentTalentPoint = 2, // Point use to upgrade Rune
        GoldenCoin  = 3, // Can be placed when purchase
        SliverCoin  = 4, // Can be placed after complete each Stage
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
        [SerializeField] private StageDataAsset _stageDataAsset;
        
        public List<InventoryData> InventoryDatas;

        public void UpdateTalentPointAmount()
        {
            // Update talent point base on data in json
        }
        public int CalculatorTotalTalentPoint()
        {
            int totalStar = _stageDataAsset.GetTotalStar();
            // for (int i = 0; i < InventoryDatas.Count; i++)
            // {
            //     if (InventoryDatas[i].InventoryType == InventoryType.CurrentTalentPoint)
            //     {
            //         InventoryData updatedInventory = InventoryDatas[i];
            //         
            //         // Talent point = total star * coefficient
            //         updatedInventory.Amount = (int)(totalStar * 1.5);
            //         InventoryDatas[i] = updatedInventory; // Reassign the modified struct back to the list
            //
            //         // If there's a callback registered, call it
            //         updatedInventory.NotifyAmountChange();
            //         break;
            //     }
            // }
            // Total Talent point = total star * coefficient
            return (int)(totalStar * 1.5);
        }
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

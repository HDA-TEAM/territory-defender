using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Common.Scripts
{
    public interface IItemSetupView<T>
    {
        void Setup(ItemViewBase<T> itemView, T data, Action<ItemViewBase<T>> onAction);
    }
    
    public abstract class ItemViewBase<T> : MonoBehaviour
    {
        protected Action<ItemViewBase<T>> OnSelected;

        public void Initialize(IItemSetupView<T> itemSetupView, T data, Action<ItemViewBase<T>> onAction)
        {
            OnSelected = onAction;
            itemSetupView.Setup(this, data, onAction);
        }
        
        protected void OnSelectedButton()
        {
            OnSelected?.Invoke(this);
        }
        
        protected abstract void SetName(T data);
    }
}

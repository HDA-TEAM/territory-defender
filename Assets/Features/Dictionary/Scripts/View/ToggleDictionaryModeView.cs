using Features.Dictionary.Scripts.View;
using System;

namespace Features.Dictionary.Scripts.ViewModel
{
    public class ToggleDictionaryModeView : UnitButtonViewBase
    {
        private Action<ToggleDictionaryModeView> _onSelected;

        public void SetUp(Action<ToggleDictionaryModeView> onSelected)
        {
            _onSelected = onSelected;
        }
        public void OnDefaultShow()
        {
            OnSelected();
        }
        protected override void OnSelected()
        {
            base.OnSelected();
            _onSelected?.Invoke(this);
        }
    }
}

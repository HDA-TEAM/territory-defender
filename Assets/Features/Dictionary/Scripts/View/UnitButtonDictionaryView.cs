using System;

namespace Features.Dictionary.Scripts.View
{
    public struct ButtonDictionaryComposite
    {
        public string UnitId;
        public string Name;
    }
    public class UnitButtonDictionaryView : UnitButtonViewBase
    {
        public ButtonDictionaryComposite DictionaryComposite;

        private Action<UnitButtonDictionaryView> _onSelected;

        public void SetUp(Action<UnitButtonDictionaryView> onSelected, ButtonDictionaryComposite buttonDictionaryComposite)
        {
            DictionaryComposite = buttonDictionaryComposite;
            _onSelected = onSelected;

            SetName(DictionaryComposite.Name);
        }
        protected override void OnSelected()
        {
            base.OnSelected();
            _onSelected?.Invoke(this);
        }
    }
}

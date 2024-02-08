using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIInHomeScreen
{
    public class ItemStageView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private Action<ItemStageView> _onSelected;

        public StageComposite StageComposite;
        private void Awake()
        {
            _button.onClick.AddListener(OnSelectedHero);
        }

        public void Setup(StageComposite stageComposite, Action<ItemStageView> onAction)
        {
            StageComposite = stageComposite;
            StageLoad(stageComposite.Mode);
        }

        private void OnSelectedHero()
        {
            Debug.Log("Stage " + StageComposite.Mode + " is opened");

            _onSelected?.Invoke(this);
        }

        private void StageLoad(string name)
        { 
            Debug.Log("Stage " + name + " is setting");
        }
    }
}



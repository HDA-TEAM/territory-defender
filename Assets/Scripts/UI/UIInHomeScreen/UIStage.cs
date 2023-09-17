using UnityEngine;
using UnityEngine.UI;

namespace UI.UIInHomeScreen
{
    public class UIStage : MonoBehaviour
    {
        private StageController _stage;
        [SerializeField] private Button _button;
        
        private void Start()
        {
            _button.onClick.AddListener(StageLoad);
        }

        public void StageLoad()
        { 
            Debug.Log("Stage is open");
            _stage = gameObject.AddComponent<StageController>();
            _stage.Load();
        }
    }
}



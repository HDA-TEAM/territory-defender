using TMPro;
using UnityEngine;

namespace GamePlay.Scripts.Menu.ResultPu
{
    public class StageSuccessRewardItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _txtAmount;
        public void SetUp(int amount)
        {
            _txtAmount.text = amount.ToString();
        }
    }
}

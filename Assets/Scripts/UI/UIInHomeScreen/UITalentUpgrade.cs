using UnityEngine;

namespace UI.UIInHomeScreen
{
    public class UITalentUpgrade : MonoBehaviour
    {
        [SerializeField] private GameObject upgradePicture;
        private TalentUpgradeController _talentUpgrade;

        private void Start()
        {
            //upgradePicture.GetComponent<Image>();
            _talentUpgrade = gameObject.AddComponent<TalentUpgradeController>();
        }

        public void TalentUpgradeLoad()
        {
            _talentUpgrade.Load(upgradePicture);
        }
    }
}


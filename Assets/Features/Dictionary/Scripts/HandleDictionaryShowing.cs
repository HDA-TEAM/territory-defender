using Features.Dictionary.Scripts.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Dictionary.Scripts
{
    public class HandleDictionaryShowing : MonoBehaviour
    {
        [SerializeField] private Button _btnTower;
        [SerializeField] private Button _btnBoss;
        [SerializeField] private EnemyDictionaryViewModel _enemyDictionaryViewModel;
        [SerializeField] private TowerDictionaryViewModel _towerDictionaryViewModel;
        public void Awake()
        {
            OpenTowerDictionary();
            _btnBoss.onClick.AddListener(OpenEnemyDictionary);
            _btnTower.onClick.AddListener(OpenTowerDictionary);
        }
        private void OpenTowerDictionary()
        {
            _towerDictionaryViewModel.SetUp();
            _towerDictionaryViewModel.gameObject.SetActive(true);
            _enemyDictionaryViewModel.gameObject.SetActive(false);
        }
        private void OpenEnemyDictionary()
        {
            _enemyDictionaryViewModel.SetUp();
            _enemyDictionaryViewModel.gameObject.SetActive(true);
            _towerDictionaryViewModel.gameObject.SetActive(false);
        }
    }
}

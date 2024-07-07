using UnityEngine;

namespace Features.Dictionary.Scripts.ViewModel
{
    public class ToggleDictionaryModeViewModel : MonoBehaviour
    {
        [SerializeField] private ToggleDictionaryModeView _towerToggleDictionaryMode;
        [SerializeField] private ToggleDictionaryModeView _enemyToggleDictionaryMode;
        
        [SerializeField] private EnemyDictionaryViewModel _enemyDictionaryViewModel;
        [SerializeField] private TowerDictionaryViewModel _towerDictionaryViewModel;

        private void Start()
        {
            SetUpViews();
            _towerToggleDictionaryMode.OnDefaultShow();
            _towerDictionaryViewModel.SetUp();
        }
        private void SetUpViews()
        {
            _towerToggleDictionaryMode.SetUp(OpenTowerDictionary);
            _enemyToggleDictionaryMode.SetUp(OpenEnemyDictionary);
        }
        private void OpenTowerDictionary(ToggleDictionaryModeView toggleDictionaryModeView)
        {
            _towerDictionaryViewModel.SetUp();
            _enemyToggleDictionaryMode.RemoveSelected();
            _towerDictionaryViewModel.gameObject.SetActive(true);
            _enemyDictionaryViewModel.gameObject.SetActive(false);
        }
        private void OpenEnemyDictionary(ToggleDictionaryModeView toggleDictionaryModeView)
        {
            _enemyDictionaryViewModel.SetUp();
            _towerToggleDictionaryMode.RemoveSelected();
            _enemyDictionaryViewModel.gameObject.SetActive(true);
            _towerDictionaryViewModel.gameObject.SetActive(false);
        }
    }
}

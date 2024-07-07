using UnityEngine;

namespace Features.Dictionary.Scripts.ViewModel
{
    public class ToggleDictionaryModeViewModel : MonoBehaviour
    {
        [SerializeField] private ToggleDictionaryModeView _towerToggleDictionaryMode;
        [SerializeField] private ToggleDictionaryModeView _enemyToggleDictionaryMode;
        
        [SerializeField] private EnemyDictionaryViewModel _enemyDictionaryViewModel;
        [SerializeField] private TowerDictionaryViewModel _towerDictionaryViewModel;
        
        [SerializeField] private CanvasGroup _enemyDictionaryViewModelCanvasGroup;
        [SerializeField] private CanvasGroup _towerDictionaryViewModelCanvasGroup;

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
            _enemyDictionaryViewModelCanvasGroup.alpha = 0;
            _towerDictionaryViewModelCanvasGroup.alpha = 1;
        }
        private void OpenEnemyDictionary(ToggleDictionaryModeView toggleDictionaryModeView)
        {
            _enemyDictionaryViewModel.SetUp();
            _towerToggleDictionaryMode.RemoveSelected();
            _enemyDictionaryViewModelCanvasGroup.alpha = 1;
            _towerDictionaryViewModelCanvasGroup.alpha = 0;
            _enemyDictionaryViewModel.gameObject.SetActive(true);
            _towerDictionaryViewModel.gameObject.SetActive(false);
        }
    }
}

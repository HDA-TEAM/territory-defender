using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [Header("Data"), Space(12)] [SerializeField]
    private InGameInventoryDataAsset _inventoryDataAsset;
    [SerializeField] private InGameResultsController _resultsController;
    private void Awake()
    {
        _inventoryDataAsset.RegisterLifeChange(OnLifeChange);
    }
    private void OnDestroy()
    {
        _inventoryDataAsset.UnRegisterLifeChange(OnLifeChange);
    }
    private void OnLifeChange(int life)
    {
        CheckingEndGame(life);
    }
    private void CheckingEndGame(int life)
    {
        if (life <= 0)
        {
            //todo
            // notify game ended 
            // show results
            Debug.Log("End game");
            _resultsController.gameObject.SetActive(true);
        }
    }
}

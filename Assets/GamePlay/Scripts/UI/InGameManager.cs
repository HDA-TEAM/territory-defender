using UnityEngine;
using UnityEngine.Serialization;

public class InGameManager : MonoBehaviour
{
    [Header("Data"), Space(12)] [SerializeField]
    private InGameInventoryRuntimeData _inventoryRuntimeData;
    [SerializeField] private InGameResultsController _resultsController;
    private void Awake()
    {
        _inventoryRuntimeData.RegisterLifeChange(OnLifeChange);
    }
    private void OnDestroy()
    {
        _inventoryRuntimeData.UnRegisterLifeChange(OnLifeChange);
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

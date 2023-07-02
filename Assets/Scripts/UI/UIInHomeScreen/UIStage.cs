using UnityEngine;

using UI.UIInHomeScreen;

public class UIStage : MonoBehaviour
{
    private StageController _stage;

    private void Start()
    {
        _stage = gameObject.AddComponent<StageController>();
    }

    public void StageLoad()
    { 
        _stage.Load();
    }
}


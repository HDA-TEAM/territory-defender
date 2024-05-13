using UnityEngine;

public class InitHomeScreen : MonoBehaviour
{
    private void Start()
    {
        UIManagerStateMachine.Instance.Init();
    }
}

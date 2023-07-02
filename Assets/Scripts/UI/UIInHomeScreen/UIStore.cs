using UnityEngine;
using UI.UIInHomeScreen;
public class UIStore : MonoBehaviour
{
    private StoreController _store;
    void Start()
    {
        _store = gameObject.AddComponent<StoreController>();
    }

    public void StoreLoad()
    {
        _store.Load();
    }
}

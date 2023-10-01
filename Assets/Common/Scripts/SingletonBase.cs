using UnityEngine;

public class SingletonBase<T>: MonoBehaviour where T : MonoBehaviour{
    private static T instance;
    public static T Instance {
        get {
            if (instance != null)
                return instance;
            instance = FindObjectOfType<T> ();
            if (instance != null)
                return instance;
            GameObject obj = new GameObject
            {
                name = typeof(T).Name,
            };
            instance = obj.AddComponent<T>();
            return instance;
        }
    }
 
    public virtual void Awake ()
    {
        if (instance == null) {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy (gameObject);
        }
    }
}

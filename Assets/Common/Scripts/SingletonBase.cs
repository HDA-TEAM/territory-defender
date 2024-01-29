using UnityEngine;

public class SingletonBase<T>: MonoBehaviour where T : MonoBehaviour{
    private static bool applicationIsQuitting = false;
    private static T instance;
    public static T Instance {
        get {
            
            // When OnDestroy is calling, so we need to prevent create the second singleton instance
            if (applicationIsQuitting)
                return null;

            // If instance is exist
            if (instance != null)
                return instance;
            
            // If instance is not exist, Try to find it
            instance = FindObjectOfType<T> ();
            if (instance != null)
                return instance;
            
            // If instance is not exist, and can't found it on scene, Create the new one
            GameObject obj = new GameObject
            {
                name = typeof(T).Name,
            };
            instance = obj.AddComponent<T>();
            
            return instance;
        }
    }
    public static bool IsAlive() => instance && !applicationIsQuitting;
    public void OnDestroy()
    {
        Debug.Log("Gets destroyed");
        applicationIsQuitting = true;
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

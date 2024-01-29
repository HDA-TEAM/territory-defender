using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolingBase : MonoBehaviour 
{
    private GameObject parent;
    private int initNumber;
    private GameObject prefab;
    private List<GameObject> poolObjects;
    public void InitPoolWithParam(int initNumber, GameObject prefab, GameObject parent)
    {
        this.parent = parent;
        this.initNumber = initNumber;
        this.prefab = prefab;
        InitPool();
    }
    
    private void InitPool()
    {
        poolObjects = new List<GameObject>();
        for (int i = 0; i < initNumber; i++)
            poolObjects.Add(InitObjectInstance());
    }
    private GameObject InitObjectInstance()
    {
        GameObject instance = Instantiate(prefab);
        instance.transform.SetParent(parent.transform);
        instance.SetActive(false);
        return instance;
    }
    public GameObject GetInstance()
    {
        foreach (GameObject i in poolObjects)
            if (i.gameObject.activeSelf == false)
                return i;
        GameObject go = InitObjectInstance();
        poolObjects.Add(go);
        return go;
    }
    public void ReturnPool(GameObject gameObject) => gameObject.gameObject.SetActive(false);

}
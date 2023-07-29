using System.Collections.Generic;
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
        {
            GameObject instance = Instantiate(prefab,parent.transform,false);
            poolObjects.Add(instance);
            instance.gameObject.transform.SetParent(parent.transform);
            instance.gameObject.SetActive(false);
        }
    }
    public GameObject GetInstance()
    {
        foreach (GameObject i in poolObjects)
        {
            if (i.gameObject.activeSelf == false)
            {
                return i;
            }
        }
        return null;
    }
    public void ReturnPool(GameObject gameObject)
    {
        gameObject.gameObject.SetActive(false);
    }
        
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [Range(1,100)] public int _poolSize = 10;

    private List<GameObject> _pool = new List<GameObject>();
    private void Awake(){        
        CreatePooler();
    }
    private void CreatePooler(){
        for (int i = 0; i < _poolSize; i++)
        {
            _pool.Add(CreateInstance());   
        }
    }
    private GameObject CreateInstance(){
        GameObject newInstance = Instantiate(_prefab);
       
        newInstance.transform.SetParent(transform);
        newInstance.SetActive(false);
        return newInstance;
    }
    public GameObject GetInstanceFromPool(){
        for (int i = 0; i < _pool.Count; i++)
        {
            if(!_pool[i].activeInHierarchy){
                _pool[i].transform.position = _prefab.transform.position;
                return _pool[i];
            }
        }
        return CreateInstance();
    }
    // Update is called once per frame
    public static void ReturnToPool(GameObject instance){
        instance.SetActive(false);
        
    }
    public static IEnumerator ReturnToPoolWithDelay(GameObject instance,float delay){
        yield return new WaitForSeconds(delay);
        instance.SetActive(false);

    }
}

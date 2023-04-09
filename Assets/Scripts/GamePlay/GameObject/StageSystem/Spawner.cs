using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create Mul object in specified gate
public class Spawner
{

    /// <summary>
    /// Spawnning object exist in pooler
    /// </summary>
    private LineRenderer _route;
    
    private ObjectPooler _pooler;

    public Spawner(ObjectPooler pooler,LineRenderer route){
        _pooler = pooler;
        _route = route;
    }
    
    public void SpawnerSingleObject(){

        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.SetActive(true);

        Human human = newInstance.GetComponent<Human>();

        if(human != null){
            human.SetRoute(_route);
        }
    }
}

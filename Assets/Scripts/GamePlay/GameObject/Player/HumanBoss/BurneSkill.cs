using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurneSkill : MonoBehaviour //Concrete Strategies 
{
    [SerializeField] private float _rangeExplore;
    
    public Player _target;

    private float _dame = 60f;

    void Start()
    {
        
    }
    void Update()
    {
        if (_target != null)
        {
            Employing();
        }   
    }

    public void Employing()
    {
        Debug.Log("Burne skill attack");
        
            DameByExplored();
        
        
        Destroy(this.gameObject, 1f);
    }

    private void DameByExplored()
    {
        GameObject[] instanceList = GameObject.FindGameObjectsWithTag("Evil");
        foreach (GameObject item in instanceList)
        {
            Evil evil = item.GetComponent<Evil>();
            if(Vector2.Distance(evil.transform.position, transform.position) < _rangeExplore)
            {
                evil.HealthReduce(_dame);
            }
            if(evil._health <= 0f)
            {
                evil.SelfDestroy();
            }
        }


    }
}

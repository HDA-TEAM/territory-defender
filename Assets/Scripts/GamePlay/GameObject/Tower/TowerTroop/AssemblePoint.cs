using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssemblePoint : MonoBehaviour
{    
    private SpriteRenderer _assemblePointSprite;
    public bool _settingAssemblePoint = false;

    public float _rangeToFire;

    private Vector2 _towerPoint;
   
    // Start is called before the first frame update
    void Start()
    {
        _towerPoint = this.transform.position;
        _assemblePointSprite = this.GetComponent<SpriteRenderer>();
        InitAssemblePoint();
    }
    void InitAssemblePoint(){
        transform.position = RouteSet.FindNearestPoint(_towerPoint);
        StartCoroutine(ShowAssemblePoint());
    }
    void Update()
    {
        if(_settingAssemblePoint == true){
            SetAssemblePoint();
        }
    }
    IEnumerator ShowAssemblePoint(){
        _assemblePointSprite.sortingLayerName = "Bullet";
        yield return new WaitForSeconds(1f);
        _assemblePointSprite.sortingLayerName = "Invisible";
    }
    private bool SetAssemblePoint(){
        
        if (Input.GetMouseButton(0))
        {
            Vector2 curMousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if(Vector2.Distance(_towerPoint,curMousePoint) < _rangeToFire){
                transform.position = curMousePoint;
                _settingAssemblePoint = false;
                StartCoroutine(ShowAssemblePoint());
                return true;
            }
        }
        return false;
    }
}

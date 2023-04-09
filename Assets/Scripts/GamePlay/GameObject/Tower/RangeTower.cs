using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeTower : MonoBehaviour{
    public List<Human> _humans;
    public float _range;

    [SerializeField] private Button _controlButton;
    
    void SetRangeScale(){
        transform.localScale = new Vector3(2*_range,2*_range,0f);
    }
    private void OnTriggerStay2D(Collider2D other){
        if(Vector2.Distance(transform.position,other.transform.position) <= _range){
            Human human = other.GetComponent<Human>();
            if(other.CompareTag("Human") && !_humans.Contains(human)){
                _humans.Add(human);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(Vector2.Distance(transform.position,other.transform.position) <= _range){
            Human human = other.GetComponent<Human>();
            if(other.CompareTag("Human") && !_humans.Contains(human)){
                _humans.Add(human);
            }
        }
    }
    public bool IsTargetInRange(){
        if(_humans[0] != null && Vector2.Distance(transform.position,_humans[0].transform.position) <= _range){
            return true;
        }
        else{
            return false;
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Human")){
            Human human = other.GetComponent<Human>();
            if(_humans.Contains(human)){
                _humans.Remove(human);
            }
        }
    }
    private void Update() {
        if(_controlButton.gameObject.activeSelf == true){
            HideRange();
        }
        else{
            ShowRange();
        }
        SetRangeScale();
    }

    public void ShowRange(){
        SpriteRenderer instance = this.GetComponent<SpriteRenderer>();
        instance.sortingLayerName = "GameObject";
    }
    public void HideRange(){
        SpriteRenderer instance = this.GetComponent<SpriteRenderer>();
        instance.sortingLayerName = "Invisible";
    }

}

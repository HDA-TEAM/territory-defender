using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;


public class InformationBoardControl : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TMP_Text _attackDameText;
    [SerializeField] private TMP_Text _attackSpeedText;
    [SerializeField] private TMP_Text _attackRangeText;
    [SerializeField] private GameObject _towerInformationBoard;

    private static float _attackDame;
    private static float _attackSpeed;
    private static float _attackRange;
    private static GameObject _towerBoard;

    public EventSystem _eventSystem;

    void Start()
    {      
        Debug.Log(SystemInfo.deviceUniqueIdentifier);
        _towerBoard = _towerInformationBoard;
    }
    
    void Update()
    {
        _attackDameText.text = _attackDame.ToString();
        _attackSpeedText.text = _attackSpeed.ToString();
        _attackRangeText.text = _attackRange.ToString();
        _towerInformationBoard.SetActive(_towerBoard.activeSelf);

        if(_eventSystem.currentSelectedGameObject == null){
            HideTowerInformation();
        } 
   
    }

    public static void ShowTowerInformation(float attackDame, float attackSpeed, float attackRange){
        _attackDame = attackDame;
        _attackSpeed = attackSpeed;
        _attackRange = attackRange;
        _towerBoard.SetActive(true);
    }
    public static void HideTowerInformation(){
        _towerBoard.SetActive(false);
    }
   
}

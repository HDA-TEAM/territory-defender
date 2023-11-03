using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NewsPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text newsTitle;

    // Start is called before the first frame update
    void Start()
    {
        newsTitle.text = UIString.Instance.NewsTitle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

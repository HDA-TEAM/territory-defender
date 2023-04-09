using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NewsPanelController : MonoBehaviour
{
    [SerializeField] TMP_Text _newsTitle;

    // Start is called before the first frame update
    void Start()
    {
        _newsTitle.text = UIString.Instance.newsTitle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

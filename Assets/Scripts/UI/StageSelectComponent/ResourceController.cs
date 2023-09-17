using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResourceController: MonoBehaviour
{
    [SerializeField] private TMP_Text _gemCount;
    [SerializeField] private TMP_Text _starCount;

    private User _user;

    // Start is called before the first frame update
    private void Start()
    {
        _user = User.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        _gemCount.text = _user._gem.ToString();
        _starCount.text = _user._star.ToString();
    }
}

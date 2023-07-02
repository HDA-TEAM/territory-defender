using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResourceController: MonoBehaviour
{
    [SerializeField] private TMP_Text gemCount;
    [SerializeField] private TMP_Text starCount;

    private User _user;

    // Start is called before the first frame update
    private void Start()
    {
        _user = User.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        gemCount.text = _user.gem.ToString();
        starCount.text = _user.star.ToString();
    }
}

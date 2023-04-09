using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResourceController: MonoBehaviour
{
    [SerializeField] TMP_Text _gemCount;
    [SerializeField] TMP_Text _starCount;

    User user;

    // Start is called before the first frame update
    void Start()
    {
        user = User.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        _gemCount.text = user.gem.ToString();
        _starCount.text = user.star.ToString();
    }
}

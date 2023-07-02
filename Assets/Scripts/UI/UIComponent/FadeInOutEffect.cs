using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutEffect : MonoBehaviour
{
    [SerializeField] private float fadeTime = 1f;
    private bool _fadeIn = true;
    private CanvasGroup _canvasGroup;

    private IEnumerator FadeCanvasGroup () {
        while (true) {
            if (_fadeIn) {
                _canvasGroup.alpha += Time.deltaTime / fadeTime;
            } else {
                _canvasGroup.alpha -= Time.deltaTime / fadeTime;
            }
            yield return null;

            if (_canvasGroup.alpha <= 0) {
                _canvasGroup.alpha = 0;
                _fadeIn = true;
            } else if (_canvasGroup.alpha >= 1) {
                _canvasGroup.alpha = 1;
                _fadeIn = false;
            }
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeCanvasGroup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

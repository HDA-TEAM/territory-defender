using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutEffect : MonoBehaviour
{
    [SerializeField] private float fadeTime = 1f;
    bool fadeIn = true;
    CanvasGroup canvasGroup;

    IEnumerator FadeCanvasGroup()
    {
        while (true)
        {
            if (fadeIn)
            {
                canvasGroup.alpha += Time.deltaTime / fadeTime;
            }
            else
            {
                canvasGroup.alpha -= Time.deltaTime / fadeTime;
            }
            yield return null;

            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                fadeIn = true;
            }
            else if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
                fadeIn = false;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeCanvasGroup());
    }

    // Update is called once per frame
    void Update()
    {

    }
}

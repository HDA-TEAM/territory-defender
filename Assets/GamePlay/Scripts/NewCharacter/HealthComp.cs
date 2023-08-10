using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;

public class HealthComp : MonoBehaviour
{
    [SerializeField] private int currentHealth = 50;
    [SerializeField] private TMP_Text txtToast;
    public async void PlayHurting(int dame)
    {
        txtToast.gameObject.SetActive(true);
        txtToast.text = dame.ToString();
        currentHealth -= dame;
        CheckDie();
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        txtToast.gameObject.SetActive(false);
    }
    private void CheckDie()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);            
        }
    }
}

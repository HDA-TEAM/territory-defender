using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class ActiveHeroAtStart : MonoBehaviour
{
    [SerializeField] private GameObject _hero;
    private async void Start()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        _hero.SetActive(true);
    }
}

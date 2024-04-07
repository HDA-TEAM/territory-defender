using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSuccessView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button _btnReplay;
    [SerializeField] private Button _btnQuit;
    [SerializeField] private List<GameObject> _stars;
    private Action _onClickQuit;
    private void Awake()
    {
        _btnQuit.onClick.AddListener(OnClickQuit);
    }
    private void OnClickQuit() => _onClickQuit?.Invoke();

    public void Setup(Action onClickQuit, int claimingStars)
    {
        _onClickQuit = onClickQuit;

        SetStar(claimingStars);

    }
    private void SetStar(int claimingStars)
    {
        for (int i = 0; i < _stars.Count; i++)
            _stars[i].SetActive(i < claimingStars);
    }
}

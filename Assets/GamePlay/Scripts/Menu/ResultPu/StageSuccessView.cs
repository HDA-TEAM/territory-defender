using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSuccessView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button _btnRestart;
    [SerializeField] private Button _btnContinue;
    [SerializeField] private List<StarView> _stars;
    [SerializeField] private float _durationPerStarDelay;
    private Action _onClickContinue;
    private void Awake()
    {
        _btnContinue.onClick.AddListener(OnClickContinue);
    }
    private void OnClickContinue() => _onClickContinue?.Invoke();

    public void Setup(Action onClickQuit, int claimingStars)
    {
        _onClickContinue = onClickQuit;

        SetStar(claimingStars);

    }
    private void SetStar(int claimingStars)
    {
        for (int i = 0; i < _stars.Count; i++)
            _stars[i].SetIconStar(i < claimingStars, _durationPerStarDelay * i);
    }
}

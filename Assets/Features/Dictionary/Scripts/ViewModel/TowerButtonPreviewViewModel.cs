using Features.Dictionary.Scripts.View;
using System;
using UnityEngine;

namespace Features.Dictionary.Scripts.ViewModel
{
    public class TowerButtonPreviewViewModel : MonoBehaviour
    {
        [SerializeField] private TowerButtonPreviewView _towerButtonPreviewLeftView;
        [SerializeField] private TowerButtonPreviewView _towerButtonPreviewRightView;

        public void Setup(Action leftClick, Action rightClick)
        {
            _towerButtonPreviewLeftView.gameObject.SetActive(leftClick != null);
            _towerButtonPreviewRightView.gameObject.SetActive(rightClick != null);
            _towerButtonPreviewLeftView.Setup(leftClick);
            _towerButtonPreviewRightView.Setup(rightClick);
        }
        
    }
}

using DG.Tweening;
using UnityEngine;

namespace GamePlay.Scripts.Tower.TowerKIT.TowerTools
{
    public class OpenTowerToolAnim : MonoBehaviour
    {
        [SerializeField] private float _durationAnim = 0.35f;
        [SerializeField] private float _startSize = 0.6f;
        private void OnEnable()
        {
            transform.localScale = _startSize * Vector3.one;
            transform.DOScale(Vector3.one, _durationAnim);
        }
    }
}

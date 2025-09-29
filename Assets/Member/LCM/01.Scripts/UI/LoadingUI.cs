using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Member.LCM._01.Scripts.UI
{
    public class LoadingUI : MonoBehaviour
    {
        [SerializeField] private Image loadingImage;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = _camera.WorldToScreenPoint(position);
            transform.rotation = _camera.transform.rotation;
        }
        public void SetTimeAndStartLoading(float time)
        {
            loadingImage.DOFillAmount(1f, time);
        }
    }
}
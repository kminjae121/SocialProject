using Core.Events;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangePanel : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO uiChannel;
    [SerializeField] private Image panel;

    private void Awake()
    {
        uiChannel.AddListener<SceneChangePanelEvent>(HandleChange);
    }

    private void OnEnable()
    {
        panel.DOFade(0, 1f);
    }

    private void OnDestroy()
    {
        uiChannel.RemoveListener<SceneChangePanelEvent>(HandleChange);
    }

    private void HandleChange(SceneChangePanelEvent evt)
    {
        panel.DOFade(evt.Enable? 1 : 0,1f).OnComplete(()=>
        {
            if (evt.Enable)
                SceneManager.LoadScene(evt.SceneName);
        });
    }
}

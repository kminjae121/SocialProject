using Core.Events;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class MessageReciever : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO messageChannel;
    [SerializeField] private TMP_Text messageTmp;

    private void Awake()
    {
        messageChannel.AddListener<MessageEvent>(HandleMessage);
        messageTmp.DOFade(0, 0f);
    }

    private void OnDestroy()
    {
        messageChannel.RemoveListener<MessageEvent>(HandleMessage);
    }

    private void HandleMessage(MessageEvent evt)
    {
        messageTmp.DOFade(0, 0f);
        DOTween.Kill(messageTmp);
        messageTmp.text = evt.Message;
        messageTmp.DOFade(1, 0.3f).OnComplete(() => messageTmp.DOFade(1, evt.LifeTime).OnComplete(() => messageTmp.DOFade(0, 0.3f)));
    }
}

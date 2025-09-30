using Core.Events;
using System;
using UnityEngine;

public class GameOverDetecter : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO resourceChannel;
    [SerializeField] private GameEventChannelSO mapChannel;
    [SerializeField] private GameEventChannelSO uiChannel;

    private int _blackoutIndex = 0;

    [Space] 
    [SerializeField] int maxBlackoutCount = 3;

    private void Awake()
    {
        _blackoutIndex = maxBlackoutCount;
        // resourceChannel.AddListener<TurnOffTheLight>(HandleBlackout);
        resourceChannel.AddListener<MoneyEvent>(HandleResource);
    }

    private void OnDestroy()
    {
        // resourceChannel.RemoveListener<TurnOffTheLight>(HandleBlackout);
        resourceChannel.RemoveListener<MoneyEvent>(HandleResource);
    }

    private void HandleResource(MoneyEvent evt)
    {
        if(evt.Money <= 0)
            uiChannel.RaiseEvent(UIEvents.SceneChangePanelEvent.Init(true, "OutOfMoneyOver"));
    }

    // private void HandleBlackout(TurnOffTheLight evt)
    // {
    //     print("블랙아웃:" + _blackoutIndex);
    //     if (evt.isTurnOff)
    //         _blackoutIndex--;
    //
    //     if(maxBlackoutCount <= _blackoutIndex)
    // }
}

public enum GameOverType
{
    Blackout,
    OutOfPopulation,
    OutOfMoney
}
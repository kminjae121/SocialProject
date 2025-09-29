using Core.Events;
using System;
using UnityEngine;

public class GameOverDetecter : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO resourceChannel;
    [SerializeField] private GameEventChannelSO mapChannel;
    [SerializeField] private GameEventChannelSO uiChannel;

    private int _blackoutIndex = 0;

    [Space] private int maxBlackoutCount = 3;

    private void Awake()
    {
        resourceChannel.AddListener<TurnOffTheLight>(HandleBlackout);
        resourceChannel.AddListener<MoneyEvent>(HandleResource);
    }

    private void OnDestroy()
    {
        resourceChannel.RemoveListener<TurnOffTheLight>(HandleBlackout);
        resourceChannel.RemoveListener<MoneyEvent>(HandleResource);
    }

    private void HandleResource(MoneyEvent evt)
    {
        if(evt.Money <= 0)
            uiChannel.RaiseEvent(UIEvents.SceneChangePanelEvent.Init(true, "OutOfMoneyOver"));
    }

    private void HandleBlackout(TurnOffTheLight evt)
    {
        if (evt.isTurnOff)
            _blackoutIndex++;

        if(maxBlackoutCount <= _blackoutIndex)
            uiChannel.RaiseEvent(UIEvents.SceneChangePanelEvent.Init(true,"BlackoutOver"));
    }
}

public enum GameOverType
{
    Blackout,
    OutOfPopulation,
    OutOfMoney
}
using Core.Events;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO resourceChannel;
    [SerializeField] private GameEventChannelSO buildingChannel;

    private void Awake()
    {
        resourceChannel.AddListener<GetResourceEvent>(HandleBlackout);
    }

    private void OnDestroy()
    {
        resourceChannel.RemoveListener<GetResourceEvent>(HandleBlackout);

    }
    private void HandleBlackout(GetResourceEvent rEvt)
    {
        var bEvt = LightEvent.lightEvent;
        bEvt.isTurnOff = rEvt.Electricity <= 0;
        buildingChannel.RaiseEvent(bEvt);
    }
}

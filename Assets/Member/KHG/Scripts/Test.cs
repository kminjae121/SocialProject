using Core.Events;
using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO buildingChannel;

    bool current;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("²¨Áü?:" + current);
            TurnOffTheLight evt = LightEvent.lightEvent;
            evt.isTurnOff = current!;
            buildingChannel.RaiseEvent(evt);
            current = !current;
        }
    }
}

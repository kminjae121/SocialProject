using Core.Events;
using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameEventChannelSO resourceChannel;

    bool current;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var evt = ResourceEvents.ElectricityEvent;
            evt.Electricity = -1;
            evt.AddedElectricity = 5000;
            resourceChannel.RaiseEvent(evt);
        }
    }
}

using Core.Events;
using UnityEngine;

public class UIEvents
{
    public static readonly SceneChangePanelEvent SceneChangePanelEvent = new();
}

public class SceneChangePanelEvent : GameEvent
{
    public bool Enable;
    public string SceneName;

    public SceneChangePanelEvent Init(bool value,string sceneName = null)
    {
        SceneName = sceneName;
        Enable = value;
        return this;
    }
}
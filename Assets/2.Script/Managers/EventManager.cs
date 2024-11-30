using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public enum Events
{
    OnGameOver,
    OnScoreUpgrade,
}

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    Dictionary<Events, Action> _eventDict = new Dictionary<Events, Action>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public void EnrollEvent(Events eventName, Action actionToEnroll)
    {
        _eventDict.TryGetValue(eventName, out Action action);

        if (action == null)
            _eventDict.Add(eventName, actionToEnroll);
        else
            action += actionToEnroll;
    }

    public void TriggerEvent(Events eventName)
    {
        _eventDict.TryGetValue(eventName, out Action action);

        if (action != null)
            action.Invoke();
        else
            Debug.LogWarning($"{eventName} is not enrolled in EventManager");

    }

    public void OnGameOver()
    {
        _eventDict.TryGetValue(Events.OnGameOver, out Action action);

        if (action != null)
            action.Invoke();
        else
            Debug.LogWarning($"{Events.OnGameOver.ToString()} is not enrolled in EventManager");
    }

}

using System;
using System.Collections.Generic;

public enum EventType
{
    SpawnFigures,
    PlaySound,
    ChangeSoundsVolume,
    IncreaseScore,
    ChangeUIColor,
    ChangeGridColor,
    AddMoney,
    SubtractMoney
}
public class EventBus
{
    private Dictionary<EventType, Delegate> eventHandlers = new Dictionary<EventType, Delegate>();

    public void Subscribe<T>(EventType eventType, Action<T> handler)
    {
        if (!eventHandlers.ContainsKey(eventType))
        {
            eventHandlers[eventType] = handler;
        }
        else
        {
            eventHandlers[eventType] = Delegate.Combine(eventHandlers[eventType], handler);
        }
    }

    public void Unsubscribe<T>(EventType eventType, Action<T> handler)
    {
        if (eventHandlers.ContainsKey(eventType))
        {
            eventHandlers[eventType] = Delegate.Remove(eventHandlers[eventType], handler);
        }
    }

    public void Publish<T>(EventType eventType, T argument)
    {
        if (eventHandlers.ContainsKey(eventType))
        {
            (eventHandlers[eventType] as Action<T>)?.Invoke(argument);
        }
    }
}

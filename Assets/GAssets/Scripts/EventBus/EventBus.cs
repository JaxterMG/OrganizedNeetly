using System;
using System.Collections.Generic;

namespace Core.EventBus
{
    
    public enum BusEventType
    {
        SpawnFigures,
        PlaySound,
        ChangeSoundsVolume,
        IncreaseScore,
        ChangeUIColor,
        ChangeGridColor,
        AddMoney,
        SubtractMoney,
        Revive
    }

    public class EventBus
    {
        private Dictionary<BusEventType, Delegate> eventHandlers = new Dictionary<BusEventType, Delegate>();

        public void Subscribe<T>(BusEventType eventType, Action<T> handler)
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

        public void Unsubscribe<T>(BusEventType eventType, Action<T> handler)
        {
            if (eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType] = Delegate.Remove(eventHandlers[eventType], handler);
            }
        }

        public void Publish<T>(BusEventType eventType, T argument)
        {
            if (eventHandlers.ContainsKey(eventType))
            {
                (eventHandlers[eventType] as Action<T>)?.Invoke(argument);
            }
        }
    }
}

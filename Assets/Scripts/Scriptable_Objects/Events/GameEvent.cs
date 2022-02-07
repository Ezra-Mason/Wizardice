using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Events/GameEvent")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> _eventListeners = new List<GameEventListener>();
    [SerializeField] private bool _log;

    public void Raise()
    {
        if (_log)
        {
            Debug.Log("Event " + this.name + " raised");
        }
        for (int i = _eventListeners.Count-1; i >= 0; i--)
            _eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!_eventListeners.Contains(listener))
            _eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (_eventListeners.Contains(listener))
            _eventListeners.Remove(listener);
    }
}

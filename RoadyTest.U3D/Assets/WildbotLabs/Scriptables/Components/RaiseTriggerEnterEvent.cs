using UnityEngine;
using WildbotLabs.Scriptables.GameEvents;

namespace WildbotLabs.Scriptables.Components
{
    public class RaiseTriggerEnterEvent : MonoBehaviour
    {
        [SerializeField] private GameEvent _gameEvent;
        [SerializeField] private GameObjectGameEvent _gameObjectGameEvent;

        private void OnTriggerEnter(Collider other)
        {
            _gameEvent?.Raise();
            _gameObjectGameEvent?.Raise(other.gameObject);
        }
    }
}
using UnityEngine;
using WildbotLabs.Scriptables.GameEvents;

namespace WildbotLabs.Scriptables.Components
{
    public class DeactiveByGameEvent : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _gameEvent;
        [SerializeField]
        private GameObject _gameObject;
        [SerializeField]
        private bool _activeOnStart = true;

        void Start()
        {
            if (_activeOnStart)
            {
                _gameObject.SetActive(true);
            }
        }

        void OnEnable()
        {
            _gameEvent.RegisterListener(UpdateActive);
        }

        void OnDisable()
        {
            _gameEvent.UnregisterListener(UpdateActive);
        }

        private void UpdateActive()
        {
            _gameObject.SetActive(false);
        }
    }
}

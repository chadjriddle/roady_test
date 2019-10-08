using UnityEngine;
using WildbotLabs.Scriptables.GameEvents;

namespace WildbotLabs.Scriptables.Components
{
    public class ActiveByGameEvent : MonoBehaviour {

        [SerializeField]
        private GameEvent _gameEvent;
        [SerializeField]
        private GameObject _gameObject;
        [SerializeField]
        private bool _deactiveOnStart = true;
    
        void Start ()
        {
            if (_deactiveOnStart)
            {
                _gameObject.SetActive(false);
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
            _gameObject.SetActive(true);
        }
    }
}

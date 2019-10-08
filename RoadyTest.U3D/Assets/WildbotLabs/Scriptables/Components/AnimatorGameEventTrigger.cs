using UnityEngine;
using WildbotLabs.Scriptables.GameEvents;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class AnimatorGameEventTrigger : MonoBehaviour
    {

        [SerializeField] private Animator _animator;
        [SerializeField] private StringReference _triggerName;
        [SerializeField] private GameEvent _gameEvent;

        void OnEnable()
        {
            _gameEvent.RegisterListener(Trigger);
        }

        void OnDisable()
        {
            _gameEvent.UnregisterListener(Trigger);
        }

        void Trigger()
        {
            _animator?.SetTrigger(_triggerName);
        }

    }
}

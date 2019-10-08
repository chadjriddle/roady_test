using UnityEngine;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class AnimatorDelayTrigger : MonoBehaviour
    {

        [SerializeField] private Animator _animator;
        [SerializeField] private StringReference _triggerName;
        [SerializeField] private FloatReference _delay;

        void OnEnable()
        {
            Invoke("Trigger", _delay);
        }

        void Trigger()
        {
            _animator?.SetTrigger(_triggerName);
        }

    }
}

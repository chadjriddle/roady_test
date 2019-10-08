using UnityEngine;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class AnimatorTriggerWhenTriggerEntered : MonoBehaviour
    {

        [SerializeField] private Animator _animator;
        [SerializeField] private StringReference _triggerName;
        [SerializeField] private StringReference _otherTag;

        private void OnTriggerEnter(Collider other)
        {
            if (string.IsNullOrWhiteSpace(_otherTag) || other.tag == _otherTag)
            {
                _animator?.SetTrigger(_triggerName);
            }
        }

    }
}

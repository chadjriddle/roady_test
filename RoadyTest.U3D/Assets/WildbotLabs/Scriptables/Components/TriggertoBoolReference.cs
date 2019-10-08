using UnityEngine;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class TriggertoBoolReference : MonoBehaviour
    {
        [SerializeField] private BoolReference _isTriggered;
        [SerializeField] private StringReference _otherTag;

        private void OnTriggerEnter(Collider other)
        {
            if (string.IsNullOrWhiteSpace(_otherTag) || other.tag == _otherTag)
            {
                _isTriggered.SetValue(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (string.IsNullOrWhiteSpace(_otherTag) || other.tag == _otherTag)
            {
                _isTriggered.SetValue(false);
            }
        }
    }
}

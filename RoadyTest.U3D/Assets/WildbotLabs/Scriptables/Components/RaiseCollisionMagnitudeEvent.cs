using UnityEngine;
using WildbotLabs.Scriptables.GameEvents;

namespace WildbotLabs.Scriptables.Components
{
    public class RaiseCollisionMagnitudeEvent : MonoBehaviour
    {
        [SerializeField] private FloatGameEvent _magnitudeEvent;
        private void OnCollisionEnter(Collision collision)
        {
            _magnitudeEvent.Raise(collision.relativeVelocity.magnitude);
        }
    }
}
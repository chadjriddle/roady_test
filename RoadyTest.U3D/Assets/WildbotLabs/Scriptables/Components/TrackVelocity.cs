using UnityEngine;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class TrackVelocity: MonoBehaviour
    {
        [SerializeField] private Vector3Reference _value;
        [SerializeField] private FloatReference _forwardVelocity;
        [SerializeField] private IntReference _runningTime;
        [SerializeField] private BoolReference _isRunning;

        private Rigidbody _rigidbody;

        // Use this for initialization
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            UpdateValue();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateValue();
        }

        private void UpdateValue()
        {
            _value?.SetValue(_rigidbody.velocity);
            _forwardVelocity?.SetValue(Mathf.Abs(_rigidbody.velocity.z));

            if (_isRunning && _isRunning.Value)
                _runningTime?.SetValue(_runningTime.Value + (int)(Time.deltaTime * 1000f));
        }
    }
}
using UnityEngine;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class SetPoistionByReference : MonoBehaviour
    {
        [SerializeField] private Vector3Reference _vectorReference;
        [SerializeField] private BoolReference _useLocalSpace;

        // Use this for initialization
        void Start ()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            if (_useLocalSpace)
            {
                transform.localPosition = _vectorReference;
            }
            else
            {
                transform.position = _vectorReference;
            }
        }

        void OnEnable()
        {
            _vectorReference.ValueChanged += UpdatePosition;
        }

        void OnDisable()
        {
            _vectorReference.ValueChanged -= UpdatePosition;
        }
    }
}
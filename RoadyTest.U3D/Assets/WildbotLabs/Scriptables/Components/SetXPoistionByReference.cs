using UnityEngine;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class SetXPoistionByReference : MonoBehaviour
    {
        [SerializeField] private FloatReference _xValue;
        [SerializeField] private BoolReference _useLocalSpace;

        // Use this for initialization
        void Start()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            if (_useLocalSpace)
            {
                transform.localPosition = new Vector3(_xValue, transform.localPosition.y, transform.localPosition.z);
            }
            else
            {
                transform.position = new Vector3(_xValue, transform.position.y, transform.position.z);
            }
        }

        void OnEnable()
        {
            _xValue.ValueChanged += UpdatePosition;
        }

        void OnDisable()
        {
            _xValue.ValueChanged -= UpdatePosition;
        }
    }
}
using UnityEngine;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class SetYPoistionByReference : MonoBehaviour
    {
        [SerializeField] private FloatReference _yValue;
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
                transform.localPosition = new Vector3(transform.localPosition.x, _yValue, transform.localPosition.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, _yValue, transform.position.z);
            }
        }

        void OnEnable()
        {
            _yValue.ValueChanged += UpdatePosition;
        }

        void OnDisable()
        {
            _yValue.ValueChanged -= UpdatePosition;
        }
    }
}
using UnityEngine;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class SetZPoistionByReference : MonoBehaviour
    {
        [SerializeField] private FloatReference _zValue;
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
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _zValue);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, _zValue);
            }
        }

        void OnEnable()
        {
            _zValue.ValueChanged += UpdatePosition;
        }

        void OnDisable()
        {
            _zValue.ValueChanged -= UpdatePosition;
        }
    }
}
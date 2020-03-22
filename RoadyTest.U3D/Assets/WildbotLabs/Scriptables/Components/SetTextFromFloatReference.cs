using UnityEngine;
using UnityEngine.UI;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class SetTextFromFloatReference : MonoBehaviour
    {
        [SerializeField] private FloatReference _value;
        [SerializeField] private Text _text;
        [SerializeField] private string _format = "{0:N2}";

        private float _lastValue = float.MinValue;

        void Start()
        {
            Reset();
        }

        // Update is called once per frame
        void Update ()
        {
            if (_value != null && _lastValue != _value.Value)
            {
                _lastValue = _value.Value;

                if (_text != null)
                {
                    _text.text = string.Format(_format, _lastValue);
                }
            }
        }

        void Reset()
        {
            if (_text == null)
            {
                _text = GetComponent<Text>();
            }
        }
    }
}
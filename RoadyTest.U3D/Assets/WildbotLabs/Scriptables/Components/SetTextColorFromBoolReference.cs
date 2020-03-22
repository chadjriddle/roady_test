using UnityEngine;
using UnityEngine.UI;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class SetTextColorFromBoolReference : MonoBehaviour
    {

        [SerializeField] private Text _text;
        [SerializeField] private BoolReference _value;
        [SerializeField] private ColorReference _trueColor;
        [SerializeField] private ColorReference _falseColor;

        // Use this for initialization
        void Awake ()
        {
            _value.ValueChanged += UpdateColor;
            _trueColor.ValueChanged += UpdateColor;
            _falseColor.ValueChanged += UpdateColor;
            UpdateColor();
        }

        private void UpdateColor()
        {
            if (_text)
            {
                _text.color = _value ? _trueColor : _falseColor;
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
using UnityEngine;
using UnityEngine.UI;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class SetTextFromBoolReference : MonoBehaviour
    {
        public BoolReference Value;
        public Text Text;
        public string TrueText = string.Empty;
        public string FalseText = string.Empty;

        private bool _lastValue = false;

        void Start()
        {
            UpdateText();
        }

        // Update is called once per frame
        void Update()
        {
            if (Text != null && Value != null && _lastValue != Value.Value)
            {
                UpdateText();
            }
        }

        private void UpdateText()
        {
            _lastValue = Value.Value;
            Text.text = Value.Value ? TrueText : FalseText;
        }
    }
}
using UnityEngine;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class KeyToBoolReference : MonoBehaviour {

        [SerializeField] private KeyCode _inputKey;
        [SerializeField] private BoolReference _value;

        // Update is called once per frame
        void Update()
        {
            _value.SetValue(Input.GetKey(_inputKey));
        }
    }
}

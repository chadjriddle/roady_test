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
            if (Input.GetKeyDown(_inputKey))
            {
                _value.SetValue(true);
            }
            else if (Input.GetKeyUp(_inputKey))
            {
                _value.SetValue(false);
            }
        }
    }
}

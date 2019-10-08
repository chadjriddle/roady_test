using UnityEngine;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class ButtonToBoolReference : MonoBehaviour {

        [SerializeField] private string _inputButton;
        [SerializeField] private BoolReference _value;

        // Update is called once per frame
        void Update () {
            _value.SetValue(Input.GetButton(_inputButton));
        }
    }
}

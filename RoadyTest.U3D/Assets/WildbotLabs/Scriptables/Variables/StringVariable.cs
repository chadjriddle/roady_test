using UnityEngine;

namespace WildbotLabs.Scriptables.Variables
{
    [CreateAssetMenu(menuName = "Variables/String")]
    public class StringVariable : GenericVariable<string>
    {
        public override void ApplyChange(string amount)
        {
            SetValue(amount);
        }

        public override void ApplyChange(GenericVariable<string> amount)
        {
            SetValue(amount.Value);
        }
    }
}
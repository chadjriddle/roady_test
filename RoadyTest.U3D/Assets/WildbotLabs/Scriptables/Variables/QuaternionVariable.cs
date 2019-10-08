using UnityEngine;

namespace WildbotLabs.Scriptables.Variables
{
    [CreateAssetMenu(menuName = "Variables/Quaternion")]
    public class QuaternionVariable : GenericVariable<Quaternion>
    {
        public override void ApplyChange(Quaternion amount)
        {
            SetValue(amount);
        }

        public override void ApplyChange(GenericVariable<Quaternion> amount)
        {
            SetValue(amount.Value);
        }
    }
}
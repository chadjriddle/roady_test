using System;
using UnityEngine;
using WildbotLabs.Scriptables.Variables;

namespace WildbotLabs.Scriptables.References
{
    [Serializable]
    public class QuaternionReference : GenericReference<Quaternion, QuaternionVariable>
    {
        public QuaternionReference() : base()
        {
        }

        public QuaternionReference(Quaternion constant) : base(constant)
        {
        }

        public QuaternionReference(QuaternionVariable variable) : base(variable)
        {
        }
    }
}
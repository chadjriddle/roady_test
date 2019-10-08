using System;
using UnityEngine;
using WildbotLabs.Scriptables.Variables;

namespace WildbotLabs.Scriptables.References
{
    [Serializable]
    public class ColorReference : GenericReference<Color, ColorVariable>
    {
        public ColorReference() : base()
        {
        }

        public ColorReference(Color constant) : base(constant)
        {
        }

        public ColorReference(ColorVariable variable) : base(variable)
        {
        }
    }
}
﻿using System;
using WildbotLabs.Scriptables.Variables;

namespace WildbotLabs.Scriptables.References
{
    [Serializable]
    public class BoolReference : GenericReference<bool, BoolVariable>
    {
        public BoolReference() : base()
        {
        }

        public BoolReference(bool constant) : base(constant)
        {
        }

        public BoolReference(BoolVariable variable) : base(variable)
        {
        }
    }
}
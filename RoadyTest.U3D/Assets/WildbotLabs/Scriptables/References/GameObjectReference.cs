using System;
using UnityEngine;
using WildbotLabs.Scriptables.Variables;

namespace WildbotLabs.Scriptables.References
{
    [Serializable]
    public class GameObjectReference : GenericReference<GameObject, GameObjectVariable>
    {
        public GameObjectReference() : base()
        {
        }

        public GameObjectReference(GameObject constant) : base(constant)
        {
        }

        public GameObjectReference(GameObjectVariable variable) : base(variable)
        {
        }
    }
}
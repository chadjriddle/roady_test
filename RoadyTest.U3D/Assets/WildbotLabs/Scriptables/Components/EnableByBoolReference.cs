﻿using UnityEngine;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class EnableByBoolReference : MonoBehaviour
    {
        [SerializeField] private BoolReference _value;
        [SerializeField] private bool _disableWhenTrue;
        [SerializeField] private Behaviour[] _behaviors;


        // Use this for initialization
        void Start ()
        {
            UpdateBehaviors();
            _value.ValueChanged += UpdateBehaviors;
        }

        private void UpdateBehaviors()
        {
            var newEnabledState = _disableWhenTrue ? !_value.Value : _value.Value;
            foreach (var behaviour in _behaviors)
            {
                if (behaviour != null)
                {
                    behaviour.enabled = newEnabledState;
                }
            }
        }
    }
}

using System;
using UnityEngine;

namespace WildbotLabs.Scriptables.GameEvents
{
    [CreateAssetMenu(menuName = "Events/Float Game Event")]
    [Serializable]
    public class FloatGameEvent : GenericGameEvent<float>
    {
    }
}
using System;
using UnityEngine;

namespace WildbotLabs.Scriptables.GameEvents
{
    [CreateAssetMenu(menuName = "Events/Vector3 Game Event")]
    [Serializable]
    public class Vector3GameEvent : GenericGameEvent<Vector3>
    {
    }
}
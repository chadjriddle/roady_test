using System;
using UnityEngine;

namespace WildbotLabs.Scriptables.GameEvents
{
    [CreateAssetMenu(menuName = "Events/GameObject Game Event")]
    [Serializable]
    public class GameObjectGameEvent : GenericGameEvent<GameObject>
    {
    }
}
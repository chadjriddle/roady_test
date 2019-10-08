using UnityEngine;
using WildbotLabs.Scriptables.GameEvents;

namespace WildbotLabs.Scriptables.Components
{
    public class RaiseGameEvent : MonoBehaviour
    {
        public GameEvent EventToRaise;

        public void Raise()
        {
            EventToRaise?.Raise();
        }
    }
}

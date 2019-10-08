using UnityEngine;
using UnityEngine.EventSystems;
using WildbotLabs.Scriptables.References;

namespace WildbotLabs.Scriptables.Components
{
    public class PointerDownToBoolReference : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        [SerializeField] private BoolReference _value;

        public void OnPointerDown(PointerEventData eventData)
        {
            _value?.SetValue(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _value?.SetValue(false);
        }
    }
}

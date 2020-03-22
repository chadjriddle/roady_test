using UnityEngine;
using UnityEngine.UI;
using WildbotLabs.Scriptables.References;

public class SyncSliderIntReference : MonoBehaviour
{
    public Slider Slider;
    public IntReference Value;

    void OnEnable()
    {
        if (Value.Variable?.UseRange == true)
        {
            Slider.minValue = Value.Variable.MinValue;
            Slider.maxValue = Value.Variable.MaxValue;
        }

        Slider.value = Value.Value;

        Value.ValueChanged += ValueChangedHandler;
    }

    void OnDisable()
    {
        Value.ValueChanged -= ValueChangedHandler;
    }

    private void ValueChangedHandler()
    {
        Slider.value = Value.Value;
    }

    // Update is called once per frame
    void Update()
    {
         Value.SetValue((int)Slider.value);
    }


}
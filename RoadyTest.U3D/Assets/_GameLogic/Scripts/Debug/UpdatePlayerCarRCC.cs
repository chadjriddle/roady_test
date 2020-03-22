using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WildbotLabs.Scriptables.References;

public class UpdatePlayerCarRCC : MonoBehaviour
{
    public FloatReference ComY;
    public FloatReference ComZ;
    public IntReference MaxSpeed;
    public IntReference CarMass;

    private RCC_CarControllerV3 _currentCarController;
    private Rigidbody _currentCarRigidbody;

    private bool _updating;

    void Update()
    {
        if (RCC_SceneManager.Instance.activePlayerVehicle != _currentCarController)
        {
            _updating = true;

            _currentCarController = RCC_SceneManager.Instance.activePlayerVehicle;
            _currentCarRigidbody = RCC_SceneManager.Instance.activePlayerVehicle.GetComponent<Rigidbody>();

            var comPos = RCC_SceneManager.Instance.activePlayerVehicle.COM.localPosition;
            ComY.SetValue(comPos.y);
            ComZ.SetValue(comPos.z);
            MaxSpeed.SetValue((int)RCC_SceneManager.Instance.activePlayerVehicle.maxspeed);
            CarMass.SetValue((int)_currentCarRigidbody.mass);

            _updating = false;
        }
    }

    void OnEnable()
    {
        ComY.ValueChanged += UpdateCar;
        ComZ.ValueChanged += UpdateCar;
        MaxSpeed.ValueChanged += UpdateCar;
        CarMass.ValueChanged += UpdateCar;
    }

    void OnDisable()
    {
        ComY.ValueChanged -= UpdateCar;
        ComZ.ValueChanged -= UpdateCar;
        MaxSpeed.ValueChanged -= UpdateCar;
        CarMass.ValueChanged -= UpdateCar;
    }

    private void UpdateCar()
    {
        if (_currentCarController != null && !_updating)
        {
            _currentCarController.COM.localPosition = new Vector3(0, ComY.Value, ComZ.Value);
            _currentCarController.maxspeed = MaxSpeed.Value;
            _currentCarRigidbody.mass = CarMass.Value; 
        }
    }
}

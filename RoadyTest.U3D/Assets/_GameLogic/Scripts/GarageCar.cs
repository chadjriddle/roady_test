using UnityEngine;

public class GarageCar : MonoBehaviour
{
    public ScriptableCar Car;
    public GameObject CarModel;

    public void SetCar(ScriptableCar car)
    {
        Car = car;
        Car.IsOwned.ValueChanged += IsOwned_ValueChanged;
        CarModel = Instantiate(Car.Model, transform);
    }

    private void OnEnable()
    {
        SetPurchased();
    }

    private void OnDestroy()
    {
        Car.IsOwned.ValueChanged -= IsOwned_ValueChanged;
    }

    private void IsOwned_ValueChanged()
    {
        SetPurchased();
    }

    private void SetPurchased()
    {
    }
}
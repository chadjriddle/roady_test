using UnityEngine;

public class CarPurchaseModel : MonoBehaviour
{
    public ScriptableCar Car;
    public float Scale = 20;

    private GameObject carModel;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetCar(ScriptableCar car)
    {
        Car = car;
        carModel = Instantiate(Car.Model, transform);
        carModel.transform.localScale = new Vector3(Scale, Scale, Scale);
    }
}

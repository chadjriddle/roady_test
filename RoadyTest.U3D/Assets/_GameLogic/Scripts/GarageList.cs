using UnityEngine;

public class GarageList : MonoBehaviour
{
    private CarsManager CarManager;

    public GameObject GarageCarPrefab;
    public float Spacing = 100;

    public ScriptableCar SelectedCar;

    // Start is called before the first frame update
    void Start()
    {
        CarManager = FindObjectOfType<CarsManager>();   
        BuildCarPanelItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BuildCarPanelItems()
    {
        var x = 0.0f;
        foreach (var car in CarManager.CarCache)
        {
            var go = Instantiate(GarageCarPrefab, transform);
            go.GetComponent<GarageCar>().SetCar(car);
            go.transform.localPosition = new Vector3(x, 0, 0);
            x += Spacing;
        }
    }
}
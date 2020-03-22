using UnityEngine;

public class CarPurchasePanel : MonoBehaviour
{
    public CarsManager CarManager;
    public GameObject CarPanelPrefab;
    public RectTransform CarPanel; 

    // Start is called before the first frame update
    void Start()
    {
        CarManager = FindObjectOfType<CarsManager>();
        BuildCarPanelItems();
    }

    private void BuildCarPanelItems()
    {
        foreach (var scriptableCar in CarManager.CarCache)
        {
            var go = Instantiate(CarPanelPrefab, CarPanel);
            go.GetComponent<CarPurchase>().Car = scriptableCar;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

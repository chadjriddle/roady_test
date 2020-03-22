using System.Collections.Generic;
using _GameLogic.Configuration;
using _GameLogic.Configuration.Generated;
using Assets._GameLogic.Configuration.Signals;
using UnityEngine;
using WildbotLabs.Scriptables.GameEvents;

public class CarsManager : MonoBehaviour
{
    private readonly Dictionary<string, ScriptableCar> _carsByProductId = new Dictionary<string, ScriptableCar>();

    [SerializeField]
    public ScriptableCar[] CarCache;

    public GameStateReference CurrentGameState;

    void Awake()
    {
        BuildCarsByProductId();
        UpdateOwnedCars();
        GameSignals.CarPurchased.AddListener(PurchaseCar);
    }

    void OnDestroy()
    {
        GameSignals.CarPurchased.RemoveListener(PurchaseCar);
    }

    public void ShowGarage()
    {
        CurrentGameState.SetValue(GameState.Garage);
    }

    private void PurchaseCar(ScriptableCar carPurchased)
    {
        if (_carsByProductId.TryGetValue(carPurchased.ProductId, out var car))
        {
            car.IsOwned.SetValue(true);
            ES3.Save<bool>(car.ProductId, true);
            Debug.Log("Car Purchased: " + car.Name);
        }
        else
        {
            Debug.LogError("Purchase Car Invalid ProductId: " + carPurchased.ProductId);
        }
    }

    public void ClearAllPurchases()
    {
        foreach (var car in CarCache)
        {
            car.IsOwned.SetValue(false);
            ES3.DeleteKey(car.ProductId);
        }
    }

    private void BuildCarsByProductId()
    {
        foreach (var scriptableCar in CarCache)
        {
            _carsByProductId.Add(scriptableCar.ProductId, scriptableCar);
        }
    }

    private void UpdateOwnedCars()
    {
        foreach (var scriptableCar in _carsByProductId)
        {
            scriptableCar.Value.IsOwned.SetValue(ES3.Load(scriptableCar.Key, false));
        }

        Debug.Log("All Cars Loaded");
    }
}

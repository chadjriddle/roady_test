using System.Collections;
using Assets._GameLogic.Configuration.Signals;
using UnityEngine;
using UnityEngine.Purchasing;

public class CarPurchase : MonoBehaviour
{
    public ScriptableCar Car;

    public GameObject iapButton;
    public GameObject checkMark;
    public CarPurchaseModel  CarPurchaseModel;

    private void Start()
    {
        var iap = iapButton.GetComponent<IAPButton>();
        iap.productId = Car.ProductId;

        Debug.Log("IAP Button Awake: " + Car.ProductId);

        Car.IsOwned.ValueChanged += IsOwned_ValueChanged;
        CarPurchaseModel.SetCar(Car);
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

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == Car.ProductId)
        {
            GameSignals.CarPurchased.Dispatch(Car);
        }   
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase of product " + product.definition.id + " failed due to " + reason);
    }

    private void SetPurchased()
    {
        Debug.Log("Set Purchased on Car " + Car.Name);
#if UNITY_EDITOR
        StartCoroutine(UpdateIapButton());
#else
        iapButton.SetActive(!Car.IsOwned);
        checkMark.SetActive(Car.IsOwned);
#endif
    }

    private IEnumerator UpdateIapButton()
    {
        yield return new WaitForEndOfFrame();
        iapButton.SetActive(!Car.IsOwned);
        checkMark.SetActive(Car.IsOwned);

        Debug.Log($"Update IAP Button {Car.Name} {Car.IsOwned.Value}");
    }
}

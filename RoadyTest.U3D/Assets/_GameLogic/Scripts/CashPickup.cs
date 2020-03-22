using UnityEngine;
using WildbotLabs.Scriptables.GameEvents;

public class CashPickup : MonoBehaviour
{
    public IntGameEvent CashEvent;
    public int CashAmount = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CashEvent.Raise(CashAmount);
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using WildbotLabs.Scriptables.References;

public class CashManager : MonoBehaviour
{
    public IntReference TotalCash;

    void Start()
    {
        TotalCash.SetValue(ES3.Load(SaveKeys.TotalCash, 0));
    }

    public void AddCash(int cashToAdd)
    {
        TotalCash.ApplyChange(cashToAdd);
        ES3.Save<int>(SaveKeys.TotalCash, TotalCash.Value);
    }
}

public static class SaveKeys
{
    public const string TotalCash = "TotalCash";
}

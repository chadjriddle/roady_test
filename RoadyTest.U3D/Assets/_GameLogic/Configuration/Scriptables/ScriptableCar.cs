using UnityEngine;
using WildbotLabs.Attributes;
using WildbotLabs.Scriptables.Variables;

[CreateAssetMenu(menuName = "GameData/ScriptableCar")]
public class ScriptableCar : ScriptableObject
{
    [SerializeField] 
    [InspectorReadOnly]
    private BoolVariable _isOwned;

    public string Name;
    public string ProductId;
    public BoolVariable IsOwned => _isOwned ?? (_isOwned = CreateInstance<BoolVariable>());
    public float Acceleration;
    public float MaxSpeed;
    public float Health;
    public float Damage;
    public float Turning;
    public float Drift;

    public GameObject Model;
}

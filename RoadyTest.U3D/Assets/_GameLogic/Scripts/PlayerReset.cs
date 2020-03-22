using UnityEngine;
using WildbotLabs.Scriptables.References;

public class PlayerReset : MonoBehaviour
{
    public Vector3Reference PlayerPosition;
    public float DeathY = -1.0f;

    private Vector3 _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = PlayerPosition.Value;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPosition.Value.y <= DeathY)
        {
            RCC_SceneManager.Instance.activePlayerVehicle.transform.position = _startPosition;
        }
    }
}

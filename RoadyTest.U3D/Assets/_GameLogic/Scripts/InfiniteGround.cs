using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WildbotLabs.Scriptables.References;

public class InfiniteGround : MonoBehaviour
{
    public Vector3Reference PlayerLocation = new Vector3Reference();

    public GameObject GroundPrefab;
    public int CacheXCount = 3;
    public int CacheZCount = 3;

    private Vector2 groundSize;
    private List<Collider> groundObjects = new List<Collider>();

    void Awake()
    {
        groundSize = DetermineGroundSize(GroundPrefab);
        UpdateGroundObjects();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector2 DetermineGroundSize(GameObject groundPrefab)
    {
        var groundCollider = groundPrefab.GetComponent<Collider>();
        return new Vector2(groundCollider.bounds.size.x, groundCollider.bounds.size.z);
    }

    private void UpdateGroundObjects()
    {
        var playerTileX = PlayerLocation.Value.x % groundSize.x;
        var playerTileZ = PlayerLocation.Value.z % groundSize.y;

        var maxTileX = playerTileX + (int)Mathf.Ceil((CacheXCount-1)/2.0f);
        var maxTileZ = playerTileZ + (int)Mathf.Ceil((CacheZCount-1)/2.0f);
        var minTileX = playerTileX - (int)Mathf.Ceil((CacheXCount-1)/2.0f);
        var minTileZ = playerTileZ - (int)Mathf.Ceil((CacheZCount-1)/2.0f);

        for (var x = minTileX; x <= maxTileX; x++)
        {
            for(var z = minTileZ; z <= maxTileZ; z++)
            {
                if (!GroundObjectExists(x, z))
                {
                    var groundObject = Instantiate(GroundPrefab, transform);
                    groundObject.transform.position = new Vector3(x, 0, z);
                    groundObjects.Add(groundObject.GetComponent<Collider>());
                }
            }
        }

    }

    private bool GroundObjectExists(float x, float z)
    {
        return groundObjects.TrueForAll(t => t.bounds.center.x == x && t.bounds.center.z == z);
    }
}

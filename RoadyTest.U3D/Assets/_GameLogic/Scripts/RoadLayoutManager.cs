using System.Linq;
using _GameLogic.Generators;
using UnityEngine;

public class RoadLayoutManager : MonoBehaviour
{
    public RoadGenerator Generator;
    public RoadTileMapping TileMappings;

    public GameObject RoadRoot;

    public Vector3 TopLeft = new Vector3(0, 0, 0);
    public Vector2 CellSize = new Vector2(20, 20);


    void Start()
    {
        Layout(RoadData.TestBlock);
    }

    public void ReGenerate()
    {
        Generator.Generate();
        Layout(Generator.CurrentBlock);
    }

    public void Layout(Block block)
    {
        ClearRoadRoot();
        for (int x = 0; x < block.Width; x++)
        {
            for (int y = 0; y < block.Height; y++)
            {
                GenerateRoadTile(x, y, block.GetCell(x, y));
            }
        }
    }

    private void ClearRoadRoot()
    {
        foreach(Transform child in RoadRoot.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void GenerateRoadTile(int x, int y, RoadTile tile)
    {
        if (tile == null)
        {
            return;
        }

        var mapping = TileMappings.TileMaps.FirstOrDefault(t => t.Id == tile.Id);
        if (mapping != null)
        {
            var go = Instantiate(mapping.TilePrefab, RoadRoot.transform);
            go.transform.eulerAngles = new Vector3(0, mapping.Rotation, 0);
            var tileX = TopLeft.x + (CellSize.x * x);
            var tileZ = TopLeft.y - (CellSize.y * y);

            go.transform.position = new Vector3(tileX, 0, tileZ);
        }
    }
}
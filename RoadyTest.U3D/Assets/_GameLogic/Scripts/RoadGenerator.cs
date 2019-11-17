using _GameLogic.Generators;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public int Seed = 100;
    public int BlockWidth = 40;
    public int BlockHeight = 40;
    public int MinRoadLength = 4;
    public int MaxRoadLength = 10;
    public int FourWayRadius = 5;

    private readonly _GameLogic.Generators.RoadGenerator _generator = new _GameLogic.Generators.RoadGenerator();

    public Block CurrentBlock { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate()
    {
        var inputs = new RoadInputs
        {
            Seed = Seed,
            BlockWidth = BlockWidth,
            BlockHeight = BlockHeight,
            MinRoadLength = MinRoadLength,
            MaxRoadLength = MaxRoadLength,
            FourWayRadius = FourWayRadius
        };

        CurrentBlock = _generator.Generate(inputs);

    }
}
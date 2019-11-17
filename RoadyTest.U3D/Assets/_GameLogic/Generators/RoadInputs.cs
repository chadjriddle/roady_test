using System;
using UnityEngine;

namespace _GameLogic.Generators
{
    [Serializable]
    public class RoadInputs
    {
        [SerializeField]
        public int Seed { get; set; }
        public int BlockWidth { get; set; }
        public int BlockHeight { get; set; }
        public int MinRoadLength { get; set; }
        public int MaxRoadLength { get; set; }
        public int FourWayRadius { get; set; }
    }
}
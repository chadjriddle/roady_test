using UnityEngine;

namespace _GameLogic.Scripts.Cars
{
    public class Axle3D : MonoBehaviour {

        public float DistanceToCG { get; set; }
        public float WeightRatio { get; set; } 
        public float SlipAngle { get; set; }
        public float FrictionForce => (TireLeft.FrictionForce + TireRight.FrictionForce) / 2f;

        public float AngularVelocity => Mathf.Min (TireLeft.AngularVelocity + TireRight.AngularVelocity);

        public float Torque => (TireLeft.Torque + TireRight.Torque) / 2f;

        public Tire3D TireLeft { get; private set; }
        public Tire3D TireRight { get; private set; }

        void Awake() {
            TireLeft = transform.Find ("TireLeft").GetComponent<Tire3D> ();
            TireRight = transform.Find ("TireRight").GetComponent<Tire3D> ();
        }

        public void Init(Rigidbody rb, float wheelBase) {

            // Weight distribution on each axle and tire
            WeightRatio = DistanceToCG / wheelBase;

            // Calculate resting weight of each Tire
            var weight = rb.mass * (WeightRatio * -Physics.gravity.y);
            TireLeft.RestingWeight = weight;
            TireRight.RestingWeight = weight;
        }

    }
}
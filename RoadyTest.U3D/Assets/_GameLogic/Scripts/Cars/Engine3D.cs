﻿using UnityEngine;

namespace _GameLogic.Scripts.Cars
{
    public class Engine3D : MonoBehaviour {
	
        [SerializeField]
        int[] TorqueCurve = { 100, 280, 325, 420, 460, 340, 300, 100 };

        [SerializeField]
        float[] GearRatios = { 5.8f, 4.5f, 3.74f, 2.8f, 1.6f, 0.79f, 4.2f };

        public int CurrentGear { get; private set; }

        public float GearRatio => GearRatios[CurrentGear];

        public float EffectiveGearRatio => GearRatios[GearRatios.GetLength(0) - 1];

        public void ShiftUp() {
            CurrentGear++;
        }

        public void ShiftDown() {
            CurrentGear--;
        }

        public float GetTorque(Rigidbody rb) {
            return GetTorque(GetRPM (rb));
        }

        public float GetRPM(Rigidbody rb) {
            return rb.velocity.magnitude / (Mathf.PI * 2 / 60f) * (GearRatio * EffectiveGearRatio);
        }

        public float GetTorque(float rpm)
        {
            if (rpm < 1000) {			
                return Mathf.Lerp (TorqueCurve [0], TorqueCurve [1], rpm / 1000f);
            }

            if (rpm < 2000) {
                return Mathf.Lerp (TorqueCurve [1], TorqueCurve [2], (rpm - 1000) / 1000f);
            }

            if (rpm < 3000) {
                return Mathf.Lerp (TorqueCurve [2], TorqueCurve [3], (rpm - 2000) / 1000f);
            }

            if (rpm < 4000) {
                return Mathf.Lerp (TorqueCurve [3], TorqueCurve [4], (rpm - 3000) / 1000f);
            }

            if (rpm < 5000) {
                return Mathf.Lerp (TorqueCurve [4], TorqueCurve [5], (rpm - 4000) / 1000f);
            }

            if (rpm < 6000) {
                return Mathf.Lerp (TorqueCurve [5], TorqueCurve [6], (rpm - 5000) / 1000f);
            }

            if (rpm < 7000) {
                return Mathf.Lerp (TorqueCurve [6], TorqueCurve [7], (rpm - 6000) / 1000f);
            }

            return TorqueCurve [6];
        }

        public void UpdateAutomaticTransmission(Rigidbody rb) {
            float rpm = GetRPM (rb);

            if (rpm > 6200) {
                if (CurrentGear < 5)
                    CurrentGear++;
            } else if (rpm < 2000) {
                if (CurrentGear > 0)
                    CurrentGear--;
            }
        }


    }
}
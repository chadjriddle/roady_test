﻿using UnityEngine;
using WildbotLabs.Scriptables.References;

namespace Assets._GameLogic.Scripts
{
    public class PlayerCarController : MonoBehaviour
    {
        [SerializeField]
        public Car Car;

        [SerializeField] private BoolReference InputRight;
        [SerializeField] private BoolReference InputLeft;
        [SerializeField] private BoolReference InputForward;
        [SerializeField] private BoolReference InputBackward;


        private void Update()
        {
            Car.InputForward.SetValue(InputForward);
            Car.InputBackward.SetValue(InputBackward);
            Car.InputRight.SetValue(InputRight);
            Car.InputLeft.SetValue(InputLeft);
            
        }

    }
}
using System.Linq;
using _GameLogic.Configuration;
using _GameLogic.Configuration.Generated;
using UnityEngine;

namespace _GameLogic.Components
{
    public class ActiveByGameStates : MonoBehaviour {

        [SerializeField]
        private GameObject _gameObject;

        [SerializeField] 
        private GameStateReference _gameState;

        [SerializeField] 
        private GameState[] _activeStates;

        void OnEnable()
        {
            UpdateActive();
            _gameState.ValueChanged += UpdateActive;
        }

        void OnDisable()
        {
            _gameState.ValueChanged -= UpdateActive;
        }

        private void UpdateActive()
        {
            _gameObject.SetActive(_activeStates.Contains(_gameState.Value));
        }
    }
}

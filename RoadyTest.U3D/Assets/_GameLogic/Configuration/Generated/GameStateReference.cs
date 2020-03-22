using System;
using WildbotLabs.Scriptables.References;


namespace _GameLogic.Configuration.Generated
{
    [Serializable]
    public class GameStateReference : GenericReference<GameState, GameStateVariable>
    {
        public GameStateReference() : base()
        {
        }

        public GameStateReference(GameState constant) : base(constant)
        {
        }

        public GameStateReference(GameStateVariable variable) : base(variable)
        {
        }
    }
}
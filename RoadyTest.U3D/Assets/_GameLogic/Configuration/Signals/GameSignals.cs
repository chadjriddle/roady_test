using WildbotLabs.Signals;

namespace Assets._GameLogic.Configuration.Signals
{
    public static class GameSignals
    {
        public static Signal<ScriptableCar> CarPurchased = new Signal<ScriptableCar>();
    }
}

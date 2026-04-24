using ScriptableObjects;

namespace Booster
{
    public interface IMagnetFlight : IBooster
    {
    }

    public interface IBooster
    {
        public BoosterType BoosterType { get; }
        float Duration { get; }
        void Activate();
        void Deactivate();
    }
}
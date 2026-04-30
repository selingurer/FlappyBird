using Cysharp.Threading.Tasks;
using ScriptableObjects;

namespace Booster
{
    public interface IMagnetFlight : IBooster
    {
    }

    public interface IBooster
    {
        public void SetData(BoosterRules data);
        public BoosterType BoosterType { get; }
        float Duration { get; }
        UniTask Activate(Bird bird);
        void Deactivate();
    }
    
}
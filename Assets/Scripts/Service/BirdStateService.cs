using Event;
using Service;

namespace DefaultNamespace
{
    public enum BirdState
    {
        DownFlap = 0,
        UpFlap = 1,
        MidFlap = 2,
        Dead = 3
    }

    public class BirdStateService : IBirdStateService
    {
        private BirdState _currentState = BirdState.MidFlap;

        public void EvaluateVelocity(float yVelocity)
        {
            if (_currentState == BirdState.Dead)
                return;

            if (yVelocity > 0.1f)
                SetState(BirdState.UpFlap);
            else if (yVelocity < -0.1f)
                SetState(BirdState.DownFlap);
        }
        
        public void OnDeath()
        {
            SetState(BirdState.Dead);
           
        }

        public void Reset()
        {
            _currentState = BirdState.MidFlap;
        }


        private void SetState(BirdState newState)
        {
            if (_currentState == newState)
                return;

            _currentState = newState;

            EventBus<BirdStateChanged>.Publish(new BirdStateChanged
            {
                BirdState = newState
            });
        }
    }

    public interface IBirdStateService : IResettable
    {
        public void EvaluateVelocity(float yVelocity);
        public void OnDeath();
    }
}
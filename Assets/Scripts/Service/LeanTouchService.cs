using System;
using Lean.Touch;

public interface ITouchService
{
    event Action OnTap;
}

public class LeanTouchService : ITouchService, IDisposable
{
    public event Action OnTap;

    public LeanTouchService()
    {
        LeanTouch.OnFingerTap += HandleFingerTap;
    }

    private void HandleFingerTap(LeanFinger finger)
    {
        OnTap?.Invoke();
    }

    public void Dispose()
    {
        LeanTouch.OnFingerTap -= HandleFingerTap;
    }
}
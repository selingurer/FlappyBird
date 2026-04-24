using Booster;
using ScriptableObjects;

public class MagnetFlightBooster : IMagnetFlight
{
    private bool _isActive;
    public bool IsActive => _isActive;

    public BoosterType BoosterType { get=> BoosterType.MagnetFlight; } 
    
    public float Duration { get; }

    public void Activate()
    {
        _isActive = true;
    }

    public void Deactivate()
    {
       _isActive = false;
    }

}
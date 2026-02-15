using DefaultNamespace;
using Service;
using UnityEngine;
using VContainer;

public class PipeFactory
{
    private readonly ObjectPool<PipePair> _pool;
    private readonly PipeLayoutCalculator _calculator;
    private readonly IDifficultyService _difficulty;

    [Inject]
    public PipeFactory(
        ObjectPool<PipePair> pool,
        PipeLayoutCalculator calculator,
        IDifficultyService difficulty)
    {
        _pool = pool;
        _calculator = calculator;
        _difficulty = difficulty;
    }

    public PipePair Create(float posX)
    {
        var pipe = _pool.GetObject();

        pipe.transform.position = new Vector3(posX, 0, 0);

        var data = _difficulty.GetCurrent();
        var layout = _calculator.Calculate(data);

        pipe.ApplyLayout(layout);
        pipe.StartMove();

        return pipe;
    }
    
    public void Return(PipePair pipe)
    {
        _pool.ReturnObject(pipe);
    }
}
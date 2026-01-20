using DefaultNamespace;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    [SerializeField] private Transform _pairTransform;
    [SerializeField] private PipePair _pairPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_pairTransform);
        builder.RegisterInstance(_pairPrefab);
        
        builder.Register<GameState>(Lifetime.Singleton);
        builder.Register<DifficultyService>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<MapService>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<GameService>(Lifetime.Singleton).AsImplementedInterfaces()
            .WithParameter(_pairPrefab)
            .WithParameter(_pairTransform);
    }
}
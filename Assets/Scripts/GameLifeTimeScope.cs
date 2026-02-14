using DefaultNamespace;
using ScriptableObjects;
using ScriptableObjects.UI;
using Service;
using Sound;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    [SerializeField] private Transform _pairTransform;
    [SerializeField] private PipePair _pairPrefab;
    [SerializeField] private BirdVisualData _birdVisualData;
    [SerializeField] private SpriteRenderer _birdSpriteRenderer;
    [SerializeField] private SoundDatas _soundDatas;
    [SerializeField] private BirdStateSoundConfig _birdSoundMap;
    [SerializeField] private UIPanelData _uiPanelData;
    [SerializeField] private Transform _canvasTransform;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_pairTransform);
        builder.RegisterInstance(_pairPrefab);
        builder.RegisterInstance(Camera.main);
        
        builder.RegisterComponentInHierarchy<Bird>()
            .As<IResettable>();
        builder.Register<ObjectPool<PipePair>>(resolver =>
                new ObjectPool<PipePair>(
                    resolver,
                    _pairPrefab,
                    _pairTransform),
            Lifetime.Singleton);
     
        builder.RegisterComponentInHierarchy<AudioPlayer>().AsImplementedInterfaces();
        builder.Register<LeanTouchService>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<PipeFactory>(Lifetime.Singleton);
        builder.Register<PipeLayoutFactory>(Lifetime.Singleton);
        builder.Register<PipeLayoutCalculator>(Lifetime.Singleton);
        builder.Register<GameStateService>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<DifficultyService>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<ScoreService>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<MapService>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<PanelController>(Lifetime.Singleton).AsImplementedInterfaces().WithParameter(_uiPanelData);
        builder.Register<GameService>(Lifetime.Singleton).AsImplementedInterfaces()
            .WithParameter(_pairPrefab)
            .WithParameter(_pairTransform);
        builder.Register<GameFlowController>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<BirdStateService>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<BirdVisualController>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf()
            .WithParameter(_birdVisualData).WithParameter(_birdSpriteRenderer);
        builder.Register<SoundService>(Lifetime.Singleton)
            .As<ISoundService>().WithParameter(_soundDatas);
        builder.Register<UIService>(Lifetime.Singleton).AsImplementedInterfaces().WithParameter(_uiPanelData).WithParameter(_canvasTransform);
        builder.Register<BirdSoundHandler>(Lifetime.Singleton).AsImplementedInterfaces().WithParameter(_birdSoundMap);
        builder.Register<SaveLoadService>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.RegisterEntryPoint<ScorePersistenceHandler>();
    
    }
}
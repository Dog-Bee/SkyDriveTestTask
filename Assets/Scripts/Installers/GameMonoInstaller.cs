using Core.Level;
using Core.Player;
using Infrastructure.Input;
using Infrastructure.Score;
using UnityEngine;
using Zenject;

public class GameMonoInstaller : MonoInstaller
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private StartGameInputHandler startGameInputHandler;
    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private EnvironmentSpawner environmentSpawner;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CameraController>().FromInstance(cameraController).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerController>().FromInstance(playerController).AsSingle();
        Container.BindInterfacesAndSelfTo<StartGameInputHandler>().FromInstance(startGameInputHandler).AsSingle();
        Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle();
        Container.Bind<PlatformSpawner>().FromInstance(platformSpawner).AsSingle();
        Container.Bind<ObstacleSpawner>().FromInstance(obstacleSpawner).AsSingle();
        Container.Bind<EnvironmentSpawner>().FromInstance(environmentSpawner).AsSingle();
    }
}
using Core.Game;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSceneInstaller", menuName = "Installers/GameSceneInstaller")]
public class GameSceneInstaller : ScriptableObjectInstaller<GameSceneInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<GameStateMenu>().AsSingle();
        Container.Bind<GameStatePlaying>().AsSingle();
        Container.Bind<GameStateGameOver>().AsSingle();
        Container.BindInterfacesAndSelfTo<TimerService>().AsSingle();//IInitializable
        
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle(); //IInitializable
    }
}
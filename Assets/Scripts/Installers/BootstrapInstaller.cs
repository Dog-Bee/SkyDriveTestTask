using Core.Camera;
using Infrastructure.Input;
using Signals;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BootstrapInstaller", menuName = "Installers/BootstrapInstaller")]
public class BootstrapInstaller : ScriptableObjectInstaller<BootstrapInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        
        DeclareSignals();
        
        Container.Bind<IInputService>().To<InputService>().AsSingle();

    }
    
    private void DeclareSignals()
    {
        Container.DeclareSignal<GameStartedSignal>();
        Container.DeclareSignal<PlayerDiedSignal>();
        Container.DeclareSignal<AsteroidPassedSignal>();
        Container.DeclareSignal<ScoreChangedSignal>();
        Container.DeclareSignal<BoostStateChangedSignal>();
        Container.DeclareSignal<GameStateChangedSignal>().OptionalSubscriber();
    }
}
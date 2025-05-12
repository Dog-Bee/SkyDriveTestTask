using Infrastructure.Score;
using UnityEngine;
using Zenject;

public class UIMonoInstaller : MonoInstaller
{
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private UIStateView stateView;
    [SerializeField] private SpeedometerView speedometerView;
    [SerializeField] private GameOverScreenView gameOverScreenView;
    public override void InstallBindings()
    {
        ViewBind();
        PresenterBind();
    }

    private void ViewBind()
    {
        Container.Bind<ScoreView>().FromInstance(scoreView).AsSingle();
        Container.Bind<UIStateView>().FromInstance(stateView).AsSingle();
        Container.Bind<SpeedometerView>().FromInstance(speedometerView).AsSingle();
        Container.Bind<GameOverScreenView>().FromInstance(gameOverScreenView).AsSingle();
    }

    private void PresenterBind()
    {
        Container.BindInterfacesAndSelfTo<ScorePresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<UIStatePresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<SpeedometerPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameOverScreenPresenter>().AsSingle();
    }
}
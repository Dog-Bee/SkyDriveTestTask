using System;
using System.Collections;
using System.Collections.Generic;
using Signals;
using UnityEngine;
using Zenject;

public class UIStatePresenter : IInitializable,IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly UIStateView _uiStateView;

    public UIStatePresenter(SignalBus signalBus, UIStateView uiStateView)
    {
        _signalBus = signalBus;
        _uiStateView = uiStateView;
    }
    
    public void Initialize()
    {
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChange);
        MenuState();
    }

    public void Dispose()
    {
        _signalBus.TryUnsubscribe<GameStateChangedSignal>(OnGameStateChange);
    }

    private void OnGameStateChange(GameStateChangedSignal signal)
    {
        switch (signal.State)
        {
            case GameStateType.Menu:
                MenuState();
                break;
            case GameStateType.Playing:
                GameState();
                break;
            case GameStateType.GameOver:
                GameOverState();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void MenuState()
    {
        _uiStateView.ShowMenu();
        _uiStateView.HideGameOver();
    }

    public void GameState()
    {
        _uiStateView.HideMenu();
        _uiStateView.ShowInGame();
    }

    public void GameOverState()
    {
        _uiStateView.HideInGame();
        _uiStateView.ShowGameOver();
    }
}

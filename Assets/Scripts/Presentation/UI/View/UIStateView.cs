using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Signals;
using UnityEngine;

public class UIStateView : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject gameOverUI;
    
    [Header("Animation Objects")]
    [SerializeField] private RectTransform topHud;
    [SerializeField] private RectTransform bottomHud;
    [SerializeField] private RectTransform startHud;
    [SerializeField] private CanvasGroup endHud;

    private float _animTime = .5f;
    public void ShowMenu()
    {
        menuUI.SetActive(true);
        Debug.Log(startHud.anchoredPosition.y);
        startHud.DOAnchorPos(Vector2.zero, _animTime).From(Vector2.down*bottomHud.sizeDelta.y).SetEase(Ease.OutBack);
    }

    public void HideMenu()
    {
        startHud.DOAnchorPos(Vector3.down*bottomHud.sizeDelta.y, _animTime).From(Vector3.zero).SetEase(Ease.OutBack).OnComplete(()=>menuUI.SetActive(false));
    }

    public void ShowInGame()
    {
        inGameUI.SetActive(true);
        topHud.DOAnchorPos(Vector3.zero, _animTime).From(Vector3.up*topHud.sizeDelta.y).SetEase(Ease.OutBack);
        bottomHud.DOAnchorPos(Vector3.zero, _animTime).From(Vector3.down * bottomHud.sizeDelta.y).SetEase(Ease.Linear);
    }

    public void HideInGame()
    {
        topHud.DOAnchorPos(Vector3.up*topHud.sizeDelta.y, _animTime).From(Vector3.zero).SetEase(Ease.OutBack);
        bottomHud.DOAnchorPos(Vector3.down * bottomHud.sizeDelta.y, _animTime).From(Vector3.zero).SetEase(Ease.Linear).OnComplete(()=>inGameUI.SetActive(false));
    }

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);
        endHud.DOFade(1,_animTime).From(0).SetEase(Ease.Linear);
    }

    public void HideGameOver()
    {
        endHud.DOFade(0,_animTime/2).From(1).SetEase(Ease.Linear).OnComplete(()=>gameOverUI.SetActive(false));

    }
    
}

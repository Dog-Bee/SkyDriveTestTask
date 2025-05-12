using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI asteroidText;
    [SerializeField] private RectTransform newHighRect;
    
    [SerializeField] private Button restartButton;

    private Tween _tween;
    public void StatUpdate(float score, int asteroids, string FormattedTime, bool isNewHighScore)
    {
        scoreText.text = $"SCORE: {score: 0.0}";
        timeText.text = $"TIME: {FormattedTime}";
        asteroidText.text = $"ASTEROIDS: {asteroids}";
        
        newHighRect.gameObject.SetActive(isNewHighScore);
        
        if (isNewHighScore)
        {
            
           _tween=newHighRect.DOScale(Vector3.one *1.2f,.8f).From(Vector3.one).SetEase(Ease.OutBack).SetLoops(-1, LoopType.Yoyo);
        }
    }

    public void Restart(System.Action onClick)
    {
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(() =>
        {
            _tween.Kill();
            onClick?.Invoke();
        });
    }
    
}

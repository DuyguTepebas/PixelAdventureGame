using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel, restartPanel, winPanel;
    [SerializeField] private Text scoreText, restartPanelScoreText, bestScoreText, startPanelBestScore, welcomeText;
    private int _bestScore = 0;
    [SerializeField] private GameManager gameManager;
    public void CloseStartPanel()
    {
        startPanel.SetActive(false);
    }

    private void Start()
    {
        //if (gameManager.Level > 0) welcomeText.enabled = false;
        _bestScore = PlayerPrefs.GetInt("BestScore", 0);
        ShowBestScore(_bestScore);
        ShowScore(PlayerPrefs.GetInt("ScoreSave", 0));
    }

    public void UpdateScoreText(int score)
    {
        HighestScore(score);
        ShowScore(score);
        ShowBestScore(_bestScore);
        //startPanelBestScore.text = "Best Score: " + _bestScore;
    }

    public void OpenRestartPanel()
    {
        restartPanel.SetActive(true);
        GameManager.isRestart = true;
    }

    public void ShowScore(int score)
    {
        // Debug.Log("Score on start panel : " + score);
        // Debug.Log("Score on restart panel : " + score);
        scoreText.text = score.ToString();
        restartPanelScoreText.text = "Score: " + score;
    }
    
    public void ShowBestScore(int bestScore)
    {
        // Debug.Log("Best Score on start panel : " + bestScore);
        // Debug.Log("Best Score on restart panel : " + bestScore);
        bestScoreText.text = "Best Score: " + bestScore;
        startPanelBestScore.text = "Best Score: " + bestScore;
    }

    public void HighestScore(int highestScore)
    {
        if (highestScore > _bestScore)
        {
            _bestScore = highestScore;
            PlayerPrefs.SetInt("BestScore",_bestScore);
        }
    }

    public void OpenWinPanel()
    {
        winPanel.SetActive(true);
    }
}//class

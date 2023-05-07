using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isStart;
    public static bool isRestart;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Player player;
    private int _level = 0;
    private int _score = 0;

    public int Level => _level;

    private void Awake()
    {
        _level = PlayerPrefs.GetInt("Level", 0);
        if (_level != SceneManager.GetActiveScene().buildIndex) SceneManager.LoadScene(_level);
    }

    private void Start()
    {
        _score = PlayerPrefs.GetInt("ScoreSave", 0);
        if(isRestart) uiManager.CloseStartPanel();
    }
    
    #region Reset Score
    public void ResetScore()
    {
        uiManager.ShowScore(_score);
        _score = 0;
        PlayerPrefs.SetInt("ScoreSave",_score);
    }
    

    #endregion
    
    #region Start Game

    public void StartGame()
    {
        isStart = true;
        uiManager.CloseStartPanel();
    }

    #endregion

    #region Try Again
    public void RestartGame()
    {
        isRestart = true;
        if (player.Health > 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
        {
            _level = 0;
            PlayerPrefs.SetInt("Level", _level);
            SceneManager.LoadScene(_level);
        }
    }
    #endregion

    #region Quit
    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion

    #region Score Increase

    public void ScoreIncrease()
    {
        _score += 5;
        PlayerPrefs.SetInt("ScoreSave",_score);
        uiManager.UpdateScoreText(_score);
    }

    #endregion

    public void NextLevel()
    {
        _level++;
        PlayerPrefs.SetInt("Level", _level);
        SceneManager.LoadScene(_level);
    }

    public void TurnLevelOne()
    {
        SceneManager.LoadScene(0);
    }
    
}// class

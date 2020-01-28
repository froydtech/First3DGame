using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public int lives;
    public bool gameOver;
    public int startScore;
    public static GameManager instance;
    public bool paused;

    void Awake()
    {
        if((instance != null) && (instance != this))
        {
           Destroy(gameObject);           
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
           
            paused = false;
        }

    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !gameOver)
        {
            TogglePauseGame();
        }
    }

    public void TogglePauseGame()
    {
        paused = !paused;
        if (!paused)
        {
            Time.timeScale = 1.0f;
        }
        else
            Time.timeScale = 0.0f;

        GameUI.instance.TogglePauseScreen(paused);
    }

    public void AddScore (int scoreToGive)
    {
        score += scoreToGive;
        GameUI.instance.UpdateScoreText();
    }

    public void LevelEnd()
    {
        // is this the last level?
        if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
        {
            WinGame();
        }
        else
        {
            startScore = score;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    public void WinGame()
    {
        GameUI.instance.SetEndScreen(true);
        Time.timeScale = 0.0f;
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver = true;
        GameUI.instance.SetEndScreen(false);
    }
}

#nullable enable
using System;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static readonly float defaultTimeScale = 1f;
    private static readonly float postGameTimeScale = 0.25f;

    private static GameManager? instance;
    public static GameManager Instance
    {
        #nullable disable
        get { return instance; }
        #nullable enable
    }

    private static int highscore = 0;

    private static int score = 0;
    public static int Score
    {
        get { return score; }
        set
        {
            if (value != score + 1)
            {
                throw new ArgumentOutOfRangeException("Can only increment score by 1!");
            } else {
                score = value;
            }
        }
    }

    public TMP_Text? scoreText;
    #nullable disable

    public TMP_Text highscoreText;

    public GameObject gameOverScreen;

    public void Awake()
    {
        Time.timeScale = defaultTimeScale;
        instance = this;
        UpdateHighscore();
    }

    public void EndGame()
    {
        Time.timeScale = postGameTimeScale;
        gameOverScreen.SetActive(true);

        Yeti.angularVelocityDegreesPerSecond = Yeti.startingAngularVelocityDegreesPerSecond;

        SpawnRocks.rockSpeedPerSecond = SpawnRocks.startingRockSpeedPerSecond;
        SpawnRocks.rockIntervalSeconds = SpawnRocks.startingRockIntervalSeconds;
        
        UpdateHighscore();

        score = 0;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        UpdateHighscore();
    }


    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void UpdateHighscore()
    {
        if (score > highscore)
        {
            highscore = score;
        }

        highscoreText.text = highscore.ToString();
    }
}

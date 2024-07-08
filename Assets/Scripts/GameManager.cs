#nullable enable
using System;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
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

    private static bool gameEnded;

    public static bool GameEnded
    {
        get { return gameEnded; }
    }

    public GameObject gameOverScreen;

    public void Awake()
    {
        Time.timeScale = 1;
        instance = this;
        UpdateHighscore();
    }

    // Start is called before the first frame update.
    public void Start()
    {
        gameEnded = false;

        score = 0;

        UpdateScoreText();

        UpdateHighscore();
    }

    public void EndGame()
    {
        gameEnded = true;

        Time.timeScale = 0.25f;
        gameOverScreen.SetActive(true);
        
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

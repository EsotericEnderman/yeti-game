#nullable enable
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

#nullable disable
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    public GameObject gameOverScreen;
#nullable enable

    private static readonly float defaultTimeScale = 1F;
    private static readonly float postGameTimeScale = 0.25F;

    private int highscore = 0;

    private int score = 0;
    public int Score { get => score; }
    public void IncrementScore()
    {
        score++;
    }

    public bool HasGameStarted() {
        return score >= 1;
    }

    public void Awake()
    {
        instance = this;

        UpdateHighscore();

        Time.timeScale = defaultTimeScale;
    }

    public void EndGame()
    {
        Time.timeScale = postGameTimeScale;
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

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

    private static int score;

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

    #nullable enable
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
        LoadData();

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
        SaveData();

        score = 0;
    }

    private void SaveData()
    {
        string jsonData = JsonUtility.ToJson(DataClass.Instance);

        if (jsonData != null)
        {
            jsonData = JsonUtility.ToJson(new DataStructure());
        }

        PlayerPrefs.SetString("data", jsonData);
    }

    private void LoadData()
    {
        string jsonData = PlayerPrefs.GetString("data");

        if (jsonData != "")
        {
            DataStructure data = JsonUtility.FromJson<DataStructure>(jsonData);

            Debug.Log(data);
            Debug.Log(data.highscore);

            Debug.Log(DataStaticClass.highscore);
            Debug.Log(DataClass.Instance);
            Debug.Log(DataClass.Instance.Highscore);

            DataStaticClass.highscore = data.highscore;
            DataClass.Instance.Highscore = data.highscore;

            UpdateHighscore();
        }
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
        if (score >= DataStaticClass.highscore)
        {
            if (DataClass.Instance != null)
            {
                DataClass.Instance.Highscore = score;
            }

            DataStaticClass.highscore = score;
        }

        highscoreText.text = DataStaticClass.highscore.ToString();
    }
}

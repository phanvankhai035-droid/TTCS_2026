using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float gameSpeed = 6f ;
    [SerializeField]
    private float speedIncrease = 0.15f;
    [SerializeField] private TextMeshProUGUI scoreText;
    private float score = 0;
    [SerializeField] private TextMeshProUGUI coinText;
    private int coin = 0;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI HighestScoreText;
    [SerializeField] private GameObject scoreTextObject;
    [SerializeField] private GameObject coinTextObject;
    [SerializeField] private GameObject gameStartMess;
    [SerializeField] private GameObject gameOverMess;
    [SerializeField] private GameObject finalScoreTextObject;
    [SerializeField] private GameObject HighestScoreTextObject;
    private bool isGameOver = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        HandleStartGameInput();
        if (!isGameOver)
        {
            UpdateGameSpeed();
            UpdateScore();
            UpdateCoin();
        }
        else
        {
            ReloadScene();
        }
    }


    private void UpdateGameSpeed()
    {
        gameSpeed += Time.deltaTime * speedIncrease;
    }
    private void UpdateScore()
    {
        score += Time.deltaTime * 10;
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }
    private void StartGame()
    {
        Time.timeScale = 0;
        scoreTextObject.SetActive(false);
        gameStartMess.SetActive(true);
        gameOverMess.SetActive(false);
        coinTextObject.SetActive(false);
        finalScoreTextObject.SetActive(false);
        HighestScoreTextObject.SetActive(false);
    }
    private void HandleStartGameInput()
    {
        if ((Input.GetKeyDown(KeyCode.Return)||Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space))&& Time.timeScale == 0)
        {
            Time.timeScale = 1;
            scoreTextObject.SetActive(true);
            gameStartMess.SetActive(false);
            coinTextObject.SetActive(true);
        }
    }
    public void GameOver()
    {
        isGameOver = true;
        gameOverMess.SetActive(true);
        scoreTextObject.SetActive(false);
        coinTextObject.SetActive(false);
        finalScoreTextObject.SetActive(true);
        HighestScoreTextObject.SetActive(true);

        int savedHighestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > savedHighestScore)
        {
            savedHighestScore = Mathf.FloorToInt(score);
            PlayerPrefs.SetInt("HighScore", savedHighestScore);
            PlayerPrefs.Save();
        }
        finalScoreText.text = "Score: " + Mathf.FloorToInt(score);
        HighestScoreText.text = "Best Score: " + savedHighestScore;
        Time.timeScale = 0;

    }
    private void ReloadScene()
    {
        if (Input.GetKeyDown(KeyCode.Return)||Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
    public void Addcoin(int points)
    {
        coin += points;
    }
    private void UpdateCoin()
    {
        coinText.text = "Coin: " + coin;
    }
}

using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float gameSpeed = 5f ;
    [SerializeField]
    private float speedIncrease = 0.15f;
    [SerializeField] private TextMeshProUGUI scoreText;
    private float score = 0;
    [SerializeField] private GameObject scoreTextObject;
    [SerializeField] private GameObject gameStartMess;
    [SerializeField] private GameObject gameOverMess;
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
    }
    private void HandleStartGameInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            scoreTextObject.SetActive(true);
            gameStartMess.SetActive(false);
        }
    }
    public void GameOver()
    {
        isGameOver = true;
        gameOverMess.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(ReloadScene());
    }
    private IEnumerator ReloadScene()
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

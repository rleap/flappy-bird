using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;

    public GameObject getReadyImage;
    public bool gameOver = false;
    public bool gamePaused = false;
    public float scrollSpeed = 2;
    public Button pauseButton;
    public Button playButton;
    public Button playAgainButton;

    public TMP_Text countDownText;
    private float currentTime = 0;
    private float startingTime = 3.2f;

    public int playerScore;
    public int playerHighScore;
    private string playerScoreStr;
    public TMP_Text scoreText;

    // Game Over Menu
    public GameObject gameOverImage;
    public GameObject gameOverMenu;
    public TMP_Text menuScoreText;
    public TMP_Text menuBestScoreText;
    public Image silverMedalImage;
    public Image goldMedalImage;

    private bool countDownSeen = false;

    public AudioSource pointSound;
    public AudioSource dieSound;

    private Dictionary<string, string> spriteDict;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        populateSpriteDict();
        PauseGame(true);
        getReadyImage.SetActive(true);
        currentTime = startingTime;
        countDownText.gameObject.SetActive(true);
        gameOverImage.SetActive(false);
        gameOverMenu.SetActive(false);
        silverMedalImage.enabled = false;
        goldMedalImage.enabled = false;
        playButton.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        playerScore = 0;
        playerHighScore = PlayerPrefs.GetInt("highScore");
        playerScoreStr = ConvertNumberToSpriteString(playerScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true)
        {
            playAgainButton.gameObject.SetActive(true);
            pauseButton.enabled = false;
        }

        // Count down timer
        if (countDownSeen == false)
        {
            currentTime -= 1 * Time.deltaTime;

            if (currentTime <= 3)
            {
                countDownText.text = spriteDict[currentTime.ToString("0")];
            }

            if (currentTime <= 0)
            {
                getReadyImage.SetActive(false);
                countDownText.gameObject.SetActive(false);
                PauseGame(false);
                countDownSeen = true;
            }
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BirdDied()
    {
        gameOver = true;
        gameOverImage.SetActive(true);
        gameOverMenu.SetActive(true);
        menuScoreText.text = playerScoreStr;
        menuBestScoreText.text = ConvertNumberToSpriteString(playerHighScore);
        dieSound.PlayDelayed(0.5f);
        if (playerScore > playerHighScore + 1)
        {
            silverMedalImage.gameObject.SetActive(true);
        }
        if (playerScore > playerHighScore + 2)
        {
            goldMedalImage.gameObject.SetActive(true);
        }

    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            Debug.Log("Game Paused");
        }
        else
        {
            Debug.Log("Game Unpause");
        }

        gamePaused = pause;
    }

    public void PauseGameButonTrue()
    {
        PauseGame(true);
    }

    public void PlayGameButton()
    {
        PauseGame(false);
    }

    public void AddScore(int points)
    {
        playerScore += points;
        playerScoreStr = ConvertNumberToSpriteString(playerScore);
        scoreText.text = playerScoreStr;
        UpdateHighScore(playerScore);
        pointSound.Play();
    }

    private void UpdateHighScore(int score)
    {
        if (score > playerHighScore)
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }

    private void ResetHighScore()
    {
        PlayerPrefs.SetInt("highScore", 0);
    }

    private string ConvertNumberToSpriteString(int num)
    {
        char[] charArr = num.ToString().ToCharArray();

        string spriteString = "";
        foreach(char n in charArr)
        {
            spriteString += spriteDict[n.ToString()];
        }
        return spriteString;
    }

    [ContextMenu("Populate Sprite Dict")]
    private void populateSpriteDict()
    {
        spriteDict = new Dictionary<string, string>();
        spriteDict.Add("0", "<sprite=8>");
        spriteDict.Add("1", "<sprite=66>");
        spriteDict.Add("2", "<sprite=32>");
        spriteDict.Add("3", "<sprite=33>");
        spriteDict.Add("4", "<sprite=34>");
        spriteDict.Add("5", "<sprite=35>");
        spriteDict.Add("6", "<sprite=36>");
        spriteDict.Add("7", "<sprite=37>");
        spriteDict.Add("8", "<sprite=38>");
        spriteDict.Add("9", "<sprite=39>");
    }
}

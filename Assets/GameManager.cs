using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int life;
    public GameObject ballPrefab;
    public Text textGameOver;
    private int score;
    private float leftTime;
    private Text textScore;
    private Text textLife;
    private Text textTimer;
    public Text textClear;
    static int highScore = 0;

    private Text textResult;
    private Text textResultLife;
    private Text textResultTime;
    private Text textResultTotal;
    private Text textHighScore;
    public GameObject nextSceneButton;
    public GameObject ReplayButton;

    private AudioSource audioSource;

    public AudioClip overSound;
    public AudioClip killSound;
    public AudioClip clearSound;

    private bool inGame;

    void Start()
    {
        life = 3;
        textGameOver.enabled = false;
        textClear.enabled = false;
        nextSceneButton.SetActive(false);
        ReplayButton.SetActive(false);

        score = 0;
        leftTime = 90f;
        audioSource = gameObject.AddComponent<AudioSource>();
        textScore = GameObject.Find("Score").GetComponent<Text>();
        textLife = GameObject.Find("PlayerLife").GetComponent<Text>();
        textTimer = GameObject.Find("TimeText").GetComponent<Text>();
        textResult = GameObject.Find("ResultScore").GetComponent<Text>();
        textResultLife= GameObject.Find("ResultLife").GetComponent<Text>();
        textResultTime = GameObject.Find("ResultTime").GetComponent<Text>();
        textResultTotal = GameObject.Find("ResultTotal").GetComponent<Text>();
        textHighScore = GameObject.Find("HighScore").GetComponent<Text>();
        SetScoreText(score);
        SetLifeText(life);
        SetHighScoreText(highScore);
        inGame = true;
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void SetLifeText(int life)
    {
        textLife.text = "Life :" + life.ToString();
    }
    private void SetHighScoreText(int highScore)
    {
        textHighScore.text = "High Score :" + highScore.ToString();
    }
    private void SetScoreText(int score)
    {
        textScore.text = "Score :" + score.ToString();

    }
    public void AddScore(int point)
    {
        if (inGame)
        {
            score += point;
            SetScoreText(score);
        }

    }
    void Update()
    {
        if (inGame)
        {
            if (score > 200)
            {
                audioSource.PlayOneShot(clearSound);
                textClear.enabled = true;
                nextSceneButton.SetActive(true);
                int scorePoint = score * 50; // スコア計算
                int scoreBall = life * 100; // スコア計算
                int scoreTime = (int)(leftTime * 1000f); // 少数値を整数型に直します(int) スコア計算
                textResult.text = "Score * 50 = " + scorePoint.ToString(); // 表示
                textResultLife.text = "Life * 100 = " + scoreBall.ToString(); // 表示
                textResultTime.text = "Time * 1000 = " + scoreTime.ToString(); // 表示
                int totalScore = scorePoint + scoreBall + scoreTime; // トータル計算
                textResultTotal.text = "Total Score :" + totalScore.ToString();

                if (highScore < totalScore) // 過去のハイスコアの値を上回っていたら
                {
                    highScore = totalScore; // ハイスコアを更新します（表示は次回のプレイ時から）
                }
                inGame = false;
            }
            leftTime -= Time.deltaTime;
            textTimer.text = "Time:" + (leftTime > 0f ? leftTime.ToString("0.00") : "0.00");
            if (leftTime < 0f)
            {
                audioSource.PlayOneShot(overSound);
                textGameOver.enabled = true;
                ReplayButton.SetActive(true);
                inGame = false;
            }

            GameObject ballObj = GameObject.Find("Player");
            if (ballObj == null)
            {
                --life;
                SetLifeText(life);
                if (life > 0)
                {
                    audioSource.PlayOneShot(killSound);
                    GameObject newBall = Instantiate(ballPrefab);
                    newBall.name = ballPrefab.name;
                }
                else
                {
                    life = 0;
                    audioSource.PlayOneShot(overSound);
                    textGameOver.enabled = true;
                    ReplayButton.SetActive(true);
                    inGame = false;
                }
            }
        }
    }
}

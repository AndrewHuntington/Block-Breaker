using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // text mesh pro

public class GameSession : MonoBehaviour
{
    // config params
    // [Range(float min, float max)]  -- sets number range in inspector
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 83;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;
    [SerializeField] TextMeshProUGUI lifeCount;

    // state variables
    [SerializeField] int currentScore = 0;
    [SerializeField] int currentLives = 2;

    // singleton pattern
    // keeps GameSession from being recreated every level, good for keeping score persistant
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false); // type this line whenever employing singleton pattern to avoid errors
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
        lifeCount.text = currentLives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void SubFromLives()
    {
        currentLives -= 1;
        lifeCount.text = currentLives.ToString();
    }

    public int CurrentLives()
    {
        return currentLives;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}

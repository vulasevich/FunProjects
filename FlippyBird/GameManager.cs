//This script controls the Game
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public int bestScore;
    public TextMeshProUGUI scoreText;
    public int score;
    public TextMeshProUGUI coinText;
    public int coins;
    public SpawnManager spawnManager;
    public PlayerControler player;
    public GameObject playerO;
    public GameObject gamePanel;
    public GameObject startPanel;
    public bool sstartPanel;


    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        player = GameObject.Find("Player").GetComponent<PlayerControler>();
        playerO = GameObject.Find("Player");
        score = 0;
        scoreText.text = score.ToString("0");
    }

    void Update()
    {

    }

    public void AddScores()
    {
        score += 1;
        scoreText.text = score.ToString("0");
    }
    public void AddCoins()
    {
        coins += 1;
        coinText.text = coins.ToString("0");
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        player.allive = true;
        playerO.transform.position = new Vector3(0, 0, 1);
        spawnManager.StartSpawnCorutine();
        sstartPanel = false;
        player.Jump();
    }

    public void EndGame()
    {

        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        player.allive = false;
        playerO.transform.position = new Vector3(0, 0, 1);
        sstartPanel = true;
        if (score > bestScore)
        {
            bestScore = score;
            bestScoreText.text = bestScore.ToString("0");
        }
    }

}

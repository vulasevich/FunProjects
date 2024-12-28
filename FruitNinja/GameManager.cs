//This script controls the game spawn and bufs 
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public List<GameObject> specialTargets;
    public float spawnRate = 1;
    public int hp = 3;
    public TextMeshProUGUI scoreText;
    public int score;
    public int shieldTime;
    public bool gameOver;
    public bool shieldActive;
    public GameObject overPanel;
    public GameObject[] hearts;


    [Header("Shield")]
    public GameObject shieldIcon;
    public TextMeshProUGUI shieldTimer;

    public void StartGame(int difficultyButton)
    {
        overPanel.SetActive(false);

        StartCoroutine(SpawnCoroutine());

        score = 0;
        scoreText.text = "Score: " + score;
    }

    IEnumerator SpawnCoroutine()
    {
        while (gameOver == false)
        {
            int random = Random.Range(0, targets.Count);
            int random2 = Random.Range(0, targets.Count * 2);
            if (random == random2)
            {
                Instantiate(specialTargets[Random.Range(0, specialTargets.Count)]);
            }
            else
            {
                Instantiate(targets[random]);
            }
            yield return new WaitForSeconds(spawnRate * Random.Range(0.9f, 1.2f));
        }


    }

    IEnumerator ShieldCoroutine()
    {

        shieldIcon.SetActive(true);
        shieldTimer.gameObject.SetActive(true);
        shieldActive = true;
        for (int shieldTime = 30; shieldTime >= 0; shieldTime--)
        {
            shieldTimer.text = shieldTime.ToString("0");
            yield return new WaitForSeconds(1);

            if (!shieldActive)
            {
                Debug.Log("break " + shieldTime);

                shieldIcon.SetActive(false);
                shieldTimer.gameObject.SetActive(false);
                break;
            }
        }
  
    }

    public void UpdateScore(int ScoreToAdd)
    {
        score += ScoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GetDamage()
    {
        hearts[hp - 1].SetActive(false);
        hp -= 1;
        if (hp <= 0)
        {
            GameOver();
        }
    }

    public void GetBuff()
    {
        Debug.Log("GetBuff");
        int random = 1;
        if (random == 1)
        {
            StartCoroutine(ShieldCoroutine());
        }
    }

    public void GameOver()
    {
        overPanel.SetActive(true);
        gameOver = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Prototype 5");
    }


}

//This script controls the Flippy Bird
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;


public class PlayerControler : MonoBehaviour
{
    public bool allive;
    public float forse;
    Rigidbody2D rb;
    public GameManager gameManager;
    public Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        if (allive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        if (transform.position.x < -3.8)
        {
            allive = false;
        }

        if (gameManager.sstartPanel)
        {
            if (transform.position.y <= -4)
            {
                Jump();
            }
        }
    }

    public void Jump()
    {
        rb.velocity = Vector2.up * forse;
        anim.Play("Bird");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
        gameManager.EndGame();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AddScore"))
        {
            gameManager.AddScores();
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            gameManager.AddCoins();
            Destroy(collision.gameObject);
        }
    }
}

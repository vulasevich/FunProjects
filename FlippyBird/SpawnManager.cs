//This script spans pipes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public PlayerControler player;
    public float SpawnRate;
    public GameObject coin;
    public GameObject Pape;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControler>();

    }

    void Update()
    {

    }

    public void StartSpawnCorutine()
    {
        StartCoroutine(SpawnCorutine());
    }

    IEnumerator SpawnCorutine()
    {
        while (player.allive)
        {
            float random = Random.Range(-3, 3);
            float random2 = Random.Range(0, 4);
            Instantiate(Pape, new Vector3(10, random, 0), Quaternion.identity);
            if (random2 == 1)
            {
                Instantiate(coin, new Vector3(10, random, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(SpawnRate * Random.Range(1, 1.3f));
        }
        StopCoroutine(SpawnCorutine());
    }    
}

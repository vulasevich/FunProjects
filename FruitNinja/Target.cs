using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody rb;
    public bool boom;
    public bool buff;
    public int price = 1;



    void Start()
    {
        rb = GetComponent<Rigidbody>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        rb.AddForce(Vector3.up * Random.Range(15, 18), ForceMode.Impulse);
        rb.AddTorque(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10), ForceMode.Impulse);
        transform.position = new Vector3(Random.Range(-8, 8), -6);
        rb.AddForce(Vector3.left * transform.position.x * Random.Range(0.9f, 1.2f), ForceMode.Impulse);


    }
    void Update()
    {




        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            if (boom)
            {
                if (!gameManager.shieldActive)
                {
                    gameManager.GetDamage();

                }
                else
                {
                                Debug.Log("Shield Deactive ");

                    gameManager.shieldActive = false;
                }
            }

            if (buff)
            {
                gameManager.GetBuff();
            }

            Destroy(gameObject);
            gameManager.UpdateScore(price);
        }

    }
}


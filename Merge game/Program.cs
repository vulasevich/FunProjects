using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towerplasing : MonoBehaviour
{
    public int type;
    public int ee;
    public GameObject[] towers;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));

            if (Vector3.Distance(mouseWorldPosition, transform.position) < 1f)
            {
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    transform.position = new Vector3(Mathf.FloorToInt(transform.position.x) + 0.5f, Mathf.FloorToInt(transform.position.y) + 0.5f);
                }
            }
        }
    }
}

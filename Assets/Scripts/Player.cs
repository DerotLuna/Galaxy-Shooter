using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    // Start is called before the first frame update (1 time)
    void Start()
    {
        //current pos = new position
        transform.position = new Vector3( 0f, 0f, 0f );
    }

    // Update is called once per frame. (After Start and 60 times per second )
    void Update()
    {
        playerMovementController();
    }

    private void playerMovementController()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime);

        limits_x();
        limits_y();
    }

    private void limits_y()
    {
        if (transform.position.y > 0f)
        {
            transform.position = new Vector3( transform.position.x, 0f, 0f );
        }
        else if(transform.position.y < -4.25f)
        {
            transform.position = new Vector3( transform.position.x, -4.25f, 0f );
        }
    }

    private void limits_x()
    {
        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0f);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0f);
        }
    }
}

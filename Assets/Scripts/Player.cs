using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;

    // Start is called before the first frame update (1 time)
    void Start()
    {
        //current pos = new position
        transform.position = new Vector3( 0f, 0f, 0f );
    }

    // Update is called once per frame. (After Start and 60 times per second )
    void Update()
    {
        float horizontalInput = Input.GetAxis( "Horizontal" );
        float verticalInput = Input.GetAxis( "Vertical" );


        transform.Translate( new Vector3( horizontalInput, verticalInput, 0 ) * speed * Time.deltaTime );
    }
}

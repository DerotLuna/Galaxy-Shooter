using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update (1 time)
    void Start()
    {
        //current pos = new position
        transform.position = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame. (After Start and 60 times per second )
    void Update()
    {
        
    }
}

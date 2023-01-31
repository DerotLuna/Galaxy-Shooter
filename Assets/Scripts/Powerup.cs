using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0F;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter2D(Collider2D other)
    {
        //other.name == "Player"
        if (other.tag == "Player")
        {
            playerCollision(other);
        }
    }

    private void playerCollision(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            if (tag == "TripleShoot")
            {
                player.tripleShootPowerupOn();
            }
            else if (tag == "SpeedBoost")
            {
                player.speedBoostPowerupOn();
            }
            else if (tag == "Shield")
            {
                player.shieldPowerupOn();
            }
        }
        Destroy(gameObject);
    }
}

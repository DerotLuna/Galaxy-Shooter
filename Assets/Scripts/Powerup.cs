using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0F;

    private float _limitScreenY = 6.0F;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -_limitScreenY)
        {
            Destroy(gameObject);
        }
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
            if (name.Contains("Triple_Shoot_Powerup"))
            {
                player.tripleShootPowerupOn();
            }
            else if (name.Contains("Speed_Boost_Powerup"))
            {
                player.speedBoostPowerupOn();
            }
            else if (name.Contains("Shield_Powerup"))
            {
                player.shieldPowerupOn();
            }
        }
        Destroy(gameObject);
    }
}

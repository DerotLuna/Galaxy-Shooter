using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    [SerializeField]
    private float _speed = 7.0F;

    private UIManager _uiManager;

    private float _limitScreenX = 7.5F;

    private float _limitScreenY = 6.5F;

    

    // Start is called before the first frame update
    void Start()
    {
        float randomX = Random.Range(-_limitScreenX, _limitScreenX);
        transform.position = new Vector3(randomX, _limitScreenY, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    private void movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -_limitScreenY)
        {
            float randomX = Random.Range(-_limitScreenX, _limitScreenX);
            transform.position = new Vector3(randomX, _limitScreenY, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerCollision(other);
        }
        else if(other.tag == "Laser")
        {
            laserCollision(other);
        }
    }

    private void playerCollision(Collider2D other)
    {
        updateScore();
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.subtractLive();
        }
        destroy();
    }

    private void laserCollision(Collider2D  other)
    {
        updateScore();
        Laser laser = other.GetComponent<Laser>();
        if (laser != null)
        {
            laser.destroy();
        }
        destroy();
    }

    private void updateScore()
    {
        if (_uiManager != null)
        {
            _uiManager.updateScore();
        }
    }

    private void destroy()
    {
        destructionAnimation();
        Destroy(gameObject);
    }

    private void destructionAnimation()
    {
        Instantiate(_enemyExplosionPrefab,
            transform.position,
            Quaternion.identity);
    }
}
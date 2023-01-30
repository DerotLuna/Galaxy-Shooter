using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShoot = false;

    [SerializeField]
    private float _speed = 5.0F;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleLaserPrefab;

    //fireRate is 0.25f
    //canFire -- has the amount of time between firing passed? Time.time
    [SerializeField]
    private float _fireRate = 0.20F;

    private float _nextFire = 0.0F;

    // Start is called before the first frame update (1 time)
    void Start()
    {
        //current pos = new position
        transform.position = new Vector3(0F, 0F, 0F);
    }

    // Update is called once per frame. (After Start and 60 times per second )
    void Update()
    {
        playerMovementController();
        shoot();
    }

    private void playerMovementController()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        limits_x();
        limits_y();
    }

    private void shoot()
    {
        //Space and right mouse click
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (Time.time > _nextFire)
            {
                if (canTripleShoot)
                {
                    //center laser, right laser, and left laser
                    Instantiate(_tripleLaserPrefab,
                    transform.position,
                    Quaternion.identity);
                }
                else
                {
                    //center laser
                    Instantiate(_laserPrefab,
                    new Vector3(transform.position.x, transform.position.y + 0.85F, transform.position.z),
                    Quaternion.identity);
                }

                _nextFire = Time.time + _fireRate;
            }
        }
    }

    private void limits_y()
    {
        if (transform.position.y > 0F)
        {
            transform.position = new Vector3(transform.position.x, 0F, 0F);
        }
        else if (transform.position.y < -4.25F)
        {
            transform.position = new Vector3(transform.position.x, -4.25F, 0F);
        }
    }

    private void limits_x()
    {
        if (transform.position.x > 9.5F)
        {
            transform.position = new Vector3(-9.5F, transform.position.y, 0F);
        }
        else if (transform.position.x < -9.5F)
        {
            transform.position = new Vector3(9.5F, transform.position.y, 0F);
        }
    }

    private void instantiateLaserPrefab(float x, float y)
    {
        Instantiate(_laserPrefab,
                    new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z),
                    Quaternion.identity);
    }

    private IEnumerator tripleShootPowerDownRoutine()
    {
        yield return new WaitForSeconds(3);
        canTripleShoot = false;
    }

    public void tripleShootPowerupOn()
    {
        canTripleShoot = true;
        StartCoroutine(tripleShootPowerDownRoutine());
    }
}

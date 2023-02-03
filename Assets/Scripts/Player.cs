using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0F;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleLaserPrefab;

    [SerializeField]
    private GameObject _playerExplosionPrefab;

    [SerializeField]
    private GameObject[] _failuresPrefabs;

    [SerializeField]
    private GameObject _shield;

    [SerializeField]
    private byte _lives = 3;

    [SerializeField]
    private AudioClip _audioLaser;

    [SerializeField]
    private AudioClip _audioPowerupOn;

    //fireRate is 0.25f
    //canFire -- has the amount of time between firing passed? Time.time
    [SerializeField]
    private float _fireRate = 0.20F;

    private float _nextFire = 0.0F;

    private UIManager _uiManager;

    private GameManager _gameManager;

    private AudioSource _audioSource;

    //Powerups
    private bool _canTripleShoot = false;

    private bool _isSpeedBoostActive = false;

    private bool _isShieldActive = false;

    // Start is called before the first frame update (1 time)
    void Start()
    {
        //current pos = new position
        transform.position = new Vector3(0F, 0F, 0F);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _audioSource = GetComponent<AudioSource>();

        if (_uiManager != null)
        {
            _uiManager.updateLives(_lives);
        }
    }

    // Update is called once per frame. (After Start and 60 times per second )
    void Update()
    {
        movement();
        shoot();
    }

    private void movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if(_isSpeedBoostActive)
        {
            _speed = 15.0F;
        }
        else
        {
            _speed = 5.0F;
        }

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
                _audioSource.PlayOneShot(_audioLaser);
                if (_canTripleShoot)
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

    private IEnumerator tripleShootPowerDownRoutine()
    {
        yield return new WaitForSeconds(6);
        _canTripleShoot = false;
    }

    public void tripleShootPowerupOn()
    {
        _canTripleShoot = true;
        activeAudioPowerupOn();
        StartCoroutine(tripleShootPowerDownRoutine());
    }

    private IEnumerator speedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(10);
        _isSpeedBoostActive = false;
    }

    public void speedBoostPowerupOn()
    {
        _isSpeedBoostActive = true;
        activeAudioPowerupOn();
        StartCoroutine(speedBoostPowerDownRoutine());
    }

    public void shieldPowerupOn()
    {
        _isShieldActive = true;
        activeAudioPowerupOn();
        _shield.gameObject.SetActive(true);
    }

    private void activeAudioPowerupOn()
    {
        _audioSource.PlayOneShot(_audioPowerupOn);
    }

    public void subtractLive()
    {
        if( _isShieldActive )
        {
            _isShieldActive = false;
            _shield.gameObject.SetActive(false);
        }
        else
        {
            _lives--;

            if (_uiManager != null)
            {
                _uiManager.updateLives(_lives);
            }

            if (_lives == 2)
            {
                _failuresPrefabs[0].SetActive(true);
            }
            else if (_lives == 1)
            {
                _failuresPrefabs[1].SetActive(true);
            }
            else
            {
                destroy();
                _gameManager.endGame();
            }
        }
    }

    private void destroy()
    {
        destructionAnimation();
        Destroy(gameObject);
    }

    private void destructionAnimation()
    {
        Instantiate(_playerExplosionPrefab,
            transform.position,
            Quaternion.identity);
    }
}

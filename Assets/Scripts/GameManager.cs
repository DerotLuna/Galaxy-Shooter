using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _gameOver = true;

    [SerializeField]
    private GameObject _player;

    private UIManager _uiManager;

    private SpawnManager _spawnManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        if (_gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _gameOver = false;
                if(_uiManager != null) 
                {
                    _uiManager.hideTitleScreen();
                    _uiManager.printScore();
                }
                if( _spawnManager!= null)
                {
                    _spawnManager.startSpawn();
                }
               
                Instantiate(_player,
                    Vector3.zero,
                    Quaternion.identity);
            }
        }

    }

    public void endGame()
    {
        _gameOver = true;
        if (_uiManager != null)
        {
            _uiManager.showTitleScreen();
            _uiManager.resetUI();
        }
        if (_spawnManager != null)
        {
            _spawnManager.stopSpawn();
        }
    }
}

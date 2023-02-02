using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float _limitScreenX = 7.5F;

    private float _limitScreenY = 6.5F;

    private bool _startRoutine = false;

    [SerializeField]
    private GameObject _enemyShipPrefab;

    [SerializeField]
    private GameObject[] _powerupPrefabs;

    // Start is called before the first frame update
    public void startSpawn()
    {
        _startRoutine = true;
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(spawnPowerupRoutine());
    }

    public void stopSpawn()
    { 
        _startRoutine = false;
    }

    private IEnumerator spawnEnemyRoutine()
    {
        while(_startRoutine)
        {
            spawnEnemy();
            yield return new WaitForSeconds(5.0F);
        }
    }

    private void spawnEnemy()
    {
        float randomX = Random.Range(-_limitScreenX, _limitScreenX);
        Instantiate(_enemyShipPrefab,
            new Vector3(randomX, _limitScreenY, 0),
            Quaternion.identity);
    }

    private IEnumerator spawnPowerupRoutine()
    {
        while (_startRoutine)
        {
            spawnPowerups();
            yield return new WaitForSeconds(5.0F);
        }
    }

    private void spawnPowerups()
    {
        float randomX = Random.Range(-_limitScreenX, _limitScreenX);
        byte randomIndex = (byte) Random.Range(0, 3);
        Instantiate(_powerupPrefabs[randomIndex],
            new Vector3(randomX, _limitScreenY, 0),
            Quaternion.identity);
    }
}

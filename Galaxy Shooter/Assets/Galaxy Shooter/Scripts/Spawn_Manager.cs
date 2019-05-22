using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyShipPrefab;

    [SerializeField]
    private GameObject[] _powerUps;

    private GameManager gameManager;  

	// Use this for initialization
	void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 
        StartCoroutine(SpawnEnemy());
        StartCoroutine(PowerUpSpawnRoutine()); 
	}
    
    public void StartSpawnRoutines()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(PowerUpSpawnRoutine()); 
    }

    public IEnumerator SpawnEnemy()
    {
        while (gameManager.gameOver == false)
        {
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7.75f, 7.75f), 6.70f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
        
    }

    public IEnumerator PowerUpSpawnRoutine()
    {
        while (gameManager.gameOver == false)
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerUps[randomPowerUp], new Vector3(Random.Range(-7.75f, 7.75f), 6.70f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
           
        }
         
    }
    
}

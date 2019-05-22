using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool canTripleShot = false;
    public bool canSpeedBoost = false;
    public bool isShieldActive = false; 
    private float _canFire = 0.0f;
    private int _hitCount = 0; 

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShot;
    
    [SerializeField]
    private float _fireRate = 0.25f;
    
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private GameObject _playerExplosion;

    [SerializeField]
    private GameObject _shieldPrefab;

    private UI_Manager _uIManager;
    private GameManager _gameManager;
    private Spawn_Manager _spawnManager;
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject[] _engines;

	// Use this for initialization
	void Start ()
    {
        transform.position = new Vector3(0, 0, 0);
        _uIManager = GameObject.Find("Canvas").GetComponent<UI_Manager>(); 
        
        if (_uIManager != null)
        {
            _uIManager.UpdateLives(_lives); 
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>(); 

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines(); 
        }

        _audioSource = GetComponent<AudioSource>();
        _hitCount = 0; 
	}
	
	// Update is called once per frame, game logic loop:
	void Update ()
    {
        Move();
        FireLaser(); 
    }

    private void Move()
    {
        //Get Position of Player: 
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (canSpeedBoost == true)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }
        
        else
        {
            //Apply Motion
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        

        //Control X-Axis Boundaries: 
        if (transform.position.y > 0)
        {
            //Resets the y value back to zero
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //Control Y-Axis Wrap Around: 
        if (transform.position.x < -9.4f)
        {
            transform.position = new Vector3(9.4f, transform.position.y, 0);
        }

        else if (transform.position.x > 9.4f)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
    }

    private void FireLaser()
    {
        //If cool down is done, then fire laser: 
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            //Check if you have the powerup: 
            if (canTripleShot == true)
            {
                Instantiate(_tripleShot, transform.position + (new Vector3 (0, 0.87f, 0)), Quaternion.identity);
            }

            //If not, do normal shoot: 
            else if (Time.time > _canFire)
            {
                _audioSource.Play(); 
                Instantiate(_laserPrefab, transform.position + (new Vector3(0, 0.88f, 0)), Quaternion.identity);
                _canFire = Time.time + _fireRate;
            }

        }
    }

    public void TripleShotPowerUpOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDown()); 
    }

    public IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false; 
    }

    public void SpeedBoostEnabled()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDown()); 
    }

    public IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false; 
    }
    
    public void ShieldsOn()
    {
        isShieldActive = true;
        _shieldPrefab.SetActive(true); 
    }

    public void LifeLost()
    {
        if (isShieldActive == true)
        {
            isShieldActive = false;
            _shieldPrefab.SetActive(false);
            return;
        }

        _hitCount++; 

        if (_hitCount == 1)
        {
            _engines[0].SetActive(true); 
        }

        else if (_hitCount == 2)
        {
            _engines[1].SetActive(true); 
        }
        

        --_lives;
        _uIManager.UpdateLives(_lives); 

        if (_lives < 1)
        {
            Instantiate(_playerExplosion, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uIManager.ShowTitleScreen(); 
            Destroy(this.gameObject); 
        }
    }
}


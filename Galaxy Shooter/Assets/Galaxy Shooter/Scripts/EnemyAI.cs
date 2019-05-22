using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    private GameObject _Explosion;

    private UI_Manager _uiManager;
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _clip; 

	// Use this for initialization
	void Start ()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _audioSource = GetComponent<AudioSource>(); 
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move(); 
	}

    private void Move()
    {
        //Move Enemy Down the Screen: 
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //If the Enemy goes out of bounds, respawn at a random x coordinate at the top, out of view: 
        if (transform.position.y <= -6.56f)
        {
            transform.position = new Vector3(Random.Range(-7.75f, 7.75f), 6.70f, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>(); 

            if (player != null)
            {
                player.LifeLost();
            }

            Instantiate(_Explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject); 

        }

        else if (other.tag == "Laser")
        {
            
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Destroy(other.gameObject);

            Instantiate(_Explosion, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
            
        }
    }
}

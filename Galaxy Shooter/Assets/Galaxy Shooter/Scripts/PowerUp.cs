using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int _powerUpID; //0 = triple, 1 = speed, 2 = shield

    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _clip; 

	// Use this for initialization
	void Start ()
    {
        _audioSource = GetComponent<AudioSource>(); 
	}
	
	// Update is called once per frame
	void Update ()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            Destroy(this.gameObject); 
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Access Player
            Player player = other.GetComponent<Player>();

            //Enable Triple: 
            if (player != null)
            {
                //Triple shot
                if (_powerUpID == 0)
                {
                    player.TripleShotPowerUpOn();
                }

                //Speed Boost: 
                else if (_powerUpID == 1)
                {
                    player.SpeedBoostEnabled(); 
                }

                //Shield: 
                else if (_powerUpID == 2)
                {
                    player.ShieldsOn(); 
                }
                
            }

            //Destroy PowerUp: 
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position); 
            Destroy(this.gameObject);

        }
        
    }
}

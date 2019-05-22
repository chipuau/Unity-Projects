using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField]
    private float _speed = 2.0f;

    private int horizontalDirection = 0;
    private int verticalDirection = 0; 

	// Use this for initialization
	void Start ()
    {
        GenerateDirection(); 
    }
	
	// Update is called once per frame
	void Update ()
    {
        Move(); 
	}

    private void GenerateDirection()
    {
        //Randomly generate the starting direction: 
        horizontalDirection = Random.Range(1, 3);
        verticalDirection = Random.Range(1, 3);

        if (Random.Range(0, 2) == 0)
        {
            horizontalDirection = -1;
        }

        else
        {
            horizontalDirection = 1;
        }

        if (Random.Range(0, 2) == 0)
        {
            verticalDirection = -1;
        }

        else
        {
            verticalDirection = 1;
        }
    }

    private void Move()
    {
        if (transform.position.y > 4.8f)
        {
            verticalDirection = -1;
        }

        else if (transform.position.y < -4.8f)
        {
            verticalDirection = 1;
        }

        if (transform.position.x < -9.2f || transform.position.x > 9.2f)
        {
            transform.position = new Vector3(0, 0, 0);
            _speed = 2.0f; 
        }

        transform.Translate((new Vector3(horizontalDirection, verticalDirection, 0)) * _speed * Time.deltaTime);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>(); 

            if (player != null)
            {
                horizontalDirection = 1;
                _speed += 0.5f; 
            }
        }

        if (other.tag == "Opponent")
        {
            horizontalDirection = -1;
            _speed += 0.5f; 
        }

    }

    public int GetVertical()
    {
        return verticalDirection; 
    }

    public float GetSpeed()
    {
        return _speed; 
    }
}

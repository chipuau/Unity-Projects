using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float _speed; 

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }

    private void Move()
    {
        //Grab current position to obtain starting point:
        float verticalInput = Input.GetAxis("Vertical");

        //Check to make sure that the paddle is in bounds before moving it: 
        if (transform.position.y > 4.2f)
        {
            transform.position = new Vector3(transform.position.x, 4.2f, transform.position.z); 
        }

        else if (transform.position.y < - 4.22f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, transform.position.z); 
        }

        //If the paddle is in bounds, move it accordingly: 
        else
        {
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
       
    }
}

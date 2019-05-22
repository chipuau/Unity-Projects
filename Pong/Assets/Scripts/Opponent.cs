using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour {

    [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    private Ball _ball;

	// Use this for initialization
	void Start ()
    {
        _ball = GameObject.Find("Ball").GetComponent<Ball>(); 
    }

    // Update is called once per frame
    void Update ()
    {
        //If Vertical Ball Velocity is positive, move positive
        //If Vertical Ball Velocity is negative, move negative
        if (_ball != null)
        {
            
            if (transform.position.y > 4.2f)
            {
                transform.position = new Vector3(transform.position.x, 4.2f, transform.position.z); 
            }

            else if (transform.position.y < -4.2f)
            {
                transform.position = new Vector3(transform.position.x, -4.2f, transform.position.z); 
            }

            if (_ball.GetVertical() == 1)
            {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }

            else if (_ball.GetVertical() == -1)
            {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }

        }
        
	}

}

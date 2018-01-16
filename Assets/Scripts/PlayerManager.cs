using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float moveRoomX = 3;
    public float tileCenterX = -4.21f;
    public float xSpeed = 5.0f;
    public float zSpeed = 5.0f;
    public float boostAcceleration = 6.0f;
    public float deceleration = 6.0f;
    public float maxZSpeed = 10.0f;


    [SerializeField]
    private Vector3 moveVector;

    private CharacterController controller;
	private float animationDuration = 4.0f;
    private bool boostIsActive = false;
    private bool decelerationIsActive = false;

    private float touchToMoveMultiplier = 0.008f;

    public void MovePlayerLeft()
    {
        moveVector.x = -1 * xSpeed;
    }
    public void MovePlayerRight()
    {
        moveVector.x = 1 * xSpeed;
    }

    public void StopMoveX()
    {
        moveVector.x = 0 * xSpeed;
    }

    public void StartBoost()
    {
        boostIsActive = true;
    }

    public void StopBoost()
    {
        boostIsActive = false;
    }

    public void StartDeceleration()
    {
        decelerationIsActive = true;
    }

    public void StopDeceleration()
    {
        decelerationIsActive = false;
    }

    
	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<CharacterController>();

        //speed in z direction
        moveVector.z = zSpeed;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Time.time < animationDuration) 
		{
			controller.Move (Vector3.forward * zSpeed * Time.deltaTime);
			return;
		}

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved )
        {

            Vector2 deltaTouchPosition = Input.GetTouch(0).deltaPosition;
      

            controller.Move( new Vector3( deltaTouchPosition.x * touchToMoveMultiplier ,0,0 ) );

        }

        //Move the player with the intialized moveVector
		controller.Move (moveVector * Time.deltaTime);

        Boost();

        Decelerate();

        PlayerInBounds();

	}

    //Ensures, that the player doesn't move beyond the boundaries
    private void PlayerInBounds()
    {
        if (transform.position.x > tileCenterX + moveRoomX)
        {
            transform.SetPositionAndRotation(new Vector3(tileCenterX + moveRoomX, transform.position.y, transform.position.z), Quaternion.identity);
        }

        else if (transform.position.x < tileCenterX - moveRoomX)
        {
            transform.SetPositionAndRotation(new Vector3(tileCenterX - moveRoomX, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }

    private void Boost()
    {
        if(boostIsActive == true)
        {
            moveVector.z += boostAcceleration * Time.deltaTime;

            if(moveVector.z > maxZSpeed)
            {
                moveVector.z = maxZSpeed;
            }

        }


    }

    private void Decelerate()
    {
        if(decelerationIsActive == true)
        {
            moveVector.z -= deceleration * Time.deltaTime;
            
            if(moveVector.z < zSpeed)
            {
                moveVector.z = zSpeed;
            }
            
        }

    }


}

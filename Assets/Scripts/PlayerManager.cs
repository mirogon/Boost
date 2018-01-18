using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public GameObject bulletPrefab;

    public float moveRoomX = 3;
    public float tileCenterX = -4.21f;

    public float xSpeed = 5.0f;
    public float zSpeed = 5.0f;
    public float maxZSpeed = 10.0f;

    public float boostAcceleration = 6.0f;
    public float deceleration = 6.0f;

    public float bulletSpeed = 18.0f;

    [SerializeField]
    private Vector3 moveVector;
    private CharacterController controller;

    private List<GameObject> bullets;

	private const float animationDuration = 4.0f;

    private const float touchToMoveMultiplier = 0.008f;

    private bool boostIsActive = false;
    private bool decelerationIsActive = false;

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

    public void Shoot()
    {
   
        bullets.Add(Instantiate(bulletPrefab));
        bullets[bullets.Count - 1].GetComponent<Transform>().SetPositionAndRotation(new Vector3(transform.position.x,transform.position.y, transform.position.z), Quaternion.identity);
    }

	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<CharacterController>();
        bullets = new List<GameObject>();
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

        //Check for swipe and move
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved )
        {

            Vector2 deltaTouchPosition = Input.GetTouch(0).deltaPosition;
      

            controller.Move( new Vector3( deltaTouchPosition.x * touchToMoveMultiplier ,0,0 ) );
            
        }

        //Move the player with the intialized moveVector
		controller.Move (moveVector * Time.deltaTime);

        //Move the bullets with the bulletSpeed
        MoveBullets();

        //If boostIsActive == true, Boost
        Boost();

        //If declerationIsActive == true, Decelerate
        Decelerate();

        //If the player is beyond the bounds, move him inside
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

    private void MoveBullets()
    {
        for( int i = 0; i < bullets.Count; i++)
        {
            bullets[i].GetComponent<Transform>().SetPositionAndRotation(new Vector3(bullets[i].transform.position.x, bullets[i].transform.position.y, bullets[i].transform.position.z + (bulletSpeed * Time.deltaTime)), Quaternion.identity);

            if(bullets[i].transform.position.z > transform.position.z + 50)
            {
                Destroy(bullets[i]);
                bullets.RemoveAt(i);
            }
        }

       

    }


}

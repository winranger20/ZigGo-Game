using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public GameObject diamondBreakParticle;
    public GameObject ballDragParticle;
    public GameObject startParticle;
    AudioSource pickUpSound;
    
    [SerializeField]
    float ballSpeed;   
    bool gameStarted;           
    bool gameOver;
    Rigidbody rb;  
    

    void Awake()
    {
        pickUpSound = GetComponent<AudioSource>();
        // Access Ball's Rigidbody
        rb = GetComponent<Rigidbody>(); 
    }

     // Use this for initialization
     void Start () 
    {
        gameStarted = false;
        gameOver = false;
    }
	
	// Update is called once per frame
	void Update () {

        // Game starts when player left clicks/taps on the screen (mobile)
        if (!gameStarted) 
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                rb.velocity = new Vector3(ballSpeed, 0, 0);
                gameStarted = true;
                // acticates ball's drag particle when ball starts moving
                ballDragParticle.SetActive(true);
                // Destroys particles at the start after 5 seconds
                Destroy(startParticle, 5f); 

                GameManager.instance.StartGame();
                
            }
            
        }

        // Draws array which shows where the ball drops
        Debug.DrawRay(transform.position, Vector3.down, Color.yellow); 

        // Adds gravity and speed to the ball so it falls down when near the edge of the platform
        if (!Physics.Raycast(transform.position, Vector3.down, 1f)) 
        {
            gameOver = true;
            rb.velocity = new Vector3(0, -25f, 0); // Speed added to the ball when it falls down
            Camera.main.GetComponent<CameraFollow> ().gameOver = true; // Camera stops following

            GameManager.instance.GameOver();
        }

        // Calls SwitchDirection Function When Left Mouse Button is clicked/ tapped on the screen (mobile)
        if (Input.GetKeyDown(KeyCode.Mouse0) && !gameOver) 
        {
            SwitchDirection();
        }
	}

    // Switches the direction of the ball to X and Z
    void SwitchDirection() 
    {
        if(rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(ballSpeed, 0, 0);
        }
        else if(rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0, 0, ballSpeed);
        }
    }

    // Destroys diamond when ball collides with a diamond
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Diamond")
        {
            GameObject breakParticle = Instantiate(diamondBreakParticle, col.gameObject.transform.position, Quaternion.identity) as GameObject;
            
            Destroy(col.gameObject);
            pickUpSound.Play();
            Destroy(breakParticle, 1f);
        }
    }
}

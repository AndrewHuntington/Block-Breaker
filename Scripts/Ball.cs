using UnityEngine;

public class Ball : MonoBehaviour
{
    //config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    //state
    Vector2 paddleToBallVector;
    public bool hasStarted = false; // made public so LoseCollider.cs can call it...better way?

    // cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2d;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBalltoPaddle();
            LaunchOnMouseClick();
        }
                
        //to get the ball to stick to the paddle, go to Edit> Project Settings > Script Execution Order
        //add the paddle and ball scripts, make paddle first (calculates the pos of the paddle first, then the ball)

    }

    public void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidBody2d.velocity = new Vector2(xPush, yPush);
            hasStarted = true;
        }
    }


    // highlight code, right click, Quick Actions and Refactorings > Extract Method to quickly make a method out of existing code
    public void LockBalltoPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    // play a sound effect
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Allows us to tweak the X and Y velocity of the ball to avoid endless loops
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor),
            Random.Range(0f, randomFactor));

        if (hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip); // .PlayOneShot() - stops audio clips from interupting themselves
            myRigidBody2d.velocity += velocityTweak; //applies the above tweak to the ball
        }
        
    }

}

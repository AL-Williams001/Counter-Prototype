
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public GameObject ballPrefab; // The ball prefab to instantiate
    public Transform shootPoint; // The point from which
    public float minForce = 300f;
    public float maxForce = 3000f;
    public float chargedSpeed = 500f; // Speed at which the ball is shot the ball will

    private float currentForce; // Current force applied to the ball
    private bool isCharging; // Flag to check if the player is charging the shot


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //start charging the shot when the left mouse button is pressed
        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button is pressed
        {
            isCharging = true; // Start charging the shot
            currentForce = minForce; // Reset the force to the minimum
        }

        //Continue charging the shot while the left mouse button is held down
        if (isCharging && Input.GetMouseButton(0)) // While the left mouse button is held down
        {
            // Increase the force gradually
            currentForce += chargedSpeed * Time.deltaTime;
            currentForce = Mathf.Clamp(currentForce, minForce, maxForce); // Clamp the force between min and max
        }

        //Shoot the ball when the left mouse button is released
        if (Input.GetMouseButtonUp(0)) // Check if the left mouse button is released
        {
            ShootBall(); // Call the method to shoot the ball
            isCharging = false; // Stop charging the shot
        }

    }

    void ShootBall()
    {
        // Instantiate the ball at the shoot point
        GameObject ball = Instantiate(ballPrefab, shootPoint.position, Quaternion.identity);
        // Get the Rigidbody component of the ball
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        Vector3 shootDirection = shootPoint.forward + shootPoint.up * 0.8f; // Calculate the shoot direction
        rb.AddForce(shootDirection.normalized * currentForce); // Apply the force to the ball

        currentForce = minForce; // Reset the force for the next shot
    }
}


using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public GameObject ballPrefab; // The ball prefab to instantiate
    public float shootForce = 30000f; // The force applied to the ball when shooting  
    public Transform shootPoint; // The point from which
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button is pressed
        {
            GameObject ball = Instantiate(ballPrefab, shootPoint.position, Quaternion.identity); // Instantiate the ball at the shoot point
            Rigidbody rb = ball.GetComponent<Rigidbody>(); // Get the Rigidbody component of the ball
            Vector3 arcDirection = shootPoint.forward * 1f + Vector3.up * 1f; // Calculate the shoot direction (forward with a slight upward angle)
            rb.AddForce(arcDirection * shootForce); // Apply force to the ball in the forward direction of the shoot point
            Destroy(ball, 5f); // Destroy the ball after 5 seconds to prevent cluttering the scene
        }
    }
}

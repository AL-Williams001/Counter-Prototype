using UnityEngine;

public class HoopTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // Check if the object entering the trigger has the tag "Ball"
        {
            Debug.Log("Ball entered the hoop!"); // Log a message to the console
            GameManager.Instance.AddScore(1); // Call the IncreaseScore method on the GameManager instance
            Destroy(other.gameObject); // Destroy the ball object that entered the hoop
        }
    }
}

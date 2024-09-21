using UnityEngine;

public class SmoothMovement : MonoBehaviour
{
    public float movingspeed = 5f; // Adjust the speed as needed

    void FixedUpdate()
    {
        float movedir = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(movedir, 0.0f, 0.0f);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = movement * movingspeed;
    }
}

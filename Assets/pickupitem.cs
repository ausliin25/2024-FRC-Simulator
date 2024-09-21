using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private Transform pickupPoint;
    public Transform Player;

    public float pickUpDistance;
    public float forceMulti;
    public float rotationSpeed = 10f; // Speed of rotation

    public bool readyToThrow;
    public bool itemIsPicked;

    private Rigidbody rb;
    private float currentAngle = 0f; // Track the current angle

    private Vector3 impulseDirection = Vector3.forward; // Default direction

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("Player").transform;
        pickupPoint = GameObject.Find("pickuppoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && itemIsPicked && readyToThrow)
        {
            forceMulti += 200 * Time.deltaTime;
            Debug.Log("Force Multi: " + forceMulti);
        }

        pickUpDistance = Vector3.Distance(Player.position, transform.position);

        if (pickUpDistance <= 2)
        {
            if (Input.GetKeyDown(KeyCode.Q) && !itemIsPicked && pickupPoint.childCount < 1)
            {
                rb.useGravity = false;
                GetComponent<BoxCollider>().enabled = false;
                rb.isKinematic = true;
                transform.position = pickupPoint.position;
                transform.parent = pickupPoint;
                transform.localRotation = Quaternion.identity;

                itemIsPicked = true;
                forceMulti = 0;
            }
        }

        if (Input.GetKey(KeyCode.P) && itemIsPicked)
        {
            // Update angle while key is held down
            currentAngle += rotationSpeed * Time.deltaTime;
            currentAngle = Mathf.Clamp(currentAngle, 0f, 60f); // Limit angle to 0-60 degrees
            transform.localRotation = Quaternion.Euler(-currentAngle, 0f, 0f); // Apply the angle
        }

        if (Input.GetKeyUp(KeyCode.Q) && itemIsPicked)
        {
            readyToThrow = true;

            if (forceMulti > 10)
            {
                Debug.Log("Throwing with force: " + forceMulti);
                transform.parent = null;
                rb.useGravity = true;
                GetComponent<BoxCollider>().enabled = true;
                rb.isKinematic = false;

                // Set the impulse direction based on current rotation
                Vector3 forceDirection = GetImpulseDirection(); // Get the direction
                AddForceAtAngle(forceMulti, forceDirection);

                itemIsPicked = false;
                forceMulti = 0;
                readyToThrow = false;

                // Reset angle after throwing
                currentAngle = 0f;
                transform.localRotation = Quaternion.identity;
            }
        }
    }

    // Get the impulse direction based on the object's rotation
    Vector3 GetImpulseDirection()
    {
        // Example: flipping the direction based on the current forward direction
        return transform.forward; // Change this to adjust the direction as needed
    }

    public void AddForceAtAngle(float force, Vector3 direction)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}

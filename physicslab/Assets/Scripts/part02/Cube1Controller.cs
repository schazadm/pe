using UnityEngine;

public class Cube1Controller : MonoBehaviour
{
    private Rigidbody rb;
    private float targetVelocity; // m/s
    private float accelerationTime; // s
    private float calculatedForce; // N
    private float calculatedAcceleration; // m/s
    private bool reachedTargetSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetVelocity = 2f;
        accelerationTime = 2f;
        calculatedAcceleration = targetVelocity / accelerationTime;
        calculatedForce = rb.mass * calculatedAcceleration;
        reachedTargetSpeed = false;
    }

    void FixedUpdate()
    {
        if (rb.velocity.x < targetVelocity && !reachedTargetSpeed)
        {
            rb.AddForce(calculatedForce, 0, 0);
            return;
        }
        reachedTargetSpeed = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = other.rigidbody;
        }
    }
}

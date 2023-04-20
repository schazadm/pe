using UnityEngine;

public class Cube2Controller : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 currVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        currVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(gameObject.name + " collided with " + other.gameObject.name);
        if (other.gameObject.CompareTag("Bounce")) bounce(other);
    }

    private void bounce(Collision other)
    {
        var speed = currVelocity.magnitude;
        var direction = Vector3.Reflect(currVelocity.normalized, other.contacts[0].normal);
        rb.velocity = direction * Mathf.Max(speed, 2f);
    }
}

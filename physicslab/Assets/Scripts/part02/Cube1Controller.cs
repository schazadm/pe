using UnityEngine;

public class Cube1Controller : MonoBehaviour
{
    // die benötigte Kraft, um den Würfel in 2s auf eine Geschwindigkeit von 2m/s zu bringen
    private float forceMagnitude = 4f;
    private Rigidbody rb;
    private Vector3 currVelocity; // m/s
    private float currentTimeStep; // s
    private float targetTime = 2f; //


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Fügt eine Kraft in X-Richtung auf den Würfel hinzu, um ihn beim Start zu beschleunigen
        rb.AddForce(transform.right * forceMagnitude, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        currVelocity = rb.velocity;
        currentTimeStep += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(gameObject.name + " collided with " + other.gameObject.name);
        if (other.gameObject.CompareTag("Bounce")) bounce(other);
        if (other.gameObject.CompareTag("Stick")) stick(other);
    }

    private void bounce(Collision other)
    {
        var speed = currVelocity.magnitude;
        var direction = Vector3.Reflect(currVelocity.normalized, other.contacts[0].normal);
        rb.velocity = direction * Mathf.Max(speed, 2f);
    }

    private void stick(Collision other)
    {
        gameObject.AddComponent<FixedJoint>();
        gameObject.GetComponent<FixedJoint>().connectedBody = other.rigidbody;
    }
}

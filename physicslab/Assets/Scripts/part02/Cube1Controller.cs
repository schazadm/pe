using UnityEngine;

public class Cube1Controller : MonoBehaviour
{
    private Rigidbody rb;
    // die benötigte Kraft, um den Würfel in 2s auf eine Geschwindigkeit von 2m/s zu bringen
    private float forceMagnitude = 4f;
    private int springConstant = 10; // N/m
    private Vector3 currVelocity; // m/s
    private float currentTimeStep; // s
    private float targetTime = 2f; // s


    private float _targetSpeed;
    private float _constantForce;
    private float _acceleration;
    private float _accelerationTime;
    private bool _reachedTargetSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _targetSpeed = 2.0f;
        _accelerationTime = 2f; // 0 < _accelerationTime < 5 seconds
        _acceleration = _targetSpeed / _accelerationTime;
        _constantForce = rb.mass * _acceleration;
        _reachedTargetSpeed = false;

        // Fügt eine Kraft in X-Richtung auf den Würfel hinzu, um ihn beim Start zu beschleunigen
        // rb.AddForce(transform.right * forceMagnitude, ForceMode.Impulse);
    }

    // private void FixedUpdate()
    // {
    //     currVelocity = rb.velocity;
    //     currentTimeStep += Time.deltaTime;
    // }
    void FixedUpdate()
    {
        currVelocity = rb.velocity;

        if (rb.velocity.x < _targetSpeed && !_reachedTargetSpeed)
        {
            rb.AddForce(_constantForce, 0, 0);
            return;
        }
        _reachedTargetSpeed = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(gameObject.name + " collided with " + other.gameObject.name);
        if (other.gameObject.CompareTag("Stick")) stick(other);
    }

    private void stick(Collision other)
    {
        gameObject.AddComponent<FixedJoint>();
        gameObject.GetComponent<FixedJoint>().connectedBody = other.rigidbody;
    }
}

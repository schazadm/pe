using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Cube1Controller : MonoBehaviour
{
    private Rigidbody rb;
    private float currentTimeStep; // s
    private float currentVelocityX; // m/s
    private float currentImpulseX; // N*s
    private float currentForce; // N
    private float targetVelocity; // m/s
    private float accelerationTime; // s
    private float calculatedForce; // N
    private float calculatedAcceleration; // m/s
    private bool reachedTargetSpeed;
    private List<List<float>> timeSeries;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timeSeries = new List<List<float>>();
        targetVelocity = 2f;
        accelerationTime = 2f;
        calculatedAcceleration = targetVelocity / accelerationTime;
        calculatedForce = rb.mass * calculatedAcceleration;
        reachedTargetSpeed = false;
    }

    void FixedUpdate()
    {
        currentTimeStep += Time.deltaTime;
        currentVelocityX = rb.velocity.x;
        currentImpulseX = rb.mass * currentVelocityX;
        currentForce = (rb.velocity.x / currentTimeStep) * rb.mass;

        timeSeries.Add(new List<float>() {
            currentTimeStep,
            rb.position.x,
            rb.velocity.x,
            currentImpulseX,
            currentForce,
        });

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

    void OnApplicationQuit()
    {
        WriteTimeSeriesToCSV();
    }

    void WriteTimeSeriesToCSV()
    {
        using (var streamWriter = new StreamWriter("time_series_cube_1.csv"))
        {
            streamWriter.WriteLine("t,x(t),v(t),p(t),F(t)");
            foreach (List<float> timeStep in timeSeries)
            {
                streamWriter.WriteLine(string.Join(",", timeStep));
                streamWriter.Flush();
            }
        }
    }
}

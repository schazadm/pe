using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Cube2Controller : MonoBehaviour
{
    private Rigidbody rb;
    private float currentTimeStep; // s
    private float currentVelocityX; // m/s
    private float currentImpulseX; // N*s
    private float currentForce; // N
    private List<List<float>> timeSeries;
    private bool isFixed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timeSeries = new List<List<float>>();
        isFixed = false;
    }

    private void FixedUpdate()
    {
        currentTimeStep += Time.deltaTime;
        if (!isFixed) return;
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
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            isFixed = true;
        }
    }

    void OnApplicationQuit()
    {
        WriteTimeSeriesToCSV();
    }

    void WriteTimeSeriesToCSV()
    {
        using (var streamWriter = new StreamWriter("time_series_cube_2.csv"))
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

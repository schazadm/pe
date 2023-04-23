using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class StatsLoggerController : MonoBehaviour
{
    public Rigidbody cubeOne;
    public Rigidbody cubeTwo;
    private float currentTimeStep; // s
    private float currentVelocityX; // m/s
    private float currentImpulseX; // N*s
    private float currentForce; // N
    private List<List<float>> timeSeries;

    void Start()
    {
        timeSeries = new List<List<float>>();
    }

    void FixedUpdate()
    {
        currentTimeStep += Time.deltaTime;
        // currentVelocityX = rb.velocity.x;
        // currentImpulseX = rb.mass * currentVelocityX;
        // currentForce = (rb.velocity.x / currentTimeStep) * rb.mass;

        // timeSeries.Add(new List<float>() {
        //     currentTimeStep,
        //     rb.position.x,
        //     rb.velocity.x,
        //     currentImpulseX,
        //     currentForce,
        // });

        timeSeries.Add(new List<float>() {
            currentTimeStep,
            cubeOne.position.x ,
            cubeTwo.position.x,
            cubeOne.velocity.x, cubeTwo.velocity.x,
            cubeOne.velocity.x * cubeOne.mass,
            cubeTwo.velocity.x * cubeTwo.mass
        });
    }

    void OnApplicationQuit()
    {
        // WriteTimeSeriesToCSV();
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

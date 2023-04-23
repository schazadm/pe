using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class StatsLoggerController : MonoBehaviour
{
    public Rigidbody c_1;
    public Rigidbody c_2;
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
        timeSeries.Add(new List<float>() {
            currentTimeStep,
            c_1.position.x,
            c_2.position.x,
            c_1.velocity.x,
            c_2.velocity.x,
            c_1.velocity.x * c_1.mass,
            c_2.velocity.x * c_2.mass,
            (c_1.velocity.x / currentTimeStep) * c_1.mass, // F=a*m=(v/deltaT)*m
            (c_2.velocity.x / currentTimeStep) * c_2.mass,
        });
    }

    void OnApplicationQuit()
    {
        WriteTimeSeriesToCSV();
    }

    void WriteTimeSeriesToCSV()
    {
        using (var streamWriter = new StreamWriter("time_series.csv"))
        {
            streamWriter.WriteLine("t,x1(t),x2(t),v1(t),v2(t),p1(t),p2(t),F1(t),F2(t)");
            foreach (List<float> timeStep in timeSeries)
            {
                streamWriter.WriteLine(string.Join(",", timeStep));
                streamWriter.Flush();
            }
        }
    }
}

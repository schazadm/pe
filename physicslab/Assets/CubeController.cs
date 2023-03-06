using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
    Accelerates the cube to which it is attached, modelling an harmonic oscillator.
    Writes the position, velocity and acceleration of the cube to a CSV file.
    
    Remark: For use in "Physics Engines" module at ZHAW, part of physics lab
    Author: kemf
    Version: 1.0
*/
public class CubeController : MonoBehaviour
{
    private Rigidbody rigidBody;

    public int springConstant; // N/m

    private float currentTimeStep; // s

    private List<List<float>> timeSeries;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        timeSeries = new List<List<float>>();

        if (currentTimeStep >= 20f)
        {
            WriteTimeSeriesToCSV();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate can be called multiple times per frame
    void FixedUpdate()
    {
        float forceX; // N
        float forceZ; // N

        // Calculate spring force on body for x component of force vector
        forceX = -rigidBody.position.x * springConstant;
        forceZ = -rigidBody.position.z * springConstant;
        rigidBody.AddForce(new Vector3(forceX, 0f, 0f)); // add force to the x-achses
        // rigidBody.AddForce(new Vector3(0f, 0f, forceZ)); // add force to the z-achses

        currentTimeStep += Time.deltaTime;
        timeSeries.Add(new List<float>() { currentTimeStep, rigidBody.position.x, rigidBody.velocity.x, forceX });
        // timeSeries.Add(new List<float>() {currentTimeStep, rigidBody.position.z, rigidBody.velocity.z, forceZ});
    }

    void OnApplicationQuit()
    {
        // WriteTimeSeriesToCSV();
    }

    void WriteTimeSeriesToCSV()
    {
        using (var streamWriter = new StreamWriter("time_series.csv"))
        {
            streamWriter.WriteLine("t,x(t),v(t),F(t) (added)");
            // streamWriter.WriteLine("t,z(t),v(t),F(t) (added)");

            foreach (List<float> timeStep in timeSeries)
            {
                streamWriter.WriteLine(string.Join(",", timeStep));
                streamWriter.Flush();
            }
        }
    }
}

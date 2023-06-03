using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class Crane : MonoBehaviour
{
    public Rigidbody c_1;
    public Rigidbody c_2;
    public GameObject plane;
    public float Cw = 1.1f;
    public float rhoAir = 1.2f;
    public float ropeLength = 6.0f;

    private Func<Vector3> getF = () => Vector3.zero;
    private float g = Physics.gravity.y;
    private Vector3 startPos;
    private Vector3 hinge;
    private Vector3 equilibrium;
    private float area;
    private float mass;
    private Boolean isTriggered;

    private float currentTimeStep; // s
    private List<List<double>> timeSeries;


    private void Start()
    {
        isTriggered = false;
        area = c_1.transform.localScale.z * c_1.transform.localScale.y;
        mass = c_1.mass + c_2.mass;
        timeSeries = new List<List<double>>();
    }

    void FixedUpdate()
    {
        if (!isTriggered) return;
        Vector3 v = c_1.velocity;
        Vector3 pos = c_1.position;
        Vector3 rope = pos - hinge;
        double alpha = Math.Atan(rope.x / rope.y);
        double F_G = -mass * g * Math.Cos(alpha);
        double F_Z = mass * v.sqrMagnitude / ropeLength;
        Vector3 F_tot = new Vector3((float)((F_G + F_Z) * Math.Sin(alpha)), (float)((F_G + F_Z) * Math.Cos(alpha)), 0);
        Vector3 airResistance = -v * v.magnitude * Cw * rhoAir * 0.5f * area;
        c_1.AddForce(F_tot + airResistance);
        //
        currentTimeStep += Time.deltaTime;
        timeSeries.Add(new List<double>() {
            currentTimeStep,
            c_1.position.x,
            c_2.position.x,
            c_1.position.y,
            c_2.position.y,
            alpha
        });
    }

    private void OnTriggerEnter(Collider pendulumTrigger)
    {
        if (isTriggered) return;
        Debug.Log("trigger enter");
        startPos = c_1.position;
        hinge = startPos + new Vector3(0, ropeLength, 0);
        equilibrium = startPos - hinge;
        // getF = () => getForce();
        Collider.Destroy(plane.GetComponent<MeshCollider>());
        isTriggered = true;
    }

    Vector3 getForce()
    {
        var v = c_1.velocity;
        var pos = c_1.position;
        var rope = pos - hinge;
        var alpha = Math.Atan(rope.x / rope.y);
        var F_G = -mass * g * Math.Cos(alpha);
        var F_Z = mass * v.sqrMagnitude / ropeLength;
        var F_tot = new Vector3((float)((F_G + F_Z) * Math.Sin(alpha)), (float)((F_G + F_Z) * Math.Cos(alpha)), 0);
        var airResistance = -v * v.magnitude * Cw * rhoAir * 0.5f * area;
        return F_tot + airResistance;
    }

    void OnApplicationQuit()
    {
        WriteTimeSeriesToCSV();
    }

    void WriteTimeSeriesToCSV()
    {
        using (var streamWriter = new StreamWriter("time_series_crane.csv"))
        {
            streamWriter.WriteLine("t,x1(t),x2(t),y1(t),y2(t),def(t)");
            foreach (List<double> timeStep in timeSeries)
            {
                streamWriter.WriteLine(string.Join(",", timeStep));
                streamWriter.Flush();
            }
        }
    }
}

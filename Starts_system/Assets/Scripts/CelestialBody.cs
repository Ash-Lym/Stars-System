using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public float mass = 1f;// Mass of the celestial body
    public Vector3 initialVelocity;//Initial velocity vector

    [HideInInspector]
    public Vector3 currentVelocity;//Current velocity vector

    void Awake()
    {
        currentVelocity = initialVelocity;
    }

    //Update velocity based on acceleration and time step
    public void UpdateVelocity(Vector3 acceleration, float timeStep)
    {
        currentVelocity += acceleration * timeStep;
    }

    public void UpdatePosition(float timeStep)
    {
        transform.position += currentVelocity * timeStep;
    }
}

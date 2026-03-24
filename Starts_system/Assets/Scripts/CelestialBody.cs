using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    // Mass parameter
    public float mass = 1f;
    // Initial velocity vector
    public Vector3 initialVelocity;

    // Current velocity state
    [HideInInspector] public Vector3 currentVelocity;
    // Current acceleration state
    [HideInInspector] public Vector3 currentAcceleration;

    void Awake()
    {
        // Initial state assignment
        currentVelocity = initialVelocity;
    }

    // Spatial position physics
    public void UpdatePosition(float timeStep)
    {
        transform.position += currentVelocity * timeStep + 0.5f * currentAcceleration * timeStep * timeStep;
    }

    // Kinematic velocity physics
    public void UpdateVelocity(Vector3 newAcceleration, float timeStep)
    {
        currentVelocity += (currentAcceleration + newAcceleration) * 0.5f * timeStep;

        // Acceleration state cache
        currentAcceleration = newAcceleration;
    }
}
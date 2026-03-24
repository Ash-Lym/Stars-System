using UnityEngine;

public class GravitySimulator : MonoBehaviour
{
    // Gravitational constant parameter
    public float gravitationalConstant = 39.4784f;
    // Target celestial bodies
    public CelestialBody[] bodies;
    // Time scale multiplier
    public float timeScale = 1f;

    // Physics subdivision parameter
    public int physicsStepsPerFrame = 20;

    void Start()
    {
        // Initial system acceleration state
        Vector3[] initialAccelerations = CalculateAccelerations();
        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].currentAcceleration = initialAccelerations[i];
        }
    }

    void FixedUpdate()
    {
        // Pause state condition
        if (timeScale == 0) return;

        // Delta time parameters
        float totalTimeStep = Time.fixedDeltaTime * timeScale;
        float subTimeStep = totalTimeStep / physicsStepsPerFrame;

        // Sub-step physics loop
        for (int step = 0; step < physicsStepsPerFrame; step++)
        {
            // Position state
            foreach (CelestialBody body in bodies)
            {
                body.UpdatePosition(subTimeStep);
            }

            // Acceleration state
            Vector3[] newAccelerations = CalculateAccelerations();

            // Velocity state
            for (int i = 0; i < bodies.Length; i++)
            {
                bodies[i].UpdateVelocity(newAccelerations[i], subTimeStep);
            }
        }
    }

    // Gravitational acceleration mathematics
    Vector3[] CalculateAccelerations()
    {
        // Acceleration array
        Vector3[] accelerations = new Vector3[bodies.Length];

        // N-body interaction logic
        for (int i = 0; i < bodies.Length; i++)
        {
            CelestialBody body_a = bodies[i];
            Vector3 acceleration = Vector3.zero;

            for (int j = 0; j < bodies.Length; j++)
            {
                // Self-interaction exclusion
                if (i == j) continue;

                CelestialBody body_b = bodies[j];
                // Distance vector
                Vector3 direction = body_b.transform.position - body_a.transform.position;
                // Squared magnitude
                float distanceSq = direction.sqrMagnitude;

                // Zero division exclusion
                if (distanceSq == 0) continue;

                // Force magnitude
                float forceMag = gravitationalConstant * body_b.mass / distanceSq;
                // Vector accumulation
                acceleration += direction.normalized * forceMag;
            }
            accelerations[i] = acceleration;
        }

        return accelerations;
    }

    // Time scale modification
    public void SetTimeScale(float newTimeScale)
    {
        timeScale = newTimeScale;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySimulator : MonoBehaviour
{
    public float gravitationalConstant = 1f;//Gravitational constant G for simulation
    public CelestialBody[] bodies;//Array of all celestial bodies in the scene
    public float timeScale = 1f;//Simulation speed multiplier
    public int physicsStepsPerFrame = 100;//Number of sub-steps per frame for stable calculation

    void FixedUpdate()
    {
        //Skip calculation if simulation is paused
        if (timeScale == 0) return;

        //Total time step for this frame (scaled by simulation speed)
        float totalTimeStep = Time.fixedDeltaTime * timeScale;
        float subTimeStep = totalTimeStep / physicsStepsPerFrame;

        //Subdivided physics calculation for stability
        for (int i = 0; i < physicsStepsPerFrame; i++)
        {
            //Calculate gravitational acceleration for all bodies
            foreach (CelestialBody body_a in bodies)
            {
                Vector3 acceleration = Vector3.zero;

                foreach (CelestialBody body_b in bodies)
                {
                    if (body_a == body_b) continue;//Skip self-interaction

                    Vector3 direction = body_b.transform.position - body_a.transform.position;
                    float distanceSq = direction.sqrMagnitude;

                    if (distanceSq == 0) continue;//Avoid division by zero

                    //Add normalized direction for correct acceleration
                    float forceMag = gravitationalConstant * body_b.mass / distanceSq;
                    acceleration += direction.normalized * forceMag;
                }

                //Update velocity with calculated acceleration
                body_a.UpdateVelocity(acceleration, subTimeStep);
            }

            //Update position of all bodies (after velocity update)
            foreach (CelestialBody body in bodies)
            {
                body.UpdatePosition(subTimeStep);
            }
        }
    }

    // Set simulation speed (0 = pause, 1 = real time, >1 = faster)
    public void SetTimeScale(float newTimeScale)
    {
        timeScale = newTimeScale;
    }
}
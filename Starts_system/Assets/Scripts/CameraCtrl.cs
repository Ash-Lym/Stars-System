using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraCtrl : MonoBehaviour
{
    public float yMinLimit = -20;
    public float yMaxLimit = 80;
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
    public float initDis = 20;
    public float minDis = 3.0f;
    public float maxDis = 50.0f;
    public float wheelSpeed = 5;
    public Transform target;

    private float distance;

    private float x = 0.0f;
    private float y = 35.0f;
    private float finalx = 0.0f;
    private float finaly = 0.0f;


    private Vector3 position;
    private Quaternion rotation;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(y, x, 0);
        transform.position = Quaternion.Euler(y, x, 0) * new Vector3(0, 0, initDis) + target.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if (Input.GetMouseButton(0))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            Debug.Log("ButtonDown" + "," + x + "," + y);
        }
        finalx = Mathf.Lerp(finalx, x, Time.deltaTime * 2);
        finaly = Mathf.Lerp(finaly, y, Time.deltaTime * 2);

        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed;
        distance = Mathf.Clamp(distance, minDis, maxDis);
        rotation = Quaternion.Euler(finaly, finalx, 0);
        position = rotation * new Vector3(0, 0, -distance) + target.position;
        transform.rotation = rotation;
        transform.position = position;
    }
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float maxSpeed;
    public float maxAccel;
    public float orientation;
    public float rotation;
    public Vector3 velocity;
    protected Steering steering;
    private Rigidbody aRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        steering = new Steering();
        aRigidBody = GetComponent<Rigidbody>();
    }

    public void SetSteering (Steering steering)
    {
        this.steering = steering;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (aRigidBody == null)
            return;
        Vector3 displacement = velocity * Time.deltaTime;
        orientation += rotation * Time.deltaTime;
        // 회전 값들의 범위를 0에서 360 사이로 제한해야 함
        if (orientation < 0.0f)
            orientation += 360.0f;
        else if (orientation > 360.0f)
            orientation -= 360.0f;
        transform.Translate(displacement, Space.World);
        transform.rotation = new Quaternion();
        transform.Rotate(Vector3.up, orientation);
    }

    public virtual void LateUpdate()
    {
        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;
        if(velocity.magnitude > maxSpeed)
        {
            velocity.Normalize ();
            velocity = velocity * maxSpeed;
        }
        if(steering.angular == 0.0f)
        {
            rotation = 0.0f;
        }
        if(steering.linear.sqrMagnitude == 0.0f)
        {
            velocity = Vector3.zero;
        }
        steering = new Steering ();
    }

    public virtual void FixedUpdate()
    {
        if(aRigidBody == null)
        {
            return;
            // next step
        }
        Vector3 displacement = velocity * Time.deltaTime;
        orientation += rotation * Time.deltaTime;
        if (orientation < 0.0f)
            orientation += 360.0f;
        else if (orientation > 360.0f)
            orientation -= 360.0f;
        aRigidBody.AddForce(displacement, ForceMode.VelocityChange);
        Vector3 orientationVector = OriToVec(orientation);
        aRigidBody.rotation = Quaternion.LookRotation(orientationVector, Vector3.up);
    } 

    public Vector3 OriToVec(float orientation)
    {
        Vector3 vector = Vector3.zero;
        vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
        vector.z = Mathf.Cos(orientation * Mathf.Deg2Rad) * 1.0f;
        return vector.normalized;
    }
}

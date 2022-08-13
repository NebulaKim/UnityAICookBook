using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leave : AgentBehaviour
{
    public float escapeRadius;
    public float dangerRadius;
    public float timeToTarget = 0.1f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        Vector3 direction = target.transform.position - transform.position;
        float distance = direction.magnitude;
        float targetSpeed;
        if (distance > dangerRadius)
            return steering;
        float reduce;
        if (distance < escapeRadius)
            reduce = 0.0f;
        else
            reduce =
        
        return steering;
    }
}

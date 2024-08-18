using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class WheelComponentDetails : ComponentDetails
{
    public float dampingRatio;
    public float frequency;
    private WheelJoint2D joint;

    public override void outOfBattery()
    {
        if (joint !=  null)
        {
            joint.useMotor = false;
        }
    }
    public override void AddJoint(GameObject otherBox)
    {
        this.joint = otherBox.AddComponent<WheelJoint2D>();
        joint.connectedBody = this.GetComponent<Rigidbody2D>();
        joint.anchor = transform.position - otherBox.transform.position;
        joint.connectedAnchor = Vector2.zero;
        joint.enableCollision = true;

        joint.useMotor = true;
        var MyNewMotor = new JointMotor2D();
        MyNewMotor.motorSpeed = -300;
        MyNewMotor.maxMotorTorque = 10000;
        joint.motor = MyNewMotor;

        var suspension = new JointSuspension2D();
        suspension.dampingRatio = dampingRatio;
        suspension.frequency = frequency;
        joint.suspension = suspension;

    }
}

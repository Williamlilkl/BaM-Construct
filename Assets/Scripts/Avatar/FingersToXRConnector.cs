using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class FingersToXRConnector : MonoBehaviour
{
    //Containers for all 10 Fingers Transforms for both Avatar and XR Hands
    public Transform[] XRigLeftHandFingers, XRigRightHandFingers;
    public Transform[] AvatLeftHandFingers, AvatRightHandFingers;

    Transform leftPalmBone, rightPalmBone;      //Need the palm bone to refer finger tip positions to
    Transform avLeft, avRight, myXRLH, myXRRH;  //pointers to avatar and XR hands objects

    public GameObject handVisualizer;

    //Note we're not using Start() here as we can only call this once the network object is being spawned
    public void Initialize()
    {
        //Fingers - UPDATED!
        AvatLeftHandFingers = new Transform[5];
        AvatRightHandFingers = new Transform[5];
        XRigLeftHandFingers = new Transform[5];
        XRigRightHandFingers = new Transform[5];

        avLeft = transform.Find("XR IK Rig").Find("Left Arm IK").Find("Left Arm IK_target");
        avRight = transform.Find("XR IK Rig").Find("Right Arm IK").Find("Right Arm IK_target");

        handVisualizer = GameObject.Find("LeftHandDebugDrawJoints");
        if (!handVisualizer) Debug.Log("ERROR: your XR system has no controllers nor hands active!");
        else
        {
            myXRLH = GameObject.Find("LeftHandDebugDrawJoints").transform.Find("Palm").transform;
            myXRRH = GameObject.Find("RightHandDebugDrawJoints").transform.Find("Palm").transform;
        }
    }

    public void MapHands()
    {
        //XR Origin - LEFT
        XRigLeftHandFingers[0] = GameObject.Find("LeftHandDebugDrawJoints").transform.Find("IndexTip").transform;
        XRigLeftHandFingers[1] = GameObject.Find("LeftHandDebugDrawJoints").transform.Find("MiddleTip").transform;
        XRigLeftHandFingers[2] = GameObject.Find("LeftHandDebugDrawJoints").transform.Find("ThumbTip").transform;
        XRigLeftHandFingers[3] = GameObject.Find("LeftHandDebugDrawJoints").transform.Find("RingTip").transform;
        XRigLeftHandFingers[4] = GameObject.Find("LeftHandDebugDrawJoints").transform.Find("LittleTip").transform;

        //XR Origin - RIGHT
        XRigRightHandFingers[0] = GameObject.Find("RightHandDebugDrawJoints").transform.Find("IndexTip").transform;
        XRigRightHandFingers[1] = GameObject.Find("RightHandDebugDrawJoints").transform.Find("MiddleTip").transform;
        XRigRightHandFingers[2] = GameObject.Find("RightHandDebugDrawJoints").transform.Find("ThumbTip").transform;
        XRigRightHandFingers[3] = GameObject.Find("RightHandDebugDrawJoints").transform.Find("RingTip").transform;
        XRigRightHandFingers[4] = GameObject.Find("RightHandDebugDrawJoints").transform.Find("LittleTip").transform;

        //Avatar - LEFT
        AvatLeftHandFingers[0] = transform.Find("XR IK Rig").Find("Left Index Finger IK").Find("Left Index Finger IK_target").transform;
        AvatLeftHandFingers[1] = transform.Find("XR IK Rig").Find("Left Middle Finger IK").Find("Left Middle Finger IK_target").transform;
        AvatLeftHandFingers[2] = transform.Find("XR IK Rig").Find("Left Thumb IK").Find("Left Thumb IK_target").transform;
        AvatLeftHandFingers[3] = transform.Find("XR IK Rig").Find("Left Ring Finger IK").Find("Left Ring Finger IK_target").transform;
        AvatLeftHandFingers[4] = transform.Find("XR IK Rig").Find("Left Pinky Finger IK").Find("Left Pinky Finger IK_target").transform;

        //Avatar - LEFT
        AvatRightHandFingers[0] = transform.Find("XR IK Rig").Find("Right Index Finger IK").Find("Right Index Finger IK_target").transform;
        AvatRightHandFingers[1] = transform.Find("XR IK Rig").Find("Right Middle Finger IK").Find("Right Middle Finger IK_target").transform;
        AvatRightHandFingers[2] = transform.Find("XR IK Rig").Find("Right Thumb IK").Find("Right Thumb IK_target").transform;
        AvatRightHandFingers[3] = transform.Find("XR IK Rig").Find("Right Ring Finger IK").Find("Right Ring Finger IK_target").transform;
        AvatRightHandFingers[4] = transform.Find("XR IK Rig").Find("Right Pinky Finger IK").Find("Right Pinky Finger IK_target").transform;

        //Palms of hands
        leftPalmBone = transform.Find("XR IK Rig").Find("Left Index Finger IK").GetComponent<ChainIKConstraint>().data.root.parent;
        rightPalmBone = transform.Find("XR IK Rig").Find("Right Index Finger IK").GetComponent<ChainIKConstraint>().data.root.parent;
    }

    public void LinkAvtFingersToXROrigin(Transform avatarHand)
    {
        for (int i = 0; i < 5; i++) //for each finger
        {

            if (avatarHand == avLeft)
            {
                AvatLeftHandFingers[i].position = leftPalmBone.position + (XRigLeftHandFingers[i].position - myXRLH.position);
                AvatLeftHandFingers[i].rotation = avatarHand.rotation;
            }

            if (avatarHand == avRight)
            {
                AvatRightHandFingers[i].position = rightPalmBone.position + (XRigRightHandFingers[i].position - myXRRH.position);
                AvatRightHandFingers[i].rotation = avatarHand.rotation;
            }
        }
    }
}

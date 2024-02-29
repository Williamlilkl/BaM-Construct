using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GrabbedObjectController : NetworkBehaviour
{
    NetworkObject avtLeftGrabbedObject, avtRightGrabbedObject;
    [SerializeField] FirstThirdPersonController fP3P;

    Transform avLeft, avRight;  //passed on via Initialize()

    //Note we're not using Start() here as we can only call this once the network object is being spawned
    public void Initialize(Transform left, Transform right)
    {
        fP3P = GetComponent<FirstThirdPersonController>();

        //These we need to have to move the grabbed object to a specific location/rotation
        avLeft = left;
        avRight = right;
    }

   //This code is called outside the HNP Update() function!
    void Update()
    {
        //Return when not ready
        if (!IsOwner) return;
        if (!fP3P) return;
        if (!fP3P.thirdPToggle) return;

        if (!fP3P.thirdPToggle.isOn) //only in First Person Mode 
        {
            //send the grabbed object(s) to the XR IK_target transforms.
            if (avtLeftGrabbedObject) moveMyGrabbedObjectServerRpc(avtLeftGrabbedObject, avLeft.position, avLeft.rotation);
            if (avtRightGrabbedObject) moveMyGrabbedObjectServerRpc(avtRightGrabbedObject, avRight.position, avRight.rotation);
        }
    }

    //===============================================
    //CALLED BY XR ORIGIN DIRECT INTERACTOR EVENTS!
    //===============================================
    public void AvatarSelectGrabEnterEventHub(NetworkObject netObj, bool whichHand) //true=left, false=right
    {
        //tell update() to align the grabbed object to the left/right hand position
        if (whichHand) avtLeftGrabbedObject = netObj;
        else avtRightGrabbedObject = netObj;
        Debug.Log("FP grab object " + netObj.NetworkObjectId);
        setIsKinematicServerRpc(netObj, true);  //turn on IsKinematic

    }

    public void AvatarSelectGrabExitEventHub(NetworkObject netObj, bool whichHand)
    {
        Debug.Log("FP Release");
        if (whichHand) avtLeftGrabbedObject = null;
        else avtRightGrabbedObject = null;
        setIsKinematicServerRpc(netObj, false); //turn off IsKinematic
    }

    //============================
    //SERVER SIDE ONLY
    //============================
    [ServerRpc]
    public void moveMyGrabbedObjectServerRpc(NetworkObjectReference grabbedObj, Vector3 position, Quaternion rotation)
    {
        if (!IsServer) return;
        if (grabbedObj.TryGet(out NetworkObject netObj))
        {
            netObj.transform.position = position;
            netObj.transform.rotation = rotation;
        }
    }

    [ServerRpc]
    public void setIsKinematicServerRpc(NetworkObjectReference grabbedObj, bool value)
    {
        if (!IsServer) return;
        if (grabbedObj.TryGet(out NetworkObject netObj))
        {
            netObj.GetComponent<Rigidbody>().isKinematic = value;
        }
    }
}

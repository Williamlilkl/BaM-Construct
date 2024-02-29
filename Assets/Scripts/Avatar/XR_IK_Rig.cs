using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class XR_IK_Rig : NetworkBehaviour
{
    // Note that this is called only when the HumanoidAvatar prefab is spawned by the server
    // This class identifies the actual bones using the HumanDescription bones and:
    // 1. Sets all IK Constraints objects fields (bones) in the XR IK Rig
    // 2. Sets the IK_Target positions/rotations in the XR IK Rig 

    [SerializeField] public GameObject Dynamic_FBX_Object;
    [SerializeField] public Avatar Dynamic_FBX_Avatar;

    Animator HumanoidAvatarAnimator;
    Dictionary<string, GameObject> BoneDict = new Dictionary<string, GameObject>();
    string[] humanBones = { "LeftUpperArm",  "LeftLowerArm", "LeftHand",                //list of all the bones we need to identfy
                            "RightUpperArm", "RightLowerArm", "RightHand",
                            "Head",
                            "Left Index Proximal", "Left Index Distal",                 //Note! If the distal has a child, then we must use that, the Humanoid model has only 3 finger digits!
                            "Right Index Proximal", "Right Index Distal",               //idem & for all the below...
                            "Left Middle Proximal", "Left Middle Distal",
                            "Right Middle Proximal", "Right Middle Distal",
                            "Left Ring Proximal", "Left Ring Distal",
                            "Right Ring Proximal", "Right Ring Distal",
                            "Left Thumb Proximal", "Left Thumb Distal",
                            "Right Thumb Proximal", "Right Thumb Distal",
                            "Left Little Proximal", "Left Little Distal",
                            "Right Little Proximal", "Right Little Distal"
    };

    public bool InitializeIK()
    {
        Debug.Log("Initialize IK");

        //IK Must be initialized for ALL avatars, not just the local avatar
        HumanoidAvatarAnimator = transform.parent.GetComponent<Animator>();
        var hD = HumanoidAvatarAnimator.avatar.humanDescription;        //helper to avoid unnecessary lengthy code

        HumanoidAvatarAnimator.avatar = Dynamic_FBX_Avatar;
            
        //Some testing
        if (!HumanoidAvatarAnimator) Debug.LogError("XR IK Rig Initialization: cannot find the Animator object");
        if (!HumanoidAvatarAnimator.avatar.isHuman) Debug.LogError("XR IK Rig Initialization: your avatar object is not configured with a human bone skeleton");

        //Fill Bone mapping lookup Dictionary
        //GameObject rootBone = transform.parent.Find(hD.human[0].boneName).gameObject;
        GameObject rootBone = Dynamic_FBX_Object.transform.Find(hD.human[0].boneName).gameObject;
        if (!rootBone) Debug.LogError("FATAL: cannot find bone structure");

        for (int i=0;i<hD.human.Length; i++)
        {
            Transform targetBone = RealBoneFind(rootBone, hD.human[i].boneName).transform;
            if (targetBone)
            {
                BoneDict.Add(hD.human[i].humanName, targetBone.gameObject);
                //Debug.Log("Bone: " + hD.human[i].humanName + " mapped to: " + targetBone.name);
            }
        }

        DisableEnableRigBuilder(false);  //turn rigbuilder OFF => THIS IS NECESSARY OTHERWISE THE ANIMATIONS WON"T WORK AFTER THE CHANGES!

        //Now we're relinking our IK Constraint Objects
        InitTwoBoneConstraintObject(transform.Find("Left Arm IK").gameObject, 0);
        InitTwoBoneConstraintObject(transform.Find("Right Arm IK").gameObject, 3);
        InitMPConstraintObject     (transform.Find("Head IK").gameObject, 6);
        InitChainIKConstraintObject(transform.Find("Left Index Finger IK").gameObject, 7);
        InitChainIKConstraintObject(transform.Find("Right Index Finger IK").gameObject, 9);
        InitChainIKConstraintObject(transform.Find("Left Middle Finger IK").gameObject, 11);
        InitChainIKConstraintObject(transform.Find("Right Middle Finger IK").gameObject, 13);
        InitChainIKConstraintObject(transform.Find("Left Ring Finger IK").gameObject, 15);
        InitChainIKConstraintObject(transform.Find("Right Ring Finger IK").gameObject, 17);
        InitChainIKConstraintObject(transform.Find("Left Thumb IK").gameObject, 19);
        InitChainIKConstraintObject(transform.Find("Right Thumb IK").gameObject, 21);
        InitChainIKConstraintObject(transform.Find("Left Pinky Finger IK").gameObject, 23);
        InitChainIKConstraintObject(transform.Find("Right Pinky Finger IK").gameObject, 25);

        DisableEnableRigBuilder(true);  //turn rigbuilder ON => THIS IS NECESSARY OTHERWISE THE ANIMATIONS WON"T WORK AFTER THE CHANGES!

        Debug.Log("XR_IK Constraints initialized");

        return (true);
    }

    //Helper function to find the GameObject for the bone with name, start searching from bone
    //Recursive so we initiate the search with searching at the root bone level and then do a recursive DFS
    GameObject RealBoneFind(GameObject bone, string name)       //recursive search
    {
        GameObject tmp = null;

        //Debug.Log("RBF " + name);

        if (bone.name == name) return (bone);                   //match!
        else
            for (int i = 0; i < bone.transform.childCount; i++) //search all children
            {
                tmp = RealBoneFind(bone.transform.GetChild(i).gameObject, name);
                if (tmp) break;                                 //if something comes back, push it upward
            }
        return (tmp);
    }

    void InitTwoBoneConstraintObject(GameObject XRIK, int LR)  //VRIK=IK object link, LR=0==left, LR=3==right
    {
        //Hands
        //Debug.Log("XR_IK_Rig: Hands");
        TwoBoneIKConstraint tbc = XRIK.GetComponent<TwoBoneIKConstraint>();

        //Explicitly set the values 
        tbc.data.targetPositionWeight = 1;
        tbc.data.targetRotationWeight = 1;
        tbc.data.hintWeight = 1;
        tbc.weight = 1;

        //Debug.Log(XRIK.name);
        tbc.data.root = BoneDict[humanBones[LR]].transform;
        tbc.data.mid =  BoneDict[humanBones[LR+1]].transform;
        tbc.data.tip =  BoneDict[humanBones[LR+2]].transform;

        //Now we set the IK Target transforms
        XRIK.transform.GetChild(0).position = XRIK.GetComponent<TwoBoneIKConstraint>().data.tip.position;
        XRIK.transform.GetChild(0).rotation = XRIK.GetComponent<TwoBoneIKConstraint>().data.tip.rotation;
    }

    void InitMPConstraintObject(GameObject XRIK, int hBindex)  //VRIK=IK object link
    {
        //Head
        //Debug.Log("XR_IK_Rig: Head");
        MultiParentConstraint mpc = XRIK.GetComponent<MultiParentConstraint>();
        mpc.weight = 1;
        mpc.data.constrainedObject = BoneDict[humanBones[hBindex]].transform;

        XRIK.transform.GetChild(0).position = XRIK.GetComponent<MultiParentConstraint>().data.constrainedObject.position;
        XRIK.transform.GetChild(0).rotation = XRIK.GetComponent<MultiParentConstraint>().data.constrainedObject.rotation;
    }

    void InitChainIKConstraintObject(GameObject XRIK, int hBindex)  //VRIK=IK object link, hBindex=root, hBindex+1=Tip
    {
        //Fingers
        //Debug.Log("XR_IK_Rig: Fingers");
        ChainIKConstraint cikc = XRIK.GetComponent<ChainIKConstraint>();

        //Explicitly set the values 
        cikc.data.root = BoneDict[humanBones[hBindex]].transform;
        cikc.data.chainRotationWeight = 1;
        cikc.data.tipRotationWeight = 1;
        cikc.data.maxIterations = 15;
        cikc.data.tolerance = 0.0001f;
        cikc.weight = 1;

        //check if the distal has another digit connected to it, in that case we need to use that child as the tip!!!
        cikc.data.tip = (BoneDict[humanBones[hBindex + 1]].transform.childCount > 0 ?
            BoneDict[humanBones[hBindex + 1]].transform.GetChild(0).transform :     //found a child, lets use that as tip! Note there should be exactly one
            BoneDict[humanBones[hBindex + 1]].transform);                           //no tip digit found so use the Humanoid defined one

        //Now we set the IK_Target transforms
        if (XRIK.transform.childCount == 0) Debug.LogError("Can't find the IK_Target childobject for " + XRIK.name);
        else
        {
            //Debug.Log("XR_IK_Rig: " + cikc.data.tip.name);
            XRIK.transform.GetChild(0).transform.position = XRIK.GetComponent<ChainIKConstraint>().data.tip.transform.position;
            XRIK.transform.GetChild(0).transform.rotation = XRIK.GetComponent<ChainIKConstraint>().data.tip.transform.rotation;
        }
    }

    //Helper function to turn ON/OFF the RigBuilder component 
    //Apparently you can't make runtime changes to IK Constraints without turning off RigBuilder and then turning it on again when you're done!
    public void DisableEnableRigBuilder(bool state)
    {
        Debug.Log((state ? "En" : "Dis") + "able RigBuilder");

        RigBuilder rB = transform.parent.GetComponent<RigBuilder>();
        if (!rB)
            Debug.LogError("RigBuilder not found!");
        else
        {
            foreach (RigLayer rl in rB.layers) rl.active = state;
            rB.enabled = state;
        }
    }
}

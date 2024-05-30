using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myPercussionModeManager : MonoBehaviour
{

    [SerializeField]
    private GameObject percussionPackageLeft;

    [SerializeField]
    private GameObject percussionPackageRight;

    [SerializeField]
    private Transform LHT;

    [SerializeField]
    private Transform RHT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        percussionPackageLeft.transform.SetPositionAndRotation(LHT.transform.position, LHT.transform.rotation);
        percussionPackageRight.transform.SetPositionAndRotation(RHT.transform.position, RHT.transform.rotation);
    }
}

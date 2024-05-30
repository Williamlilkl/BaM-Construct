using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFollowingDiaphragm : MonoBehaviour
{  
    private GameObject RHTD;
    private GameObject Scope;

    // Start is called before the first frame update
    void Start()
    {
        RHTD = this.gameObject.GetComponentInParent<HandLocationSet>().RHTD;
        Scope = this.gameObject;
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        Scope.transform.SetPositionAndRotation(RHTD.transform.position, RHTD.transform.rotation);
    }
}

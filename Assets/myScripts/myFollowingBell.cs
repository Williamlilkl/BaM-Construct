using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFollowingBell : MonoBehaviour
{
    private GameObject RHTB;
    private GameObject Scope;

    // Start is called before the first frame update
    void Start()
    {
        RHTB = this.gameObject.GetComponentInParent<HandLocationSet>().RHTB;
        Scope = this.gameObject;
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        Scope.transform.SetPositionAndRotation(RHTB.transform.position, RHTB.transform.rotation);
    }
}

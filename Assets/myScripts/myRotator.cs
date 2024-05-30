using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class myRotator : MonoBehaviour
{

    [SerializeField]
    private GameObject scope;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        scope.transform.Rotate(Vector3.forward, 0.3f);
    }
}

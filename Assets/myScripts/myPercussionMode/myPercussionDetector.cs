using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myPercussionDetector : MonoBehaviour
{

    [SerializeField]
    private GameObject percussionPointer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "collider")
        percussionPointer.GetComponent<myPercussionPointer>().enabled = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "collider")
        percussionPointer.GetComponent<myPercussionPointer>().enabled = false;
    }
}

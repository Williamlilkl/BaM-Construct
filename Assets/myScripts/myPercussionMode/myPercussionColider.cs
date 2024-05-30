using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myPercussionCollider : MonoBehaviour
{

    [SerializeField]
    private GameObject percussionClicker;

    [SerializeField]
    private GameObject percussionDetector;

    [SerializeField]
    private GameObject ShieldL;

    [SerializeField]
    private GameObject ShieldR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.name == "Percussion Package Left"){
            
            ShieldL.SetActive(true);
            ShieldR.SetActive(true);  
            percussionClicker.SetActive(true);
            percussionDetector.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        
        if (other.gameObject.name == "Percussion Package Left"){
            
            ShieldL.SetActive(false);
            ShieldR.SetActive(false);  
            percussionClicker.SetActive(false);
            percussionDetector.SetActive(false);
        }
    }

}

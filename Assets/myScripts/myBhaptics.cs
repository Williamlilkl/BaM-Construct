using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;
using Unity.VisualScripting;

public class myBhaptics : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    { 
        Debug.Log(other.gameObject.GetComponentInParent<AudioSource>());
    }

    private void OnTriggerStay(Collider other)
    {

        if(other.GetComponentInParent<AudioSource>() != null){
        audioSource =other.GetComponentInParent<AudioSource>();
        audioSource.Pause();
        Debug.Log("Paused"); 
        }

        BhapticsLibrary.Play(BhapticsEvent.LH1);

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class myScopeSound : MonoBehaviour
{
    private AudioSource audioSourceSmall;
    private AudioSource audioSourceLarge;

    private void Start() {
    
    Component[] audioSources = gameObject.GetComponentsInParent<AudioSource>();
    
    if (audioSources.Length > 1) {
        audioSourceSmall = (AudioSource)audioSources[0];
        audioSourceLarge = (AudioSource)audioSources[1];
    }
    else {
        audioSourceSmall = (AudioSource)audioSources[0];
    }

    }
    void OnTriggerEnter(Collider collider) {
        Debug.Log( collider.gameObject.name);
        if (collider.gameObject.name == "small"){
            audioSourceSmall.Play();
        }
        else if (collider.gameObject.name == "large" && audioSourceLarge != null){
            audioSourceLarge.Play();
        }    
    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.name == "small"){
            audioSourceSmall.Pause();
        }
        else if (collider.gameObject.name == "large" && audioSourceLarge != null){
            audioSourceLarge.Pause();
        }
    }
}

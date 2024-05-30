using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Bhaptics.SDK2;
using UnityEngine.XR.Hands;
using System.Runtime.Serialization.Formatters;
using UnityEditor;
using UnityEngine.Video;

public class myMultiMediaInteractable : MonoBehaviour
{
    private XRBaseInteractable interactable;
    private string interactorName;
    
    [SerializeField]
    GameObject FollowPanel;

    [SerializeField]
    VideoPlayer VideoPlayerPanel;


    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(myBhaptics);
        interactable.hoverExited.AddListener(myBhapticsStop);
    }

    public void myBhaptics(BaseInteractionEventArgs hover)
    {
        
        if(hover.interactorObject is XRDirectInteractor)
        {
            Debug.Log("Direct");
            
            XRDirectInteractor interactor = (XRDirectInteractor)hover.interactorObject;
            interactorName = interactor.ToString();
            Debug.Log(interactorName);

            FollowPanel.SetActive(true);
            VideoPlayerPanel.Play();

        }

    }

    public void myBhapticsStop(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRDirectInteractor)
        {
            VideoPlayerPanel.Stop();
            FollowPanel.SetActive(false);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

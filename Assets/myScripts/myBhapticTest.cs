using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Bhaptics.SDK2;
using UnityEngine.XR.Hands;
using System.Runtime.Serialization.Formatters;
using UnityEditor;

public class myBhapticsTest : MonoBehaviour
{
    private XRBaseInteractable interactable;
    private string interactorName;


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
            XRDirectInteractor interactor = (XRDirectInteractor)hover.interactorObject;
            interactorName = interactor.ToString();
            Debug.Log(interactorName);

            BhapticsLibrary.PlayLoop(BhapticsEvent.TESTHAND , 1f , 1f, 20f , 0f , 285 , 15000);
        }

    }

    public void myBhapticsStop(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRDirectInteractor)
        {
            BhapticsLibrary.StopAll();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

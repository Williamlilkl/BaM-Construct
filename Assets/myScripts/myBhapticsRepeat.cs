using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Bhaptics.SDK2;
using UnityEngine.XR.Hands;
using System.Runtime.Serialization.Formatters;
using UnityEditor;

public class myBhapticsInteractableRepeat : MonoBehaviour
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
            Debug.Log("Direct");
            
            XRDirectInteractor interactor = (XRDirectInteractor)hover.interactorObject;
            interactorName = interactor.ToString();
            Debug.Log(interactorName);

            

            if(interactorName.CompareTo("Direct Interactor_R") == -1)
            {
                if(interactorName.CompareTo("Direct Interactor_L_4") == 1)
                {
                Debug.Log("little");
                BhapticsLibrary.PlayLoop(BhapticsEvent.LH5 , 1f , 1f, 20f , 0f , 285 , 1000);
                }

                else if(interactorName.CompareTo("Direct Interactor_L_3") == 1)
                {
                Debug.Log("ring");
                BhapticsLibrary.PlayLoop(BhapticsEvent.LH4 , 1f , 1f, 20f , 0f , 285 , 1000);
                }

                else if(interactorName.CompareTo("Direct Interactor_L_2") == 1)
                {
                Debug.Log("middle");
                BhapticsLibrary.PlayLoop(BhapticsEvent.LH3 , 1f , 1f, 20f , 0f , 285 , 1000);
                }

                else if(interactorName.CompareTo("Direct Interactor_L_1") == 1){
                Debug.Log("index");
                BhapticsLibrary.PlayLoop(BhapticsEvent.LH2 , 1f , 1f, 20f , 0f , 285 , 1000);
                }

                else if(interactorName.CompareTo("Direct Interactor_L") == 1){
                Debug.Log("thumb");
                BhapticsLibrary.PlayLoop(BhapticsEvent.LH1 , 1f , 1f, 20f , 0f , 285 , 1000);
                }

                else
                Debug.Log("Error");
            }

            else
            {
                if(interactorName.CompareTo("Direct Interactor_R_4") == 1)
                {
                Debug.Log("little");
                BhapticsLibrary.PlayLoop(BhapticsEvent.RH5 , 1f , 1f, 20f , 0f , 285 , 1000);
                }

                else if(interactorName.CompareTo("Direct Interactor_R_3") == 1)
                {
                Debug.Log("ring");
                BhapticsLibrary.PlayLoop(BhapticsEvent.RH4 , 1f , 1f, 20f , 0f , 285 , 1000);
                }

                else if(interactorName.CompareTo("Direct Interactor_R_2") == 1)
                {
                Debug.Log("middle");
                BhapticsLibrary.PlayLoop(BhapticsEvent.RH3 , 1f , 1f, 20f , 0f , 285 , 1000);
                }

                else if(interactorName.CompareTo("Direct Interactor_R_1") == 1){
                Debug.Log("index");
                BhapticsLibrary.PlayLoop(BhapticsEvent.RH2 , 1f , 1f, 20f , 0f , 285 , 1000);
                }

                else if(interactorName.CompareTo("Direct Interactor_R") == 1){
                Debug.Log("thumb");
                BhapticsLibrary.PlayLoop(BhapticsEvent.RH1 , 1f , 1f, 20f , 0f , 285 , 1000);
                }

                else
                Debug.Log("Error");
            }

            

            

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

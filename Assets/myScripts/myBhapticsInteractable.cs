using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Bhaptics.SDK2;
using UnityEngine.XR.Hands;
using System.Runtime.Serialization.Formatters;
using UnityEditor;

public class myBhapticsInteractable : MonoBehaviour
{
    private XRBaseInteractable interactable;
    private string interactorName;


    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(myBhaptics);
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
                BhapticsLibrary.Play(BhapticsEvent.LH5);
                }

                else if(interactorName.CompareTo("Direct Interactor_L_3") == 1)
                {
                Debug.Log("ring");
                BhapticsLibrary.Play(BhapticsEvent.LH4);
                }

                else if(interactorName.CompareTo("Direct Interactor_L_2") == 1)
                {
                Debug.Log("middle");
                BhapticsLibrary.Play(BhapticsEvent.LH3);
                }

                else if(interactorName.CompareTo("Direct Interactor_L_1") == 1){
                Debug.Log("index");
                BhapticsLibrary.Play(BhapticsEvent.LH2);
                }

                else if(interactorName.CompareTo("Direct Interactor_L") == 1){
                Debug.Log("thumb");
                BhapticsLibrary.Play(BhapticsEvent.LH1);
                }

                else
                Debug.Log("Error");
            }

            else
            {
                if(interactorName.CompareTo("Direct Interactor_R_4") == 1)
                {
                Debug.Log("little");
                BhapticsLibrary.Play(BhapticsEvent.RH5);
                }

                else if(interactorName.CompareTo("Direct Interactor_R_3") == 1)
                {
                Debug.Log("ring");
                BhapticsLibrary.Play(BhapticsEvent.RH4);
                }

                else if(interactorName.CompareTo("Direct Interactor_R_2") == 1)
                {
                Debug.Log("middle");
                BhapticsLibrary.Play(BhapticsEvent.RH3);
                }

                else if(interactorName.CompareTo("Direct Interactor_R_1") == 1){
                Debug.Log("index");
                BhapticsLibrary.Play(BhapticsEvent.RH2);
                }

                else if(interactorName.CompareTo("Direct Interactor_R") == 1){
                Debug.Log("thumb");
                BhapticsLibrary.Play(BhapticsEvent.RH1);
                }

                else
                Debug.Log("Error");
            }

        }

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

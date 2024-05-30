using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Bhaptics.SDK2;
using UnityEngine.XR.Hands;
using System.Runtime.Serialization.Formatters;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.Hands;
using System;
using Unity.VisualScripting;

public class myChanger : MonoBehaviour
{
    
    private XRBaseInteractable interactable;
    private string interactorName;

    [SerializeField]
    private GameObject scope;

    [SerializeField]
    private Transform scopeParent;

    [SerializeField]
    private GameObject scope2;

    [SerializeField]
    private GameObject rope;

    [SerializeField]
    private GameObject LH_v;

    [SerializeField]
    private GameObject RH_v;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverExited.AddListener(Changer);
    }

    public void Changer(BaseInteractionEventArgs exit)
    {
        if(exit.interactorObject is XRDirectInteractor)
        {

        XRDirectInteractor interactor = (XRDirectInteractor)exit.interactorObject;
        interactorName = interactor.ToString();

        if( LH_v.activeSelf && RH_v.activeSelf ){

        if(interactorName.CompareTo("Direct Interactor_R") >= 1)
        {
            Instantiate(scope , scopeParent);
            scope2.SetActive(false);
            RH_v.SetActive(false);
            Debug.Log("R");
            
        }

        else
        {
            Debug.Log("L");
        }

        }

        }

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

}

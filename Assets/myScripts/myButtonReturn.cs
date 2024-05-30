using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class myButtonReturn : MonoBehaviour
{
    [SerializeField]
    private GameObject scope;

    [SerializeField]
    private GameObject scope2;

    [SerializeField]
    private GameObject RH_v;

    public Button yourButton;

        void Start()
            {
                Button btn = yourButton.GetComponent<Button>();
                btn.onClick.AddListener(TaskOnClick);
                
            }

        void TaskOnClick()
            {
                if (scope.GetComponentInChildren<DestroySelf>() != null){
                    scope.GetComponentInChildren<DestroySelf>().destroySelf();

                    scope2.SetActive(true);
            
                    RH_v.SetActive(true);
                }

                if (scope.GetComponentInChildren<DestroySelf>() != null){
                    scope.GetComponentInChildren<DestroySelf>().destroySelf();

                    scope2.SetActive(true);
            
                    RH_v.SetActive(true);
                }
            }
}







        
            
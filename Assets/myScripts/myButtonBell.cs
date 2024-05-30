using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class myButtonBell : MonoBehaviour
{
    [SerializeField]
    private GameObject scope;

   public Button yourButton;
        void Start()
            {
               Button btn = yourButton.GetComponent<Button>();
                btn.onClick.AddListener(TaskOnClick);
            }
      
        void TaskOnClick()
            {
                if(scope.transform.GetChild(0).gameObject != null){
                    scope.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
                    scope.transform.GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(true);
                    scope.transform.GetChild(0).GetChild(1).GetComponent<myFollowingDiaphragm>().enabled = false;
                    scope.transform.GetChild(0).GetChild(1).GetComponent<myFollowingBell>().enabled = true;
                }
            }
}

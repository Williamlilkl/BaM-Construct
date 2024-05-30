using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TellingMode : MonoBehaviour
{
    public Button yourButton;

    [SerializeField]
    private AudioSource[] modeChangingSound;

    private int modeIndex;

        void Start()
            {
                Button btn = yourButton.GetComponent<Button>();
                btn.onClick.AddListener(TaskOnClick);

                for (int i = 0; i < modeChangingSound.Length; i++){
                    if (this.gameObject.name == modeChangingSound[i].name){
                        modeIndex = i;
                    }
                }
            }

        void TaskOnClick()
        {
            modeChangingSound[modeIndex].Play();
        }
}

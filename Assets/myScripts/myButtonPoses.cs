using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class myButtonPoses : MonoBehaviour
{
    [SerializeField]
    private GameObject[] poses;

    [SerializeField]
    private Animator bedAnimator;



    public Button yourButton;

    private string[] triggerNames = {"to45","to90","toLeft","to180"};

    private string[] buttonNames = {"Pose1","Pose2","Pose3","Pose4"};

    private string[] bedNames = {"to45","to180","to180","to180"};

    private int buttonIndex;

        void Start()
            {
                Button btn = yourButton.GetComponent<Button>();
                btn.onClick.AddListener(TaskOnClick);

                for (int i = 0; i < buttonNames.Length; i++){
                if (this.gameObject.name == buttonNames[i]){
                    buttonIndex = i;
                }
                }
                        
            }

        void TaskOnClick()
        {
            this.gameObject.GetComponent<AudioSource>().Play();

            Invoke(nameof(AnimationPlay) , 1.8f);
        }

        void AnimationPlay(){
            
            for (int i = 0; i < poses.Length; i++){
                if (poses[i].activeSelf == true){
                    poses[i].transform.GetChild(0).GetComponent<Animator>().SetTrigger(triggerNames[buttonIndex]);
                    poses[i].transform.GetChild(0).GetComponent<Animator>().Play("New State");
                }      
            }
            
            bedAnimator.Play(bedNames[buttonIndex]);

        }
}




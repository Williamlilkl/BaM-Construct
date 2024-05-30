using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class myButtonPop : MonoBehaviour
{
    public Button yourButton;

    [SerializeField]
    private AudioSource buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
        {
            if (buttonSound != null)
            {
                buttonSound.Play();
            }
        }
}

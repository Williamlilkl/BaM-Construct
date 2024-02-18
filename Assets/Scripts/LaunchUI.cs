using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LaunchUI : MonoBehaviour
{
    [SerializeField]
    private Button HostButton;
    [SerializeField]
    private Button ClientButton;
    [SerializeField]
    private Toggle VoiceToggle;
    //GRAB
    [SerializeField] Vector3 ObjectSpawnerPosition;

    private void Awake()
    {
        HostButton.onClick.AddListener(() =>
        {
            //add code here
            NetworkManager.Singleton.StartHost();

            //GRAB!
            GameObject spawner = Resources.Load("Table") as GameObject;
            GameObject go = Instantiate(spawner, ObjectSpawnerPosition, Quaternion.identity);
            go.GetComponent<NetworkObject>().Spawn();
            go.GetComponent<GrabbableCreator>().SpawnGrabbables();
            
            //BUGFIX
            TurnOnOffUI(false);
        });

        ClientButton.onClick.AddListener(() =>
        {
            //add code here
            NetworkManager.Singleton.StartClient();

            //BUGFIX
            TurnOnOffUI(false);
        });

        VoiceToggle.onValueChanged.AddListener(delegate
             { VivoxToggle(VoiceToggle); });

    }

    void VivoxToggle(Toggle voiceToggle)
    {
        Debug.Log("Voice " + voiceToggle.isOn);
    }

    //BUGFIX
    void TurnOnOffUI(bool value)
    {
        if (!value)
        {
            HostButton.gameObject.SetActive(value);
            ClientButton.gameObject.SetActive(value);
            VoiceToggle.gameObject.SetActive(value);
        }
    }

}

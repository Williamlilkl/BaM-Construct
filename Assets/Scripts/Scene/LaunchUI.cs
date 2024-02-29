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
    private Button GenderButton;
    [SerializeField]
    private Button BreedButton;
    [SerializeField]
    private Toggle VoiceToggle;
    //GRAB
    [SerializeField] Vector3 ObjectSpawnerPosition;

    private void Awake()
    {
        HostButton.onClick.AddListener(() =>
        {
            //add code here
            NetworkManager.Singleton.StartHost(); //the server will now spawn the InitialNetworkPrefab

            //GRAB!
            GameObject spawner = Resources.Load("Table") as GameObject;
            GameObject go = Instantiate(spawner, ObjectSpawnerPosition, Quaternion.identity);
            go.GetComponent<NetworkObject>().Spawn();
            go.GetComponent<GrabbableCreator>().SpawnGrabbables();
        });


        ClientButton.onClick.AddListener(() =>      //the server will now spawn the InitialNetworkPrefab
        {
            /*
            //NO LONGER NEEDED AS IN 2nd STAGE WE DON"T NEED TO AGAIN LOAD THE RESOURCES AS NETWORKPREFABS!
            //Detect whether we reconnect and ensure we load all prefabs in the nm prefablist
            ServerRepository serverRepo     = GameObject.Find("ServerRepository").GetComponent<ServerRepository>();
            string prefabList               = serverRepo.ReturnOnlinePrefabs();
            string hashList                 = serverRepo.ReturnOnlineHashes();

            //Second Stage: first prepare the network prefab list BEFORE we sign in!
            
            if (serverRepo.GetPIN() != "")
            {
                Debug.Log("====> CLIENT: STAGE 2 <====");
                serverRepo.Dump("STAGE 2");
                GameObject.Find("Network Manager").GetComponent<NetworkPrefabLoader>().UpdateNetworkPrefabList(prefabList, hashList); //CHECK WHETHER THIS IS NEEDED!
            }
            else Debug.Log("CLIENT: STAGE1");
            */

            NetworkManager.Singleton.StartClient();
        });


        //TEMPORARY AVATAR SELECTORS, WILL BE REPLACED WITH FUTURE PIN CODE MECHANISM
        GenderButton.onClick.AddListener(() =>
        {
            TMPro.TextMeshProUGUI t = GenderButton.transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>();
            t.text = (t.text == "Male" ? "Female" : "Male");  //flip
        });


        //TEMPORARY AVATAR SELECTORS, WILL BE REPLACED WITH FUTURE PIN CODE MECHANISM
        BreedButton.onClick.AddListener(() =>
        {
            TMPro.TextMeshProUGUI t = BreedButton.transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>();
            t.text = (t.text == "Human" ? "Bot" : "Human");  //flip
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
            VoiceToggle.gameObject.SetActive(value);    //causes an exception when client sign in so need to fix
        }
    }

}

What is this project
====================
This is a fully functional Unity project. The project provides a baseline Multiplayer VR Application with:
* Netcode for GameObjects Host and Client option
* Ad-hoc join capability with Just In Time client/server protocol and a server repository to synchronize prefabs across all clients post app launch
* 2-Stage Client Sign-In process 
* Hands, head and detailed finger tracking from your XR system to your avatars
* 4 preconfigured avatars (male/female, human/bot), easily replace with your own avatars
* Auto-IK component to automatically configure all IK constraints of your avatars hands, head and fingers
* Checkbox for using a local or a web-based prefab/avatar resource repository
* Tool to generate asset bundles to store on the web-based repository
* 2 different object grab implementations
* First Person and Third Person mode via the UI (FP3P)
* Vivox Voice Services with Spatial audio (need to add your own credentials)
* Debug Console (3rd party free component) for getting debug information on standalone VR/XR systems
* OpenXR based so in principle platform-agnostic

What is new in this Branch
==========================
This branch is updated from the MAIN branch (Episode 8 of the Build a Metaverse video on Youtube):
* Introduces an automatic XR IK component that automatically generates the IK constraints so loaded a new FBX is much easier
* Allows for multiple avatars, where in the main branch all players had the same avatar
* Allows for ad-hoc joining the network
* Choose whether you want to load resources from your local Unity-application repository (hard-coded) or from your remote webserver using AssetBundles
* Introduces a Unity Editor Menu Item to generate AssetBundles -> zip them and transfer to your webserver -> unzip: done.

Why is this update such a big deal?
===================================
Each player must have a player prefab in the Network Manager. Then when a client joins and the player prefab is spawned:
1. The new client must have all already-connected network prefab Resources loaded and added to its own local network prefab list in its local NetworkManager
2. All remote clients that are connected to the server must have the new client’s prefab Resource loaded and added to their local Network Manager
3. The server must have the new client’s prefab Resource loaded and added to their local Network Manager

In other words, you can’t connect to the server unless you have all resources loaded but you can’t load all resources until you have connected to the server and the server told you which resources to load. Hence it is complex to implement Dynamic Prefabs with ad-hoc join and that is what we need for a multiplayer VR implementation.

There is also bug in Netcode for GameObjects (1.5.2). When the server adds a new player prefab to the network prefab list, it generates a hash code (GlobalObjectId) so that it can identify objects on the network. Unfortunately that process is unreliable which means you can’t safely assume that Netcode will generate a unique hashcode for each prefab. The project therefore implements a workaround for hashcode issues during the network join process.

How does this code work
=======================
To address the architectural issues with Netcode, there now is a 2-stage workaround sign-in process for each client:

    1. The client connects to the server with a generic DynamicPrefabStarter object as Player Prefab – sign-in stage
        1. the user informs the server what prefab it wants to join in with (manually pulled from the UI currently)
        2. the server blocks any new join attempts
        3. the new client loads the resources from all already-logged-in remote clients 
        4. the new clientadds these prefabs to the local network prefab list
        5. the server asks all remote clients to load the new client’s resource/prefab
        6. when loaded, the remote clients add these prefabs to their local network prefab lists
        7. the server then waits until 3 and 5 are done loading
        8. the client stores prefabs and network hashes locally
        9. the server disconnects the client and updates the client UI
           
    2. The client connects to the server again – join stage
        1. The client now has all prefabs loaded in the local network prefab list
        2. The user launches StartClient() by pressing the Join button
        3. The server spawns the player with the prefab as client Prefab
        4. the server unblocks for new join attempts

Steps to get started
====================
1. Pull the branch
2. Open Unity Editor, when prompted about Errors, select Ignore and do not start in safe mode
3. Go to Packages/xr interaction toolkit/Runtime/Inputs
4. Open the file XRInputModalityManager.cs and go to line 210 and 211 and change these to: public InputMode m_LeftInputMode; public InputMode m_RightInputMode;
5. In the VivoxVoiceManager, enter your Vivox service credentials (if not used (yet), leave them blank)
6. In Prefabs, click on the DynamicPrefabStarter object, select its ClientServerJoinProtocol script and ensure that UseWebRepository is UNCHECKED
7. Check the Platform setting under Unity Menu->File->Build Settings, if you want to compile for standalone VR (eg. Meta Quest1,2,3) select Android otherwise select Windows
8. Finally, go to File -> Open Scene -> SampleScene
   
If you want to use remote prefabs
=================================
1. In Prefabs, click on the DynamicPrefabStarter object, select its ClientServerJoinProtocol script and ensure that UseWebRepository is CHECKED
2. Enter the url of your own webserver where you want the AssetBundles to be stored
3. In the Assets menu in Unity, select Build Asset Bundles
4. When that is completed, open a File Explorer and find your Assets folder, there is an AssetBundle folder with a windows and an android subfolder
5. ZIP the AssetBundle folder and transfer it to your webserver, unpack under the the url you entered in step 2

Compatibility
=============
* Currently this code is tested for Windows and Meta Quest 1,2 and3
* If you want to add Mac with a Web Repository then you must update the Editor/AssetBundlesBuild script to generate Mac AssetBundles ...
* AND open Assets/Scripts/Network/ClientServerJoinProtocol.cs and add code in the OnNetworkSpawn() method (pretty straightforward)
* If you have another VR brand headset, you will need to configure that headset in Project Settings -> XR Plugin Management

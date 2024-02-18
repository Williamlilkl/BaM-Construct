What is this project?
This is a basic vendor agnostic multiplayer VR baseline Unity project with avatar head/hand tracking, detailed finger tracking, grab capabilities, 3rd/first person view, leg animation and spatial audio/voice services using Vivox.

How to install
You will get the message that the project contains errors. Click Ignore. Then search and open the file XRInputModalityManager.cs and go to line 210 and 211 and change these to:
        public InputMode m_LeftInputMode;
        public InputMode m_RightInputMode;

Also when you want to use Vivox ensure to get your service credentials and enter these in the VivoxVoiceManager component in the main scene.

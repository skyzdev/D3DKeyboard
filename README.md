# D3D Keyboard
HoloLens D3D Keyboard was originally created for HoloLens.
V2.0 was updated to work with the new Mixed Reality Toolkit (MRTK) on HoloLens2 and Oculus Quest. 

# Demo video on HoloLens 2: 
[![Demo video on HoloLens 2](https://i.imgur.com/rOZQKEG.png)](https://youtu.be/lTtBDCKlrXA)
__________________________________________________________________________________________
# Demo video on Oculus Quest: 
[![Demo video on Oculus Quest](https://i.imgur.com/6aGOeI5.png)](https://youtu.be/p1wQHyZKwhk)
__________________________________________________________________________________________

# Special Thanks:
Eric Provencher: Test scene "KeyboardMRTKQuest.unity" was implemented based on a fork from Eric Provencher's MRTK-Quest (https://github.com/provencher/MRTK-Quest). MRTK-Quest is a Mixed Reality Toolkit (MRTK) extension for Oculus Quest, now with support for Rift/Rift S as well. It was built to showcase the hand-driven interaction model designed by Microsoft for HoloLens 2, on the Oculus ecosystem.

Anand Mulay: HoloLens2 testing video above is credited to volunteer tester Anand Mulay.
__________________________________________________________________________________________

# Overview:
This repository features a full-size 3D keyboard for XR app developers, to be used in D3D build, for in-App username / password / url entries. Organized as Drag-and-drop prefabs. Hand-crafted keyboard includes: uppercase, lowercase, digits, and most symbols available on a standard English keyboard. Special keys: "⇧": Shift; "␣": Space; "⇦": Backspace; "⏎": Return, starting on a new line. Non-special keys can be easily customized. Built-in with Show/Hide (green “done”) button. Four original recordings of key-typing sound.  

Three test scenes are included for demonstration purposes using HoloLens, HoloLens 2 or Oculus Quest. 

# Tested successfully with: 
1.	Unity version: 2018.4.16f1
2.	MixedRealityToolkit v2.2.0
3.	Visual Studio 2019 (for deploying to Hololens or HoloLens 2)
4.	Oculus Integration v13 (for deploying to Oculus Quest)

# Typing: 
1a. HoloLens2 or Oculus Quest: Use hand tracking to tap on keys. There should be a typing sound and color change as feedback.

1b. HoloLens1: Look at a key steadily such that the circular “Gaze” cursor is on the key, and perform an air tap “Select” gesture. 

2. The typed character will be added to the Text field of the Text Mesh component of gameObject “InputTextDisplay” under “keyboardone” prefab, and thus displayed above the keyboard.  


# Special keys:  
 “⇧”: Shift between lowercase and uppercase keyboard displays and inputs;  
 “␣”: Space;  
 “⇦”: Backspace;  
 “⏎”: Return, starting on a new line;  
“done” green button: Shows / Hides the keyboard.  

# Customization: 
Customization to the special keys would require changes to the scripts (which is not part of the scope of this documentation).  
To customize a non-special key (i.e. keys other than “⇧”, “␣”, “⇦”, “⏎”, and “done” green button):  

1.	Locate the key which needs modification under the gameObject named “keyboardSet” within the hierarchy of “keyboardone”. The gameObjects for non-special keys are named with 5 characters: “key”+ 1 character for lowercase + 1 character for uppercase. 

2.	Change the lowercase and uppercase character part of the name to the characters of your choosing. E.g. “keyaA” to “keybB” 

# Folder structure: 
Scripts: 
1.	KeyboardGG.cs: This script is attached to each gameObject representing a key, handing user interactions with each key 

2.	KeyboardMain.cs: This script is attached to the top level gameObject “keyboardone”, handling keyboard level changes such as shifting between uppercase and lowercase displays, showing and hiding the keyboard.  

Prefabs: Keyboard prefabs are in the “Models” folder 

Sounds: Key-typing sound files are in the “Sounds” folder  

Other: Courier font is used for the display of characters overlaying each key. This is recommended for equally spaced display of characters. 

_____________________________________________________________________________________________________________________________





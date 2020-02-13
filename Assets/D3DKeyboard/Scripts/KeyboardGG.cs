//Author: Sky Zhou, Matrix Inception Inc.
//HoloLens D3D Keyboard v2.0
//Date: 2020-02-02
//This script is attached to each key, and prompts an action when a key is selected.
//

using UnityEngine;
using System.Collections;

public class KeyboardGG : MonoBehaviour
{

    public GameObject KeyboardOne;
    KeyboardMain keyboardMain;

    string inputString; 
    string keyName;
    int keySoundIndex;
    AudioSource audioSource;
    Color keyColor;
    float lastTypeTime;

    // Use this for initialization
    void Start () {
        if (KeyboardOne == null)
        {
            KeyboardOne = FindObjectOfType<KeyboardMain>().gameObject;
        }
        keyboardMain = KeyboardOne.GetComponent<KeyboardMain>();      
        audioSource = KeyboardOne.GetComponent<AudioSource>();
        keyColor = GetComponent<MeshRenderer>().material.GetColor("_Color");

        if (GetComponent<Microsoft.MixedReality.Toolkit.UI.PressableButton>() != null && GetComponent<Microsoft.MixedReality.Toolkit.UI.PressableButton>().enabled)
        {
            GetComponent<Microsoft.MixedReality.Toolkit.UI.PressableButton>().ButtonPressed.AddListener(UI_OnClick);
        }

        if (GetComponentInChildren<Microsoft.MixedReality.Toolkit.UI.Interactable>() != null)
        {
            GetComponentInChildren<Microsoft.MixedReality.Toolkit.UI.Interactable>().OnClick.AddListener(UI_OnClick);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.realtimeSinceStartup - lastTypeTime > 0.1f && Time.realtimeSinceStartup - lastTypeTime < 0.15f)
        {
            GetComponent<MeshRenderer>().material.SetColor("_Color", keyColor);
        }
    }


    public void UI_OnClick()
    {
        keySoundIndex = (int)Mathf.Round(Random.Range(-0.49f, keyboardMain.keySounds.Length - 0.51f));
        audioSource.clip = keyboardMain.keySounds[keySoundIndex];
        audioSource.loop = false;
        audioSource.Play();

        GetComponent<MeshRenderer>().material.SetColor("_Color", keyColor + new Color(0.2f, 0.2f, 0.2f, 0f));
        lastTypeTime = Time.realtimeSinceStartup;

        keyName = gameObject.name;
        //inputString = keyboardMain.InputDisplay.GetComponent<TextMesh>().text;
        inputString = keyboardMain.GetCurrentInputText();

        switch (keyName)
        {
            case "keyBackspace":
                if (inputString.Length > 0)
                {
                    //check whether backspace should remove a line
                    if (inputString.Length > 1 && inputString.Substring(inputString.Length - 2, 2) == System.Environment.NewLine)
                    {
                        keyboardMain.InputDisplay.transform.localPosition += Vector3.up * (-0.07f);
                        inputString = inputString.Substring(0, inputString.Length - 2);
                    }
                    else
                    {
                        inputString = inputString.Substring(0, inputString.Length - 1);
                    }
                }
                break;
            case "keyShift":
                keyboardMain.OnShift();
                break;
            case "keySpace":
                inputString += " ";
                break;
            case "keyReturn":
                inputString += System.Environment.NewLine;
                keyboardMain.InputDisplay.transform.localPosition += Vector3.up * 0.07f;
                break;
            case "keyDone":
                keyboardMain.CloseKeyboard();
                break;
            case "keyDoneOutside":
                keyboardMain.OpenKeyboard();
                break;
            default:
                if (keyboardMain.ShiftOn)
                {
                    inputString += keyName.Substring(4, 1);
                }
                else
                {
                    inputString += keyName.Substring(3, 1);
                }
                break;
        }

        keyboardMain.SetCurrentInputText(inputString);
        inputString = null;
    }


    public void DisplayChar()
    {
        if (transform.childCount > 0 && transform.GetChild(0).name == "char")
        {
            keyName = gameObject.name;

            switch (keyName)
            {
                case "keyBackspace":

                    break;
                case "keyShift":

                    break;
                case "keySpace":

                    break;
                case "keyReturn":

                    break;
                case "keyDone":

                    break;
                case "keyDoneOutside":

                    break;
                default:
                    if (keyboardMain.ShiftOn)
                    {
                        transform.GetChild(0).GetComponent<TextMesh>().text = keyName.Substring(4, 1);
                    }
                    else
                    {
                        transform.GetChild(0).GetComponent<TextMesh>().text = keyName.Substring(3, 1);
                    }
                    break;
            }
        }
    }
}

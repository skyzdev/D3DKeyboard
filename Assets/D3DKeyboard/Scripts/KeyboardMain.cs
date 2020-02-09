//Author and copyright owner: Matrix Inception Inc.
//HoloLens D3D Keyboard v1.3
//Date: 2017-02-22
//This script controls higher level functions of the keyboard, namely Shift, Show / Hide, and Move / Pin.

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
//using UnityEngine.Windows.Speech;
//using HoloToolkit.Unity.InputModule;

public class KeyboardMain : MonoBehaviour {

    public TextMesh InputDisplay;
    public TextMesh instructionText;
    public bool ShiftOn;
    public bool IsDone;
    public GameObject keyboardUpper;
    public GameObject keyboardLower;
    public GameObject keyboardSet;
    public GameObject keyDone;
    public GameObject keyDoneOutside;
    public bool IsMoving;
    public AudioClip[] keySounds;
    int noteCount = 0;
    public int maxChar;
    public int displayCharLength;
    float displayScale = 0.1f;
    public enum InputType {anything = 0, email, username, password, roomname};

    Vector3 inputStartPos;
    Vector3 instructStartPos;
    Vector3 keyDoneStartPos;
    Vector3 keyboardStartPos;

    public GameObject UI_Canvas;
    public string[] inputStrings;
    public InputType[] fieldTypes;

    //Dictionary<string, int> typeIndex;
    int[] maxChars= {9999, 50, 20, 20, 20 };
    int[] minChars= {0, 5, 4, 6, 5 };
    string[] allowedCharStrings = new string[5] {"", "abcdefghijklmnopqrstuvwxyz1234567890@.-_", "abcdefghijklmnopqrstuvwxyz1234567890-", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_+=[]{},./<>?;:'|`~ ", "Rom1234567890"};
    //string[] requiredCharStrings = new string[3] { "@.", "", "" };

    List<char>[] allowedChars;
    //List<char>[] requiredChars;

    public GameObject keyboardAll;
    public TextMesh[] inputFields;
    public int inputFieldIndex=0;

    string currentInputText="";

    // Use this for initialization
    void Start() {
        if (keyboardAll == null)
        {
            keyboardAll = transform.Find("keyboardAll").gameObject;
        }

        displayScale = InputDisplay.transform.localScale.x;
        //typeIndex = new Dictionary<string, int>();

        // Add some elements to the dictionary. When elements are  
        // added through the ICollection<T> interface, the keys 
        // and values must be wrapped in KeyValuePair objects. 
        //
        //typeIndex.Add("email", 0);
        //typeIndex.Add("username", 1);
        //typeIndex.Add("password", 2);
        //typeIndex.Add("roomname", 3);

        allowedChars = new List<char>[allowedCharStrings.Length];
        for (int i = 0; i < allowedChars.Length; i++)
        {
            allowedChars[i] = allowedCharStrings[i].ToCharArray().ToList<char>();
        }

        //requiredChars = new List<char>[requiredCharStrings.Length];
        //for (int i = 0; i < requiredChars.Length; i++)
        //{
        //    requiredChars[i] = requiredCharStrings[i].ToCharArray().ToList<char>();
        //}

        if (keyboardUpper.gameObject.activeSelf|| keyboardLower.gameObject.activeSelf) { 
            keyboardUpper.SetActive(ShiftOn);
            keyboardLower.SetActive(!ShiftOn);
        }
        keyboardSet.SetActive(!IsDone);

        foreach (KeyboardGG kgg in keyboardSet.transform.GetComponentsInChildren<KeyboardGG>())
        {
            kgg.DisplayChar();
        }

        inputStartPos = InputDisplay.transform.localPosition;
        instructStartPos = instructionText.transform.localPosition;
        keyDoneStartPos = keyDone.transform.localPosition;
        keyboardStartPos = transform.localPosition;

        //InputDisplay.text = PlayerPrefs.GetString("UserName");
        displayScale = InputDisplay.transform.localScale.x;

    }

    public void SetCurrentInputText(string s)
    {
        currentInputText = s;
        if (inputFieldIndex >= 0 && inputFieldIndex < inputFields.Length)
        {
            inputStrings[inputFieldIndex] = s;
        }
    }

    public string GetCurrentInputText()
    {
        return currentInputText;
    }

    public void RefreshCurrentDisplayText()
    {
        if (inputFieldIndex >=0 && inputFieldIndex < inputFields.Length)
        {
            // inputFields[inputFieldIndex].text = (fieldTypes[inputFieldIndex] == InputType.password ? MaskPassword(currentInputText) : currentInputText);
            InputDisplay.text = (fieldTypes[inputFieldIndex] == InputType.password ? MaskPassword(currentInputText) : currentInputText);
        }
        else
        {
            InputDisplay.text = currentInputText;
        }
    }

    // Update is called once per frame
    void Update()
    {
        string currText = currentInputText;
        if (maxChar > 0)
        {

            if (currText.Length > maxChar)
            {
                currentInputText = currText.Substring(0, maxChar);
            }
        }
        currText = currentInputText;
        if (displayCharLength > 0)
        {
            if (currText.Length > displayCharLength)
                InputDisplay.transform.localScale = new Vector3(displayScale * displayCharLength / currText.Length, InputDisplay.transform.localScale.y, InputDisplay.transform.localScale.z);
        }
        RefreshCurrentDisplayText();
        
    }




    public void OnShift()
    {
        ShiftOn = !ShiftOn;

        if (keyboardUpper.gameObject.activeSelf || keyboardLower.gameObject.activeSelf)
        {
            keyboardUpper.SetActive(ShiftOn);
            keyboardLower.SetActive(!ShiftOn);
        }
        foreach (KeyboardGG kgg in keyboardSet.transform.GetComponentsInChildren<KeyboardGG>())
        {
            kgg.DisplayChar();
        }
    }

    //The green square is the "Done" key, and it's kept as a separate key from the rest of the keyboard. 
    //Once selected it shows or hides the keyboard. Additional scripts can be added here to submit the message.
    public void OnDone()
    {
        IsDone = !IsDone;
        if (IsDone) {
            instructionText.text = " ";

            if (UI_Canvas != null) UI_Canvas.SetActive(true);
            if (keyDoneOutside != null) keyDoneOutside.SetActive(true);
        }
        else
        {
            instructionText.text = "Click green button again when done ("+minChars[(int)fieldTypes[inputFieldIndex]] + " to "+maxChars[(int)fieldTypes[inputFieldIndex]] + " chars)";

            if (UI_Canvas != null) UI_Canvas.SetActive(false);
            if (keyDoneOutside != null) keyDoneOutside.SetActive(false);
        }
        keyboardSet.SetActive(!IsDone);
    }

    public void OpenKeyboard(int index=0)
    {
        inputFieldIndex = index;
        keyboardAll.SetActive(true);
        OnDone();
        currentInputText = inputStrings[index];
        //InputDisplay.text = inputStrings[index];
        if(UI_Canvas!=null) UI_Canvas.SetActive(false);      
    }

    public void CloseKeyboard()
    {
        string finishedText = (fieldTypes[inputFieldIndex] == InputType.password|| fieldTypes[inputFieldIndex] == InputType.roomname ? InputDisplay.text:InputDisplay.text.ToLower()) ;
        if (finishedText.Length > maxChars[(int)fieldTypes[inputFieldIndex]])
        {
            instructionText.text = "Input of " + finishedText.Length + " chars was too long. Max " + maxChars[(int)fieldTypes[inputFieldIndex]] + " chars.";
        }
        else if (finishedText.Length>0 && finishedText.Length < minChars[(int)fieldTypes[inputFieldIndex]])
        {
            instructionText.text = "Input of " + finishedText.Length + " chars was too short. Min " + minChars[(int)fieldTypes[inputFieldIndex]] + " chars.";
        }
        else
        {
            if (!ContainsBadCharacters(finishedText))
            {
                if (fieldTypes[inputFieldIndex] == InputType.email && finishedText.Length>0 && !(finishedText.Contains("@") && finishedText.Contains(".")))
                {
                    instructionText.text = "Invalid email address";
                }
                else
                {
                    if (UI_Canvas != null) UI_Canvas.SetActive(true);
                    OnDone();
                    inputFields[inputFieldIndex].text = (fieldTypes[inputFieldIndex] == InputType.password ? MaskPassword(finishedText): finishedText);
                    inputStrings[inputFieldIndex] = finishedText;
                    //inputStrings[inputFieldIndex] = inputStrings[inputFieldIndex].Length > maxChars[inputFieldIndex] ? inputStrings[inputFieldIndex].Substring(0, maxChars[inputFieldIndex]) : inputStrings[inputFieldIndex];
                    keyboardAll.SetActive(false);
                }
            }
        }
    }

    string MaskPassword(string text)
    {
        string textToReturn = text;
        if (text.Length < 1) { textToReturn = ""; }
        else if (text.Length < 2) { }
        else if (text.Length < 3) { textToReturn = "*"+ text.Substring(1); }
        else { textToReturn = text.Substring(0, 1) + "********************".Substring(0, Mathf.Max(1, Mathf.Min(text.Length - 2, 18))) + text.Substring(text.Length - 1, 1); }
        return textToReturn;
    }

    bool ContainsBadCharacters(string testText)
    {
        if (allowedChars[(int)fieldTypes[inputFieldIndex]].Count <= 0)
        {
            return false;
        }

        bool containsBadChar = false;
        foreach (char ch in testText)
        {
            if (!allowedChars[(int)fieldTypes[inputFieldIndex]].Contains(ch))
            {
                containsBadChar = true;
                Debug.Log(ch + " is not allowed.");
                instructionText.text = "'" + ch + "' is not allowed.";
                break;
            }
        }
        return containsBadChar;
    }

    public void RefreshInputFieldText()
    {
        for (int i = 0; i < inputFields.Length; i++)
        {
            inputFields[i].text = (fieldTypes[i] == InputType.password? MaskPassword(inputStrings[i]) : inputStrings[i]);
        }
    }

}

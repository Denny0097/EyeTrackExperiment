using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveInputFields : MonoBehaviour
{
    // References to TMP_InputFields
    public TMP_InputField _inputQuizNum;
    public TMP_InputField Target_X;
    public TMP_InputField Target_Y;

    // Keys for PlayerPrefs
    private const string InputQuizNumKey = "InputQuizNum";
    private const string InputTargetXKey = "InputTargetX";
    private const string InputTargetYKey = "InputTargetY";

    void Start()
    {
        // Load the saved input field content if it exists
        if (PlayerPrefs.HasKey(InputQuizNumKey))
        {
            _inputQuizNum.text = PlayerPrefs.GetString(InputQuizNumKey);
        }
        if (PlayerPrefs.HasKey(InputTargetXKey))
        {
            Target_X.text = PlayerPrefs.GetString(InputTargetXKey);
        }
        if (PlayerPrefs.HasKey(InputTargetYKey))
        {
            Target_Y.text = PlayerPrefs.GetString(InputTargetYKey);
        }


        // Add listeners to save the input field content whenever it changes
        _inputQuizNum.onValueChanged.AddListener(SaveInputQuizNumContent);
        Target_X.onValueChanged.AddListener(SaveInputTargetXContent);
        Target_Y.onValueChanged.AddListener(SaveInputTargetYContent);
    }

    void SaveInputQuizNumContent(string input)
    {
        // Save the input field content to PlayerPrefs
        PlayerPrefs.SetString(InputQuizNumKey, input);
        PlayerPrefs.Save();
    }

    void SaveInputTargetXContent(string input)
    {
        PlayerPrefs.SetString(InputTargetXKey, input);
        PlayerPrefs.Save();
    }

    void SaveInputTargetYContent(string input)
    {
        PlayerPrefs.SetString(InputTargetYKey, input);
        PlayerPrefs.Save();
    }


}

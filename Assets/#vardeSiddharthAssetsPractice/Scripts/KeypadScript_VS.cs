using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadScript_VS : MonoBehaviour
{
    [SerializeField]
    string password;
    [SerializeField]
    Text inputText;

    string playerInput;

    public void TakeInput(int number)
    {
        playerInput += number;
        inputText.text = playerInput;
    }

    public void OnEnterPressed()
    {
        if(password == playerInput)
        {
            Debug.Log("Password Is Correct");
        }
        else
        {
            playerInput = "";
            inputText.text = "Wrong Password";
        }
    }
}

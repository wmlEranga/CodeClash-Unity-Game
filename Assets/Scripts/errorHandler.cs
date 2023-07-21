using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class errorHandler : MonoBehaviour
{
    public TextMeshProUGUI errorTXT;

    public static errorHandler Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void moveError()
    {
        //Debug.Log(str);
        Debug.Log("wrong input");
        errorTXT.text = "wrong input";


    }

    public void conditionalError()
    {
        Debug.Log("wrong input");
        errorTXT.text = "wrong input";
    }
}

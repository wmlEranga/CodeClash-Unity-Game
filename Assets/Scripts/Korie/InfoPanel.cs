using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{

    public GameObject infoPanel;
    // Start is called before the first frame update
    void Start()
    {
        infoPanel.SetActive(false);
    }

    public void infoIn()
    {
        infoPanel.SetActive(true);
    }

    public void infoOut()
    {
        infoPanel.SetActive(false);
    }
}

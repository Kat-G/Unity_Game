using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject panel;
    public GameObject startpanel;

    public void Open()
    {   
        startpanel.SetActive(false);
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
        startpanel.SetActive(true);
    }
}
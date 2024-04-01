using System;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameObject mapPanel; 
    private bool isPanelActive = false;
    void Start()
    {
        
    }

    void Update()
    {
        // M tuþuna basýldýðýnda Map Panel'ý aç/kapat.
        if (Input.GetKeyDown(KeyCode.N))
        {
            ToggleMapPanel(!isPanelActive);
        }
    }

    void ToggleMapPanel(bool activate)
    {
        isPanelActive = activate;
        mapPanel.SetActive(isPanelActive);
    }


}

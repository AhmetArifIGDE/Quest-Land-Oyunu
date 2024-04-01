using System;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject mapPanel; 
    private bool isPanelActive = false;
    void Start()
    {
        
    }

    void Update()
    {
        // M tu�una bas�ld���nda Map Panel'� a�/kapat.
        if (Input.GetKeyDown(KeyCode.M))
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

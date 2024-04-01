using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuKontrol : MonoBehaviour
{
   public void OynaButton()
    {
        SceneManager.LoadScene(1);
    }
    public void HikayeButton()
    {
        SceneManager.LoadScene(2);
    }
    public void CikButton()
    {
        Application.Quit();
    }
}

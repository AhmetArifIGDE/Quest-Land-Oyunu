using StarterAssets;
using UnityEngine;

public class HazineEtkilesim : MonoBehaviour
{
    public GameObject hazinePanel; // Unity Inspector'dan Panel'ý atayýn.
    public GameObject playerController; // Unity Inspector'dan oyuncu kontrol nesnesini atayýn.
    //public ParticleSystem patlamaEfekti;
    private bool isInside = false; // Oyuncu HazineEtkilesim içinde mi?
    private bool isPanelActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true; // Oyuncu içeri girdi.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false; // Oyuncu çýktý.

        }
    }

    public void TogglePanel(bool activate)
    {
        isPanelActive = activate;
        hazinePanel.SetActive(isPanelActive);

        if (!isPanelActive)
        {
            // Panel kapatýldýðýnda kontrolleri geri ver
            playerController.GetComponent<FirstPersonController>().enabled = true;

            // Mouse'un görünürlüðünü kontrol et
            Cursor.visible = false; // imleci görünmez yap
            Cursor.lockState = CursorLockMode.Locked; // imleci ekranda kilitli yap
        }
        else
        {
            // Panel açýldýðýnda kontrolleri kapat
            playerController.GetComponent<FirstPersonController>().enabled = false;

            // Mouse'un görünürlüðünü kontrol et
            Cursor.visible = true; // imleci görünür yap
            Cursor.lockState = CursorLockMode.None; // imleci ekranda kilitlenmemiþ yap
        }
    }

    void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E))
        {
            // E tuþuna basýldýðýnda Panel'ý aç/kapat.
            TogglePanel(!isPanelActive);
        }


    }

}

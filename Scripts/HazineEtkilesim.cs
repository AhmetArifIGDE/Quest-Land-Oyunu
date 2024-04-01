using StarterAssets;
using UnityEngine;

public class HazineEtkilesim : MonoBehaviour
{
    public GameObject hazinePanel; // Unity Inspector'dan Panel'� atay�n.
    public GameObject playerController; // Unity Inspector'dan oyuncu kontrol nesnesini atay�n.
    //public ParticleSystem patlamaEfekti;
    private bool isInside = false; // Oyuncu HazineEtkilesim i�inde mi?
    private bool isPanelActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true; // Oyuncu i�eri girdi.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false; // Oyuncu ��kt�.

        }
    }

    public void TogglePanel(bool activate)
    {
        isPanelActive = activate;
        hazinePanel.SetActive(isPanelActive);

        if (!isPanelActive)
        {
            // Panel kapat�ld���nda kontrolleri geri ver
            playerController.GetComponent<FirstPersonController>().enabled = true;

            // Mouse'un g�r�n�rl���n� kontrol et
            Cursor.visible = false; // imleci g�r�nmez yap
            Cursor.lockState = CursorLockMode.Locked; // imleci ekranda kilitli yap
        }
        else
        {
            // Panel a��ld���nda kontrolleri kapat
            playerController.GetComponent<FirstPersonController>().enabled = false;

            // Mouse'un g�r�n�rl���n� kontrol et
            Cursor.visible = true; // imleci g�r�n�r yap
            Cursor.lockState = CursorLockMode.None; // imleci ekranda kilitlenmemi� yap
        }
    }

    void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E))
        {
            // E tu�una bas�ld���nda Panel'� a�/kapat.
            TogglePanel(!isPanelActive);
        }


    }

}

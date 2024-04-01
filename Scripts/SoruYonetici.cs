using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoruYonetici : MonoBehaviour
{
    public TextMeshProUGUI soruText;   
    public GameObject hazinePanel;  
    public GameObject playerController;
    public GameObject DogruSayisiSayac;
    public GameObject TebrikMesajPanel;
    private int dogruCevapSayisi;
    public List<Button> cevapButonList;
    private List<Soru> soruDataSet = new List<Soru>();
    public List<BoxCollider> boxColliders;
    
    public GameObject[] patlamaEfekti;
    public GameObject kirmiziPanel;
    public AudioSource yanlisSes;
    
    public float kirmiziEkranSure = 1f;
    public float efektSure = 2.3f;

    void Start()
    {

        SoruSetiniYukle();
        YeniSoruGoster();
        // Yanl�� sesin varsay�lan durumu kapal� olsun
        yanlisSes.Stop();
        
            // AudioSource bile�enini al
            yanlisSes = GetComponent<AudioSource>();
            
            
            

        
    }
 
    
    void SoruSetiniYukle()
    {
        // JSON dosyas�n� oku ve i�eri�i al
        TextAsset sorularJSON = Resources.Load<TextAsset>("sorular");
        string jsonText = sorularJSON.text;

        // JSON'u deserializasyon ile parse et
        SoruVeriSeti soruVeriSeti = JsonUtility.FromJson<SoruVeriSeti>(jsonText);

        // Sorular listesini y�kle
        soruDataSet = soruVeriSeti.sorular;
    }

    void YeniSoruGoster()
    {
        if (soruText == null || cevapButonList == null)
        {
            Debug.LogError("soruText veya cevapButonList null. Uygulamay� kontrol edin.");
            return;
        }

        if (soruDataSet.Count > 0)
        {
            int rastgeleSoruIndex = Random.Range(0, soruDataSet.Count);
            Soru aktifSoru = soruDataSet[rastgeleSoruIndex];
            soruText.text = aktifSoru.soruMetni;

            for (int i = 0; i < aktifSoru.cevaplar.Count && i < cevapButonList.Count; i++)
            {
                cevapButonList[i].GetComponentInChildren<TextMeshProUGUI>().text = aktifSoru.cevaplar[i];
                int cevapIndex = i;

                cevapButonList[i].onClick.RemoveAllListeners();
                cevapButonList[i].onClick.AddListener(() => CevapKontrol(cevapIndex, aktifSoru));
                
                
            }

            soruDataSet.RemoveAt(rastgeleSoruIndex);
        }
        else
        {
            Debug.Log("Soru bitti!");
        }
    }

    void CevapKontrol(int secilenCevapIndex, Soru soru)
    {


        Debug.Log("Se�ilen Cevap Index: " + secilenCevapIndex);

        if (soru.cevaplar[secilenCevapIndex] == soru.dogruCevap)
        {


            
            dogruCevapSayisi++; // Do�ru cevap al�nd���nda saya� artt�r�l�r
            Debug.Log("Do�ru Cevap Say�s�: " + dogruCevapSayisi);

            // DogruSayisiSayac nesnesinin Text veya ba�ka bir bile�eni �zerinden ekrana yans�tma i�lemini yap�n.
            DogruSayisiSayac.GetComponent<TextMeshProUGUI>().text = dogruCevapSayisi.ToString() + "/3";

            playerController.GetComponent<FirstPersonController>().enabled = true;

            // Mouse'un g�r�n�rl���n� kontrol et
            Cursor.visible = false; // imleci g�r�nmez yap
            Cursor.lockState = CursorLockMode.Locked; // imleci ekranda kilitli yap

            if (dogruCevapSayisi >= 3)
            {
                // Saya� 3'e ula�t���nda TebrikMesajPanel'i etkinle�tir
                if (TebrikMesajPanel != null)
                {
                    TebrikMesajPanel.SetActive(true);
                    
                }
                else
                {
                    Debug.LogError("TebrikMesajPanel not assigned!");
                }
            }

            

            // Hazine etkile�im objesini inaktif et
            hazinePanel.SetActive(false);
            
            //patlamaEfekti.SetActive(true) ;

            foreach (var item in patlamaEfekti)
            {
                item.SetActive(true);

            }
            // Do�ru cevap al�nd���nda efekti ba�lat
            //patlamaEfekti.SetActive(true);
            
            // Belirtilen s�re sonra efekti durdur
            StartCoroutine(OtomatikEfektDurdur());


        }
        else
        {
            StartCoroutine(KirmiziEkranEfekti());
            


        }
        IEnumerator KirmiziEkranEfekti()
        {
            yanlisSes.Play();
            // K�rm�z� paneli aktif et
            kirmiziPanel.SetActive(true);

            // Belirtilen s�re boyunca beklet
            yield return new WaitForSeconds(kirmiziEkranSure);

            // K�rm�z� paneli devre d��� b�rak
            kirmiziPanel.SetActive(false);
             
        }
        IEnumerator OtomatikEfektDurdur()
        {
            // Efektin belirli bir s�re aktif olmas�n� beklet
            yield return new WaitForSeconds(efektSure);
            foreach (var item in patlamaEfekti)
            {
                item.SetActive(false);
            }
            // Belirtilen s�re sonra efekti durdur
            //patlamaEfekti.SetActive(false);
            Debug.Log("3saniye sonra durdu");
        }

        

        // �lerleme ve yeni soruyu g�sterme
        YeniSoruGoster();
    }
    

}

[System.Serializable]
public class SoruVeriSeti
{
    public List<Soru> sorular;
}

[System.Serializable]
public class Soru
{
    public string soruMetni;
    public List<string> cevaplar;
    public string dogruCevap;
    // Di�er gerekli alanlar� buraya ekleyebilirsiniz
}

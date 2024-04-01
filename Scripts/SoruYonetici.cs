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
        // Yanlýþ sesin varsayýlan durumu kapalý olsun
        yanlisSes.Stop();
        
            // AudioSource bileþenini al
            yanlisSes = GetComponent<AudioSource>();
            
            
            

        
    }
 
    
    void SoruSetiniYukle()
    {
        // JSON dosyasýný oku ve içeriði al
        TextAsset sorularJSON = Resources.Load<TextAsset>("sorular");
        string jsonText = sorularJSON.text;

        // JSON'u deserializasyon ile parse et
        SoruVeriSeti soruVeriSeti = JsonUtility.FromJson<SoruVeriSeti>(jsonText);

        // Sorular listesini yükle
        soruDataSet = soruVeriSeti.sorular;
    }

    void YeniSoruGoster()
    {
        if (soruText == null || cevapButonList == null)
        {
            Debug.LogError("soruText veya cevapButonList null. Uygulamayý kontrol edin.");
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


        Debug.Log("Seçilen Cevap Index: " + secilenCevapIndex);

        if (soru.cevaplar[secilenCevapIndex] == soru.dogruCevap)
        {


            
            dogruCevapSayisi++; // Doðru cevap alýndýðýnda sayaç arttýrýlýr
            Debug.Log("Doðru Cevap Sayýsý: " + dogruCevapSayisi);

            // DogruSayisiSayac nesnesinin Text veya baþka bir bileþeni üzerinden ekrana yansýtma iþlemini yapýn.
            DogruSayisiSayac.GetComponent<TextMeshProUGUI>().text = dogruCevapSayisi.ToString() + "/3";

            playerController.GetComponent<FirstPersonController>().enabled = true;

            // Mouse'un görünürlüðünü kontrol et
            Cursor.visible = false; // imleci görünmez yap
            Cursor.lockState = CursorLockMode.Locked; // imleci ekranda kilitli yap

            if (dogruCevapSayisi >= 3)
            {
                // Sayaç 3'e ulaþtýðýnda TebrikMesajPanel'i etkinleþtir
                if (TebrikMesajPanel != null)
                {
                    TebrikMesajPanel.SetActive(true);
                    
                }
                else
                {
                    Debug.LogError("TebrikMesajPanel not assigned!");
                }
            }

            

            // Hazine etkileþim objesini inaktif et
            hazinePanel.SetActive(false);
            
            //patlamaEfekti.SetActive(true) ;

            foreach (var item in patlamaEfekti)
            {
                item.SetActive(true);

            }
            // Doðru cevap alýndýðýnda efekti baþlat
            //patlamaEfekti.SetActive(true);
            
            // Belirtilen süre sonra efekti durdur
            StartCoroutine(OtomatikEfektDurdur());


        }
        else
        {
            StartCoroutine(KirmiziEkranEfekti());
            


        }
        IEnumerator KirmiziEkranEfekti()
        {
            yanlisSes.Play();
            // Kýrmýzý paneli aktif et
            kirmiziPanel.SetActive(true);

            // Belirtilen süre boyunca beklet
            yield return new WaitForSeconds(kirmiziEkranSure);

            // Kýrmýzý paneli devre dýþý býrak
            kirmiziPanel.SetActive(false);
             
        }
        IEnumerator OtomatikEfektDurdur()
        {
            // Efektin belirli bir süre aktif olmasýný beklet
            yield return new WaitForSeconds(efektSure);
            foreach (var item in patlamaEfekti)
            {
                item.SetActive(false);
            }
            // Belirtilen süre sonra efekti durdur
            //patlamaEfekti.SetActive(false);
            Debug.Log("3saniye sonra durdu");
        }

        

        // Ýlerleme ve yeni soruyu gösterme
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
    // Diðer gerekli alanlarý buraya ekleyebilirsiniz
}

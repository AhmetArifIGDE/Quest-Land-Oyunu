using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class DialogMenager : MonoBehaviour
{
    [SerializeField] private TMP_Text diyalogText;
    [SerializeField] private string[] cumleler;
    [SerializeField] private float yazmaHizi;
    [SerializeField] private GameObject devambutonu;
    [SerializeField] private GameObject oyunuBaslat;
    private int index;
    private void Start()
    {
        StartCoroutine(Yaz());
    }
    private void Update()
    {
        if(diyalogText.text == cumleler[index])
        {
            devambutonu.SetActive(true);
        }
        else
        {
            devambutonu.SetActive(false);
        }
    }
    IEnumerator Yaz()
    {
        foreach(char harf in cumleler[index].ToCharArray())
        {
            diyalogText.text += harf;
            yield return new WaitForSeconds(yazmaHizi);
        }
    }
    public void DevamEt()
    {
        if(index < cumleler.Length - 1)
        {
            index++;
            diyalogText.text = "";
            StartCoroutine(Yaz());
        }
        else
        {
            diyalogText.text = "";
            oyunuBaslat.SetActive(true);

        }
    }
    public void OyunuBaslat()
    {
        SceneManager.LoadScene("map2");
    }
}

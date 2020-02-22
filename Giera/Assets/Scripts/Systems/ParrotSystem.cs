using UnityEngine;
using TMPro;

public class ParrotSystem : MonoBehaviour
{
    public float minTimeBetweenKraa, maxTimeBetweenKraa;
    public float kraaTime;
    public string[] kraas;
    public GameObject worldCanvas;//tekst musi byc 1. dzieckiem canvas
    private TextMeshProUGUI kraaText;
    private float kraaTimer;
    private bool isKraaing = false;

    void Awake()
    {
        worldCanvas.SetActive(false);
        kraaText = worldCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void DisplayKraa(int index)
    {
        isKraaing = true;
        kraaText.SetText(kraas[index]);
        worldCanvas.SetActive(true);
        
    }

    private void StopKraa()
    {
        isKraaing = false;
        worldCanvas.SetActive(false);
        kraaTimer = kraaTime;
    }

    private void GenerateTime()
    {
        kraaTimer = Random.Range(minTimeBetweenKraa, maxTimeBetweenKraa);
    }

    void Update()
    {
        if(kraaTimer<0)
        {
            if (isKraaing) StopKraa();
            else DisplayKraa(Random.Range(0, kraas.Length));
        }
        else
        {
            kraaTimer -= Time.deltaTime;
        }
    }
}

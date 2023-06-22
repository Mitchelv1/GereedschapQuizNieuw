using Newtonsoft.Json.Linq;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NormalVraag : MonoBehaviour
{
    /*    public TextMeshProUGUI GoedTxt;
        public TextMeshProUGUI FoutTxt;*/
    /*    public Text TestTxt;*/
/*    public TextMeshProUGUI TestTxt;*/
    public TextMeshProUGUI VraagTxt;
    public TextMeshProUGUI AntwoordATxt;
    public TextMeshProUGUI AntwoordBTxt;
    public TextMeshProUGUI AntwoordCTxt;
    public TextMeshProUGUI AntwoordDTxt;
    public GameObject Antwoord_A;
    public GameObject Antwoord_B;
    public GameObject Antwoord_C;
    public GameObject Antwoord_D;
/*    public GameObject CanvasPopup;*/
/*    public GameObject VerderBtn;*/
    public GameObject Volgende;
    public GameObject Vorige;
    public GameObject Inleveren;

    public static string Type;
    public static int vraagCountString;
    public int Terug;

    public static int Goed = StateNameController.Goed;
    public static int Fout = StateNameController.Fout;
/*    public static int vraagCount;*/
    private void Start()
    {
        CheckVraag();
    }
    public void VraagReset()
    {
        if (StateNameController.vraagCount == 1)
        {
            Vorige.SetActive(false);
        }
        else
        {
            Vorige.SetActive(true);
        }
        Volgende.SetActive(false);
        Inleveren.SetActive(false);
/*        CanvasPopup.SetActive(false);*/
/*        GoedTxt.gameObject.SetActive(false);
        FoutTxt.gameObject.SetActive(false);*/
        Antwoord_A.GetComponent<Button>().enabled = true;
        Antwoord_B.GetComponent<Button>().enabled = true;
        Antwoord_C.GetComponent<Button>().enabled = true;
        Antwoord_D.GetComponent<Button>().enabled = true;
        Antwoord_A.GetComponent<Image>().color = new Color32(61, 126, 219, 255);
        Antwoord_B.GetComponent<Image>().color = new Color32(61, 126, 219, 255);
        Antwoord_C.GetComponent<Image>().color = new Color32(61, 126, 219, 255);
        Antwoord_D.GetComponent<Image>().color = new Color32(61, 126, 219, 255);
    }
    public void CheckVraag()
    {
        VraagReset();
        string filePath = Path.Combine("Assets", "vragen.txt");
        string vragenTxt = File.ReadAllText(filePath);
        JObject vragenJson = JObject.Parse(vragenTxt);
        vraagCountString = StateNameController.vraagCount + 1;
        Type = (string)vragenJson["vragen"][vraagCountString.ToString()]["type"];
/*        TestTxt.text = vraagCount.ToString();
*/        switch (Type)
        {
            case "normal":
                StateNameController.vraagCount++;
                Normaal();
/*                TestTxt.text = StateNameController.vraagCount.ToString();*/
                break;

            case "image":
                SceneManager.LoadScene(2);
                break;
        }
    }

    public void VorigeVraag()
    {
        VraagReset();
        if (StateNameController.laatsteVraag == true)
        {
            StateNameController.laatsteVraag = false;
        }
        string filePath = Path.Combine("Assets", "vragen.txt");
        string vragenTxt = File.ReadAllText(filePath);
        JObject vragenJson = JObject.Parse(vragenTxt);
        vraagCountString = StateNameController.vraagCount - 1;
        Terug = StateNameController.vraagCount - 2;
        Type = (string)vragenJson["vragen"][vraagCountString.ToString()]["type"];
        /*        TestTxt.text = vraagCount.ToString();
        */
        switch (Type)
        {
            case "normal":
                StateNameController.vraagCount--;
                Normaal();
                /*                TestTxt.text = StateNameController.vraagCount.ToString();*/
                break;

            case "image":
                StateNameController.vraagCount = Terug;
                SceneManager.LoadScene(2);
                break;
        }
    }

    public void Normaal()
    {
        VraagReset();
        string filePath = Path.Combine("Assets", "vragen.txt");
        string vragenTxt = File.ReadAllText(filePath);
        JObject vragenJson = JObject.Parse(vragenTxt);

        /*int vraagCountString = vraagCount;*/
        Type = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()]["type"];
        string[] antwoordenJson = new string[4];
        string vraagJson = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()]["vraag"];
        for (int i = 0; i < 4; i++)
        {
            antwoordenJson[i] = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()][$"antwoord_{i + 1}"];
        }
        AntwoordATxt.text = antwoordenJson[0];
        AntwoordBTxt.text = antwoordenJson[1];
        AntwoordCTxt.text = antwoordenJson[2];
        AntwoordDTxt.text = antwoordenJson[3];
        VraagTxt.text = vraagJson;
/*        StateNameController.vraagCount = vraagCount;*/
    }
}
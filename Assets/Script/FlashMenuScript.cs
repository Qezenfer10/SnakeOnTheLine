using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    int time = 0;

    public GameObject LightOnText;
    void Start()
    {
        LightOnText.SetActive(true);
        time = 0;
        StartCoroutine(LightMenu(0.5f));
    }

    public void OnStart()
    {
        LightOnText.SetActive(true);
        time = 0;
        StartCoroutine(LightMenu(0.5f));
    }

    public IEnumerator LightMenu(float time)
    {
        Debug.Log("isiq on");
        if(GameManager.instance.lightOn) GameManager.instance.flashLight.SetActive(true);
        else GameManager.instance.flashLight.SetActive(false);

        yield return new WaitForSeconds(time);

        if (this.time >= 3)
        {
                PhoneScript.instance.ApplyMode(Application.AppMenu);
        }
        else
        {

            this.time++;
            LightOnText.SetActive(!LightOnText.activeSelf);
            LightOnText.GetComponent<TextMeshProUGUI>().text = "LIGHT " + (GameManager.instance.lightOn ? "ON" : "OFF");
            StartCoroutine(LightMenu(time));
        }
    }
}

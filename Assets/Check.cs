using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    [SerializeField] GameObject Phone;
    public GameObject canvas;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log(Phone.activeSelf + " Alindi " +  collision.gameObject);
            Destroy(gameObject);
            Destroy(canvas);
            Phone.SetActive(true);
            GameManager.instance.isPhone = true;
            GameManager.instance.ActivePhone();
        }
    }
}

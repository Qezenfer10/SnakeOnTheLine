using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSnakeGrowing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.tag == "Apple")
        {
            Debug.Log(other.name);

            SnakeScript.instance.SnakeGrowing();

            //GameManager.instance.goo.Remove(gameObject);

            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log(other.name);

            SnakeScript.instance.SnakeGrowing();
            //GameManager.instance.goo.Remove(gameObject);

            Destroy(gameObject);
        }
    }
}

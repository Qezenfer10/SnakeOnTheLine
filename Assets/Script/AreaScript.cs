using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScript : MonoBehaviour
{
    public Camera areaCamera;


    public Camera SetMainCamera()
    {
        GameManager.instance.SetMainCamera(areaCamera);

        return areaCamera;
    }
}

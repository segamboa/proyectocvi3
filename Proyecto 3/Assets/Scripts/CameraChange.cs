using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public Camera[] cameras;

    public void camMainMove(){
        cameras[0].enabled = true;
        cameras[1].enabled = false;
    }

    public void camTwoMain(){
        cameras[0].enabled = false;
        cameras[1].enabled = true;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.J)){
            camMainMove();
        }
        if(Input.GetKeyDown(KeyCode.K)){
            camTwoMain();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{

    private float min;
    private float grados;
    public float timeSpeed = 300;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        min += timeSpeed * Time.deltaTime;
        if(min>= 1440){
            min=0;
        }

        grados = min/4;
        this.transform.localEulerAngles = new Vector3(grados, -90f, 0f);
    }
}

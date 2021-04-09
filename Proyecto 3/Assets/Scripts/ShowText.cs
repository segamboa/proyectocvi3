using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{

    public  string textValue;
    public Text textElement;
    bool activeF = false;

    // Start is called before the first frame update
    void Start()
    {
        textElement.gameObject.SetActive(false);
    }

    // Update is called once per frame
    IEnumerator waiter()
    {
            if(activeF==true){
            textValue = "Flat shading enabled";
            }else{
            textValue = "Flat shading disabled";
            }
            textElement.text = textValue;
            textElement.gameObject.SetActive(true);
            yield return new WaitForSeconds(3.0f);
            textElement.gameObject.SetActive(false);

    }

    IEnumerator waiter1(){
            textValue = "New terrain generated";
            textElement.text = textValue;
            textElement.gameObject.SetActive(true);
            yield return new WaitForSeconds(3.0f);
            textElement.gameObject.SetActive(false);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.F)){
            activeF = !activeF;
        StartCoroutine(waiter());
        }
        if(Input.GetKeyDown(KeyCode.G)){
        StartCoroutine(waiter1());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 7.0f;
    public float lookSpeed = 90.0f;
    public float jumpForce = 5.0f;
    private Camera cam = null;
    private Rigidbody rb = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.angularDrag = 10.0f;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        cam = this.transform.GetComponentInChildren<Camera>();
        cam.transform.position = this.transform.position;
        cam.transform.rotation = this.transform.rotation;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        this.transform.Translate(Vector3.right * horz * moveSpeed * Time.deltaTime);      
        this.transform.Translate(Vector3.forward * vert * moveSpeed * Time.deltaTime);

        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");
        this.transform.localRotation *= Quaternion.AngleAxis(mousex * lookSpeed * Time.deltaTime, Vector3.up);
        this.transform.localRotation *= Quaternion.AngleAxis(mousey * lookSpeed * Time.deltaTime, Vector3.left);

        if(Input.GetButtonDown("Jump") == true){
            rb.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

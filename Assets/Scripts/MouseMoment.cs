using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoment : MonoBehaviour
{
    // Start is called before the first frame update
    float xRotation;
    float yRotation;
    [SerializeField]
    Transform RootPlayer;
    void Start()
    {
        
    }

    void MouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime*100f;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime*100f;
        
        xRotation -= mouseY;
        yRotation += mouseX; 
        xRotation = Mathf.Clamp(xRotation, -90f, 60f);
        RootPlayer.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }
    void Update()
    {
        MouseMovement();
    }

    private void LateUpdate()
    {
        transform.position=RootPlayer.transform.position+new Vector3(0f, 0.5f, 0f);
      
    }
}

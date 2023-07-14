using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    Vector3 currentRotation, targetRotation, targetposition,currentPosition, initialGunPosition;
    public Transform cam;

    [SerializeField] float recoilX;
    [SerializeField] float recoilY;
    [SerializeField] float recoilZ;

    [SerializeField] float kickBackZ;
    public Vector3 I_tareget { get; private set; }

    public float snapiness, returnAmount;
    void Start()
    {
        initialGunPosition = transform.localPosition;
      
    }

    // Update is called once per frame
    void Update()
    {
        I_tareget = targetRotation;
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * returnAmount);
        currentRotation=Vector3.Slerp(currentRotation,targetRotation,Time.deltaTime * snapiness);
        back();
    }

    public void recoil()
    {
        targetposition -= new Vector3(0,0,kickBackZ);
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
    void back()
    {
        targetposition = Vector3.Lerp(targetposition,initialGunPosition, Time.deltaTime * returnAmount);
        currentPosition = Vector3.Slerp(currentPosition,targetposition, Time.fixedDeltaTime * snapiness);
        transform.localPosition = currentPosition;
    }
}

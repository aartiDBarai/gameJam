using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachOnCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D otherRigidbody;
    public Joint joint;
    private Transform parent;
    void Start()
    {
        parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Do not execute the collision logic if spacebar is pressed
            return;
        }
        if (collision.gameObject.GetComponent<Rigidbody2D>() == otherRigidbody) {
            // Attach the two GameObjects
            
        }
        //transform.parent = collision.gameObject.transform;
        // Get the other object
        GameObject otherObject = collision.gameObject;

        //Debug.Log("other obj " + otherObject.name);
        //Debug.Log(transform.childCount);

        Vector3 pos = transform.localPosition;
        if(transform.childCount == 1)
        {
            //Debug.Log("destroy " + transform.GetChild(0).gameObject.name);
            Destroy(transform.GetChild(0).gameObject);
            
        }

        //Vector3 temp = transform.position;
        // Attach the other object to this object
        otherObject.transform.parent = transform;

        //transform.position = temp;
        
        
        
        
        
        //Vector3 eulerRotation = otherObject.transform.rotation.eulerAngles;
        //otherObject.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
        
        
        //transform.localScale = otherObject.transform.localScale;
        //transform.localRotation = otherObject.transform.localRotation;
        
        //transform.localPosition = parent.localPosition;
        //transform.localRotation = parent.localRotation;
        
        //otherObject.transform.localScale = otherObject.transform.localScale;
        //otherObject.transform.localRotation = otherObject.transform.localRotation;
        
        // Attach the other object to this object using a Joint component
        //joint.connectedBody(otherObject, );
        Debug.Log("collide");
    }
}

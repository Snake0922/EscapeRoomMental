using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearsCollision : MonoBehaviour
{
    public enum CollisionFor { nextGear,previousGear}
    public CollisionFor collisionFor;
    private EngranajeController engranajeControllerMyparent;

    private void Start()
    {
        engranajeControllerMyparent = GetComponentInParent<EngranajeController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EngranajeController>()!=null)
        {
            if(collisionFor==CollisionFor.nextGear)
            {
                engranajeControllerMyparent.nextGear = other.gameObject;
            }
            else
            {
                engranajeControllerMyparent.previousGear = other.gameObject;
            }
        }
        if(other.gameObject.CompareTag("Null"))
        {
            if (collisionFor == CollisionFor.nextGear)
            {
                engranajeControllerMyparent.nextGear = null;
            }
            else
            {
                engranajeControllerMyparent.previousGear = null;
            }
        }
    }
}

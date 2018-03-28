using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNode : MonoBehaviour
{
    public OilRig whoSend;
    RaycastHit hit;
    private void Update()
    {
        if(Physics.Raycast(new Ray(transform.position+Vector3.back,Vector3.forward),out hit))
        {
            if(hit.collider.CompareTag("Oil"))
            {
                whoSend.setOilWorking(true, hit.collider.gameObject.GetComponent<Oil>());
            }
        }
    }
}

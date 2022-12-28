using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot : MonoBehaviour
{
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask targetLayeR;

    [SerializeField] private Transform originTransform;

    private void Update()
    {
        Vector3 fwd = -originTransform.forward;

        if (Physics.Raycast(originTransform.position,fwd,raycastDistance,targetLayeR))
        {
            Debug.Log("Hedef Algýlandý");
        }
            
    }

    private void OnDrawGizmos()
    {
        

        Gizmos.color = Color.blue;
        Vector3 direction = -originTransform.forward * raycastDistance;
        Gizmos.DrawRay(transform.position, direction);

    }
}

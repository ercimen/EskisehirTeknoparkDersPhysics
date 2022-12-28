using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoTargetWithCollider : MonoBehaviour
{
    [SerializeField] private Transform rotationObject;
    [SerializeField] private float rotateTime;

    private void OnTriggerEnter(Collider other)
    {
        // Daha maliyetsiz ve hýzlý çalýþan eriþme yöntemi.
        if (other.TryGetComponent(out IHitable hitobject))
        {
         //   _transform.LookAt(other.transform.position); Anýnda o pozisyona bakar.
            rotationObject.transform.DOLookAt(other.transform.position, rotateTime); // belirttiðimiz sürede rotate olarak bakar.
        }

    }
}

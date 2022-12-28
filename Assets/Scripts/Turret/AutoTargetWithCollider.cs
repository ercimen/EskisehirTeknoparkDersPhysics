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
        // Daha maliyetsiz ve h�zl� �al��an eri�me y�ntemi.
        if (other.TryGetComponent(out IHitable hitobject))
        {
         //   _transform.LookAt(other.transform.position); An�nda o pozisyona bakar.
            rotationObject.transform.DOLookAt(other.transform.position, rotateTime); // belirtti�imiz s�rede rotate olarak bakar.
        }

    }
}

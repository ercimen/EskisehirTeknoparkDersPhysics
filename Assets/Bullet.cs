using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.AddForce(transform.forward * 1000f, ForceMode.Acceleration);

        // Delay ile method �a��rma.
        Invoke(nameof(ReleaseMe), 1f);
        }

    private void ReleaseMe()
    {
        /// instance = bulletpool class field� oldugu i�in i�indeki public methodlar� kullanabiliyoruz.
        BulletPool.instance.ReleaseBullet(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
      
        // karsi taraf�n getcompenentine eri�me y�ntemlerinden biri
        /*
        IHitable hitobject = other.GetComponent<IHitable>();

        if (hitobject!=null)
        {
            hitobject.GetHit();
        }
        */
        // Daha maliyetsiz ve h�zl� �al��an eri�me y�ntemi.
        if (other.TryGetComponent(out IHitable hitobject2))
        {
            hitobject2.GetHit();
        }
    }

}

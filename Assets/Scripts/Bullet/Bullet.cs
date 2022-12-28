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

        // Delay ile method çaðýrma.
        Invoke(nameof(ReleaseMe), 1f);
        }

    private void ReleaseMe()
    {
        /// instance = bulletpool class fieldý oldugu için içindeki public methodlarý kullanabiliyoruz.
        BulletPool.instance.ReleaseBullet(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
      
        // karsi tarafýn getcompenentine eriþme yöntemlerinden biri
        /*
        IHitable hitobject = other.GetComponent<IHitable>();

        if (hitobject!=null)
        {
            hitobject.GetHit();
        }
        */
        // Daha maliyetsiz ve hýzlý çalýþan eriþme yöntemi.
        if (other.TryGetComponent(out IHitable hitobject2))
        {
            hitobject2.GetHit();
        }
    }

}

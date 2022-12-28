using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawner;
    [SerializeField] private float spawnDelay;

    private WaitForSeconds _delay; // Coroutine garbage oluþturmasýn diye çünkü newleyerek çaðýrýyoruz. O yüzden cashledik.
    private void Start()
    {
        _delay = new WaitForSeconds(spawnDelay); // Sadece bir kere newleyerek coroutine içinde sonsuz kez newlemekten kurtulduk.
        StartCoroutine(BulletSpawnLoop());
    }


    /// <summary>
    /// Bu yöntem performans açýsýndan oldukça masraflý bir yöntemdir. O yüzden Object Pool kullanacaðýz.
    /// </summary>
    /// <returns></returns>
    IEnumerator BulletSpawnLoop()
    {

        while (true) // Sonsuz bir döngü açar.
        {
            var position = spawner.position;
            var rotation = spawner.rotation;
            Instantiate(bulletPrefab, position, rotation);
            yield return _delay;
          //  yield return new WaitForSeconds(0.2f); // Garbage oluþturur. Sürekli newleyerek bellek kullanýmýný arttýrýr.
            
        }
    }

}

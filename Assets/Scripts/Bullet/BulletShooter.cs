using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawner;
    [SerializeField] private float spawnDelay;

    private WaitForSeconds _delay; // Coroutine garbage olu�turmas�n diye ��nk� newleyerek �a��r�yoruz. O y�zden cashledik.
    private void Start()
    {
        _delay = new WaitForSeconds(spawnDelay); // Sadece bir kere newleyerek coroutine i�inde sonsuz kez newlemekten kurtulduk.
        StartCoroutine(BulletSpawnLoop());
    }


    /// <summary>
    /// Bu y�ntem performans a��s�ndan olduk�a masrafl� bir y�ntemdir. O y�zden Object Pool kullanaca��z.
    /// </summary>
    /// <returns></returns>
    IEnumerator BulletSpawnLoop()
    {

        while (true) // Sonsuz bir d�ng� a�ar.
        {
            var position = spawner.position;
            var rotation = spawner.rotation;
            Instantiate(bulletPrefab, position, rotation);
            yield return _delay;
          //  yield return new WaitForSeconds(0.2f); // Garbage olu�turur. S�rekli newleyerek bellek kullan�m�n� artt�r�r.
            
        }
    }

}

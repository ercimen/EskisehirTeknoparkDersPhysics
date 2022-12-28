using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    /// <summary>
    /// Singleton Pattern :
    /// bulletPool Static tan�mlad�m. ismini instance olarak belirledim. 
    /// </summary>
    public static BulletPool instance; 

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletCount;

    [SerializeField] private Transform spawner;
    [SerializeField] private float spawnDelay;

    private WaitForSeconds _delay;

    private List<GameObject> _bulletList = new(); // Pool Listesi

    private int _currentBulletNumber;
    private void Awake()
    {
        instance = this;

        _delay = new WaitForSeconds(spawnDelay); // Sadece bir kere newleyerek coroutine i�inde sonsuz kez newlemekten kurtulduk.
        InitBulletList(); // Awake olurken listemizi olu�turuyoruz. Art�k instantiate yapmak yerine listeden eri�im sa�layaca��z.
    }

    private void Start()
    {
        StartCoroutine(BulletSpawnLoop());
    }

    private void InitBulletList()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            _bulletList.Add(bullet);

        }
    }

    private void GetBullet()
    {
        Vector3 position = spawner.position;
        Quaternion rotation = spawner.rotation;

        _bulletList[_currentBulletNumber].transform.position = position;
        _bulletList[_currentBulletNumber].transform.rotation = rotation;

        _bulletList[_currentBulletNumber].SetActive(true);
        
        ControlBulletNumber();

    }

    private void ControlBulletNumber()
    {
        _currentBulletNumber++;

        if (_currentBulletNumber>=_bulletList.Count)
        {
           // SetDisableAllBullets(); �ptal ettik.
            _currentBulletNumber = 0;
        }
    }


    /// <summary>
    /// Tekrardan bulletlar� set edebilmek ad�na Disable etmeyi denedik ama g�rsel bug olu�turdu�u i�in kullanm�yoruz.
    /// </summary>
    private void SetDisableAllBullets()
    {
        for (int i = 0; i < _bulletList.Count; i++)
        {
            _bulletList[i].SetActive(false);
        }
    }



    /// <summary>
    /// 1. hatam�z : Listeyi olu�turmadan Liste elemanlar�na eri�meye �al��t�k. 
    /// Bu y�zden Corotini startta ba�latarak problemi ��zd�k.
    /// 2. hatam�z : currentBulletNumber' listenin d���na ��k�yor kontrol�n� yapmal�y�z.
    /// </summary>
    IEnumerator BulletSpawnLoop()
    {

        while (true) // Sonsuz bir d�ng� a�ar.
        {
            GetBullet();
            yield return _delay;
            //  yield return new WaitForSeconds(0.2f); // Garbage olu�turur. S�rekli newleyerek bellek kullan�m�n� artt�r�r.
        }
    }



    public void ReleaseBullet(GameObject bulletObject)
    {
        for (int i = 0; i < _bulletList.Count; i++)
        {
            if (_bulletList[i] == bulletObject)
            {
                _bulletList[i].SetActive(false);
            }
        }
    }
}

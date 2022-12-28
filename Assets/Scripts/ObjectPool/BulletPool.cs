using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    /// <summary>
    /// Singleton Pattern :
    /// bulletPool Static tanýmladým. ismini instance olarak belirledim. 
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

        _delay = new WaitForSeconds(spawnDelay); // Sadece bir kere newleyerek coroutine içinde sonsuz kez newlemekten kurtulduk.
        InitBulletList(); // Awake olurken listemizi oluþturuyoruz. Artýk instantiate yapmak yerine listeden eriþim saðlayacaðýz.
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
           // SetDisableAllBullets(); Ýptal ettik.
            _currentBulletNumber = 0;
        }
    }


    /// <summary>
    /// Tekrardan bulletlarý set edebilmek adýna Disable etmeyi denedik ama görsel bug oluþturduðu için kullanmýyoruz.
    /// </summary>
    private void SetDisableAllBullets()
    {
        for (int i = 0; i < _bulletList.Count; i++)
        {
            _bulletList[i].SetActive(false);
        }
    }



    /// <summary>
    /// 1. hatamýz : Listeyi oluþturmadan Liste elemanlarýna eriþmeye çalýþtýk. 
    /// Bu yüzden Corotini startta baþlatarak problemi çözdük.
    /// 2. hatamýz : currentBulletNumber' listenin dýþýna çýkýyor kontrolünü yapmalýyýz.
    /// </summary>
    IEnumerator BulletSpawnLoop()
    {

        while (true) // Sonsuz bir döngü açar.
        {
            GetBullet();
            yield return _delay;
            //  yield return new WaitForSeconds(0.2f); // Garbage oluþturur. Sürekli newleyerek bellek kullanýmýný arttýrýr.
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

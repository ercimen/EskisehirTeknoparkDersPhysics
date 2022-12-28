using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    /// <summary>
    /// Singleton Pattern :
    /// bulletPool Static tanýmladým. ismini instance olarak belirledim. 
    /// </summary>
    public static EnemyPool instance;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyCount;

    [SerializeField] private Transform spawner;
    [SerializeField] private float spawnDelay;

    private WaitForSeconds _delay;

    private List<GameObject> _enemyList = new(); // Pool Listesi

    private int _currentEnemyNumber;
    private void Awake()
    {
        instance = this;

        _delay = new WaitForSeconds(spawnDelay); // Sadece bir kere newleyerek coroutine içinde sonsuz kez newlemekten kurtulduk.
        InitEnemyList(); // Awake olurken listemizi oluþturuyoruz. Artýk instantiate yapmak yerine listeden eriþim saðlayacaðýz.
    }

    private void Start()
    {
        StartCoroutine(EnemySpawnLoop());
    }

    private void InitEnemyList()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            _enemyList.Add(enemy);

        }
    }

    private void GetEnemy()
    {
        // Eðer aktif etmeye çalýþtýðým düþman zaten aktifse bir þey yapmadan geri dön
        if (_enemyList[_currentEnemyNumber].activeInHierarchy) return;

        Vector3 position = RandomPointOnCircleEdge(5f);

        _enemyList[_currentEnemyNumber].transform.position = position;
        _enemyList[_currentEnemyNumber].SetActive(true);

        ControlEnemyNumber();

    }

    private void ControlEnemyNumber()
    {
        _currentEnemyNumber++;

        if (_currentEnemyNumber >= _enemyList.Count)
        {
            // SetDisableAllBullets(); Ýptal ettik.
            _currentEnemyNumber = 0;
        }
    }


    /// <summary>
    /// Tekrardan bulletlarý set edebilmek adýna Disable etmeyi denedik ama görsel bug oluþturduðu için kullanmýyoruz.
    /// </summary>
    private void SetDisableAllEnemies()
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            _enemyList[i].SetActive(false);
        }
    }



    /// <summary>
    /// 1. hatamýz : Listeyi oluþturmadan Liste elemanlarýna eriþmeye çalýþtýk. 
    /// Bu yüzden Corotini startta baþlatarak problemi çözdük.
    /// 2. hatamýz : currentBulletNumber' listenin dýþýna çýkýyor kontrolünü yapmalýyýz.
    /// </summary>
    IEnumerator EnemySpawnLoop()
    {

        while (true) // Sonsuz bir döngü açar.
        {
            GetEnemy();
            yield return _delay;
            //  yield return new WaitForSeconds(0.2f); // Garbage oluþturur. Sürekli newleyerek bellek kullanýmýný arttýrýr.
        }
    }



    public void ReleaseEnemy(GameObject enemyObject)
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            if (_enemyList[i] == enemyObject)
            {
                _enemyList[i].SetActive(false);
            }
        }
    }



    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = UnityEngine.Random.insideUnitCircle.normalized * radius;
        return new Vector3(vector2.x, 0, vector2.y);
    }
}

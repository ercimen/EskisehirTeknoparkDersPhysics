using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    /// <summary>
    /// Singleton Pattern :
    /// bulletPool Static tan�mlad�m. ismini instance olarak belirledim. 
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

        _delay = new WaitForSeconds(spawnDelay); // Sadece bir kere newleyerek coroutine i�inde sonsuz kez newlemekten kurtulduk.
        InitEnemyList(); // Awake olurken listemizi olu�turuyoruz. Art�k instantiate yapmak yerine listeden eri�im sa�layaca��z.
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
        // E�er aktif etmeye �al��t���m d��man zaten aktifse bir �ey yapmadan geri d�n
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
            // SetDisableAllBullets(); �ptal ettik.
            _currentEnemyNumber = 0;
        }
    }


    /// <summary>
    /// Tekrardan bulletlar� set edebilmek ad�na Disable etmeyi denedik ama g�rsel bug olu�turdu�u i�in kullanm�yoruz.
    /// </summary>
    private void SetDisableAllEnemies()
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            _enemyList[i].SetActive(false);
        }
    }



    /// <summary>
    /// 1. hatam�z : Listeyi olu�turmadan Liste elemanlar�na eri�meye �al��t�k. 
    /// Bu y�zden Corotini startta ba�latarak problemi ��zd�k.
    /// 2. hatam�z : currentBulletNumber' listenin d���na ��k�yor kontrol�n� yapmal�y�z.
    /// </summary>
    IEnumerator EnemySpawnLoop()
    {

        while (true) // Sonsuz bir d�ng� a�ar.
        {
            GetEnemy();
            yield return _delay;
            //  yield return new WaitForSeconds(0.2f); // Garbage olu�turur. S�rekli newleyerek bellek kullan�m�n� artt�r�r.
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

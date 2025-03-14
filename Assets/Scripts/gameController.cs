using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField] Texture2D cursor;
    [SerializeField] string outsideRVSceneString;
    [SerializeField] string insideRVSceneString;

    [SerializeField] float baseSpawnTimer;
    
    float spawnTimer;

    [SerializeField] public float weaponDamage;

    [SerializeField] GameObject spiderPrefab;

    [SerializeField] GameObject bullet;

    [SerializeField] public GameObject waveNumberText;

    int currentWave = 1;

    bool waveSpawned = false;

    public int enemiesRemainingCurrentWave = 0;

    [SerializeField] int baseEnemiesPerWave;

    int enemiesToSpawnForWave;

    [SerializeField] float bulletsPerSecond;

    float bulletCooldownTimer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletCooldownTimer = 1/bulletsPerSecond;
        spawnTimer = baseSpawnTimer;
        enemiesToSpawnForWave = baseEnemiesPerWave;
        enemiesRemainingCurrentWave = enemiesToSpawnForWave;

        waveNumberText.GetComponent<TextMeshProUGUI>().SetText("Wave: " + currentWave);

        GetComponent<AudioSource>().Play();

        if (SceneManager.GetActiveScene().name == outsideRVSceneString)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == outsideRVSceneString && waveSpawned == false)
        {
            enemiesRemainingCurrentWave = enemiesToSpawnForWave;
            StartCoroutine(SpawnEnemies(enemiesToSpawnForWave, spawnTimer));
            waveSpawned = true;
            enemiesToSpawnForWave += baseEnemiesPerWave;
        }

        if (waveSpawned == true && enemiesRemainingCurrentWave <= 0)
        {
            currentWave++;
            waveNumberText.GetComponent<TextMeshProUGUI>().SetText("Wave: " + currentWave);
            waveSpawned = false;
        }

        if (bulletCooldownTimer > 0)
        {
            bulletCooldownTimer -= Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            SwitchView();
        }

        if (SceneManager.GetActiveScene().name == outsideRVSceneString)
        {
            if (Input.GetMouseButton(0) && bulletCooldownTimer <= 0)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                bulletCooldownTimer = 1 / bulletsPerSecond;
            }
        }

    }

    public void SwitchView()
    {
        if(SceneManager.GetActiveScene().name == outsideRVSceneString)
        {
            SceneManager.LoadScene(insideRVSceneString);
        }

        else
        {
            SceneManager.LoadScene(outsideRVSceneString);
        }
    }

    IEnumerator SpawnEnemies(int numEnemiesToSpawn, float spawnTimer)
    {
        int i = 0;
        while (i < numEnemiesToSpawn)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnTimer);
            i++;

        }
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(0, 7.45f), UnityEngine.Random.Range(0, 5));

        if (UnityEngine.Random.Range(0, 2) == 1) {
            spawnPosition.x *= -1;
        }

        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            spawnPosition.y *= -1;
        }

        if (Math.Abs(spawnPosition.y) < 3 && Math.Abs(spawnPosition.x) < 4.8f)
        {
            if(spawnPosition.y < 0)
            {
                spawnPosition.y -= 3;
            }
            else
            {
                spawnPosition.y += 3;
            }

            if (spawnPosition.x < 0)
            {
                spawnPosition.x -= 4.8f;
            }
            else
            {
                spawnPosition.x += 4.8f;
            }

        }

        Instantiate(spiderPrefab, spawnPosition, Quaternion.identity);
    }
}

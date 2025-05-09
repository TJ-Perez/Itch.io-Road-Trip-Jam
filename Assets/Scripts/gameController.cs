using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField] Texture2D cursor;
    [SerializeField] public string outsideRVSceneString;
    [SerializeField] public string insideRVSceneString;
    [SerializeField] public string mainMenuString;

    [SerializeField] float baseSpawnTimer;
    
    float spawnTimer;

    [SerializeField] public float weaponDamage;

    [SerializeField] GameObject spiderBase;
    [SerializeField] GameObject spiderHighHealth;
    [SerializeField] GameObject spiderHighSpeed;
    [SerializeField] GameObject spiderHighDamage;

    GameObject[] spiderVariants;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletSlug;

    [SerializeField] public GameObject waveNumberText;

    public int currentWave = 1;

    public bool waveSpawned = false;

    public int enemiesRemainingCurrentWave = 0;

    [SerializeField] int baseEnemiesPerWave;

    int enemiesToSpawnForWave;

    [SerializeField] float bulletsPerSecond;

    float bulletCooldownTimer = 0;


    [SerializeField] public float totalHealth;
    [SerializeField] public float currentHealth;

    [SerializeField] public FloatingHealthBar rvFloatingHealthBar;

    [SerializeField] GameObject healthBarCanvas;
    [SerializeField] GameObject roadBackground;
    [SerializeField] GameObject waveUICanvas;

    public static GameController Instance;

    [SerializeField] private AudioSource soundEffectPlayer;

    [SerializeField] AudioClip mainMenuStartGame;

    [SerializeField] GameObject upgradeCanvas;

    [SerializeField] AudioSource musicPlayer;

    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip insideRVMusic;
    [SerializeField] AudioClip outsideRVMusic;

    public Boolean shotgunUpgrade = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (musicPlayer == null)
        {
            musicPlayer = gameObject.GetComponent<AudioSource>();
        }


        spiderVariants = new GameObject[] {spiderBase, spiderHighDamage, spiderHighHealth, spiderHighSpeed };
        bulletCooldownTimer = 1/bulletsPerSecond;
        spawnTimer = baseSpawnTimer;
        enemiesToSpawnForWave = baseEnemiesPerWave;
        enemiesRemainingCurrentWave = enemiesToSpawnForWave;

        if (soundEffectPlayer == null)
        {
            soundEffectPlayer = GameObject.FindGameObjectWithTag("soundEffectSource").GetComponent<AudioSource>();
        }

        if (upgradeCanvas == null)
        {
            upgradeCanvas = GameObject.FindGameObjectWithTag("UpgradeCanvas");
        }

        if (SceneManager.GetActiveScene().name == outsideRVSceneString)
        {
            healthBarCanvas.SetActive(true);
            roadBackground.SetActive(true);
            waveUICanvas.SetActive(true);
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }

        rvFloatingHealthBar.UpdateHealthBar(currentHealth, totalHealth);


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

        else if (waveSpawned == true && enemiesRemainingCurrentWave <= 0 && SceneManager.GetActiveScene().name == outsideRVSceneString)
        {
            currentWave++;
            waveNumberText.GetComponent<TextMeshProUGUI>().SetText("Wave: " + currentWave);
            SwitchView();
        }

        if (bulletCooldownTimer > 0)
        {
            bulletCooldownTimer -= Time.deltaTime;
        }

        if (SceneManager.GetActiveScene().name == outsideRVSceneString)
        {
            if (Input.GetMouseButton(0) && bulletCooldownTimer <= 0)
            {
                GameObject turret = GameObject.FindGameObjectWithTag("turret");
                if (shotgunUpgrade)
                {
                    Instantiate(bulletSlug, turret.transform.position, Quaternion.identity);

                }
                else
                {
                    Instantiate(bullet, turret.transform.position, Quaternion.identity);
                }
                bulletCooldownTimer = 1 / bulletsPerSecond;
            }
        }

    }

    public void SwitchView()
    {

        if (SceneManager.GetActiveScene().name == outsideRVSceneString)
        {
            SceneManager.LoadScene(insideRVSceneString);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            StartCoroutine(DelayUpgradeCanvasLoad());
        }

        else
        {
            SceneManager.LoadScene(outsideRVSceneString);
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    IEnumerator DelayUpgradeCanvasLoad()
    {
        yield return new WaitForSeconds(1);
        upgradeCanvas.SetActive(true);
        upgradeCanvas.GetComponent<UpgradeScript>().RefreshUpgrades();
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
        float minimumX = 5.8f;
        float minimumY = 5;

        Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(0, 8.45f), UnityEngine.Random.Range(0, 6));

        if (UnityEngine.Random.Range(0, 2) == 1) {
            spawnPosition.x *= -1;
        }

        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            spawnPosition.y *= -1;
        }

        if (Math.Abs(spawnPosition.y) < minimumY && Math.Abs(spawnPosition.x) < minimumX)
        {
            if(spawnPosition.y < 0)
            {
                spawnPosition.y -= minimumY;
            }
            else
            {
                spawnPosition.y += minimumY;
            }

            if (spawnPosition.x < 0)
            {
                spawnPosition.x -= minimumX;
            }
            else
            {
                spawnPosition.x += minimumX;
            }
        }


        //enemy variants spawn at higher waves

        if (currentWave == 1)
        {
            Instantiate(spiderBase, spawnPosition, Quaternion.identity);
        }
        else if (currentWave == 2)
        {
            Instantiate(spiderVariants[UnityEngine.Random.Range(0, 2)], spawnPosition, Quaternion.identity);
        }
        else if (currentWave == 3)
        {
            Instantiate(spiderVariants[UnityEngine.Random.Range(0, 3)], spawnPosition, Quaternion.identity);
        }
        else if (currentWave == 4)
        {
            Instantiate(spiderVariants[UnityEngine.Random.Range(0, 4)], spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(spiderVariants[UnityEngine.Random.Range(1, 4)], spawnPosition, Quaternion.identity);
        }
    }
    public void OnHit(float damage)
    {
        currentHealth -= damage;
        rvFloatingHealthBar.UpdateHealthBar(currentHealth, totalHealth);
    }

    public void LoadGame()
    {
        soundEffectPlayer.PlayOneShot(mainMenuStartGame);
        SceneManager.LoadScene(insideRVSceneString);

        healthBarCanvas.SetActive(true);
        roadBackground.SetActive(true);
        waveUICanvas.SetActive(true);

        waveNumberText.GetComponent<TextMeshProUGUI>().SetText("Wave: " + currentWave);
        GetComponent<AudioSource>().Play();

    }
}

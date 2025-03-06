using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField] Texture2D cursor;
    [SerializeField] string outsideRVSceneString;
    [SerializeField] string insideRVSceneString;

    [SerializeField] float spawnTimer;

    [SerializeField] public float weaponDamage;

    [SerializeField] GameObject spiderPrefab;

    [SerializeField] float bulletSpeed;

    [SerializeField] GameObject bullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<AudioSource>().Play();

        if (SceneManager.GetActiveScene().name == outsideRVSceneString)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);

        }
            InvokeRepeating(nameof(SpawnEnemy), spawnTimer, spawnTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwitchView();
        }

        if (SceneManager.GetActiveScene().name == outsideRVSceneString)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 bulletVector = Vector2.MoveTowards(transform.position, Input.mousePosition, bulletSpeed);
                Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().linearVelocity = bulletVector;
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

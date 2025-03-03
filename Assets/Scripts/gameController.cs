using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{

    [SerializeField] Texture2D cursor;
    [SerializeField] string outsideRVSceneString;
    [SerializeField] string insideRVSceneString;

    [SerializeField] float spawnTimer;

    [SerializeField] public float weaponDamage;

    [SerializeField] GameObject spiderPrefab;

    float timer = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float seconds = (int)timer;

        if (spawnTimer % seconds == 10)
        {
            SpawnEnemy();
            timer = 0;
        }

        if (SceneManager.GetActiveScene().name == outsideRVSceneString)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);

            if (Input.GetKeyDown(KeyCode.F))
            {
                SwitchView();
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
        Instantiate(spiderPrefab, new Vector3(0, -1, 0), Quaternion.identity);
    }

    public static explicit operator gameController(GameObject v)
    {
        throw new NotImplementedException();
    }
}

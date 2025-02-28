using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{

    [SerializeField] Texture2D cursor;
    [SerializeField] string outsideRVSceneString;
    [SerializeField] string insideRVSceneString;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().ToString() == outsideRVSceneString)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Console.WriteLine("F down");
        }


        if (Input.GetKeyDown(KeyCode.F) && SceneManager.GetActiveScene().ToString() == outsideRVSceneString)
        {
            SceneManager.LoadScene(insideRVSceneString);
        }
    }
}

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
        this.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{

    [SerializeField] Texture2D cursor;
    [SerializeField] Scene outsideRVScene;
    [SerializeField] Scene insideRVScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene() == outsideRVScene)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && SceneManager.GetActiveScene() == outsideRVScene)
        {
            SceneManager.LoadScene(insideRVScene.ToString());
        }
    }
}

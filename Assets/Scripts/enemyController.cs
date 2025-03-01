using UnityEngine;

public class enemyController : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] gameController gameController;

    Camera m_Camera;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        m_Camera = Camera.main;
    }


    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.position = Vector2.MoveTowards(transform.position, target.transform.position, speed);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.gameObject == gameObject)
            {
                OnEnemyClicked(gameObject);
            }
        }
    }

    public void OnEnemyClicked(GameObject gameObject)
    {
        health -= gameController.weaponDamage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

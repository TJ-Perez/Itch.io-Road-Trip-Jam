using UnityEngine;

public class enemyController : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private float totalHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float speed;
    [SerializeField] gameController gameController;
    [SerializeField] floatingHealthBar floatingHealthBar;

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

        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        if (gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.position = Vector2.MoveTowards(transform.position, target.transform.position, speed);

        if (rb.position.x < target.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && hit.collider.gameObject == gameObject)
            {
                OnClicked();
            }
        }
    }

    public void OnClicked()
    {
        currentHealth -= gameController.weaponDamage;
        floatingHealthBar.UpdateHealthBar(currentHealth, totalHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}

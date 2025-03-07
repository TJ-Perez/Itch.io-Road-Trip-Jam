using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private float totalHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float speed;
    [SerializeField] GameController gameController;
    [SerializeField] FloatingHealthBar floatingHealthBar;

    [SerializeField] AudioClip enemyDeathSound;
    [SerializeField] AudioClip enemyHitSound;

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
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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
    }

    public void OnHit()
    {

        Debug.Log("Hit");
        currentHealth -= gameController.weaponDamage;
        floatingHealthBar.UpdateHealthBar(currentHealth, totalHealth);

        if (currentHealth <= 0)
        {
            GameObject.FindGameObjectWithTag("soundEffectSource").GetComponent<AudioSource>().PlayOneShot(enemyDeathSound);
            Destroy(gameObject);
        }
        else
        {
            GameObject.FindGameObjectWithTag("soundEffectSource").GetComponent<AudioSource>().PlayOneShot(enemyHitSound);
        }

    }
}

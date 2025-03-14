using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject attackTarget;
    [SerializeField] private float totalHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float speed;
    [SerializeField] GameController gameController;
    [SerializeField] FloatingHealthBar floatingHealthBar;

    [SerializeField] AudioClip enemyDeathSound;
    [SerializeField] AudioClip enemyHitSound;

    [SerializeField] Animator enemyAnimator;

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

        if(attackTarget == null)
        {
            attackTarget = GameObject.FindGameObjectWithTag("Player");
        }

        if (gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAnimator.GetBool("shouldExplode"))
        {
            return;
        }

        rb.position = Vector2.MoveTowards(transform.position, attackTarget.transform.position, speed);

        if (rb.position.x < attackTarget.transform.position.x)
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
            gameController.enemiesRemainingCurrentWave--;
            Destroy(gameObject);
        }
        else
        {
            GameObject.FindGameObjectWithTag("soundEffectSource").GetComponent<AudioSource>().PlayOneShot(enemyHitSound);
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RV"))
        {
            Debug.Log("Player Hit");
            enemyAnimator.SetBool("shouldExplode", true);
            //play sound for explosion?

            //wait for duration of explosion animation
            StartCoroutine(DelayDestroyBySeconds(.5f));
        }
    }

    IEnumerator DelayDestroyBySeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("Done waiting");
        gameController.enemiesRemainingCurrentWave--;
        Destroy(gameObject);

    }
}

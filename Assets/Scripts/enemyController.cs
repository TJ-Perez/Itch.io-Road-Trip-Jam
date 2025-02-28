using UnityEngine;

public class enemyController : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] gameController gameController;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.position = Vector2.MoveTowards(transform.position, target.transform.position, speed);

        if(Input.GetMouseButtonDown(0) )
        {
            health -= gameController.weaponDamage;
        }
    }

}

using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] float bulletSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 mousePos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        Vector2 bulletVector = Vector2.MoveTowards(transform.position, mousePos, bulletSpeed);
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = bulletVector;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().OnHit();
            Destroy(gameObject);
        }
    }
}

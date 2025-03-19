using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] float bulletSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SelfDestruct());

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        Vector2 bulletVector = Vector2.MoveTowards(transform.position, mousePos, bulletSpeed);
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = bulletVector;
        gameObject.transform.Rotate(0, 0, Mathf.Atan2(bulletVector.y, bulletVector.x) * Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().OnHit();
            Destroy(gameObject);
        }
    }
}

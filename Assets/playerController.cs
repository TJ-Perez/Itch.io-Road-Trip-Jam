using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Collider2D outsideViewCollider;
    [SerializeField] gameController gameController;
    [SerializeField] Collider2D playerCollider;

    public bool isAbleViewOutside = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody2D>().transform.position += new Vector3(-moveSpeed, 0,0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody2D>().transform.position += new Vector3(moveSpeed, 0, 0);
        }

        if (Input.GetKey(KeyCode.F))
        {
            if (isAbleViewOutside) {

                gameController.SwitchView();

            }
        }
    }
}

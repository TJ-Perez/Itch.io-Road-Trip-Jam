using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float horizontalMoveVelocity;
    [SerializeField] float jumpVelocity;
    [SerializeField] Collider2D outsideViewCollider;
    [SerializeField] GameController gameController;
    [SerializeField] Collider2D playerCollider;

    public bool isAbleViewOutside = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocityX = -horizontalMoveVelocity;
        }

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocityX = horizontalMoveVelocity;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocityY = jumpVelocity;
        }


        if (Input.GetKey(KeyCode.F))
        {
            if (isAbleViewOutside) {

                gameController.waveSpawned = false;
                gameController.SwitchView();


            }
        }
    }
}

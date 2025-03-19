using UnityEngine;

public class turretController : MonoBehaviour
{

    private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad - 90;
        transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        Debug.DrawLine(transform.position, mousePos, Color.red, Time.deltaTime);
    }
}

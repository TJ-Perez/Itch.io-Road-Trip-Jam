using UnityEngine;
using UnityEngine.SceneManagement;

public class RVController : MonoBehaviour
{

    [SerializeField] private float totalHealth;
    [SerializeField] private float currentHealth;

    [SerializeField] FloatingHealthBar floatingHealthBar;

    [SerializeField] GameController gameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void OnHit(float damage)
    //{
    //    gameController.currentHealth -= damage;

    //    totalHealth -= damage; 
    //    floatingHealthBar.UpdateHealthBar(totalHealth, currentHealth);
    //}
}

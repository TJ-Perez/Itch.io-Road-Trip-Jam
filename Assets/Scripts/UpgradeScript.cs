using Unity.VisualScripting;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{

    GameController gameController;

    [SerializeField] FloatingHealthBar RVHealthBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        RVHealthBar = GameObject.FindGameObjectWithTag("RVHealthBar").GetComponent<FloatingHealthBar>();
        if (gameController.currentWave <= 1) 
        {
            gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void healthUpgrade()
    {
        gameController.totalHealth += 40;
        gameController.currentHealth += 40;
        RVHealthBar.UpdateHealthBar(gameController.currentHealth, gameController.totalHealth);
        gameObject.SetActive(false);
    }

    public void damageUpgrade()
    {
        gameController.weaponDamage += 2;
        gameObject.SetActive(false);
    }

}

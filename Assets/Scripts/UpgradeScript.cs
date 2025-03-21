using Unity.VisualScripting;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{

    [SerializeField] GameController gameController;

    [SerializeField] FloatingHealthBar RVHealthBar;
    [SerializeField] AudioSource soundEffectPlayer;
    [SerializeField] AudioClip upgradeSound;

    [SerializeField] GameObject shotgunUpgradeButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        RVHealthBar = GameObject.FindGameObjectWithTag("RVHealthBar").GetComponent<FloatingHealthBar>();
        if (gameController.currentWave <= 1) 
        {
            gameObject.SetActive(false);
        }

        if (soundEffectPlayer == null)
        {
            soundEffectPlayer = GameObject.FindGameObjectWithTag("soundEffectSource").GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshUpgrades()
    {
        if (gameController.currentWave >= 3 && gameController.shotgunUpgrade == false)
        {
            shotgunUpgradeButton.SetActive(true);
        }
        else
        {
            shotgunUpgradeButton.SetActive(false);
        }
    }

    public void HealthUpgrade()
    {
        gameController.totalHealth += 40;
        gameController.currentHealth += 40;
        RVHealthBar.UpdateHealthBar(gameController.currentHealth, gameController.totalHealth);
        soundEffectPlayer.PlayOneShot(upgradeSound);
        gameObject.SetActive(false);
    }

    public void DamageUpgrade()
    {
        gameController.weaponDamage += 2;
        soundEffectPlayer.PlayOneShot(upgradeSound);
        gameObject.SetActive(false);
    }

    public void ShotgunUpgrade()
    {
        gameController.shotgunUpgrade = true;
        gameController.weaponDamage += 1;
        soundEffectPlayer.PlayOneShot(upgradeSound);
        gameObject.SetActive(false);
    }

}

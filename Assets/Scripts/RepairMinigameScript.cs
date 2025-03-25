using NUnit.Framework;
using TMPro;
using UnityEngine;

public class RepairMinigameScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI repairText;

    [SerializeField] GameController gameController;

    public bool playerInRepairZone = false;
    public bool repairReady = false;

    char repairKey = '-';

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (playerInRepairZone && gameController.currentHealth < gameController.totalHealth)
            {
                ReadyRepair();
            }
        }

        if (Input.anyKeyDown && playerInRepairZone)
        {
            foreach (char c in Input.inputString)
            {
                if (char.ToUpper(c) == repairKey)
                {
                    if (gameController.currentHealth < gameController.totalHealth)
                    {
                        gameController.currentHealth += 5;
                        gameController.rvFloatingHealthBar.UpdateHealthBar(gameController.currentHealth, gameController.totalHealth);
                        repairReady = false;

                    }
                    if (playerInRepairZone && gameController.currentHealth < gameController.totalHealth)
                    {
                        ReadyRepair();
                    }
                    else
                    {
                        repairText.SetText("");
                    }
                }
            }
        }


    }

    private void ReadyRepair()
    {
        StoreRandomKey();
        repairText.SetText("Press " + repairKey + " to repair.");
        repairReady = true;
    }

    private void StoreRandomKey()
    {
        repairKey = (char)('A' + Random.Range(0, 26));
        if (repairKey == 'F' || repairKey == 'W' || repairKey == 'A' || repairKey == 'S' || repairKey == 'D')
        {
            repairKey = 'G';
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        playerInRepairZone = true;
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        playerInRepairZone = false;
    }
}

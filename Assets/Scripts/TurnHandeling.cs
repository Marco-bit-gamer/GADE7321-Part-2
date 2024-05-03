using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.CompilerServices;

public class TurnHandeling : MonoBehaviour
{
    public TMP_Text Message;

    bool PlayerTurn = false;
    bool PlayerAttack = false;
    bool EnemyTurn = false;
    bool EnemyAttack = false;
    int count = 0;
    int countPB = 0;
    int countEB = 0;

    public GameObject PrefabEnemySword;
    public GameObject PrefabPlayerSword;
    public GameObject PrefabEnemyBow;
    public GameObject PrefabPlayerBow;
    public GameObject PrefabEnemyShield;
    public GameObject PrefabPlayerShield;
    public GameObject PrefabEmpty;

    public GameObject PlayerAttackObject;
    public GameObject EnemyAttackObject;

    bool enemyInRangeSword = false;
    bool enemyInRangeBow = false;
    bool enemyInRangeShield = false;
    bool playerInRangeSword = false;
    bool playerInRangeBow = false;
    bool playerInRangeShield = false;

    private void Awake()
    {
        int randChoice = Random.Range(1, 3);

        if (randChoice == 1) //Check if it is Player's turn
        {
            PlayerTurn = true;
            EnemyTurn = false;
            Message.text = "Player1 Starts";
        }
        else if (randChoice == 2) //Check if it is Enemy's turn
        {
            EnemyTurn = true;
            PlayerTurn = false;
            Message.text = "Player2 Starts";
        }
    }
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Handle mouse click
            HandleMouseClick();
        }
    }

    void HandleMouseClick()
    {
        // Get click position
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

        // Handle click based on turn
        if (PlayerTurn)
        {
            HandlePlayerTurn(hit);
        }
        else if (EnemyTurn)
        {
            HandleEnemyTurn(hit);
        }
    }

    void HandlePlayerTurn(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            if (PlayerTurn && hit.collider.CompareTag("Empty"))
            {
                ReplaceObject(hit.collider.gameObject, PrefabPlayerSword);
                PlayerTurn = false;
                EnemyTurn = false;
                PlayerAttack = true;
                EnemyAttack = false;
                Message.text = "Player2 Go";
                count++;
            }
            if (PlayerTurn && (hit.collider.CompareTag("EnemySword") || hit.collider.CompareTag("EnemyBow") || hit.collider.CompareTag("EnemyShield")))

            {
                Message.text = "Player2 has already taken the terrain, choose another";
            }

            if (countPB < 4 && PlayerTurn && hit.collider.CompareTag("PlayerSword"))
            {
                ReplaceObject(hit.collider.gameObject, PrefabPlayerBow);
                EnemyTurn = false;
                EnemyAttack = false;
                PlayerTurn = false;
                PlayerAttack = true;
                Message.text = "Player 1 can attack";
                countPB++;
            }
            
            if(countPB > 4)
            {
                Message.text = "You can only have 3 archers choose again";
            }

            if (PlayerTurn && hit.collider.CompareTag("PlayerBow"))
            {
                ReplaceObject(hit.collider.gameObject, PrefabPlayerShield);
                EnemyTurn = false;
                EnemyAttack = false;
                PlayerTurn = false;
                PlayerAttack = true;
                Message.text = "Player 1 can attack";
                countPB--;
            }

        }

        if (PlayerAttack && count == 3)
        {
            HandlePlayerAttack(hit);
        }
        else
        {
            PlayerTurn = false;
            EnemyTurn = true;
            PlayerAttack = false;
            EnemyAttack = false;
        }
    }

    void HandleEnemyTurn(RaycastHit2D hit)
    {
        if (EnemyTurn && hit.collider.CompareTag("Empty"))
        {
            ReplaceObject(hit.collider.gameObject, PrefabEnemySword);
            PlayerTurn = false;
            EnemyTurn = false;
            PlayerAttack = false;
            EnemyAttack = true;
            Message.text = "Player1 Go";
            count++;
        }

        if (EnemyTurn && (hit.collider.CompareTag("PlayerSword") || hit.collider.CompareTag("PlayerBow") || hit.collider.CompareTag("EnemyShield")))
        {
            Message.text = "Player1 has already taken the terrain, choose another";
        }

        if (countEB < 4 && EnemyTurn && hit.collider.CompareTag("EnemySword"))
        {
            ReplaceObject(hit.collider.gameObject, PrefabEnemyBow);
            PlayerTurn = false;
            PlayerAttack = false;
            EnemyTurn = false;
            EnemyAttack = true;
            Message.text = "Player 2 can attack";
            countEB++;
        }

        if (countEB > 4)
        {
            Message.text = "You can only have 3 archers choose again";
        }

        if (EnemyTurn && hit.collider.CompareTag("EnemyBow"))
        {
            ReplaceObject(hit.collider.gameObject, PrefabEnemyShield);
            PlayerTurn = false;
            PlayerAttack = false;
            EnemyTurn = false;
            EnemyAttack = true;
            Message.text = "Player 2 can attack";
            countEB--;
        }

        if (EnemyAttack && count == 3)
        {
            // Handle enemy's attack
            HandleEnemyAttack(hit);
        }
        else
        {
            PlayerTurn = true;
            EnemyTurn = true;
            PlayerAttack = false;
            EnemyAttack = false;
        }

    }

    void HandlePlayerAttack(RaycastHit2D hit)
    {
        // Handle player's attack logic
        if (PlayerAttack && hit.collider.CompareTag("Empty"))
        {
            Message.text = "You can't attack an empty place try again";
        }

        if (PlayerAttack && (hit.collider.CompareTag("PlayerSword") || hit.collider.CompareTag("PlayerBow") || hit.collider.CompareTag("PlayerShield")))
        {
            Message.text = "You can not hit your own troops";
        }

        if (PlayerAttack && hit.collider.CompareTag("EnemySword")) // Player's turn to attack
        {
            if (PlayerAttackObject.tag == "PlayerSword")
            {
                if (enemyInRangeSword)
                {
                    if (enemyInRangeSword && hit.collider.CompareTag("EnemySword")) // Check if an enemy sword is clicked
                    {
                        // Perform attack action
                        ReplaceObject(hit.collider.gameObject, PrefabPlayerSword);
                        PlayerTurn = false;
                        PlayerAttack = false;
                        EnemyTurn = true;
                        EnemyAttack = false;
                        Message.text = "Player 2's turn";
                    }

                    if (enemyInRangeSword && hit.collider.CompareTag("EnemyBow")) // Check if an enemy sword is clicked
                    {
                        // Perform attack action
                        ReplaceObject(hit.collider.gameObject, PrefabEnemySword);
                        PlayerTurn = false;
                        PlayerAttack = false;
                        EnemyTurn = true;
                        EnemyAttack = false;
                        Message.text = "Player 2's turn";
                        countEB--;
                    }

                }
                if (!enemyInRangeSword)
                {
                    // Display message indicating no enemy in range or invalid target
                    PlayerTurn = false;
                    PlayerAttack = false;
                    EnemyTurn = true;
                    EnemyAttack = false;
                    Message.text = "No enemy in range or there is a shield you can't attack. Player 2's turn";
                }
            }

            if (PlayerAttackObject.tag == "PlayerBow")
            {
                if (enemyInRangeBow)
                {
                    if (enemyInRangeBow && hit.collider.CompareTag("EnemySword")) // Check if an enemy sword is clicked
                    {
                        // Perform attack action
                        ReplaceObject(hit.collider.gameObject, PrefabEmpty);
                        PlayerTurn = false;
                        PlayerAttack = false;
                        EnemyTurn = true;
                        EnemyAttack = false;
                        Message.text = "Player 2's turn";
                    }

                    if (enemyInRangeBow && hit.collider.CompareTag("EnemyBow")) // Check if an enemy sword is clicked
                    {
                        // Perform attack action
                        ReplaceObject(hit.collider.gameObject, PrefabEmpty);
                        PlayerTurn = false;
                        PlayerAttack = false;
                        EnemyTurn = true;
                        EnemyAttack = false;
                        Message.text = "Player 2's turn";
                        countEB--;
                    }
                }
                if (!enemyInRangeBow)
                {
                    // Display message indicating no enemy in range or invalid target
                    PlayerTurn = false;
                    PlayerAttack = false;
                    EnemyTurn = true;
                    EnemyAttack = false;
                    Message.text = "No enemy in range or there is a shield you can't attack. Player 2's turn";
                }
            }

            if (PlayerAttackObject.tag == "PlayerShield")
            {
                if (enemyInRangeShield && hit.collider.CompareTag("EnemySword")) // Check if an enemy sword is clicked
                {
                    // Perform attack action
                    ReplaceObject(hit.collider.gameObject, PrefabEmpty);
                    PlayerTurn = false;
                    PlayerAttack = false;
                    EnemyTurn = true;
                    EnemyAttack = false;
                    Message.text = "Player 2's turn";
                }

                if (enemyInRangeShield && hit.collider.CompareTag("EnemyBow")) // Check if an enemy sword is clicked
                {
                    // Perform attack action
                    ReplaceObject(hit.collider.gameObject, PrefabEnemySword);
                    PlayerTurn = false;
                    PlayerAttack = false;
                    EnemyTurn = true;
                    EnemyAttack = false;
                    Message.text = "Player 2's turn";
                    countEB--;
                }

                if (!enemyInRangeShield)
                {
                    // Display message indicating no enemy in range or invalid target
                    PlayerTurn = false;
                    PlayerAttack = false;
                    EnemyTurn = true;
                    EnemyAttack = false;
                    Message.text = "No enemy in range or there is a shield you can't attack. Player 2's turn";
                }
            }
        }
    }

    void HandleEnemyAttack(RaycastHit2D hit)
    {
        // Handle enemy's attack logic
        if (EnemyAttack && hit.collider.CompareTag("Empty"))
        {
            Message.text = "You can't attack an empty place try again";
        }

        if (EnemyAttack && (hit.collider.CompareTag("EnemySword") || hit.collider.CompareTag("EnemyBow") || hit.collider.CompareTag("EnemyShield")))

        {
            Message.text = "You can not hit your own troops";
        }

        if (EnemyAttack && hit.collider.CompareTag("PlayerSword")) // Player's turn to attack
        {
            if (EnemyAttackObject.tag == "EnemySword")
            {
                if (playerInRangeSword && hit.collider.CompareTag("PlayerSword")) // Check if an enemy sword is clicked
                {
                    // Perform attack action
                    ReplaceObject(hit.collider.gameObject, PrefabEnemySword);
                    PlayerTurn = true;
                    PlayerAttack = false;
                    EnemyTurn = false;
                    EnemyAttack = false;
                    Message.text = "Player 1's turn";
                }

                if (playerInRangeSword && hit.collider.CompareTag("PlayerBow")) // Check if an enemy sword is clicked
                {
                    // Perform attack action
                    ReplaceObject(hit.collider.gameObject, PrefabPlayerBow);
                    PlayerTurn = true;
                    PlayerAttack = false;
                    EnemyTurn = false;
                    EnemyAttack = false;
                    Message.text = "Player 1's turn";
                    countPB--;
                }
                if (!playerInRangeSword)
                {
                    // Display message indicating no enemy in range or invalid target
                    PlayerTurn = true;
                    PlayerAttack = false;
                    EnemyTurn = false;
                    EnemyAttack = false;
                    Message.text = "No enemy in range or there is a shield you can't attack. Player 2's turn";
                }
            }

            if (EnemyAttackObject.tag == "EnemyBow")
            {
                if (playerInRangeBow && hit.collider.CompareTag("PlayerSword")) // Check if an enemy sword is clicked
                {
                    // Perform attack action
                    ReplaceObject(hit.collider.gameObject, PrefabEmpty);
                    PlayerTurn = true;
                    PlayerAttack = false;
                    EnemyTurn = false;
                    EnemyAttack = false;
                    Message.text = "Player 2's turn";
                }

                if (playerInRangeBow && hit.collider.CompareTag("PlayerBow")) // Check if an enemy sword is clicked
                {
                    // Perform attack action
                    ReplaceObject(hit.collider.gameObject, PrefabEmpty);
                    PlayerTurn = true;
                    PlayerAttack = false;
                    EnemyTurn = false;
                    EnemyAttack = false;
                    Message.text = "Player 2's turn";
                    countPB--;
                }
                if (!playerInRangeBow)
                {
                    // Display message indicating no enemy in range or invalid target
                    PlayerTurn = true;
                    PlayerAttack = false;
                    EnemyTurn = false;
                    EnemyAttack = false;
                    Message.text = "No enemy in range or there is a shield you can't attack. Player 2's turn";
                }
            }

            if (EnemyAttackObject.tag == "EnemyShield")
            {
                if (playerInRangeShield && hit.collider.CompareTag("PlayerSword")) // Check if an enemy sword is clicked
                {
                    // Perform attack action
                    ReplaceObject(hit.collider.gameObject, PrefabEmpty);
                    PlayerTurn = true;
                    PlayerAttack = false;
                    EnemyTurn = false;
                    EnemyAttack = false;
                    Message.text = "Player 2's turn";
                }

                if (playerInRangeShield && hit.collider.CompareTag("PlayerBow")) // Check if an enemy sword is clicked
                {
                    // Perform attack action
                    ReplaceObject(hit.collider.gameObject, PrefabPlayerSword);
                    PlayerTurn = true;
                    PlayerAttack = false;
                    EnemyTurn = false;
                    EnemyAttack = false;
                    Message.text = "Player 2's turn";
                    countPB--;
                }
                if (!playerInRangeShield)
                {
                    // Display message indicating no enemy in range or invalid target
                    PlayerTurn = true;
                    PlayerAttack = false;
                    EnemyTurn = false;
                    EnemyAttack = false;
                    Message.text = "No enemy in range or there is a shield you can't attack. Player 2's turn";
                }
            }
        }
    }

    void ReplaceObject(GameObject oldObject, GameObject newObject)
    {
        if (newObject != null) // Check if newObject is not null
        {
            Instantiate(newObject, oldObject.transform.position, oldObject.transform.rotation);

            if (newObject.tag == "PlayerSword" || newObject.tag == "PlayerBow" || newObject.tag == "PlayerShield")
            {
                PlayerAttackObject = newObject;
            }

            if (newObject.tag == "EnemySword" || newObject.tag == "EnemyBow" || newObject.tag == "EnemyShield")
            {
                EnemyAttackObject = newObject;
            }

            Destroy(oldObject);
        }
        else
        {
            Debug.LogError("New object is null!"); // Log an error if newObject is null
        }
    }


    bool EnemyInRangeSword()
    {
        return PlayerAttackObject.GetComponent<PlayerSwordCheck>().countELeftSw > 0 ||
               PlayerAttackObject.GetComponent<PlayerSwordCheck>().countELeftBo > 0 ||
               PlayerAttackObject.GetComponent<PlayerSwordCheck>().countERightSw > 0 ||
               PlayerAttackObject.GetComponent<PlayerSwordCheck>().countERightBo > 0;
    }

    bool EnemyInRangeBow()
    {
        return PlayerAttackObject.GetComponent<PlayerBowCheck>().countEDLeftSwU > 0 ||
               PlayerAttackObject.GetComponent<PlayerBowCheck>().countEDLeftBoU > 0 ||
               PlayerAttackObject.GetComponent<PlayerBowCheck>().countEDRightSwU > 0 ||
               PlayerAttackObject.GetComponent<PlayerBowCheck>().countEDRightBoU > 0 ||
               PlayerAttackObject.GetComponent<PlayerBowCheck>().countEDLeftSwD > 0 ||
               PlayerAttackObject.GetComponent<PlayerBowCheck>().countEDLeftBoD > 0 ||
               PlayerAttackObject.GetComponent<PlayerBowCheck>().countEDRightSwD > 0 ||
               PlayerAttackObject.GetComponent<PlayerBowCheck>().countEDRightBoD > 0;
    }

    bool EnemyInRangeShield()
    {
        return PlayerAttackObject.GetComponent<PlayerShieldCheck>().countELeftSw > 0 ||
               PlayerAttackObject.GetComponent<PlayerShieldCheck>().countELeftBo > 0 ||
               PlayerAttackObject.GetComponent<PlayerShieldCheck>().countERightSw > 0 ||
               PlayerAttackObject.GetComponent<PlayerShieldCheck>().countERightBo > 0 ||
               PlayerAttackObject.GetComponent<PlayerShieldCheck>().countEDLeftSwU > 0 ||
               PlayerAttackObject.GetComponent<PlayerShieldCheck>().countEDLeftBoU > 0 ||
               PlayerAttackObject.GetComponent<PlayerShieldCheck>().countEDRightSwU > 0 ||
               PlayerAttackObject.GetComponent<PlayerShieldCheck>().countEDRightBoU > 0 ||
               PlayerAttackObject.GetComponent<PlayerShieldCheck>().countEDLeftSwD > 0 ||
               PlayerAttackObject.GetComponent<PlayerShieldCheck>().countEDLeftBoD > 0 ||
               PlayerAttackObject.GetComponent<PlayerShieldCheck>().countEDRightSwD > 0 ||
               PlayerAttackObject.GetComponent<PlayerShieldCheck>().countEDRightBoD > 0;
    }

    bool PlayerInRangeSword()
    {
        return EnemyAttackObject.GetComponent<EnemySwordCheck>().countPLeftSw > 0 ||
               EnemyAttackObject.GetComponent<EnemySwordCheck>().countPLeftBo > 0 ||
               EnemyAttackObject.GetComponent<EnemySwordCheck>().countPRightSw > 0 ||
               EnemyAttackObject.GetComponent<EnemySwordCheck>().countPRightBo > 0;
    }

    bool PlayerInRangeBow()
    {
        return EnemyAttackObject.GetComponent<EnemyBowCheck>().countPDLeftSwU > 0 ||
               EnemyAttackObject.GetComponent<EnemyBowCheck>().countPDLeftBoU > 0 ||
               EnemyAttackObject.GetComponent<EnemyBowCheck>().countPDRightSwU > 0 ||
               EnemyAttackObject.GetComponent<EnemyBowCheck>().countPDRightBoU > 0 ||
               EnemyAttackObject.GetComponent<EnemyBowCheck>().countPDLeftSwD > 0 ||
               EnemyAttackObject.GetComponent<EnemyBowCheck>().countPDLeftBoD > 0 ||
               EnemyAttackObject.GetComponent<EnemyBowCheck>().countPDRightSwD > 0 ||
               EnemyAttackObject.GetComponent<EnemyBowCheck>().countPDRightBoD > 0;
    }

    bool PlayerInRangeShield()
    {
        return EnemyAttackObject.GetComponent<EnemyShieldCheck>().countPLeftSw > 0 ||
               EnemyAttackObject.GetComponent<EnemyShieldCheck>().countPLeftBo > 0 ||
               EnemyAttackObject.GetComponent<EnemyShieldCheck>().countPRightSw > 0 ||
               EnemyAttackObject.GetComponent<EnemyShieldCheck>().countPRightBo > 0 ||
               EnemyAttackObject.GetComponent<EnemyShieldCheck>().countPDLeftSwU > 0 ||
               EnemyAttackObject.GetComponent<EnemyShieldCheck>().countPDLeftBoU > 0 ||
               EnemyAttackObject.GetComponent<EnemyShieldCheck>().countPDRightSwU > 0 ||
               EnemyAttackObject.GetComponent<EnemyShieldCheck>().countPDRightBoU > 0 ||
               EnemyAttackObject.GetComponent<EnemyShieldCheck>().countPDLeftSwD > 0 ||
               EnemyAttackObject.GetComponent<EnemyShieldCheck>().countPDLeftBoD > 0 ||
               EnemyAttackObject.GetComponent<EnemyShieldCheck>().countPDRightSwD > 0 ||
               EnemyAttackObject.GetComponent<EnemyShieldCheck>().countPDRightBoD > 0;
    }
}




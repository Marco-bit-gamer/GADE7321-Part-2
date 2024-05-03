using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordCheck : MonoBehaviour
{
    public string targetTagSw = "PlayerSword"; // Set to the tag of the enemy sword
    public string targetTagBo = "PlayerBow"; // Set to the tag of the enemy bow
    public string targetTagSh = "PlayerShield"; // Set to the tag of the enemy shield
    public float maxRaycastDistance = 2f; // Set the maximum distance my the raycast

    public int countPLeftSw = 0;
    public int countPRightSw = 0;
    public int countPLeftBo = 0;
    public int countPRightBo = 0;
    public int countPLeftSh = 0;
    public int countPRightSh = 0;

    void Update()
    {
        countPLeftSw = 0;
        countPRightSw = 0;
        countPLeftBo = 0;
        countPRightBo = 0;
        countPLeftSh = 0;
        countPRightSh = 0;

        // Check left direction
        RaycastHit2D[] hitsLeft = Physics2D.RaycastAll(transform.position, Vector2.left, maxRaycastDistance);
        foreach (RaycastHit2D hit in hitsLeft)
        {
            if (hit.collider != null && hit.collider.CompareTag(targetTagSw))
            {
                countPLeftSw++;
            }

            if (hit.collider != null && hit.collider.CompareTag(targetTagBo))
            {
                countPLeftBo++;
            }

            if (hit.collider != null && hit.collider.CompareTag(targetTagSh))
            {
                countPLeftSh++;
            }
        }

        // Check right direction
        RaycastHit2D[] hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right, maxRaycastDistance);
        foreach (RaycastHit2D hit in hitsRight)
        {
            if (hit.collider != null && hit.collider.CompareTag(targetTagSw))
            {
                countPRightSw++;
            }

            if (hit.collider != null && hit.collider.CompareTag(targetTagBo))
            {
                countPRightBo++;
            }

            if (hit.collider != null && hit.collider.CompareTag(targetTagSh))
            {
                countPRightSh++;
            }
        }
    }
}



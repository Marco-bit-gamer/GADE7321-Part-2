using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordCheck : MonoBehaviour
{
    public string targetTagSw = "EnemySword"; // Set to the tag of the enemy sword
    public string targetTagBo = "EnemyBow"; // Set to the tag of the enemy bow
    public string targetTagSh = "EnemyShield"; // Set to the tag of the enemy shield
    public float maxRaycastDistance = 2f; // Set the maximum distance my the raycast

    public int countELeftSw = 0;
    public int countERightSw = 0;
    public int countELeftBo = 0;
    public int countERightBo = 0;
    public int countELeftSh = 0;
    public int countERightSh = 0;

    void Update()
    {
        countELeftSw = 0;
        countERightSw = 0;
        countELeftBo = 0;
        countERightBo = 0;
        countELeftSh = 0;
        countERightSh = 0;

        // Check left direction
        RaycastHit2D[] hitsLeft = Physics2D.RaycastAll(transform.position, Vector2.left, maxRaycastDistance);
        foreach (RaycastHit2D hit in hitsLeft)
        {
            if (hit.collider != null && hit.collider.CompareTag(targetTagSw))
            {
                countELeftSw++;
            }

            if (hit.collider != null && hit.collider.CompareTag(targetTagBo))
            {
                countELeftBo++;
            }

            if (hit.collider != null && hit.collider.CompareTag(targetTagSh))
            {
                countELeftSh++;
            }
        }

        // Check right direction
        RaycastHit2D[] hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right, maxRaycastDistance);
        foreach (RaycastHit2D hit in hitsRight)
        {
            if (hit.collider != null && hit.collider.CompareTag(targetTagSw))
            {
                countERightSw++;
            }

            if (hit.collider != null && hit.collider.CompareTag(targetTagBo))
            {
                countERightBo++;
            }

            if (hit.collider != null && hit.collider.CompareTag(targetTagSh))
            {
                countERightSh++;
            }
        }
    }
}


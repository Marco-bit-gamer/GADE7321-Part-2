using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldCheck : MonoBehaviour
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
    public int countEDLeftSwU = 0;
    public int countEDRightSwU = 0;
    public int countEDLeftBoU = 0;
    public int countEDRightBoU = 0;
    public int countEDLeftShU = 0;
    public int countEDRightShU = 0;
    public int countEDLeftSwD = 0;
    public int countEDRightSwD = 0;
    public int countEDLeftBoD = 0;
    public int countEDRightBoD = 0;
    public int countEDLeftShD = 0;
    public int countEDRightShD = 0;

    void Update()
    {
        countELeftSw = 0;
        countERightSw = 0;
        countELeftBo = 0;
        countERightBo = 0;
        countELeftSh = 0;
        countERightSh = 0;
        countEDLeftSwU = 0;
        countEDRightSwU = 0;
        countEDLeftBoU = 0;
        countEDRightBoU = 0;
        countEDLeftShU = 0;
        countEDRightShU = 0;
        countEDLeftSwD = 0;
        countEDRightSwD = 0;
        countEDLeftBoD = 0;
        countEDRightBoD = 0;
        countEDLeftShD = 0;
        countEDRightShD = 0;

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

        countEDLeftSwU = CountHitsInDirection(-1, 1.5f);
        countEDRightSwU = CountHitsInDirection(1, -1.5f);
        countEDLeftBoU = CountHitsInDirection(-1, 1.5f);
        countEDRightBoU = CountHitsInDirection(1, -1.5f);
        countEDLeftShU = CountHitsInDirection(-1, 1.5f);
        countEDRightShU = CountHitsInDirection(1, -1.5f);

        countEDLeftSwU = CountHitsInDirection(1, 1.5f);
        countEDRightSwU = CountHitsInDirection(-1, -1.5f);
        countEDLeftBoU = CountHitsInDirection(1, 1.5f);
        countEDRightBoU = CountHitsInDirection(-1, -1.5f);
        countEDLeftShU = CountHitsInDirection(1, 1.5f);
        countEDRightShU = CountHitsInDirection(-1, -1.5f);

    }
    private int CountHitsInDirection(float directionX, float directionY)
    {
        int count = 0;
        Vector2 raycastDirection = new Vector2(directionX, directionY).normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, raycastDirection, maxRaycastDistance);
        Debug.DrawRay(transform.position, raycastDirection * maxRaycastDistance, Color.red);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag(targetTagSw))
                    count++;
                else if (hit.collider.CompareTag(targetTagBo))
                    count++;
                else if (hit.collider.CompareTag(targetTagSh))
                    count++;
            }
        }
        return count;
    }
}



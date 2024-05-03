using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowCheck : MonoBehaviour
{
    public string targetTagSw = "EnemySword"; // Set to the tag of the enemy sword
    public string targetTagBo = "EnemyBow"; // Set to the tag of the enemy bow
    public string targetTagSh = "EnemyShield"; // Set to the tag of the enemy shield
    public float maxRaycastDistance = 5f; // Set the maximum distance my the raycast

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

    private void Update()
    {
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

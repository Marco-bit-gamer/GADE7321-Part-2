using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBowCheck : MonoBehaviour
{
    public string targetTagSw = "PlayerSword"; // Set to the tag of the enemy sword
    public string targetTagBo = "PlayerBow"; // Set to the tag of the enemy bow
    public string targetTagSh = "PlayerShield"; // Set to the tag of the enemy shield
    public float maxRaycastDistance = 5f; // Set the maximum distance my the raycast

    public int countPDLeftSwU = 0;
    public int countPDRightSwU = 0;
    public int countPDLeftBoU = 0;
    public int countPDRightBoU = 0;
    public int countPDLeftShU = 0;
    public int countPDRightShU = 0;

    public int countPDLeftSwD = 0;
    public int countPDRightSwD = 0;
    public int countPDLeftBoD = 0;
    public int countPDRightBoD = 0;
    public int countPDLeftShD = 0;
    public int countPDRightShD = 0;

    private void Update()
    {
        countPDLeftSwU = CountHitsInDirection(-1, 1.5f);
        countPDRightSwU = CountHitsInDirection(1, -1.5f);
        countPDLeftBoU = CountHitsInDirection(-1, 1.5f);
        countPDRightBoU = CountHitsInDirection(1, -1.5f);
        countPDLeftShU = CountHitsInDirection(-1, 1.5f);
        countPDRightShU = CountHitsInDirection(1, -1.5f);

        countPDLeftSwU = CountHitsInDirection(1, 1.5f);
        countPDRightSwU = CountHitsInDirection(-1, -1.5f);
        countPDLeftBoU = CountHitsInDirection(1, 1.5f);
        countPDRightBoU = CountHitsInDirection(-1, -1.5f);
        countPDLeftShU = CountHitsInDirection(1, 1.5f);
        countPDRightShU = CountHitsInDirection(-1, -1.5f);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinE : MonoBehaviour
{
    public string targetTag = "EnemyShield"; // Set to the tag of the pentagon objects I want to detect
    public float maxRaycastDistance = 5f; // Set the maximum distance my the raycast
    public GameObject WinPanel;

    private int countLeft = 0;
    private int countRight = 0;
    private int countDiagLeft = 0;
    private int countDiagRight = 0;

    void Update()
    {
        countLeft = 0;
        countRight = 0;
        countDiagLeft = 0;
        countDiagRight = 0;

        if (countLeft == 3 || countRight == 3 || countDiagLeft == 3 || countDiagRight == 3)
        {
            WinPanel.SetActive(true);
        }

        // Check left direction
        RaycastHit2D[] hitsLeft = Physics2D.RaycastAll(transform.position, Vector2.left, maxRaycastDistance);
        foreach (RaycastHit2D hit in hitsLeft)
        {
            if (hit.collider != null && hit.collider.CompareTag(targetTag))
            {
                countLeft++;
            }
            Debug.DrawRay(transform.position, Vector2.left * maxRaycastDistance, Color.red);
        }

        // Check right direction
        RaycastHit2D[] hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right, maxRaycastDistance);
        foreach (RaycastHit2D hit in hitsRight)
        {
            if (hit.collider != null && hit.collider.CompareTag(targetTag))
            {
                countRight++;
            }
        }

        // Check diagonally left direction
        RaycastHit2D[] hitsDiagLeft = Physics2D.RaycastAll(transform.position, new Vector2(-1, 1), maxRaycastDistance);
        foreach (RaycastHit2D hit in hitsDiagLeft)
        {
            if (hit.collider != null && hit.collider.CompareTag(targetTag))
            {
                countDiagLeft++;
            }
        }

        // Check diagonally right direction
        RaycastHit2D[] hitsDiagRight = Physics2D.RaycastAll(transform.position, new Vector2(1, 1), maxRaycastDistance);
        foreach (RaycastHit2D hit in hitsDiagRight)
        {
            if (hit.collider != null && hit.collider.CompareTag(targetTag))
            {
                countDiagRight++;
            }
        }
    }
}

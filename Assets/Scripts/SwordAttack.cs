using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwordAttack : MonoBehaviour
{
    public float attackRadius = 2f;
    public float swingDuration = 0.5f; // This controls how long the animation plays
    public float damageDelay = 0.5f;   // New variable to delay damage after animation begins
    public Animator animator;
    public Text scoreText;

    private int score = 0;

    void Start()
    {
        UpdateScoreUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(SwingAndCheck());
        }
    }

    IEnumerator SwingAndCheck()
    {
        animator.Play("SwingSword"); // or use SetTrigger if your Animator uses a trigger
        yield return new WaitForSeconds(damageDelay); // Wait this long before applying damage

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= attackRadius)
            {
                Destroy(enemy);
                score += 1;
                UpdateScoreUI();
            }
        }

        yield return new WaitForSeconds(swingDuration - damageDelay); // Wait for rest of animation if needed
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public Collider2D attackCollider;

    public float damage = 3;
    public enum AttackDirection { left, right }

    public AttackDirection attackDirection;

    Vector2 rightAttackOffset;

    private void Start() => rightAttackOffset = transform.position;
    

    public void AttackRight()
    {
        attackCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        attackCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack() => attackCollider.enabled = false;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                Player player = GetComponentInParent<Player>();

                if (player != null && player.collectedRocks > 0)
                {
                    enemy.Health -= damage;
                    player.collectedRocks--;
                    player.slimesKilled++;
                }
            }
        }
    }
}

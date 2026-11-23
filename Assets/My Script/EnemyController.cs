using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f; // 敌人移动速度
    public float detectionRange = 10f; // 敌人检测主角范围
    public float attackRange = 1f; // 敌人攻击范围
    public float health = 50f; // 敌人血量
    public Animator animator; // 敌人动画组件引用
    public GameObject deathEffectPrefab; // 死亡特效预制体
    public AudioSource deathSound; // 死亡音效
    public Action OnDeath; // 敌人死亡事件
    public HealthBar healthBar; // 敌人血量条脚本引用

    private Transform playerTransform;
    private bool isMoving;
    private bool isAttacking;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // 假设主角标签为“Player”
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer > attackRange)
            {
                isMoving = true;
                isAttacking = false;
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                isMoving = false;
                isAttacking = true;
                AttackPlayer();
            }
        }
        else
        {
            isMoving = false;
            isAttacking = false;
        }

        // 更新动画参数
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsAttacking", isAttacking);
    }

    void AttackPlayer()
    {
        // 假设敌人通过接触主角来攻击
        playerTransform.GetComponent<PlayerController>().TakeDamage(10f);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        healthBar.TakeDamage(damage); // 调用血量条脚本的TakeDamage方法
    }

    void Die()
    {
        // 播放死亡音效
        deathSound.Play();

        // 实例化死亡特效
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

        // 触发死亡事件
        OnDeath?.Invoke();

        // 禁用敌人控制器
        enabled = false;
    }
 
}
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 主角移动速度
    public float rotationSpeed = 100f; // 主角旋转速度
    public float health = 100f; // 主角血量
    public Animator animator; // 主角动画组件引用
    public Transform gunMuzzle; // 枪口位置
    public GameObject bulletPrefab; // 子弹预制体
    public Transform bulletSpawn; // 子弹发射位置
    public float bulletSpeed = 10f; // 子弹速度
    public float bulletDamage = 20f; // 子弹伤害
    public GameObject deathEffectPrefab; // 死亡特效预制体
    public AudioSource shootSound; // 射击音效
    public AudioSource hitSound; // 受伤音效
    public AudioSource deathSound; // 死亡音效
    public GameObject healthUI; // 血量UI对象

    private Vector3 moveDirection;
    private bool isMoving;
    private bool isShooting;

    void Update()
    {
        // 获取鼠标位置，计算主角旋转方向
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 targetPosition = ray.GetPoint(rayDistance);
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 处理移动输入
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moveDirection = new Vector3(x, 0, z).normalized;
        if (moveDirection != Vector3.zero)
        {
            isMoving = true;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            isMoving = false;
        }

        // 处理射击输入
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            Shoot();
        }
        else
        {
            isShooting = false;
        }

        // 更新动画参数
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsShooting", isShooting);
    }

    void Shoot()
    {
        // 播放射击音效
        shootSound.Play();

        // 实例化子弹
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(gunMuzzle.forward * bulletSpeed, ForceMode.Impulse);
    }

    public void TakeDamage(float damage)
    {
        // 播放受伤音效
        hitSound.Play();

        health -= damage;
        healthUI.GetComponent<HealthUI>().UpdateHealth(health); // 假设血量UI脚本名为HealthUI

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 播放死亡音效
        deathSound.Play();

        // 实例化死亡特效
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

        // 停止所有动画
        animator.enabled = false;

        // 禁用主角控制器
        enabled = false;
    }
}
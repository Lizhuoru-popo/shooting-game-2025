using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // 子弹预制体
    public Transform bulletSpawn; // 子弹发射位置
    public float bulletSpeed = 10f; // 子弹速度
    public AudioSource shootSound; // 射击音效

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 鼠标左键点击
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 实例化子弹
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

        // 播放射击音效
        shootSound.Play();
    }
}
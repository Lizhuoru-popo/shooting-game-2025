using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 20f; // 子弹伤害
    public AudioSource hitSound; // 子弹击中音效

    void Start()
    {
        // 获取子弹预制体上的AudioSource组件
        hitSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet hit something!"); // 添加调试信息

        if (collision.gameObject.CompareTag("Enemy")) // 假设敌人的标签为"Enemy"
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage); // 调用敌人的TakeDamage方法

            // 播放击中音效
            hitSound.Play();

            // 销毁子弹
            Destroy(gameObject);
        }
    }
}
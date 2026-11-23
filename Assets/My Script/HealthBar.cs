using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar; // 血量条Image组件引用
    public float maxHealth = 100f; // 最大血量
    public float currentHealth; // 当前血量

    void Start()
    {
        currentHealth = maxHealth; // 初始化当前血量为最大血量
        UpdateHealthBar(); // 更新血量条显示
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthBar(); // 更新血量条显示

        if (currentHealth <= 0)
        {
            Die(); // 如果血量降到0或以下，调用死亡方法
        }
    }

    private void UpdateHealthBar()
    {
        // 根据当前血量更新血量条的填充比例
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void Die()
    {
        // 在这里添加死亡逻辑，例如播放死亡动画、显示死亡UI等
        Debug.Log("Entity is dead!");
    }
}
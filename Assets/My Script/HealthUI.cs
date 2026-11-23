using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthBar; // 血量条Image组件引用

    public void UpdateHealth(float health)
    {
        healthBar.fillAmount = health / 100f; // 假设最大血量为100
    }
}

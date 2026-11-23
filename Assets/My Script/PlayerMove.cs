using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 movement;
    private Rigidbody rig;
    private Animator anim;
    public HealthBar healthBar; // 主角血量条脚本引用
    private RaycastHit hitInfo;
    float raylength = 100f;
    int floorMask;
    // Start is called before the first frame update
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        floorMask = LayerMask.GetMask("Floor");
    }
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Move(h, v);
        Animating(h, v);
        Turning();
    }
    //主角移动
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rig.MovePosition(transform.position + movement);
    }
    //主角动画
    void Animating(float h, float v)
    {
        bool iswalking = h != 0 || v != 0;
        anim.SetBool("IsWalking", iswalking);
    }
    //主角转向
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camRay, out hitInfo, raylength, floorMask))
        {
            Vector3 playerToMouse = hitInfo.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rig.MoveRotation(newRotation);
        }
    }
    public void TakeDamage(float damage)
    {
        healthBar.TakeDamage(damage); // 调用血量条脚本的TakeDamage方法
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // 假设敌人的标签为"Enemy"
        {
            TakeDamage(10f); // 减少10点血量
        }
    }
}

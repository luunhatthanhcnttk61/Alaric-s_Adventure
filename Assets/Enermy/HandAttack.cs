// using UnityEngine;
// using System.Collections.Generic;

// public class HandAttack : MonoBehaviour
// {
//     public int damage = 10; // Lượng sát thương gây ra
//     private bool canDamage = false;
//     private HashSet<Collider> damagedTargets = new HashSet<Collider>();

//         void OnTriggerEnter(Collider other)
//         {
//             if (canDamage && other.CompareTag("Player") && !damagedTargets.Contains(other))
//             {
//                 HealthManager playerHealth = other.GetComponent<HealthManager>();
//                 if (playerHealth != null)
//                 {
//                     Vector3 direction = (other.transform.position - transform.position).normalized; // Hướng đẩy lùi
//                     playerHealth.HurtPlayer(damage, direction);
//                     damagedTargets.Add(other); // Đánh dấu rằng mục tiêu đã bị tấn công
//                     Debug.Log("Da tan cong player" + damage);
//                 }
//             }
//         }

//     void OnTriggerExit(Collider other)
//     {
//         if (damagedTargets.Contains(other))
//         {
//             damagedTargets.Remove(other); // Xóa dấu vết khi mục tiêu rời khỏi phạm vi va chạm
//         }
//     }

//     public void EnableDamage()
//     {
//         canDamage = true;
//     }

//     public void DisableDamage()
//     {
//         canDamage = false;
//         damagedTargets.Clear(); // Xóa tất cả dấu vết khi kết thúc tấn công
//     }
// }
using UnityEngine;
using System.Collections.Generic;

public class HandAttack : MonoBehaviour
{
    public int damage = 10; // Lượng sát thương gây ra
    private bool canDamage = false;
    private HashSet<Collider> damagedTargets = new HashSet<Collider>();
    public Collider handCollider;
    
    public void Start()
    {
        //handCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (canDamage && other.CompareTag("Player") && !damagedTargets.Contains(other))
        {
            HealthManager playerHealth = other.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                Vector3 direction = (other.transform.position - transform.position).normalized; // Hướng đẩy lùi
                playerHealth.HurtPlayer(damage, direction);
                damagedTargets.Add(other); // Đánh dấu rằng mục tiêu đã bị tấn công
                Debug.Log("Da tan cong player" + damage);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (damagedTargets.Contains(other))
        {
            damagedTargets.Remove(other); // Xóa dấu vết khi mục tiêu rời khỏi phạm vi va chạm
        }
    }

    public void EnableDamage()
    {
        canDamage = true;
        //handCollider.enabled = true;
    }

    public void DisableDamage()
    {
        canDamage = false;
        //handCollider.enabled = false;
        damagedTargets.Clear(); // Xóa tất cả dấu vết khi kết thúc tấn công
    }
}
    
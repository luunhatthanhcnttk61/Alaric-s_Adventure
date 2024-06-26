using UnityEngine;
using System.Collections; 

public class Sword : MonoBehaviour
{
    public float basicAttackCooldown = 1f; 
    public float comboAttackCooldown = 3f; 
    public float magicCooldown = 4f; 
    public float areaDamageCooldown = 7f; 

    public int basicAttackDamage = 30; 
    public int comboAttackDamage = 70;
    public int magicDamage = 90; 
    public int areaDamage = 150; 

    public int basicAttackManaCost = 10; 
    public int comboAttackManaCost = 30; 
    public int magicManaCost = 40; 
    public int areaDamageManaCost = 70;

    private float basicAttackTimer = 0f;
    private float comboAttackTimer = 0f;
    private float magicTimer = 0f;
    private float areaDamageTimer = 0f;
    private int swordDamage = 0;
    private int baseDamage = 0; // Thêm baseDamage để lưu giá trị gốc của sword

    public int currentMana = 300;
    public int maxMana = 300;
    private float manaRegenTimer = 0f;
    public int manaRegenRate = 5; 

    public Collider swordCollider; 

    void Start()
    {
        swordCollider.enabled = false; 
    }

    void Update()
    {
        basicAttackTimer -= Time.deltaTime;
        comboAttackTimer -= Time.deltaTime;
        magicTimer -= Time.deltaTime;
        areaDamageTimer -= Time.deltaTime;
        
        manaRegenTimer -= Time.deltaTime;
        if (manaRegenTimer <= 0)
        {
            if (currentMana < maxMana)
            {
                currentMana += manaRegenRate;
                currentMana = Mathf.Clamp(currentMana, 0, maxMana); 
            }
            manaRegenTimer = 1f; 
        }
    }

    public void BasicAttack()
    {
        if (basicAttackTimer <= 0 && currentMana >= basicAttackManaCost)
        {
            ReduceMana(basicAttackManaCost);
            swordDamage = basicAttackDamage + baseDamage;
            Debug.Log("Basic Attack! Damage: " + swordDamage);
            
            StartCoroutine(EnableSwordColliderTemporarily(1f));
            basicAttackTimer = basicAttackCooldown;
        }
    }

    public void ComboAttack()
    {
        if (comboAttackTimer <= 0 && currentMana >= comboAttackManaCost)
        {
            ReduceMana(comboAttackManaCost);
            swordDamage = comboAttackDamage + baseDamage;
            Debug.Log("Combo Attack! Damage: " + swordDamage);

            StartCoroutine(EnableSwordColliderTemporarily(2.5f));
            comboAttackTimer = comboAttackCooldown;
        }
    }

    public void Magic()
    {
        if (magicTimer <= 0 && currentMana >= magicManaCost)
        {
            ReduceMana(magicManaCost);
            swordDamage = magicDamage + baseDamage;
            Debug.Log("Magic! Damage: " + swordDamage);

            StartCoroutine(EnableSwordColliderTemporarily(1f));

            magicTimer = magicCooldown;
        }
    }

    public void AreaDamage()
    {
        if (areaDamageTimer <= 0 && currentMana >= areaDamageManaCost)
        {
            ReduceMana(areaDamageManaCost);
            swordDamage = areaDamage + baseDamage;
            Debug.Log("Area Damage! Damage: " + swordDamage);

            StartCoroutine(EnableSwordColliderTemporarily(3f));
            areaDamageTimer = areaDamageCooldown;
        }
    }

    public void UpdateSwordDamage(int newBaseDamage)
    {
        baseDamage = newBaseDamage;
        Debug.Log("Base sword damage updated: " + baseDamage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            EnermyAI botHealth = other.GetComponent<EnermyAI>();
            if (botHealth != null)
            {
                botHealth.TakeDamage(swordDamage);
            }
        }
    }

    public void ReduceMana(int amount)
    {
        currentMana -= amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana); 
    }

    private IEnumerator EnableSwordColliderTemporarily(float time)
    {
        swordCollider.enabled = true; 
        yield return new WaitForSeconds(time); 
        swordCollider.enabled = false; 
    }
}

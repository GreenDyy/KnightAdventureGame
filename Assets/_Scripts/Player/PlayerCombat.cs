using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat instance;
    public Animator myAnimator;
    public Transform attackPoint;
    public Transform attackRangePoint;
    //Attack & damage
    public float attackRange = 0.9f;
    public LayerMask enemyLayers;
    public float attackRate = 3f;
    private float nextAttackTime = 0f;
    public GameObject clover;
    public int amountOfClover = 5;
    //Type damage
    public int normalAttackDamage = 21;
    public int strongAttackDamage = 42;
    //Stamina
    public StaminaBarPlayer staminaBar;
    public int maxStamina = 100;
    public float curStamina;
    public int costNormalAttack = 15;
    public int costStrongAttack = 30;
    private int staminaRecoveryRate = 20;


    private void Reset()
    {
        attackPoint = transform.Find("AttackPoint");
    }

    private void Awake()
    {
        instance = this;
        myAnimator = GetComponent<Animator>();
        enemyLayers = LayerMask.GetMask("Enemy");
        attackPoint = transform.Find("AttackPoint");
        attackRangePoint = transform.Find("AttackRangePoint");
        staminaBar = GameObject.Find("Canvas/StaminaBarPlayer").GetComponent<StaminaBarPlayer>();
        clover = GameObject.Find("CloverAmmo");
        clover.SetActive(false);
    }
    private void Start()
    {
        curStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        amountOfClover = CloverCounter.instance.startClovers;
    }
    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    StrongAttack();
                }
                else
                {
                    NormalAttack();
                }
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetMouseButtonDown(1) && amountOfClover > 0)
            {
                CloverAttack();
            }
        }
        
    }
    private void FixedUpdate()
    {
        RecoverStamina();
    }

    private void RecoverStamina()
    {
        if (curStamina < maxStamina)
        {
            curStamina += staminaRecoveryRate * Time.fixedDeltaTime;
            staminaBar.SetStamina((int)curStamina);
        }
        else
        {
            curStamina = maxStamina;
        }
    }

    void NormalAttack()
    {
        if (curStamina < costNormalAttack)
        {
            Debug.Log("Không đủ stamina để thực hiện");
        }
        else
        {
            curStamina -= costNormalAttack;
            staminaBar.SetStamina((int)curStamina);
            //trong này sẽ kích hoạt animation
            myAnimator.SetTrigger("NormalAttack");
            AudioManager.instance.PlaySFX(AudioManager.instance.playerSlash);
            //phát hiện kẻ địch trong phạm vi
            Invoke(nameof(DelayNormalAttack), 0.2f);
        }
    }

    void DelayNormalAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        if (hitEnemies.Length > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<DamageReceiver>().TakeDamage(normalAttackDamage);
                Debug.Log("Bạn vừa thực hiện NormalAttack gây " + normalAttackDamage + " sát thương lên " + enemy.name);
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                if (PlayerMovement.instance.isRight)
                {
                    enemyRb.AddForce(Vector2.right * 4, ForceMode2D.Impulse);
                }
                else
                {
                    enemyRb.AddForce(Vector2.left * 4, ForceMode2D.Impulse);
                }
            }
        }
    }

    void StrongAttack()
    {
        if (curStamina < costStrongAttack)
        {
            Debug.Log("Không đủ stamina để thực hiện");
        }
        else
        {
            curStamina -= costStrongAttack;
            staminaBar.SetStamina((int)curStamina);
            //trong này sẽ kích hoạt animation
            myAnimator.SetTrigger("StrongAttack");
            AudioManager.instance.PlaySFX(AudioManager.instance.playerHeavySlash);
            //phát hiện kẻ địch trong phạm vi
            Invoke(nameof(DelayStrongAttack), 0.4f);

        }
    }

    void DelayStrongAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        if (hitEnemies.Length > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<DamageReceiver>().TakeDamage(strongAttackDamage);
                Debug.Log("Bạn vừa thực hiện StronglAttack gây " + strongAttackDamage + " sát thương lên " + enemy.name);
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                if (PlayerMovement.instance.isRight)
                {
                    enemyRb.AddForce(Vector2.right * 4, ForceMode2D.Impulse);
                }
                else
                {
                    enemyRb.AddForce(Vector2.left * 4, ForceMode2D.Impulse);
                }
            }
        }
    }

    void CloverAttack()
    {
        GameObject cloverClone = Instantiate(clover);
        cloverClone.SetActive(true);
        cloverClone.transform.position = attackRangePoint.position;
        AudioManager.instance.PlaySFX(AudioManager.instance.playerThrowClover);
        //đẩy đi
        Rigidbody2D cloverRb = cloverClone.GetComponent<Rigidbody2D>();
        if (PlayerMovement.instance.isRight )
        {
            cloverRb.velocity = new Vector2(20, cloverRb.velocity.y);
            cloverClone.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        }
        else
        {
            cloverRb.velocity = new Vector2(-20, cloverRb.velocity.y);
            cloverClone.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        }
        myAnimator.SetTrigger("TriggerThrow");
        amountOfClover--;
        CloverCounter.instance.DecreaseClovers(1);
    }

    private void OnDrawGizmos()// just show in scene
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); //tâm vẽ và bán kính
    }

}

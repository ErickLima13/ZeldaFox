using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private CharacterController characterController;
    private Animator animator;

    [Header("Config Player")]
    public float movementSpeed = 3f;

    [Header("Attack Config")]
    [SerializeField] private ParticleSystem fxAttack;
    private bool isAttack;
    [SerializeField] private Transform hitBox;
    [Range(0.2f, 1f)] [SerializeField] private float hitRange = 0.5f;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private Collider[] hitInfo;
    [SerializeField] private int amountDamage;


    private Vector3 direction;

    private bool isWalk;
  
    private void Initialization()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();
        UpdateAnimator();
    }

    #region My Metods
    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f) //Rotation
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }

        characterController.Move(movementSpeed * Time.deltaTime * direction);
    }

    private void Attack()
    {
        if (!isAttack)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                isAttack = true;
                animator.SetTrigger("Attack");
                fxAttack.Emit(1);

                hitInfo = Physics.OverlapSphere(hitBox.position, hitRange,hitMask);

                foreach(Collider c in hitInfo)
                {
                    c.gameObject.SendMessage("GetHit", amountDamage, SendMessageOptions.DontRequireReceiver);
                }

            }
        }
       
    }

    private void UpdateAnimator()
    {
        animator.SetBool("isWalk", isWalk);
    }

    private void AttackIsDone()
    {
        isAttack = false;
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        if(hitBox != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitBox.position, hitRange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "":
                
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "":
               
                break;
        }
    }


}

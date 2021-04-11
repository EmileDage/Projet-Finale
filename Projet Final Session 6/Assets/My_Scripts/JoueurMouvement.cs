using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class JoueurMouvement : MonoBehaviour{

    public CharacterController controller;
    public float speed = 6;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float vertical = 0;
    float horizontal = 0;

    [SerializeField] GameObject animator_obj;
    private Animator animator;

    //si on veut que la cam suit le joueur et determine sa direction
    public Transform cam;

    public void Start()
    {
        animator = animator_obj.gameObject.GetComponent<Animator>();
        if (animator != null) {
            Debug.Log("animator is assigned");
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        horizontal = value.x;
        vertical = value.y;
    }


    private void Update()
    {      
        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0) {
            animator.SetBool("IsMoving", true);
        }else
            animator.SetBool("IsMoving", false);


        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        { //si input to move
            //rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //mouvement
            Vector3 movDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(movDirection.normalized * speed * Time.deltaTime);
        }
    }
}

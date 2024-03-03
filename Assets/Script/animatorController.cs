using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class animatorController : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");
        float horizontal = Utils.GetHorizontal(this.name);
        float vertical = Utils.GetVertical(this.name);
        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }

        // if click left mouse button, triger TriggerAttack
        if (Utils.GetTriggerAttack(this.name))
        {
            animator.SetTrigger("TriggerAttack");
        }
    }
}

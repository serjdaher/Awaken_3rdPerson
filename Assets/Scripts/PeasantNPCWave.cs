using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PeasantNPCWave : MonoBehaviour


{
    private Animator animator;
    public GameObject exclamation;
    public Canvas pressE;

    public GameObject test;

    private GameObject npcHeadAim;
    private MultiAimConstraint aim;

    private float targetweight;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        pressE.enabled = true;
        animator.SetBool("IsBow", false);

        // Set NPCHeadAim Rig weight.
        npcHeadAim = GameObject.Find("NPCHeadAim");
        aim = npcHeadAim.GetComponent<MultiAimConstraint>();
        targetweight = 0f;
    }

    private void Update()
    {
        aim.weight = Mathf.Lerp(aim.weight, targetweight, Time.deltaTime * 10f);

        if (Input.GetKeyDown(KeyCode.E))
        {
            pressE.enabled = false;
            animator.SetBool("IsBow", true);
            StartCoroutine(SetFalse());
            StartCoroutine(SetTrue());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            test.SetActive(true);
            exclamation.SetActive(true);
            animator.SetBool("IsWaving", true);
            StartCoroutine(SetFalse());

            //NPC Rig Multi-Constraint change to 1 to follow Player upon trigger.
            targetweight = 1f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("IsWaving", false);
        exclamation.SetActive(false);
        test.SetActive(false);
        targetweight = 0f;
    }

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(2); // wait for 2 seconds.
        animator.SetBool("IsWaving", false);
        animator.SetBool("IsBow", false);
    }

    IEnumerator SetTrue()
    {
        yield return new WaitForSeconds(2.5f); // wait for 5 seconds.
        pressE.enabled = true;
    }
}

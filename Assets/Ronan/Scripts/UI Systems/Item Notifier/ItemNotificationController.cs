using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemNotificationController : MonoBehaviour
{
    public TMP_Text ItemNamePanel;
    public Image ButtonIcon;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetItemName(string itemName)
    {
        animator.ResetTrigger("Hide");
        animator.SetTrigger("Show");
        ItemNamePanel.text = itemName;
    }


    public void Hide()
    {
        if (gameObject.activeSelf)
        {

            animator.ResetTrigger("Show");
            animator.SetTrigger("Hide");
            StartCoroutine(DeactivateIn(1f));
        }
    }

    IEnumerator DeactivateIn(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}

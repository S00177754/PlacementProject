using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationElement : MonoBehaviour
{
    public Image NotificationPanel;
    public Image NotificationIcon;
    public TMP_Text Text;
    public Animator animator;

    private void Start()
    {
        Invoke("Destroy", 2);
    }

    public void SetNotification(string text, Sprite icon, Color panelColor, Animator animate)
    {
        Text.text = text;
        NotificationIcon.sprite = icon;
        animator = animate;

        Color color = panelColor;
        color.a = 0.6f;
        NotificationPanel.color = color;
    }

    private void Destroy()
    {
        animator.SetTrigger("Hide");
        StartCoroutine(DestroyIn(1.4f));
    }

    IEnumerator DestroyIn(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    #region "singleton"
    public static PlayerHealth Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }
    #endregion

    private int health;
    private int maxHealth = 5;

    public int GetHealth() { return health; }

    [SerializeField] private Image fill;
    [SerializeField] private RectTransform fillSize;
    [SerializeField] private Gradient gradient;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        // If less than 0 = 0 / else if > maxlife = maxlife / else life - amount
        health = health - amount <= 0 ? 0 : health - amount > maxHealth ? maxHealth : health - amount;

        Vector3 CurrentScale = fillSize.localScale;
        CurrentScale.x = (float)health / (float)maxHealth;
        fillSize.localScale = CurrentScale;
        fill.color = gradient.Evaluate(CurrentScale.x);

        PlayerScript.Instance.GetAnimator().SetTrigger("TakingDamage");
        PlayerMovement.Instance.enabled = false;
        StartCoroutine(AbleToMove());
    }

    private IEnumerator AbleToMove()
    {
        yield return new WaitForSeconds(1.2f);
        PlayerMovement.Instance.enabled = true;
    }
}

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
    [SerializeField] private GameObject dizzy;

    private float timer;
    private bool canTakeDamage = true;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (canTakeDamage)
        {
            // If less than 0 = 0 / else if > maxlife = maxlife / else life - amount
            health = health - amount <= 0 ? 0 : health - amount > maxHealth ? maxHealth : health - amount;

            Vector3 CurrentScale = fillSize.localScale;
            CurrentScale.x = (float)health / (float)maxHealth;
            fillSize.localScale = CurrentScale;
            fill.color = gradient.Evaluate(CurrentScale.x);

            if (health > 0)
                UnableToMove();
            else
                GameManager.Instance.Respawn();
        }
    }

    public void ResetLife()
    {
        health = maxHealth;
        Vector3 CurrentScale = fillSize.localScale;
        CurrentScale.x = 1;
        fillSize.localScale = CurrentScale;
        fill.color = gradient.Evaluate(1);
    }

    private void UnableToMove()
    {
        StartCoroutine(ArriereSarrasin(GetComponent<Transform>().position));

        PlayerScript.Instance.GetAnimator().SetTrigger("TakingDamage");
        PlayerMovement.Instance.enabled = false;
        dizzy.SetActive(true);
        canTakeDamage = false;
        StartCoroutine(AbleToMove());
    }

    private IEnumerator ArriereSarrasin(Vector3 start)
    {
        while (timer < 1.5f)
        {
            timer += Time.deltaTime;
            GetComponent<Transform>().position = Vector3.Lerp(start, new Vector3(start.x, start.y, start.z - 2), timer);
            yield return null;
        }
        timer = 0;
    }

    private IEnumerator AbleToMove()
    {
        yield return new WaitForSeconds(1.2f);
        PlayerMovement.Instance.enabled = true;
        dizzy.SetActive(false);
        canTakeDamage = true;
    }
}

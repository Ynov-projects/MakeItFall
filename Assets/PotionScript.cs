using UnityEngine;
using UnityEngine.UI;

public class PotionScript : MonoBehaviour
{
    public ScriptablePotion potion;

    public bool alreadyCollected;

    [SerializeField] private bool throwable;

    private void OnCollisionEnter(Collision collision)
    {
        if (throwable)
        {
            potion.Quantity--;
            UIManager.Instance.DisplayPotions();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
        else if (collision.transform.tag == "Player")
        {
            potion.Quantity++;
            GameManager.Instance.AppearInfo(potion.Id, potion.Name, potion.Description);
            gameObject.SetActive(false);
        }
    }
}

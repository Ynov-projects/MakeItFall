using UnityEngine;
using UnityEngine.UI;

public class PotionScript : MonoBehaviour
{
    public ScriptablePotion potion;

    public bool alreadyCollected;

    [SerializeField] private bool throwable;

    private void OnCollisionEnter(Collision collision)
    {
        if (throwable && collision.transform.tag != "Player")
        {
            switch (potion.Effect)
            {
                case "Unique":
                    UniqueEffect();
                    break;
                case "Zone":
                    ZoneEffect();
                    break;
                case "Global":
                    GlobalEffect();
                    break;
                default:
                    break;
            }
            potion.Quantity--;
            UIManager.Instance.DisplayPotions();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
        else if (!throwable && collision.transform.tag == "Player")
        {
            potion.Quantity++;
            GameManager.Instance.AppearInfo(potion.Id, potion.Name, potion.Description);
            gameObject.SetActive(false);
        }
    }

    private void UniqueEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Reversable"))
                nearbyObject.GetComponent<ReverseOneGravity>().StartStartSpell();
        }
    }

    private void ZoneEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Reversable"))
            {
                nearbyObject.GetComponent<ReverseOneGravity>().StartStartSpell();
            }
        }
    }

    private void GlobalEffect()
    {
        /*GetComponent<InverseGravityScript>().ReverseAllGravity();*/
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Reversable"))
            {
                nearbyObject.GetComponent<InverseGravityScript>().ReverseAllGravity();
            }
        }
    }
}
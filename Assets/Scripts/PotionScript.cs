using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PotionScript : MonoBehaviour
{
    public ScriptablePotion potion;

    public bool alreadyCollected;

    [SerializeField] private ParticleSystem ps;

    [SerializeField] private bool throwable;

    private void OnCollisionEnter(Collision collision)
    {
        if (throwable && collision.transform.tag != "Player" && potion.Quantity > 0)
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
            StartCoroutine(PlayingParticleSystem());
        }
        else if (!throwable && collision.transform.tag == "Player")
        {
            potion.Quantity++;
            GameManager.Instance.AppearInfo(potion.Id, potion.Name, potion.Description);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayingParticleSystem()
    {
        ps.Play();
        yield return new WaitForSeconds(.15f);
        gameObject.SetActive(false);
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
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
        GameManager.Instance.StartSpell();
    }
}
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    public ScriptablePotion potion;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("oups je suis tombé chef");
        potion.Quantity--;
        UIManager.Instance.DisplayPotions();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}

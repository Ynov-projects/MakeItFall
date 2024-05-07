using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] prefabPotions;

    [SerializeField] private GameObject pnlPotion;
    [SerializeField] private Text pnlTitle;
    [SerializeField] private Text pnlDesc;

    [SerializeField] private float zForce;

    public int selectedPotion { get; private set; }

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;
    }

    private void Start()
    {
        selectedPotion = 0;
        foreach (var potion in prefabPotions)
            potion.GetComponent<PotionScript>().potion.Quantity = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) ThrowPotion();

        if (Input.GetKeyDown(KeyCode.K)) StartSpell();
    }

    private void AddPotion(int potion)
    {
        prefabPotions[potion].GetComponent<PotionScript>().potion.Quantity++;
        UIManager.Instance.DisplayPotions();
    }

    public void AppearInfo(int id, string title, string desc)
    {
        if (!prefabPotions[id].GetComponent<PotionScript>().alreadyCollected)
        {
            // Code pour mettre en haut à droite de l'écran
            pnlPotion.SetActive(true);
            pnlTitle.text = title;
            pnlDesc.text = desc;
            StartCoroutine(StopAnimation());
            prefabPotions[id].GetComponent<PotionScript>().alreadyCollected = true;
        }
    }

    private IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(2f);
        pnlPotion.SetActive(false);
    }

    public void ThrowPotion()
    {
        GameObject potion = prefabPotions[selectedPotion];
        if (potion.GetComponent<PotionScript>().potion.Quantity > 0)
        {
            potion.SetActive(true);
            potion.transform.localPosition = new Vector3(0, 1.5f, .7f);
            potion.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, zForce));
        }
    }

    public void ChangePotion(int _selectedPotion)
    {
        selectedPotion = _selectedPotion % prefabPotions.Length;
    }

    public void ReverseAllGravity()
    {
        Physics.gravity = new Vector3(0, Physics.gravity.y > 1f ? -9.8f : 9.8f, 0);
        PlayerMovement.Instance.rotatePlayer();
    }
    public IEnumerator InvokingSpell()
    {
        ReverseAllGravity();
        yield return new WaitForSeconds(5f);
        ReverseAllGravity();
    }

    public void StartSpell()
    {
        StartCoroutine(InvokingSpell());
    }
}

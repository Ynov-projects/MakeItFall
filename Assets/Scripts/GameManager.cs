using NUnit.Framework;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] prefabPotions;

    public int selectedPotion { get; private set; }

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null) Destroy(Instance);
        Instance = this;
    }

    private void Start()
    {
        selectedPotion = 0;
        foreach(var potion in prefabPotions)
            potion.GetComponent<PotionScript>().potion.Quantity = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) ThrowPotion();

        if (Input.GetKeyDown(KeyCode.K)) AddPotion(0);
        if (Input.GetKeyDown(KeyCode.L)) AddPotion(1);
        if (Input.GetKeyDown(KeyCode.M)) AddPotion(2);
    }

    public void ThrowPotion()
    {
        GameObject potion = prefabPotions[selectedPotion];
        if (potion.GetComponent<PotionScript>().potion.Quantity > 0)
        {
            potion.SetActive(true);
            potion.transform.localPosition = new Vector3(0, -1, 0);
            potion.GetComponent<Rigidbody>().AddForce(new Vector3(-300, 0, 0));
        }
    }

    private void AddPotion(int potion)
    {
        prefabPotions[0].GetComponent<PotionScript>().potion.Quantity++;
        UIManager.Instance.DisplayPotions();
    }

    public void ChangePotion(int _selectedPotion)
    {
        selectedPotion = _selectedPotion % prefabPotions.Length;
    }
}

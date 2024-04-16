using NUnit.Framework;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ScriptablePotion[] potions;

    public int selectedPotion { get; private set; }

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null) Destroy(Instance);
        Instance = this;
    }

    private void Start()
    {
        foreach(var potion in potions)
            potion.Quantity = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) potions[0].Quantity++;
        if (Input.GetKeyDown(KeyCode.L)) potions[1].Quantity++;
        if (Input.GetKeyDown(KeyCode.M)) potions[2].Quantity++;
        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.M)) UIManager.Instance.DisplayPotions();
    }

    public void ChangePotion(int _selectedPotion)
    {
        selectedPotion = _selectedPotion % potions.Length;
    }
}

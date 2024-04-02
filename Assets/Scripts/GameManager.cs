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

    public void ChangePotion(int _selectedPotion)
    {
        selectedPotion = _selectedPotion % potions.Length;
    }
}

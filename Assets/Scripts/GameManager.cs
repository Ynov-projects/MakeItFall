using NUnit.Framework;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ScriptablePotion[] potions;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null) Destroy(Instance);
        Instance = this;
    }
}

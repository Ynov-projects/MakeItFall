using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] prefabPotions;

    [SerializeField] private GameObject pnlPotion;
    [SerializeField] private Text pnlTitle;
    [SerializeField] private Text pnlDesc;

    [SerializeField] private float zForce;

    private Vector3 spawnPosition;
    [SerializeField] private Transform player;
    private Dictionary<GameObject, Vector3> potions;
    private Dictionary<GameObject, Vector3> reversables;

    public static Player input;

    public int selectedPotion { get; private set; }

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;

        input = new Player();
        input.Gameplay.Enable();
    }

    private void Start()
    {
        selectedPotion = 0;
        spawnPosition = player.position;
        ResetPotions();

        potions = new Dictionary<GameObject, Vector3>();
        GameObject[] allPotions = GameObject.FindGameObjectsWithTag("Potion");
        foreach (GameObject o in allPotions)
            potions.Add(o, o.transform.position);

        reversables = new Dictionary<GameObject, Vector3>();
        GameObject[] everyReversables = GameObject.FindGameObjectsWithTag("Reversable");
        foreach (GameObject o in everyReversables)
            reversables.Add(o, o.transform.position);
    }

    private void Update()
    {
        if (input.Gameplay.LaunchPotion.triggered) ThrowPotion();
        if (input.Gameplay.Respawn.triggered) Respawn();
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

    public void ChangeCheckpoint(Vector3 position)
    {
        spawnPosition = position;
    }

    private void ResetPotions()
    {
        foreach (var potion in prefabPotions)
            potion.GetComponent<PotionScript>().potion.Quantity = 0;
    }

    public void Respawn()
    {
        player.position = spawnPosition;
        ResetPotions();
        PlayerHealth.Instance.ResetLife();


        foreach (KeyValuePair<GameObject, Vector3> potion in potions)
            if (potion.Key.transform.position.z >= spawnPosition.z && !potion.Key.activeSelf)
            {
                potion.Key.SetActive(true);
                potion.Key.transform.position = potion.Value;
            }

        foreach (KeyValuePair<GameObject, Vector3> reversable in reversables)
            reversable.Key.transform.position = reversable.Value;
    }
}

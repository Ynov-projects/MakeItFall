using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region "singleton"
    public static PlayerScript Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }
    #endregion

    [SerializeField] private Animator animator;

    public Animator GetAnimator() { return animator; }
}

using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] protected int scoreAmount;

    public static Action<int> addScore;
}

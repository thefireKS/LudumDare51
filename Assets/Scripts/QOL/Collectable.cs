using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    protected int scoreAmount;

    public static Action<int> AddScore;
}

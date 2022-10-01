using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PackageManager : MonoBehaviour
{
    [SerializeField] private Waypoint playerPointArrow;
    [SerializeField] private Sprite[] packageImages;
    [SerializeField] private GameObject[] itemInHands;
    [SerializeField] private GameObject[] houses;
    [SerializeField] private Image currentImage;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;
    private float timer;

    private void Start()
    {
        Restart();
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
    }

    private void FixedUpdate()
    {
        if (timer >= 10f)
            Restart();

        timer += Time.fixedDeltaTime;
    }

    private void PackageRoll()
    {
        int current = Random.Range(0, Mathf.Min(packageImages.Length, itemInHands.Length));

        currentImage.sprite = packageImages[current];

        for (int i = 0; i < itemInHands.Length; i++)
            itemInHands[i].SetActive(false);
        
        itemInHands[current].SetActive(true);
    }

    private void HouseRoll()
    {
        int current = Random.Range(0, houses.Length);
        houses[current].GetComponent<Delivery>().Selected();
        playerPointArrow.SetNewPoint(houses[current].transform);
    }

    private void Restart()
    {
        timer = 0f;
        PackageRoll();
        HouseRoll();
    }

    public void DeliverySucceed()
    {
        int bonus = (int) (10 - timer) % 10;
        score += 1 + bonus;

        Restart();
    }
}

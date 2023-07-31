using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ImageRandomPlacement[] randomPlacements;

    public int[] imageCount;

    public static GameManager instance;
    // Start is called before the first frame update


    // Update is called once per frame
    private void Awake()
    {
        instance = this;
    }

    public void ClearObj()
    {
        for (int i = 0; i < imageCount.Length; i++)
        {
            imageCount[i] = 0;
        }

        for (int i = 0; i < randomPlacements.Length; i++)
        {
            randomPlacements[i].ClearObj();
        }

        Invoke("SpawnRandomObj", 0.1f);
    }

    public void SpawnRandomObj()
    {
        for (int i = 0; i < randomPlacements.Length; i++)
        {
            if (randomPlacements[i].gameObject.activeSelf)
                randomPlacements[i].RandomlyPlaceImages();
        }
        Invoke("Capture",0.1f);
    }

    public void Capture()
    {
        FindObjectOfType<Capture2>().CaptureAndSaveScreenshot();
    }
}
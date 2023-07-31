using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ImageRandomPlacement : MonoBehaviour
{
    public SpriteRenderer backgroundImage;
    public List<Sprite> imageSprites;
    public int numImages = 5;
    public float Minsize, MaxSize;
    public int sortOrder;
    public Vector2 spawnArea;
    public Vector2 spawnArea1;
    public bool inside;
    private Sprite imageSprite;


    public void RandomlyPlaceImages()
    {
        int currentImagesCount;
        if (inside)
            currentImagesCount = Random.Range(1, numImages);
        else
        {
            currentImagesCount = numImages;
        }

        for (int i = 0; i < currentImagesCount; i++)
        {
            SetImageRandom();

            GameObject newImageObject = new GameObject("RandomImage_" + i);
            SpriteRenderer imageRenderer = newImageObject.AddComponent<SpriteRenderer>();
            imageRenderer.sprite = imageSprite;


            float x = Random.Range(spawnArea.x, spawnArea1.x);
            float y = Random.Range(spawnArea.y, spawnArea1.y);

            float sizeX = Random.Range(Minsize, MaxSize);

            newImageObject.transform.position = new Vector3(x, y, 0f);
            newImageObject.transform.localScale = new Vector3(sizeX, sizeX, sizeX);
            newImageObject.GetComponent<SpriteRenderer>().sortingOrder = sortOrder;

            newImageObject.gameObject.transform.SetParent(transform);

            if (!inside)
            {
                Color fadeColor = newImageObject.GetComponent<SpriteRenderer>().color;
                newImageObject.GetComponent<SpriteRenderer>().color =
                    new Color(fadeColor.r, fadeColor.g, fadeColor.b, Random.Range(0.2f, .6f));
            }
            else
            {
                Color fadeColor = newImageObject.GetComponent<SpriteRenderer>().color;
                newImageObject.GetComponent<SpriteRenderer>().color = new Color(fadeColor.r, fadeColor.g, fadeColor.b,
                    Random.Range(0.7f, .95f));
            }
        }
    }

  
    int index;
    public void SetImageRandom()
    {
        int min = 0;
        int randomIndex;
       
        if (inside)
        {
            randomIndex = Random.Range(0, imageSprites.Count);
            for (int i = 0; i < GameManager.instance.imageCount.Length; i++)
            {
                if (min > GameManager.instance.imageCount[i])
                {
                    min = GameManager.instance.imageCount[i];
                    index = i;
                }
            }

            if (GameManager.instance.imageCount[randomIndex] <= min)
            {
                imageSprite = imageSprites[randomIndex];
                GameManager.instance.imageCount[randomIndex] += 1;
            }else
            {
                randomIndex = Random.Range(0, imageSprites.Count);
                imageSprite = imageSprites[randomIndex];
                GameManager.instance.imageCount[randomIndex] += 1;
            }
        }
        else
        {
            randomIndex = 0;
            imageSprite = imageSprites[randomIndex];
        }
    }
    
    public void ClearObj()
    {
        if (transform.childCount > 0)
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(spawnArea.x, spawnArea.y, 0), 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector3(spawnArea.x, spawnArea1.y, 0), 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector3(spawnArea1.x, spawnArea1.y, 0), 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector3(spawnArea1.x, spawnArea.y, 0), 1);
    }
}
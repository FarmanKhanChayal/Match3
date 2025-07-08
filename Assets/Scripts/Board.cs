using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Board : MonoBehaviour
{

    [SerializeField] private int height;
    [SerializeField] private int width;

    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Gem[] gemPrefabs;
    [SerializeField] private GemManager gemManager;

    [HideInInspector] public Gem[,] gemList;
    

    private MatchFinder matchFinder;
   

    public int Height
    {
        get
        {
            return height;
        }
    }

    public int Width
    {
        get
        {
            return width;
        }
    }

    private void Awake()
    {
        matchFinder = FindObjectOfType<MatchFinder>();
        gemList = new Gem[Height, Width];
       
    }

    void Start()
    {
        SetBoard();
        
        matchFinder.FindGemMatch();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetBoard()
    {
        for(int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height ; j++)
            {
                SpawnTile(i, j);
                SpawnGem(i,j);
            }
        }
    }

    void SpawnTile(int x, int y)
    {
        Tile tile = Instantiate(tilePrefab, new Vector3(x,y,0),quaternion.identity, transform);
        tile.name = $"tile {x},{y}";
    }

    void SpawnGem(int x, int y)
    {
        
        if (x > 1 || y > 1)
        {
            while (true)
            {
                int randomVal2 = UnityEngine.Random.Range(0, gemPrefabs.Length);
                Gem gem2 = gemPrefabs[randomVal2];

                if(x > 1)
                {
                    if (gemList[x - 1, y].Type != gem2.Type && gemList[x - 2, y].Type != gem2.Type)
                    {
                        if(y > 1)
                        {
                            if (gemList[x, y-1].Type != gem2.Type && gemList[x, y-2].Type != gem2.Type)
                            {
                                gem2 = Instantiate(gemPrefabs[randomVal2], new Vector3(x, y, 0), quaternion.identity, gemManager.transform);
                                gem2.name = $"gem {x}, {y}";
                                gemList[x, y] = gem2;
                                gem2.initialize(new Vector2(x, y));

                                return;
                            }
                        }
                        else
                        {
                            gem2 = Instantiate(gemPrefabs[randomVal2], new Vector3(x, y, 0), quaternion.identity, gemManager.transform);
                            gem2.name = $"gem {x}, {y}";
                            gemList[x, y] = gem2;
                            gem2.initialize(new Vector2(x, y));

                            return;
                        }
                       
                    }
                }
                else if (y > 1)
                {
                    if (gemList[x, y - 1].Type != gem2.Type && gemList[x, y - 2].Type != gem2.Type)
                    {
                        gem2 = Instantiate(gemPrefabs[randomVal2], new Vector3(x, y, 0), quaternion.identity, gemManager.transform);
                        gem2.name = $"gem {x}, {y}";
                        gemList[x, y] = gem2;
                        gem2.initialize(new Vector2(x, y));

                        return;
                    }
                }

            }
        }
        else
        {
            int randomVal = UnityEngine.Random.Range(0, gemPrefabs.Length);
            Gem gem = Instantiate(gemPrefabs[randomVal], new Vector3(x, y, 0), quaternion.identity, gemManager.transform);
            gem.name = $"gem {x}, {y}";

            gemList[x, y] = gem;
            gem.initialize(new Vector2(x, y));
        }
    }

    

}








































































































































































































































































































































































































































































































































































































































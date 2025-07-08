using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFinder : MonoBehaviour
{
    public static MatchFinder Instance; 
    public List<Gem> MatchedGemsList;
    private Board boardManager;

    private void Awake()
    {
        Instance = this;
        boardManager = FindObjectOfType<Board>();
        MatchedGemsList = new List<Gem>();
    }

    public void FindGemMatch()
    {
        MatchedGemsList.Clear();

        for (int i = 0; i < boardManager.Width; i++)
        {
            for (int j = 0; j < boardManager.Height; j++)
            {
                Gem currentGem = boardManager.gemList[i,j];

                if(currentGem == null)
                {
                    continue;
                }

                // Find Horizontal Match
                if (i > 0 && i < boardManager.Width - 1)
                {
                    Gem leftGem = boardManager.gemList[i-1, j];
                    Gem rightGem = boardManager.gemList[i + 1, j];

                    if (currentGem.Type == leftGem?.Type &&  currentGem.Type == rightGem?.Type)
                    {
                        currentGem.IsMatched = true;
                        leftGem.IsMatched = true;
                        rightGem.IsMatched = true;

                        MatchedGemsList.Add(currentGem);
                        MatchedGemsList.Add(leftGem);
                        MatchedGemsList.Add(rightGem);

                    }
                }

                // Find Vertical Match
                if (j > 0 && j < boardManager.Height - 1)
                {
                    Gem BottomGem = boardManager.gemList[i, j-1];
                    Gem TopGem = boardManager.gemList[i, j+1];

                    if (currentGem.Type == BottomGem?.Type && currentGem.Type == TopGem?.Type)
                    {
                        currentGem.IsMatched = true;
                        BottomGem.IsMatched = true;
                        TopGem.IsMatched = true;

                        MatchedGemsList.Add(currentGem);
                        MatchedGemsList.Add(BottomGem);
                        MatchedGemsList.Add(TopGem);
                    }
                }
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    public static GemManager Instance;

    private Gem FirstclickedGem;
    private Gem LastclickedGem;
    private MatchFinder matchfinder;
    private Board boardManager;
    private Gem gem;

    private void Awake()
    {
        Instance = this;
        matchfinder = FindObjectOfType<MatchFinder>();
        boardManager = FindObjectOfType<Board>();
        gem = FindObjectOfType<Gem>();

    }

    public void GemClicked(Gem gem)
    {
        if(FirstclickedGem != null && LastclickedGem != null)
        {
            FirstclickedGem = null;
            LastclickedGem = null;
        }

        if(FirstclickedGem == null)
        {
            FirstclickedGem = gem;
            Debug.Log(FirstclickedGem.name);
        }
        else
        {
            LastclickedGem = gem;
            Debug.Log(FirstclickedGem.name);
            SwapGemIfValid();
        }
    }

    public void SwapGemIfValid()
    {
        Vector2 firstGemPosition = FirstclickedGem.positionIndex;
        Vector2 lastGemPosition = LastclickedGem.positionIndex;

        if(Mathf.Abs(firstGemPosition.x - lastGemPosition.x) > 1 || Mathf.Abs(firstGemPosition.y - lastGemPosition.y) > 1)
        {
            return;
        }

        SwapGem(FirstclickedGem, LastclickedGem);
        //matchfinder.FindGemMatch();
        //boardManager.ReArrangeGems();

    }

    public void SwapGem(Gem gem1, Gem gem2)
    {

        Vector2 tempPositionIndex = gem1.positionIndex;
        gem1.positionIndex = gem2.positionIndex;
        gem2.positionIndex = tempPositionIndex;

        GemAnimation(gem1,gem2);

        boardManager.gemList[(int)gem1.positionIndex.x, (int)gem1.positionIndex.y] = gem1;
        boardManager.gemList[(int)gem2.positionIndex.x, (int)gem2.positionIndex.y] = gem2;

        string tempName = gem1.transform.name;
        gem1.transform.name = gem2.transform.name;
        gem2.transform.name = tempName;

        StartCoroutine(WaitForGemAnimnation(gem1 ,gem2));

        
    }

    private IEnumerator WaitForGemAnimnation(Gem gem1,Gem gem2)
    {
        yield return new WaitForSeconds(0.6f);

        MatchFinder.Instance.FindGemMatch();



        if (!gem2.IsMatched && !gem1.IsMatched)
        {
            GemAnimation(gem1, gem2);

            Vector2 tempPositionIndex = gem2.positionIndex;
            gem2.positionIndex = gem1.positionIndex;
            gem1.positionIndex = tempPositionIndex;

            string tempName = gem2.name;
            gem2.name = gem1.name;
            gem1.name = tempName;

            boardManager.gemList[(int)gem1.positionIndex.x, (int)gem1.positionIndex.y] = gem1;
            boardManager.gemList[(int)gem2.positionIndex.x, (int)gem2.positionIndex.y] = gem2;

        }
        else
        {

            StartCoroutine(ReArrangeGems());
        }

        

    }

    private void GemAnimation(Gem gem1,Gem gem2)
    {
        Vector2 tempPosition = gem1.transform.position;
        gem1.transform.DOMove(gem2.transform.position, 0.5f);
        gem2.transform.DOMove(tempPosition, 0.5f);
    }

    public IEnumerator ReArrangeGems()
    {
        yield return new WaitForSeconds(1f);

        foreach (Gem matchedGem in MatchFinder.Instance.MatchedGemsList)
        {
            Vector2 pos = matchedGem.positionIndex;
            boardManager.gemList[(int)pos.x, (int)pos.y] = null;
            Debug.Log(pos);
            Destroy(matchedGem.gameObject);
            FirstclickedGem = null;
            LastclickedGem = null;
        }

        for (int i = 0; i < boardManager.Width; i++)
        {
            int nullCount = 0;
            for (int j = 0; j < boardManager.Height; j++)
            {
                if (boardManager.gemList[i, j] == null)
                {
                    nullCount++;
                    Debug.Log($"{i} {j}");
                }
                else
                {
                    boardManager.gemList[i, j].transform.DOMove(new Vector3(i, j - nullCount, 0), 0.6f);
                    boardManager.gemList[i, j] = boardManager.gemList[i, j - nullCount];
                    boardManager.gemList[i, j].positionIndex.y = boardManager.gemList[i, j].positionIndex.y - nullCount;
                }
            }
        }
    }




}

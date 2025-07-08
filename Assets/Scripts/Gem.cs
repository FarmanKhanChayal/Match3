using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public Vector2 positionIndex;
    private GemManager gemManager;
    [SerializeField] private Gemtype gemType;




    public Gemtype Type
    {
        get
        {
            return gemType;
        }
    }

    public bool IsMatched;

    private void Awake()
    {
        gemManager = GemManager.Instance;
    }

    public void initialize(Vector2 position)
    {
        this.positionIndex = position;
    }

    private void OnMouseDown()
    {
     
        gemManager.GemClicked(this);

    } 

   

   
}

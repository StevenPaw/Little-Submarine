using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectUndoManager : MonoBehaviour
{
    public enum ObjectMovementType
    {
        EMPTY, UP, DOWN, LEFT, RIGHT
    }

    [SerializeField] private int maxMoves = 0;
    
    [SerializeField] private ObjectMovementType[] moves;
    [SerializeField] private int currentMove = 0;

    private void Awake()
    {
        moves = new ObjectMovementType[maxMoves];
    }

    public bool addMove(ObjectMovementType type)
    {
        if (currentMove < maxMoves)
        {
            moves[currentMove] = type;
            currentMove += 1;
            return true; //If there is space for undo-moves return true
        }
        else
        {
            return false; //If max moves is reached, return false
        }
    }

    public ObjectMovementType getUndoMove()
    {
        if (currentMove > 0)
        {
            currentMove -= 1;
            return moves[currentMove];
        }
        else
        {
            return ObjectMovementType.EMPTY; //Returns empty undo if on Start Position
        }
    }
}

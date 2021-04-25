using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField] private Transform movePoint;
    [SerializeField] private LayerMask WhatStopsMovement;
    [SerializeField] private ObjectUndoManager objectUndoManager;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool canMoveOtherBlocks = false;
    [SerializeField] private LayerMask WhatCanBeMoved;
    [SerializeField] private String blockType;
    private void Start()
    {
        objectUndoManager = GetComponent<ObjectUndoManager>();
        movePoint.parent = null;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }

    public void UndoMove()
    {
        ObjectUndoManager.ObjectMovementType undoMove = objectUndoManager.getUndoMove();
        if (undoMove == ObjectUndoManager.ObjectMovementType.UP)
        {
            MoveBlockBack(Vector2.down, moveSpeed);
        }
        if (undoMove == ObjectUndoManager.ObjectMovementType.DOWN)
        {
            MoveBlockBack(Vector2.up, moveSpeed);
        }
        if (undoMove == ObjectUndoManager.ObjectMovementType.LEFT)
        {
            MoveBlockBack(Vector2.right, moveSpeed);
        }
        if (undoMove == ObjectUndoManager.ObjectMovementType.RIGHT)
        {
            MoveBlockBack(Vector2.left, moveSpeed);
        }
    }
    
    public bool MoveBlock(Vector2 moveDirection, float moveSpeedIn)
    {
        moveSpeed = moveSpeedIn;

        //RIGHT MOVEMENT
        if (moveDirection == Vector2.right)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f), .2f, WhatStopsMovement))
            {
                //DO NOTHING
            }
            else if (Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f), .2f, WhatCanBeMoved))
            {
                if (canMoveOtherBlocks)
                {
                    if (objectUndoManager.addMove(ObjectUndoManager.ObjectMovementType.RIGHT))
                    {
                        movePoint.position += new Vector3(1f, 0f);
                        return true;
                    }
                }
            }
            else
            {
                if (objectUndoManager.addMove(ObjectUndoManager.ObjectMovementType.RIGHT))
                {
                    movePoint.position += new Vector3(1f, 0f);
                    return true;
                }
            }
        }
        //LEFT MOVEMENT
        else if (moveDirection == Vector2.left)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f), .2f, WhatStopsMovement))
            {
                //DO NOTHING
            }
            else if (Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f), .2f, WhatCanBeMoved))
            {
                if (canMoveOtherBlocks)
                {
                    if (objectUndoManager.addMove(ObjectUndoManager.ObjectMovementType.LEFT))
                    {
                        movePoint.position += new Vector3(-1f, 0f);
                        return true;
                    }
                }
            }
            else
            {
                if (objectUndoManager.addMove(ObjectUndoManager.ObjectMovementType.LEFT))
                {
                    movePoint.position += new Vector3(-1f, 0f);
                    return true;
                }
            }
        }

        //UP MOVEMENT
        else if (moveDirection == Vector2.up)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f), .2f, WhatStopsMovement))
            {
                
            }
            else if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f), .2f, WhatCanBeMoved))
            {
                if (canMoveOtherBlocks)
                {
                    if (objectUndoManager.addMove(ObjectUndoManager.ObjectMovementType.UP))
                    {
                        movePoint.position += new Vector3(0f, 1f);
                        return true;
                    }
                }
            }
            else
            {
                if (objectUndoManager.addMove(ObjectUndoManager.ObjectMovementType.UP))
                {
                    movePoint.position += new Vector3(0f, 1f);
                    return true;
                }
            }
        }
        //DOWN MOVEMENT
        else if (moveDirection == Vector2.down)
        {
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f), .2f, WhatStopsMovement))
            {
                //DO NOTHING
            }
            else if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f), .2f, WhatCanBeMoved))
            {
                if (canMoveOtherBlocks)
                {
                    if (objectUndoManager.addMove(ObjectUndoManager.ObjectMovementType.DOWN))
                    {
                        movePoint.position += new Vector3(0f, -1f);
                        return true;
                    }
                }
            }
            else
            {
                if (objectUndoManager.addMove(ObjectUndoManager.ObjectMovementType.DOWN))
                {
                    movePoint.position += new Vector3(0f, -1f);
                    return true;
                }
            }
        }

        return false;
    }
    
    public bool MoveBlockBack(Vector2 moveDirection, float moveSpeedIn)
    {
        moveSpeed = moveSpeedIn;
        
        //RIGHT MOVEMENT
        if (moveDirection == Vector2.right)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f), .2f, WhatStopsMovement))
            {
                movePoint.position += new Vector3(1f, 0f);
                return true;
            }
        }
        //LEFT MOVEMENT
        else if (moveDirection == Vector2.left)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f), .2f, WhatStopsMovement))
            {
                movePoint.position += new Vector3(-1f, 0f);
                return true;
            }
        }

        //UP MOVEMENT
        else if (moveDirection == Vector2.up)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f), .2f, WhatStopsMovement))
            {
                movePoint.position += new Vector3(0f, 1f);
                return true;
            }
        }
        //DOWN MOVEMENT
        else if (moveDirection == Vector2.down)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f), .2f, WhatStopsMovement))
            {
                movePoint.position += new Vector3(0f, -1f);
                return true;
            }
        }

        return false;
    }

    public String getBlockType()
    {
        return blockType;
    }
}
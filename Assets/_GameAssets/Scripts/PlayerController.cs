using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Range(0.0f,10.0f)] [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform movePoint;

    [SerializeField] private LayerMask WhatStopsMovement;
    [SerializeField] private LayerMask WhatCanBePushed;

    [SerializeField] private Animator anim;

    [SerializeField] private UndoManager undoManager;
    [SerializeField] private MessengerManager messengerManager;

    [SerializeField] private KeyCode undoKey;

    [SerializeField] private bool canMove = true;

    private Vector2 rawAxis;
    private PauseMenu pauseMenu;
    
    private void Start()
    {
        movePoint.parent = null;
        undoManager = GetComponent<UndoManager>();
        messengerManager = GameObject.Find("MessengerManager").GetComponent<MessengerManager>();
        rawAxis = Vector2.zero;
        pauseMenu = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (canMove)
            {

                //RIGHT MOVEMENT
                if (rawAxis.x == 1f)
                {
                    anim.SetFloat("Horizontal", 1f);
                    anim.SetFloat("Vertical", 0f);
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(rawAxis.x, 0f),
                        .2f, WhatStopsMovement))
                    {
                        //DO NOTHING
                    }
                    else if (Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(rawAxis.x, 0f), .2f, WhatCanBePushed))
                    {
                        GameObject pushedObject = Physics2D.OverlapCircle(
                                movePoint.position + new Vector3(rawAxis.x, 0f), .2f,
                                WhatCanBePushed)
                            .gameObject;

                        PushBlock(pushedObject, Vector2.right);
                    }
                    else
                    {
                        if (undoManager.addMove(UndoManager.MovementType.RIGHT))
                        {
                            movePoint.position += new Vector3(rawAxis.x, 0f);
                        }
                        else
                        {
                            messengerManager.MessageTooManyMoves();
                        }
                    }
                }
                //LEFT MOVEMENT
                else if (rawAxis.x == -1f)
                {
                    anim.SetFloat("Horizontal", -1f);
                    anim.SetFloat("Vertical", 0f);
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(rawAxis.x, 0f),
                        .2f, WhatStopsMovement))
                    {
                        //DO NOTHING
                    }
                    else if (Physics2D.OverlapCircle(
                        movePoint.position + new Vector3(rawAxis.x, 0f), .2f, WhatCanBePushed))
                    {
                        GameObject pushedObject = Physics2D.OverlapCircle(
                                movePoint.position + new Vector3(rawAxis.x, 0f), .2f,
                                WhatCanBePushed)
                            .gameObject;

                        PushBlock(pushedObject, Vector2.left);
                    }
                    else
                    {
                        if (undoManager.addMove(UndoManager.MovementType.LEFT))
                        {
                            movePoint.position += new Vector3(rawAxis.x, 0f);
                        }
                        else
                        {
                            messengerManager.MessageTooManyMoves();
                        }
                    }
                }

                //UP MOVEMENT
                else if (rawAxis.y == 1f)
                {
                    anim.SetFloat("Vertical", 1f);
                    anim.SetFloat("Horizontal", 0f);
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, rawAxis.y), .2f,
                        WhatStopsMovement))
                    {
                        //DO NOTHING
                    }
                    else if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, rawAxis.y),
                        .2f, WhatCanBePushed))
                    {
                        GameObject pushedObject = Physics2D.OverlapCircle(
                                movePoint.position + new Vector3(0f, rawAxis.y), .2f,
                                WhatCanBePushed)
                            .gameObject;

                        PushBlock(pushedObject, Vector2.up);
                    }
                    else
                    {
                        if (undoManager.addMove(UndoManager.MovementType.UP))
                        {
                            movePoint.position += new Vector3(0f, rawAxis.y);
                        }
                        else
                        {
                            messengerManager.MessageTooManyMoves();
                        }
                    }
                }
                //DOWN MOVEMENT
                else if (rawAxis.y == -1f)
                {
                    anim.SetFloat("Vertical", -1f);
                    anim.SetFloat("Horizontal", 0f);
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, rawAxis.y), .2f,
                        WhatStopsMovement))
                    {
                        //DO NOTHING
                    }
                    else if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, rawAxis.y),
                        .2f, WhatCanBePushed))
                    {
                        GameObject pushedObject = Physics2D.OverlapCircle(
                                movePoint.position + new Vector3(0f, rawAxis.y), .2f,
                                WhatCanBePushed)
                            .gameObject;

                        PushBlock(pushedObject, Vector2.down);
                    }
                    else
                    {
                        if (undoManager.addMove(UndoManager.MovementType.DOWN))
                        {
                            movePoint.position += new Vector3(0f, rawAxis.y);
                        }
                        else
                        {
                            messengerManager.MessageTooManyMoves();
                        }
                    }
                }

                anim.SetBool("moving", false);
            }
            else
            {
                anim.SetBool("moving", true);
            }
        }
    }

    private void PushBlock(GameObject blockToPush, Vector2 direction)
    {
        if (blockToPush.GetComponent<MovingBlock>().MoveBlock(direction, moveSpeed))
        {
            if (direction == Vector2.down)
            {
                if (undoManager.addMove(UndoManager.MovementType.PUSHDOWN))
                {
                    movePoint.position += new Vector3(direction.x, direction.y);
                }
                else
                {
                    messengerManager.MessageTooManyMoves();
                }
            }
            if (direction == Vector2.up)
            {
                if (undoManager.addMove(UndoManager.MovementType.PUSHUP))
                {
                    movePoint.position += new Vector3(direction.x, direction.y);
                }
                else
                {
                    messengerManager.MessageTooManyMoves();
                }
            }
            if (direction == Vector2.left)
            {
                if (undoManager.addMove(UndoManager.MovementType.PUSHLEFT))
                {
                    movePoint.position += new Vector3(direction.x, direction.y);
                }
                else
                {
                    messengerManager.MessageTooManyMoves();
                }
            }
            if (direction == Vector2.right)
            {
                if (undoManager.addMove(UndoManager.MovementType.PUSHRIGHT))
                {
                    movePoint.position += new Vector3(direction.x, direction.y);
                }
                else
                {
                    messengerManager.MessageTooManyMoves();
                }
            }
        }
    }
    
    private void UndoMovement()
    {
        UndoManager.MovementType undoAction = undoManager.getUndoMove(); //Gets the Undo action and decreases used Moves

        //Do the Opposite of the saved Move:
        if (undoAction == UndoManager.MovementType.UP)
        {
            movePoint.position += new Vector3(0f, -1f);
            anim.SetFloat("Horizontal", 0f);
            anim.SetFloat("Vertical", 1f);
        }
        if (undoAction == UndoManager.MovementType.DOWN)
        {
            movePoint.position += new Vector3(0f, 1f);
            anim.SetFloat("Horizontal", 0f);
            anim.SetFloat("Vertical", -1f);
        }
        if (undoAction == UndoManager.MovementType.RIGHT)
        {
            movePoint.position += new Vector3(-1f, 0f);
            anim.SetFloat("Horizontal", 1f);
            anim.SetFloat("Vertical", 0f);
        }
        if (undoAction == UndoManager.MovementType.LEFT)
        {
            movePoint.position += new Vector3(1f, 0f);
            anim.SetFloat("Horizontal", -1f);
            anim.SetFloat("Vertical", 0f);
        }
        
        //For Pushing Blocks
        if (undoAction == UndoManager.MovementType.PUSHUP)
        {
            GameObject pushedObject = Physics2D.OverlapCircle(
                    movePoint.position + new Vector3(0f, 1f), .2f, WhatCanBePushed).gameObject;

            pushedObject.GetComponent<MovingBlock>().UndoMove();
            
            movePoint.position += new Vector3(0f, -1f);
            anim.SetFloat("Horizontal", 0f);
            anim.SetFloat("Vertical", 1f);
        }
        if (undoAction == UndoManager.MovementType.PUSHDOWN)
        {
            GameObject pushedObject = Physics2D.OverlapCircle(
                movePoint.position + new Vector3(0f, -1f), .2f, WhatCanBePushed).gameObject;

            pushedObject.GetComponent<MovingBlock>().UndoMove();

            movePoint.position += new Vector3(0f, 1f);
            anim.SetFloat("Horizontal", 0f);
            anim.SetFloat("Vertical", -1f);
        }
        if (undoAction == UndoManager.MovementType.PUSHRIGHT)
        {
            GameObject pushedObject = Physics2D.OverlapCircle(
                movePoint.position + new Vector3(1f, 0f), .2f, WhatCanBePushed).gameObject;

            pushedObject.GetComponent<MovingBlock>().UndoMove();

            movePoint.position += new Vector3(-1f, 0f);
            anim.SetFloat("Horizontal", 1f);
            anim.SetFloat("Vertical", 0f);
        }
        if (undoAction == UndoManager.MovementType.PUSHLEFT)
        {
            GameObject pushedObject = Physics2D.OverlapCircle(
                movePoint.position + new Vector3(-1f, 0f), .2f, WhatCanBePushed).gameObject;

            pushedObject.GetComponent<MovingBlock>().UndoMove();

            movePoint.position += new Vector3(1f, 0f);
            anim.SetFloat("Horizontal", -1f);
            anim.SetFloat("Vertical", 0f);
        }
    }

    public void SetCanMove(bool canMoveIn)
    {
        canMove = canMoveIn;
    }

    public void BTN_Undo(InputAction.CallbackContext ctx)
    {
        if (canMove)
        {
            UndoMovement();
        }
    }

    public void BTN_Move(InputAction.CallbackContext ctx)
    {
        if (canMove)
        {
            rawAxis = ctx.ReadValue<Vector2>();
        }
    }

    public void BTN_Pause(InputAction.CallbackContext ctx)
    {
        pauseMenu.PauseGame();
    }
}
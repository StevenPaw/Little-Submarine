using System;
using UnityEngine;

public class MessengerManager : MonoBehaviour
{
    [SerializeField] private String standardTitle = "Message:";
    [SerializeField] private String warningTitle = "WARNING!";
    
    /// <summary>
    /// Shows a standard Message to the Player
    /// </summary>
    /// <param name="title">Title of the message Box</param>
    /// <param name="text">Text to output</param>
    public void StandardMessage (String title, String text)
    {
        //TODO: Show Message Box with Text
        SendMessage(title, text);
    }
    
    /// <summary>
    /// Shows a standard Message to the Player
    /// </summary>
    /// <param name="text">Text to output</param>
    public void StandardMessage (String text)
    {
        String title = standardTitle;
        SendMessage(title, text);
    }
    
    /// <summary>
    /// Shows a warning Message to the Player
    /// </summary>
    /// <param name="title">Title of the message Box</param>
    /// <param name="text">Text to output</param>
    public void WarningMessage (String title, String text)
    {
        SendMessage(title, text);
    }
    /// <summary>
    /// Shows a warning Message to the Player
    /// </summary>
    /// <param name="text">Text to output</param>
    public void WarningMessage (String text)
    {
        String title = warningTitle;
        SendMessage(title, text);
    }

    //Standard-Functions
    public void MessageTooManyMoves()
    {
        StandardMessage("TOO MANY MOVES!", "You did too many moves, so you need to undo something");
    }
    
    //Send-Functions
    private void SendMessage(String title, String text)
    {
        Debug.Log(title + " - " + text);
        //TODO: Show Message Box with Text
    }
    
    private void SendWarning(String title, String text)
    {
        Debug.LogWarning(title + " - " + text);
        //TODO: Show Warning Box with Text
    }
}

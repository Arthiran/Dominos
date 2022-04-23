// Arthiran Sivarajah - 100660300, Aaron Chan - 100657311
// Chatbox messaging system modified from: https://youtu.be/IRAeJgGkjHk

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagesManager : MonoBehaviour
{
    public int maxMessages = 25;

    public GameObject chatPanel;
    public GameObject textObject;

    public GameObject chatPanel2;
    public GameObject textObject2;

    private List<Message> messageList = new List<Message>();
    private List<HighScore> hsList = new List<HighScore>();

    public InputField chatInputField;

    public Client clientScript;

    public void SendMessageToChat(string _text)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();
        newMessage.text = _text;
        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);
    }

    public void SendHSToPanel(string _text)
    {
        if (hsList.Count >= maxMessages)
        {
            Destroy(hsList[0].textObject.gameObject);
            hsList.Remove(hsList[0]);
        }

        HighScore newHS = new HighScore();
        newHS.text = _text;
        GameObject newText = Instantiate(textObject2, chatPanel2.transform);
        newHS.textObject = newText.GetComponent<Text>();
        newHS.textObject.text = newHS.text;
        hsList.Add(newHS);
    }

    public void ClientChatMessage()
    {
        clientScript.SendClientMessage("MSG " + chatInputField.text);
        chatInputField.text = "";
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}

[System.Serializable]
public class HighScore
{
    public string text;
    public Text textObject;
}

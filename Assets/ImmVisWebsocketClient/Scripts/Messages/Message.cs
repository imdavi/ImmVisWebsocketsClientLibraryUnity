
using System;
using UnityEngine;

[Serializable]
public class Message : BaseMessage<Message>
{
    public Message(string type) : base(type) { }
}
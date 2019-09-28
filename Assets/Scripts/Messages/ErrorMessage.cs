using System;
using UnityEngine;

[Serializable]
class ErrorMessage  : BaseMessage<GetImage>
{
    [SerializeField] private string cause;

    public string Cause
    {
        get { return cause; }
    }

    public ErrorMessage(string cause) : base("error")
    {
        this.cause = cause;
    }

    public static ErrorMessage Create(string cause){
        return new ErrorMessage(cause);
    }
}
using System;

namespace HelloWorld.Common
{
    [Serializable]
    public class Message
    {
        public Message(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}
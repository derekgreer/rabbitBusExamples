using System;

namespace RabbitBusSpeedTest
{
    [Serializable]
    class TestMessage
    {
        public TestMessage(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}
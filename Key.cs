namespace External_ESP_Base
{
    using System;

    internal class Key
    {
        private bool keyDown;
        private int keyId;
        private string keyName;

        public Key(int keyId, string keyName)
        {
            this.keyId = keyId;
            this.keyName = keyName;
        }

        public int Id =>
            this.keyId;

        public bool IsKeyDown
        {
            get => 
                this.keyDown;
            set
            {
                this.keyDown = value;
            }
        }

        public string Name =>
            this.keyName;
    }
}


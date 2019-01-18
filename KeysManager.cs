namespace External_ESP_Base
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    internal class KeysManager
    {
        private int interval = 20;
        private Dictionary<int, Key> keys = new Dictionary<int, Key>();
        private Thread thread;

        [field: CompilerGenerated]
        public event KeyHandler KeyDownEvent;

        [field: CompilerGenerated]
        public event KeyHandler KeyUpEvent;

        public KeysManager()
        {
            this.thread = new Thread(new ParameterizedThreadStart(this.Update));
            this.thread.Start();
        }

        public void AddKey(Keys key)
        {
            int num = (int) key;
            if (!this.keys.ContainsKey(num))
            {
                this.keys.Add(num, new Key(num, key.ToString()));
            }
        }

        public void AddKey(int keyId, string keyName)
        {
            if (!this.keys.ContainsKey(keyId))
            {
                this.keys.Add(keyId, new Key(keyId, keyName));
            }
        }

        public bool IsKeyDown(int keyId)
        {
            Key key;
            return (this.keys.TryGetValue(keyId, out key) && key.IsKeyDown);
        }

        protected void OnKeyDown(int Id, string Name)
        {
            if (this.KeyDownEvent != null)
            {
                this.KeyDownEvent(Id, Name);
            }
        }

        protected void OnKeyUp(int Id, string Name)
        {
            if (this.KeyUpEvent != null)
            {
                this.KeyUpEvent(Id, Name);
            }
        }

        private void Update(object sender)
        {
            while (true)
            {
                if (this.keys.Count > 0)
                {
                    List<Key> list = new List<Key>(this.keys.Values);
                    if ((list != null) && (list.Count > 0))
                    {
                        foreach (Key key in list)
                        {
                            if (Convert.ToBoolean((int) (Managed.GetKeyState(key.Id) & 0x8000)))
                            {
                                if (!key.IsKeyDown)
                                {
                                    key.IsKeyDown = true;
                                    this.OnKeyDown(key.Id, key.Name);
                                }
                            }
                            else if (key.IsKeyDown)
                            {
                                key.IsKeyDown = false;
                                this.OnKeyUp(key.Id, key.Name);
                            }
                        }
                    }
                }
                Thread.Sleep(this.interval);
            }
        }

        public delegate void KeyHandler(int Id, string Name);
    }
}


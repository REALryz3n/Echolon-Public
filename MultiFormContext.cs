namespace External_ESP_base
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    public class MultiFormContext : ApplicationContext
    {
        private int openForms;

        public MultiFormContext(params Form[] forms)
        {
            this.openForms = forms.Length;
            foreach (Form form1 in forms)
            {
                form1.FormClosed += delegate (object s, FormClosedEventArgs args) {
                    if (Interlocked.Decrement(ref this.openForms) == 0)
                    {
                        base.ExitThread();
                    }
                };
                form1.Show();
            }
        }
    }
}


using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace fixSpaceKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int[] keysTofix = {9 ,112 , 113};
            fixMyKeys fmk = new fixMyKeys(keysTofix, this);
        }

        private void HandleHotkey(int key)
              {
            switch (key)
            {
                case 9:
                    SendKeys.Send(" ");
                    break;
                case 112:
                    SendKeys.SendWait("{ENTER}");
                    break;
                case 113:
                    SendKeys.Send("{BACKSPACE}");
                    break;
                default:                   
                    break;
            }                
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312) {
                int id = m.WParam.ToInt32();
                HandleHotkey(id);
            }               
            base.WndProc(ref m);
        }
    }

    public class fixMyKeys
    {
        public fixMyKeys (int[] keys, Form form)
        {
            foreach (int key in keys)
            {
                addEventListener(key, form);
            }
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        private void addEventListener(int keyCode, Form form)
        {
            RegisterHotKey(form.Handle, keyCode, 4, keyCode);
        }
    }
}

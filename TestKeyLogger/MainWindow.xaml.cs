using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Keystroke.API;
using Keystroke.API.CallbackObjects;

namespace TestKeyLogger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private IntPtr HookId;
        public static string temp = "";
        public static bool isRecord = false;

        public KeystrokeAPI api = new KeystrokeAPI();
        public MainWindow()
        {
            Clipboard.SetText("");
            InitializeComponent();

            LbRecording.Text = "Not Recording";
            LbRecording.Foreground = new SolidColorBrush(Colors.Black);

            api.CreateKeyboardHook(pressed =>
                {
                    TbLog.Text = $" Window => {pressed.CurrentWindow}, Key => {pressed} : {pressed.KeyCode.ToString()}, CapsLockOn => {pressed.CapsLockOn}\n" + TbLog.Text;
                    Console.WriteLine(pressed );
                    Console.WriteLine(pressed.KeyCode.ToString());
                    Console.WriteLine(pressed.CapsLockOn);
                    Console.WriteLine(pressed.CurrentWindow);
                    handleKeyPressed(pressed);
                }
            );
        }

        private void handleKeyPressed(KeyPressed kp)
        {
            if (kp.KeyCode == KeyCode.F1 && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if(isRecord) return;
                TbLog.Text = $"Control + F1 pressed => start Record \n" + TbLog.Text;
                LbRecording.Text = "Recording";
                LbRecording.Foreground = new SolidColorBrush(Colors.Red);
          
                Console.WriteLine("Control + F1 pressed");
                isRecord = true;
                Console.WriteLine("isRecord :" + isRecord);
                temp = "";
                Clipboard.SetText("");
                cbText.Text = "";
                return;
            }

            if (!isRecord)
            {
                return ;
            }

            if (kp.KeyCode == KeyCode.Enter || kp.KeyCode == KeyCode.Return)
            {
                LbRecording.Text = "Not Record";
                LbRecording.Foreground = new SolidColorBrush(Colors.Black);
                TbLog.Text = $"Enter pressed => Save to clipboard \n" + TbLog.Text;
                Console.WriteLine("Enter pressed");
                Clipboard.SetText(temp);
                cbText.Text = temp;
                temp = "";
                isRecord = false;
                return;
            }
            temp = temp + kp;
            LbText.Text = temp;
        }

        protected override void OnClosed(EventArgs e)
        {
          //stop??
        }
    }

   
}
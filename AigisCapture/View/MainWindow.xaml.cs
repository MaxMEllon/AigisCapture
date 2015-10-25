using AigisCapture.Common;
using AigisCapture.Model;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace AigisCapture.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region WindowSettings 依存関係プロパティ

        public IWindowSettings WindowSettings
        {
            get { return (IWindowSettings)this.GetValue(WindowSettingsProperty); }
            set { this.SetValue(WindowSettingsProperty, value); }
        }
        public static readonly DependencyProperty WindowSettingsProperty =
            DependencyProperty.Register("WindowSettings", typeof(IWindowSettings), typeof(MainWindow), new UIPropertyMetadata(null));

        #endregion
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += (sender, e) => this.DragMove();
            this.ExitButton.Click += (sender, e) =>
            {
                this.Close();
                XMLFileManager.WriteXml<Settings>(Env.SETTINGS);
            };
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // 外部からウィンドウ設定の保存・復元クラスが与えられていない場合は、既定実装を使用する
            if (this.WindowSettings == null)
            {
                this.WindowSettings = new WindowSettings(this);
            }

            this.WindowSettings.Reload();

            if (this.WindowSettings.Placement.HasValue)
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                var placement = this.WindowSettings.Placement.Value;
                placement.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
                placement.flags = 0;
                placement.showCmd = (placement.showCmd == SW.SHOWMINIMIZED) ? SW.SHOWNORMAL : placement.showCmd;

                NativeMethods.SetWindowPlacement(hwnd, ref placement);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!e.Cancel)
            {
                WINDOWPLACEMENT placement;
                var hwnd = new WindowInteropHelper(this).Handle;
                NativeMethods.GetWindowPlacement(hwnd, out placement);

                this.WindowSettings.Placement = placement;
                this.WindowSettings.Save();
            }
        }
    }
}
using AigisCapture.Common;
using AigisCapture.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace AigisCapture.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region public string X { set; get; }
        private string _X;
        public string X
        {
            get { return _X; }
            set
            {
                if (_X != value)
                {
                    _X = value;
                    RaisePropertyChanged("X");
                }
            }
        }
        #endregion

        #region public string Y { set; get; }
        private string _Y;
        public string Y
        {
            get { return _Y; }
            set
            {
                if (_Y != value)
                {
                    _Y = value;
                    RaisePropertyChanged("Y");
                }
            }
        }
        #endregion

        #region public bool IsChecked { get; set; }
        private bool _IsChecked;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set 
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    RaisePropertyChanged("IsChecked");
                }
            }
        }
        #endregion

        #region public string Message { get; set; }
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set
            {
                if (_Message != value)
                {
                    _Message = value;
                    RaisePropertyChanged("Message");
                }
            }
        }
        #endregion

        private ImageSupporter imageSupporter;

        public MainViewModel()
        {
            imageSupporter = new ImageSupporter();
            Message = "起動しました";
        }

        #region ICommand SaveCommand
        private ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new RelayCommand(
                        () =>
                        {
                            try
                            {
                                Point pos = new Point(int.Parse(X), int.Parse(Y));
                                imageSupporter.ScreanShot(pos, IsChecked);
                                Message = "スクリーンショットを撮影しました";
                            }
                            catch(Exception e)
                            {
                                Log.Write("MainViewModel::SaveCommand", e);
                                Message = "座標を指定してください";
                            }
                            finally
                            {
                                Task.Delay(200);
                            }
                        });
                }
                return _SaveCommand;
            }
        }
        #endregion

        #region ICommand SaveDirectoryConfigCommand
        private ICommand _SaveDirectoryConfigCommand;
        public ICommand SaveDirectoryConfigCommand
        {
            get
            {
                if (_SaveDirectoryConfigCommand == null)
                {
                    _SaveDirectoryConfigCommand = new RelayCommand(
                        () =>
                        {
                            FolderBrowserDialog dialog = new FolderBrowserDialog();
                            dialog.Description = "保存先のフォルダを指定してください";
                            dialog.RootFolder = Environment.SpecialFolder.Desktop;
                            dialog.ShowNewFolderButton = true;
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                Env.SETTINGS.SaveDirectory = dialog.SelectedPath;
                                Message = "保存先を設定しました";
                            }
                            else
                            {
                                Message = "保存先の設定をキャンセルしました";
                            }
                        });
                }
                return _SaveDirectoryConfigCommand;
            }
        }
        #endregion

        #region ICommand AutoLocationCommand
        private ICommand _AutoLocationCommand;
        public ICommand AutoLocationCommand
        {
            get
            {
                if (_AutoLocationCommand == null)
                {
                    _AutoLocationCommand = new RelayCommand(
                        () =>
                        {
                            Point pos = imageSupporter.OverlapLocation;
                            X = pos.X.ToString();
                            Y = pos.Y.ToString();
                            Message = "画面位置を設定しました";
                        });
                }
                return _AutoLocationCommand;
            }
        }
        #endregion
    }
}
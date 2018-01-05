using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;

using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Microsoft.Win32;

using RoadViewClassifier.Helpers;
using RoadViewClassifier.Model;

namespace RoadViewClassifier.ViewModel
{
    class ViewModelMain : ViewModelBase
    {
        // Наша картинка
        ImageSource _Image;
        public ImageSource OriginalImage
        {
            get => _Image;
            set
            {
                if (_Image != value)
                {
                    _Image = value;
                    RaisePropertyChanged("OriginalImage");

                    _IsFileOpened = value != null;
                }
            }
        }

        // Результирующая картинка
        ImageSource _Mask;
        public ImageSource Mask
        {
            get => _Mask;
            set
            {
                if (_Mask != value)
                {
                    _Mask = value;
                    RaisePropertyChanged("Mask");
                }
            }
        }

        bool _IsFileOpened;
        public bool IsFileOpened
        {
            get => _IsFileOpened;
            set
            {
                _IsFileOpened = value;
                RaisePropertyChanged("IsFileOpened");
            }
        }

        public RelayCommand OpenFileCommand { get; set; }
        public RelayCommand SaveFileCommand { get; set; }

        readonly OpenFileDialog dialog;

        public ViewModelMain()
        {
            IsFileOpened = false;
            
            OpenFileCommand = new RelayCommand(OpenFile);
            SaveFileCommand = new RelayCommand(SaveFile);

            // Инициализация OpenFileDialog
            dialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = "Image files (*.JPG)|*.jpg"
            };
        }

        void OpenFile(object parameter)
        {
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    OriginalImage = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);

                    var bmp = (Bitmap) Image.FromStream(stream);
                    var res = BitmapProcessor.ProportionMask(bmp, System.Drawing.Color.Green, 0.65);

                    Mask = Imaging.CreateBitmapSourceFromHBitmap(res.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }                
            }
        }

        void SaveFile(object parameter)
        {

        }
    }
}

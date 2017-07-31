using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ColorIt.Annotations;
using Microsoft.Win32;

namespace ColorIt
{
    public abstract class PatternViewModel : INotifyPropertyChanged
    {
        #region fields

        protected bool PreventReload;

        private Brush _brush;

        #endregion

        #region properties

        protected abstract IPattern Pattern { get; set; }
        
        public Brush Brush
        {
            get => _brush;
            set
            {
                if (Equals(value, _brush)) return;
                _brush = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public void SaveImage(int width, int height)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "PNG Files(*.png)|*.png",
                FileName = "raum.png"
            };
            if (!dialog.ShowDialog() ?? false) return;

            var target = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            var visual = new DrawingVisual();
            var drawingContext = visual.RenderOpen();

            drawingContext.DrawRectangle(Pattern.GetBrush(), null, new Rect(new Point(0, 0), new Point(width, height)));
            drawingContext.Close();
            target.Render(visual);

            var frame = BitmapFrame.Create(target);
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(frame);
            
            using (var stream = File.Create(dialog.FileName))
            {
                encoder.Save(stream);
            }
            Process.Start(dialog.FileName);
        }

        public void SaveTable(int width, int height)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Text Files(*.txt)|*.txt",
                FileName = "raum.txt"
            };
            if (!dialog.ShowDialog() ?? false) return;

            var content = Pattern.GetFile(width, height);

            File.WriteAllText(dialog.FileName, content);
            Process.Start(dialog.FileName);
        }

        public abstract void Reset();
        
        public void Reload()
        {
            if (PreventReload) return;
            Brush = Pattern.GetBrush();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
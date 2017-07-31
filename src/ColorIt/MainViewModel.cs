using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ColorIt.Annotations;

namespace ColorIt
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region fields
        
        private int _roomWidth;
        private int _roomHeight;

        private ICommand _resetCommand;
        private ICommand _saveImageCommand;
        private ICommand _saveTableCommand;
        private PatternViewModel _pattern;

        #endregion

        #region properties

        public int RoomWidth
        {
            get => _roomWidth;
            set
            {
                if (value == _roomWidth) return;
                _roomWidth = value;
                OnPropertyChanged();
                Pattern?.Reload();
            }
        }

        public int RoomHeight
        {
            get => _roomHeight;
            set
            {
                if (value == _roomHeight) return;
                _roomHeight = value;
                OnPropertyChanged();
                Pattern?.Reload();
            }
        }

        public PatternViewModel Pattern
        {
            get => _pattern;
            set
            {
                if (Equals(value, _pattern)) return;
                _pattern = value;
                OnPropertyChanged();
            }
        }

        public ICommand ResetCommand => _resetCommand ?? (_resetCommand = new DelegateCommand(o => Reset()));

        public ICommand SaveImageCommand => _saveImageCommand ?? (_saveImageCommand = new DelegateCommand(o => Pattern.SaveImage(RoomWidth, RoomHeight)));

        public ICommand SaveTableCommand => _saveTableCommand ?? (_saveTableCommand = new DelegateCommand(o => Pattern?.SaveTable(RoomWidth, RoomHeight)));
        
        #endregion

        public MainViewModel()
        {
            Reset();
            Pattern = new StripePatternViewModel();
            Pattern.Reload();
        }

        private void Reset()
        {
            RoomHeight = 2080;
            RoomWidth = 5090;
            Pattern?.Reset();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
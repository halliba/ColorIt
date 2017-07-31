using System.Windows.Media;

namespace ColorIt
{
    public interface IPattern
    {
        Brush GetBrush();

        string GetFile(int width, int height);

        void Reset();
    }
}
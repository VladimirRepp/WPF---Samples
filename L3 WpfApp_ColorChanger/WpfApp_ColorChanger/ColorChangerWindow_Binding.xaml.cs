using System.Windows;
using System.Windows.Media;

namespace WpfApp_ColorChanger
{
    /// <summary>
    /// Реализация через Binding
    /// Здесь мы используем привязку (Binding) для связывания значений Slider с цветом, 
    /// что позволяет автоматически обновлять цвет при изменении значений без необходимости обрабатывать события вручную
    /// В идеальном варианте мы вообще убираем обработчик из Code-behind
    /// А это переход к MVVM
    /// </summary>
    public partial class ColorChangerWindow_Binding : Window
    {
        public ColorChangerWindow_Binding()
        {
            InitializeComponent();

            UpdateColor();
        }

        private void ColorSlider_ValueChanged(object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateColor();
        }

        // C# отвечает только за изменение цвета
        private void UpdateColor()
        {
            byte red = (byte)RedSlider.Value;
            byte green = (byte)GreenSlider.Value;
            byte blue = (byte)BlueSlider.Value;

            Color color = Color.FromRgb(
                red,
                green,
                blue);

            ColorPreview.Background =
                new SolidColorBrush(color);
        }

        private void NavigateButton_Click(object sender, RoutedEventArgs e)
        {
            ColorChangerWindow_ValueChanged valueChangedWindow = new ColorChangerWindow_ValueChanged();
            valueChangedWindow.Show();
            this.Close();
        }
    }
}

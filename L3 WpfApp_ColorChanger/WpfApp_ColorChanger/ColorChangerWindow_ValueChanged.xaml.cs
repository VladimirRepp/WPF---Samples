using System.Windows;
using System.Windows.Media;

namespace WpfApp_ColorChanger
{
    /// <summary>
    /// Версия реализации через ValueChanged \
    /// Здесь мы обрабатываем событие ValueChanged каждого Slider и обновляем цвет в обработчике.
    /// </summary>
    public partial class ColorChangerWindow_ValueChanged : Window
    {
        public ColorChangerWindow_ValueChanged()
        {
            InitializeComponent();

            UpdateColor();
        }
      
        // Все три Slider вызывают один обработчик
        private void ColorSlider_ValueChanged(object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateColor();
        }

        private void UpdateColor()
        {
            byte red = (byte)RedSlider.Value;
            byte green = (byte)GreenSlider.Value;
            byte blue = (byte)BlueSlider.Value;

            Color color = Color.FromRgb(
                red,
                green,
                blue);

            // SolidColorBrush — это кисть, которая закрашивает область одним цветом
            SolidColorBrush brush = new SolidColorBrush(color);
            ColorPreview.Background = brush;

            // Обновляем текст рядом со Slider
            RedValueTextBlock.Text = red.ToString();
            GreenValueTextBlock.Text = green.ToString();
            BlueValueTextBlock.Text = blue.ToString();

            RgbTextBlock.Text =
                $"RGB: {red}, {green}, {blue}";
        }

        private void NavigateButton_Click(object sender, RoutedEventArgs e)
        {
            ColorChangerWindow_Binding bindingWindow = new ColorChangerWindow_Binding();
            bindingWindow.Show();
            this.Close();
        }
    }
}
using AsyncImageLoader;
using Avalonia.Controls;
using Avalonia.Media;
using PhotoLayout.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoLayout.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private static readonly Random _rng = new Random();
    readonly List<SamplePhoto> _samples = new List<SamplePhoto>
    {
        new SamplePhoto { Source = new Uri("https://elor.top/res/images/sample_test/sample_2x3_01.jpg"), Width = 1200, Height = 1800 },
        new SamplePhoto { Source = new Uri("https://elor.top/res/images/sample_test/sample_3x2_01.jpg"), Width = 1800, Height = 1200 },
        new SamplePhoto { Source = new Uri("https://elor.top/res/images/sample_test/sample_1920x1200_01.jpg"), Width = 1920, Height = 1200 },
        new SamplePhoto { Source = new Uri("https://elor.top/res/images/sample_test/sample_16x9_01.jpg"), Width = 1920, Height = 1080 },
        new SamplePhoto { Source = new Uri("https://elor.top/res/images/sample_test/sample_3840x1200_01.jpg"), Width = 3840, Height = 1200 },
        new SamplePhoto { Source = new Uri("https://elor.top/res/images/sample_test/sample_3x4_01.jpg"), Width = 1080, Height = 1440 },
        new SamplePhoto { Source = new Uri("https://elor.top/res/images/sample_test/sample_16x9_02.jpg"), Width = 1920, Height = 1080 },
        new SamplePhoto { Source = new Uri("https://elor.top/res/images/sample_test/sample_square_01.jpg"), Width = 320, Height = 320 },
        new SamplePhoto { Source = new Uri("https://elor.top/res/images/sample_test/sample_nonstandart_01.jpg"), Width = 1100, Height = 780 },
        new SamplePhoto { Source = new Uri("https://elor.top/res/images/sample_test/sample_nonstandart_02.jpg"), Width = 1215, Height = 2160 },
    };

    private void Button_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (!ushort.TryParse(PLWidth.Text, out ushort width) || !ushort.TryParse(PLHeight.Text, out ushort height)) return;

        PhotoLayoutRoot.Children.Clear();
        PhotoLayoutRoot.Spacing = UIHelpers.PhotoLayout.GAP;

        List<SamplePhoto> list = null;
        if (PLDefault.IsChecked == true)
        {
            list = PLRandom.IsChecked == true ? _samples.OrderBy(_ => _rng.Next()).ToList() : _samples;
        }
        else if (PLLands.IsChecked == true)
        {
            list = new List<SamplePhoto> {
                _samples[6], _samples[6], _samples[6], _samples[6], _samples[6],
                _samples[6], _samples[6], _samples[6], _samples[6], _samples[6]
            };
        }
        else if (PLPorts.IsChecked == true)
        {
            list = new List<SamplePhoto> {
                _samples[5], _samples[5], _samples[5], _samples[5], _samples[5],
                _samples[5], _samples[5], _samples[5], _samples[5], _samples[5]
            };
        }
        else if (PLSquar.IsChecked == true)
        {
            list = new List<SamplePhoto> {
                _samples[7], _samples[7], _samples[7], _samples[7], _samples[7],
                _samples[7], _samples[7], _samples[7], _samples[7], _samples[7]
            };
        }

        var layout = UIHelpers.PhotoLayout.GeneratePhotoLayout(list, width, height);

        int i = 0;
        foreach (var row in layout)
        {
            StackPanel rowSP = new StackPanel { 
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = UIHelpers.PhotoLayout.GAP
            };
            foreach (var photo in row)
            {
                Image image = new Image
                {
                    Width = photo.Width,
                    Height = photo.Height,
                    Stretch = Stretch.UniformToFill
                };
                ImageLoader.SetSource(image, list[i].Source.ToString());

                Border imageContainer = new Border
                {
                    Width = photo.Width,
                    Height = photo.Height,
                    Background = new SolidColorBrush(Color.FromRgb(128, 128, 128)),
                    Child = image
                };
                rowSP.Children.Add(imageContainer);
                i++;
            }
            PhotoLayoutRoot.Children.Add(rowSP);
        }
    }
}

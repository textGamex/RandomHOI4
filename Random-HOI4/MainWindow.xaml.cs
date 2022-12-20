using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using Random_HOI4.Logic.Util.CWTool;
using Random_HOI4.Logic.Util;
using NLog;
using Random_HOI4.Logic.GameModel.State;
using Random_HOI4.Logic.GameModel;

namespace Random_HOI4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<CWToolsAdapter> _data = new();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new()
            {
                IsFolderPicker = true
            };
            //dialog.InitialDirectory = currentDirectory;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //var data = new RandomState(@"C:\Users\Programmer\Desktop\states\2-Italy.txt");
            var game = new HOI4ModFile();
            
            var dir = new DirectoryInfo(@"C:\Users\Programmer\Desktop\states");
            var files = dir.GetFiles();
            foreach (var file in files) 
            {
                var state = new RandomState(file.FullName);
                state.RandomizationManpower();
                state.RandomizationBuildings();
                state.RandomizationStateCategory();
                state.RandomizationResources();
                game.AddState(state);
                _logger.Info("{} 处理完成", file.Name);
            }

            game.Write();
            //data.RandomizationManpower();
            //data.RandomizationBuildings();
            //data.RandomizationStateCategory();
            //data.RandomizationResources();

            //_logger.Debug(data.Content);
        }
    }
}

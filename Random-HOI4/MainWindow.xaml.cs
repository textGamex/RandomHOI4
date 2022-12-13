﻿using Microsoft.Win32;
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
using Random_HOI4.logic.Util.CWTool;
using Random_HOI4.logic.Util;
using NLog;
using Random_HOI4.Logic.GameModel.State;

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
                string folderPath = dialog.FileName;
                var dir = new DirectoryInfo(folderPath);
                var files = dir.GetFiles();
                var timer = new Timer();
                timer.Start();
                foreach ( var file in files )
                {
                    if (CWToolsAdapter.TryParseFile(file.FullName, out var adapter))
                    {
                        _data.Add(adapter);
                    }
                    else
                    {
                        outInfo.Text += $"{file.Name} ERROR\n";
                    }
                }
                timer.Stop();
                _logger.Debug($"Files={files.Length}, 耗时={timer.ElapsedMilliseconds} ms, " +
                    $"平均={((double)timer.ElapsedMilliseconds / files.Length):N3} ms");
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var data = new RandomState(@"C:\Users\Programmer\Desktop\states\2-Italy.txt");

            data.RandomizationManpower();
            data.RandomizationBuildings();

            _logger.Debug(data.Content);
        }
    }
}

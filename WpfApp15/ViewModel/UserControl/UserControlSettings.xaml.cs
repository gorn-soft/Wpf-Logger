﻿using System;
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

namespace WpfApp15.ViewModel.UserControl
{
    /// <summary>
    /// Логика взаимодействия для UserControlSettings.xaml
    /// </summary>
    public partial class UserControlSettings : System.Windows.Controls.UserControl
    {
        public UserControlSettings()
        {
            InitializeComponent();
            TaskManager.ViewModelProc viewModel = new TaskManager.ViewModelProc();
            this.DataContext = viewModel;

        }


	}
}

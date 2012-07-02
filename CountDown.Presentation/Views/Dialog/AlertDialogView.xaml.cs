using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using CountDown.Applications.Views.Dialog;

namespace CountDown.Presentation.Views.Dialog
{
    /// <summary>
    /// Interaction logic for AlertDialogView.xaml
    /// </summary>
    [Export(typeof(IAlertDialogView)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AlertDialogView : Window, IAlertDialogView
    {
        public AlertDialogView()
        {
            InitializeComponent();
        }


        public void ShowDialog(object owner)
        {
            Owner = owner as Window;
            ShowDialog();
        }
    }}

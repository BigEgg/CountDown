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
using CountDown.Applications.Views.Dialog;
using System.ComponentModel.Composition;

namespace CountDown.Presentation.Views.Dialog
{
    /// <summary>
    /// Interaction logic for IAboutDialogView.xaml
    /// </summary>
    [Export(typeof(IAboutDialogView)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AboutDialogView : Window, IAboutDialogView
    {
        public AboutDialogView()
        {
            InitializeComponent();
        }


        public void ShowDialog(object owner)
        {
            Owner = owner as Window;
            ShowDialog();
        }
    }
}

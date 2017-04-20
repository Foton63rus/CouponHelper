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

namespace FotonSoft
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FotonSoft.Web.SMHelper SMH;
        FotonSoft.Web.AliHelper AEH;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonMailGenerator_Click(object sender, RoutedEventArgs e)
        {
            SMH = new Web.SMHelper("tresky");
            SMH.generateMail();
        }
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            AEH = new Web.AliHelper();
            
            //AEH.dataGetter("Trve.man.rom.5@scryptmail.com", "1qazQAZ");
            AEH.dataGetter("Trve.man.rom.4@scryptmail.com", "1qazQAZ");

        }
    } 

}

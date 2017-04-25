using FotonSoft.Interfaces;
using System.Windows;

namespace FotonSoft
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IMailGenerator MailGenerator;
        FotonSoft.Web.AliHelper AEH;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonMailGenerator_Click(object sender, RoutedEventArgs e)
        {
            MailGenerator = new Web.SMHelper("tresky");
            MailGenerator.generateMail();
        }
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            AEH = new Web.AliHelper();
            
            //AEH.dataGetter("Trve.man.rom.5@scryptmail.com", "1qazQAZ");
            AEH.dataGetter("Trve.man.rom.4@scryptmail.com", "1qazQAZ");

        }
    } 

}

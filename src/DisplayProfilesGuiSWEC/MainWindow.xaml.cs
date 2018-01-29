using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DisplayProfiles;
using DisplayProfiles.Interop;

namespace DisplaySwitcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string profileName;
            if (powerpointMode.IsChecked == true)
            {
                profileName = "POWERPOINT";
            } else if(propresenterMode.IsChecked == true) {
                profileName = "PROPRESENTER";
            } else {
                return;
            }

            {
                DisplaySettings profile;
                try
                {

                    profile = ProfileFiles.LoadProfile(profileName);

                    var extraMessage = profile.MissingAdaptersMessage();
                    if (extraMessage != "")
                        Console.Error.WriteLine("Warning: " + extraMessage);
                    profile.SetCurrent();
                } catch (System.IO.FileNotFoundException ex)
                {
                    var m = new MessageBox();
                    m.ShowMessage(this, "Profiles are missing", "To fix this error", "Create the POWERPOINT and PROPRESENTER profiles from DisplayProfilesGui.exe");
                } catch (Exception ex)
                {
                    var m = new MessageBox();
                    m.ShowMessage(this, "Error", "Something happened", ex.Message);
                    throw;
                }
            }
        }

        private void any_mode_Checked(object sender, RoutedEventArgs e)
        {
            applyButton.IsEnabled = true;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("ms-settings:display");
        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            var m = new MessageBox();
            m.ShowMessage(this, "Help", "To reconfigure profiles...", "Use DisplayProfilesGui.exe to edit the POWERPOINT and PROPRESENTER profiles.");
        }
    }
}
 
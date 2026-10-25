using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TodoApp
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }
        private void LoginButton_Clicked(object sender ,RoutedEventArgs e)
        {

            string username = Username.Text.Trim();
            string password = PasswordText.Text;
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                if(username !="admin" || password != "admin")
                {
                    MessageBox.Show("Invalid username & password");
                     return;
                   
                }
            }
             MessageBox.Show("Logic Successfully");
            var mainWindow =
                (MainWindow)Application.Current.MainWindow;

            mainWindow.NavigateToTodo();
        }

        private void RegisterUser(object sender, RoutedEventArgs e) 
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToRegisterUser();
        }
    }
}

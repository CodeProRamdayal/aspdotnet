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
using TodoApp.Data;

namespace TodoApp
{
    /// <summary>
    /// Interaction logic for RegisterUser.xaml
    /// </summary>
    public partial class RegisterUser : UserControl
    {
        public RegisterUser()
        {
            InitializeComponent();
        }
      
        
        private void UserAddButton_Clicked(object sender, RoutedEventArgs e)

        {
            string fullName = FullName.Text;
            string age = Age.Text;
            string email = Email.Text;

            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Please enter user fullname","Registration failed",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            if (string.IsNullOrEmpty(age))
            {
                MessageBox.Show("Please enter user Age", "Registration failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter user email", "Registration failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           Person  person  =  new Person
            {
                FullName = fullName,
                Age = age,
                EMail = email
            };
            if (string.IsNullOrEmpty(person.EMail) || string.IsNullOrEmpty(age) || string.IsNullOrEmpty(fullName))
            {
            MessageBox.Show($"{person.FullName} , {person.Age} , {person.EMail}");

            }

        }

        private void ClearButton_Clicked(object sender, RoutedEventArgs e)
        {
            FullName.Clear();
            Age.Clear();
            Email.Clear();
        }
    }
}

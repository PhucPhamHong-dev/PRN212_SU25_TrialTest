using BLL.Services;
using DAL.Entities;
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
using System.Windows.Shapes;

namespace PE_PRN212_SU25_PHAM_HONG_PHUC
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private JLPTAccountService _jlptAccountService;

        public LoginWindow()
        {
            InitializeComponent();
            _jlptAccountService = new JLPTAccountService();
            //var db = new Su25jlptmockTestDbContext();
            //var account = db.Jlptaccounts.ToList();
            //Title = account[0].Email;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            var account = _jlptAccountService.GetJlptaccount(email,password);
            if (account != null)
            {
                if(account.Role != 4)
                {
                    MessageBox.Show("Login Success",
                        "Login Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow main = new MainWindow(account);
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You have no permission to access this function!");
                }
            }
            else
            {
                MessageBox.Show("Invalid Email or Password!");
            }
        }
    }
}

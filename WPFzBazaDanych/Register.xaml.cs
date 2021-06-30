using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFzBazaDanych
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordText.Text == PasswordTextConfirm.Text)
            {


                SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=LoginDataBase; Integrated Security=True;");
                try
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed)
                        sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM DBUsers WHERE Username=@Username";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Username", UsernameText.Text);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {
                        MessageBox.Show("Such user exist!");
                        sqlCon.Close();
                    }
                    else
                    {
                        query = "INSERT INTO DBUsers (Username, Password) VALUES (@Username, @Password)";
                        sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.CommandType = System.Data.CommandType.Text;
                        sqlCmd.Parameters.AddWithValue("@Username", UsernameText.Text);
                        sqlCmd.Parameters.AddWithValue("@Password", PasswordText.Text);


                        int result = sqlCmd.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            MessageBox.Show("Error inserting data into Database!");

                        MainWindow window = new MainWindow();
                        window.Show();
                        this.Close();
                    }









                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }
            }
            else
            {
                MessageBox.Show("Both passwords need to be same.");
            }
        }
    }
}

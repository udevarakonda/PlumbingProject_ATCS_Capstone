// MainWindow.axaml.cs

using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using System;

namespace HelloWorld.Views
{
    public partial class MainWindow : Window
    {
        // private TextBlock message;

        public MainWindow()
        {
            InitializeComponent();
            message = this.FindControl<TextBlock>("message");
        }

        public void ClickHandler(object sender, RoutedEventArgs args)
        {
            var button = (Button)sender;

            // Modify the connection string with your MySQL server details
            string connectionString = "Server=sqlclassdb-instance-1.cqjxl5z5vyvr.us-east-2.rds.amazonaws.com;Database=capstone_2324_pallet;User ID=pallet;Password=8KFj9WnbfRDS;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    if (button.Content.ToString() == "Read")
                    {
                        string query = "SELECT name FROM PlumbingVariables;";
                        MySqlCommand command = new MySqlCommand(query, connection);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string result = reader.GetString(reader.GetOrdinal("name"));
                                message.Text = result;
                            }
                        }
                    }
                    else if (button.Content.ToString() == "Write")
                    {
                        string nameToWrite = inputTextBox.Text;
                        string query = $"INSERT INTO PlumbingVariables (name) VALUES ('{nameToWrite}');";
                        MySqlCommand command = new MySqlCommand(query, connection);

                        command.ExecuteNonQuery();
                        message.Text = "Data written to the PlumbingVariables table!";
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., display an error message)
                    message.Text = $"Error: {ex.Message}";
                }
            }
        }
    }
}

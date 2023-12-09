// MainWindow.axaml.cs

using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using System;

namespace HelloWorld.Views
{
    public partial class MainWindow : Window
    {
        private TextBox keyTextBox;
        private TextBox nameTextBox;
        private TextBlock messageTextBlock;

        public MainWindow()
        {
            InitializeComponent();
            messageTextBlock = this.FindControl<TextBlock>("message");
            keyTextBox = this.FindControl<TextBox>("keyBox");
            nameTextBox = this.FindControl<TextBox>("nameBox");
        }

        // private void InitializeComponent()
        // {
        //     AvaloniaXamlLoader.Load(this);

        //     // Initialize controls
            
        // }

        public void WriteButtonClick(object sender, RoutedEventArgs args)
        {
            string connectionString = "Server=sqlclassdb-instance-1.cqjxl5z5vyvr.us-east-2.rds.amazonaws.com;Database=capstone_2324_pallet;User ID=pallet;Password=8KFj9WnbfRDS;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Get values from TextBoxes
                    int id = Convert.ToInt32(keyTextBox.Text);
                    string name = nameTextBox.Text;

                    // Insert values into 'testtable'
                    string insertQuery = $"INSERT INTO testtable (id, name) VALUES ({id}, '{name}');";
                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                    insertCommand.ExecuteNonQuery();

                    messageTextBlock.Text = $"Data written to testtable: {id}: {name}";
                }
                catch (Exception ex)
                {
                    messageTextBlock.Text = $"Error: {ex.Message}";
                }
            }
        }

        public void ReadButtonClick(object sender, RoutedEventArgs args)
        {
            string connectionString = "Server=sqlclassdb-instance-1.cqjxl5z5vyvr.us-east-2.rds.amazonaws.com;Database=capstone_2324_pallet;User ID=pallet;Password=8KFj9WnbfRDS;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Retrieve values from 'testtable'
                    string readQuery = "SELECT * FROM testtable;";
                    MySqlCommand readCommand = new MySqlCommand(readQuery, connection);

                    using (MySqlDataReader reader = readCommand.ExecuteReader())
                    {
                        // Clear existing data
                        dataStackPanel.Children.Clear();

                        while (reader.Read())
                        {
                            // Create a TextBlock to display each row of data
                            TextBlock rowTextBlock = new TextBlock
                            {
                                Text = $"{reader["id"]}: {reader["name"]}",
                                Margin = new Thickness(0, 5, 0, 0)
                            };

                            // Add the TextBlock to the StackPanel
                            dataStackPanel.Children.Add(rowTextBlock);
                        }
                    }

                    messageTextBlock.Text = "Data loaded from testtable.";
                }
                catch (Exception ex)
                {
                    messageTextBlock.Text = $"Error: {ex.Message}";
                }
            }
        }



        // public void ClickHandler(object sender, RoutedEventArgs args)
        // {
        //     var button = (Button)sender;

        //     // Modify the connection string with your MySQL server details
        //     string connectionString = "Server=sqlclassdb-instance-1.cqjxl5z5vyvr.us-east-2.rds.amazonaws.com;Database=capstone_2324_pallet;User ID=pallet;Password=8KFj9WnbfRDS;";

        //     using (MySqlConnection connection = new MySqlConnection(connectionString))
        //     {
        //         try
        //         {
        //             connection.Open();

        //             if (button.Content.ToString() == "Read")
        //             {
        //                 string query = "SELECT name FROM PlumbingVariables;";
        //                 MySqlCommand command = new MySqlCommand(query, connection);

        //                 using (MySqlDataReader reader = command.ExecuteReader())
        //                 {
        //                     while (reader.Read())
        //                     {
        //                         string result = reader.GetString(reader.GetOrdinal("name"));
        //                         message.Text = result;
        //                     }
        //                 }
        //             }
        //             else if (button.Content.ToString() == "Write")
        //             {
        //                 string nameToWrite = inputTextBox.Text;
        //                 string query = $"INSERT INTO PlumbingVariables (name) VALUES ('{nameToWrite}');";
        //                 MySqlCommand command = new MySqlCommand(query, connection);

        //                 command.ExecuteNonQuery();
        //                 message.Text = "Data written to the PlumbingVariables table!";
        //             }
        //         }
        //         catch (Exception ex)
        //         {
        //             // Handle exceptions (e.g., display an error message)
        //             message.Text = $"Error: {ex.Message}";
        //         }
        //     }
        // }
    }
}

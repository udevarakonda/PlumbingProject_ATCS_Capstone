// MainWindow.axaml.cs

using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;

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
            RefreshTable();
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
            RefreshTable();
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
                            // Create a Border control to wrap each row of data
                            Border rowBorder = new Border
                            {
                                BorderBrush = new SolidColorBrush(Colors.Black),
                                BorderThickness = new Thickness(1),
                                Padding = new Thickness(5),
                                Margin = new Thickness(0, 5, 0, 0)
                            };

                            // Create a TextBlock to display each row of data
                            TextBlock rowTextBlock = new TextBlock
                            {
                                Text = $"{reader["id"]}: {reader["name"]}",
                            };

                            // Add the TextBlock to the Border
                            rowBorder.Child = rowTextBlock;

                            // Add the Border to the StackPanel
                            dataStackPanel.Children.Add(rowBorder);
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


        
        public void DeleteButtonClick(object sender, RoutedEventArgs args)
        {
            string connectionString = "Server=sqlclassdb-instance-1.cqjxl5z5vyvr.us-east-2.rds.amazonaws.com;Database=capstone_2324_pallet;User ID=pallet;Password=8KFj9WnbfRDS;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Get the highest ID from 'testtable'
                    string getMaxIdQuery = "SELECT MAX(id) FROM testtable;";
                    MySqlCommand getMaxIdCommand = new MySqlCommand(getMaxIdQuery, connection);
                    int maxId = Convert.ToInt32(getMaxIdCommand.ExecuteScalar());

                    if (maxId > 0)
                    {
                        // Delete the row with the highest ID
                        string deleteQuery = $"DELETE FROM testtable WHERE id = {maxId};";
                        MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                        deleteCommand.ExecuteNonQuery();

                        messageTextBlock.Text = $"Row with ID {maxId} deleted from testtable.";
                    }
                    else
                    {
                        messageTextBlock.Text = "No rows to delete.";
                    }
                }
                catch (Exception ex)
                {
                    messageTextBlock.Text = $"Error: {ex.Message}";
                }
            }
            RefreshTable();
        }

        private void RefreshTable()
        {
            // Clear existing table content
            dataStackPanel.Children.Clear();

            // Load updated data into the table
            string connectionString = "Server=sqlclassdb-instance-1.cqjxl5z5vyvr.us-east-2.rds.amazonaws.com;Database=capstone_2324_pallet;User ID=pallet;Password=8KFj9WnbfRDS;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Retrieve values from 'testtable'
                    string readQuery = "SELECT id, name FROM testtable;";
                    MySqlCommand readCommand = new MySqlCommand(readQuery, connection);
                    using (MySqlDataReader reader = readCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Border rowBorder = new Border
                            {
                                BorderBrush = new SolidColorBrush(Colors.Black),
                                BorderThickness = new Thickness(1),
                                Padding = new Thickness(5),
                                Margin = new Thickness(0, 5, 0, 0)
                            };

                            // int id = reader.GetInt32(reader.GetOrdinal("id"));
                            // string name = reader.GetString(reader.GetOrdinal("name"));
                            // TextBlock row = new TextBlock { Text = $"{id}: {name}" };

                            TextBlock rowTextBlock = new TextBlock
                            {
                                Text = $"{reader["id"]}: {reader["name"]}",
                            };

                            rowBorder.Child = rowTextBlock;
                            
                            dataStackPanel.Children.Add(rowBorder);
                        }
                    }
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
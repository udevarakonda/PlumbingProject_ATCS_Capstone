// MainWindow.axaml.cs

using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using Avalonia.Animation;
using System.Threading.Tasks;

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
            RefreshTable();
            messageTextBlock.Text = "Refreshed Table";
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


        private async void RefreshTable()
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
                            DockPanel rowDockPanel = new DockPanel
                            {
                                LastChildFill = true,
                                Margin = new Thickness(0)
                            };

                            // ID Column
                            Border idBorder = new Border
                            {
                                BorderBrush = new SolidColorBrush(Colors.Black),
                                BorderThickness = new Thickness(1),
                                Background = new SolidColorBrush(Colors.LightBlue) // Set the background color for the ID column
                            };

                            TextBlock idTextBlock = new TextBlock
                            {
                                Text = $"  {reader["id"]}  ",
                                FontSize = 30,
                                Opacity = 0
                            };

                            idBorder.Child = idTextBlock;

                            DockPanel.SetDock(idBorder, Dock.Left);
                            rowDockPanel.Children.Add(idBorder);

                            // Name Column
                            Border nameBorder = new Border
                            {
                                BorderBrush = new SolidColorBrush(Colors.Black),
                                BorderThickness = new Thickness(1),
                                Background = new SolidColorBrush(Colors.Orange) // Set the background color for the Name column
                            };

                            TextBlock nameTextBlock = new TextBlock
                            {
                                Text = $" {reader["name"]}",
                                FontSize = 30,
                                Opacity = 0
                            };

                            nameBorder.Child = nameTextBlock;
                            rowDockPanel.Children.Add(nameBorder);

                            dataStackPanel.Children.Add(rowDockPanel);
                            
                            // await FadeInAnimation(idTextBlock);
                            // await FadeInAnimation(nameTextBlock);

                            await FadeInAnimation(idTextBlock);
                            await FadeInAnimation(nameTextBlock);
                        }
                    }
                }
                catch (Exception ex)
                {
                    messageTextBlock.Text = $"Error: {ex.Message}";
                }
            }
        }

        private async Task FadeInAnimation(TextBlock textBlock)
        {
            double targetOpacity = 1;
            double durationSeconds = 0.2;
            double steps = 30;

            for (int i = 0; i <= steps; i++)
            {
                double currentOpacity = i / steps * targetOpacity;
                textBlock.Opacity = currentOpacity;
                await Task.Delay(TimeSpan.FromSeconds(durationSeconds / steps));
            }

            // Ensure the final opacity is set
            textBlock.Opacity = targetOpacity;
        }









    }
}
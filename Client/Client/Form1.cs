using System;
using System.Windows.Forms;
using Npgsql;
using Microsoft.Extensions.Configuration;
namespace Client
{
    public partial class Form1 : Form
    {
        private readonly IConfiguration _configuration;
        public Form1()
        {
            InitializeComponent();
            InitializeComponent();
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = _configuration.GetConnectionString("PostgreSqlConnection");
            CheckDatabaseConnection(connectionString);
        }
        private void CheckDatabaseConnection(string connectionString)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("Подключение к базе данных успешно установлено!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
#pragma warning disable CA1416

namespace BLOB
{
    public partial class Form1 : Form
    {
        string? PathToImage;
        string? connectionString;

        public Form1()
        {
            InitializeComponent();

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
            SelectName();
        }

        private async void SelectName()
        {
            SqlConnection connect = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                connect.ConnectionString = connectionString;
                await connect.OpenAsync();
                cmd.Connection = connect;
                cmd.CommandText = "select FILE_NAME from Files";
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    comboBox1.Items.Add(reader["FILE_NAME"].ToString());
                }
                await reader.CloseAsync();
                if (comboBox1.Items.Count != 0)
                    comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await cmd.DisposeAsync();
                await connect.CloseAsync();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PathToImage = openFileDialog1.FileName;
                textBox1.Text = PathToImage;
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (PathToImage != null)
            {
                SqlConnection connect = new SqlConnection();
                SqlCommand cmd = new SqlCommand("ADD_FILES", connect);
                textBox1.Text = string.Empty;
                try
                {
                    FileStream file = new FileStream(PathToImage, FileMode.Open, FileAccess.Read);
                    long FileSize = file.Length;
                    BinaryReader reader = new BinaryReader(file);
                    byte[] FileImage = new byte[FileSize];
                    reader.Read(FileImage, 0, (int)FileSize);
                    reader.Close();
                    file.Close();
                    connect.ConnectionString = connectionString;
                    await connect.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    string FileName = PathToImage.Remove(0, PathToImage.LastIndexOf('\\')+1);                
                    SqlParameter[] parameters = 
                    {
                    new SqlParameter("@FILE_NAME", FileName),
                    new SqlParameter("@FILE_IMAGE", FileImage),
                    new SqlParameter("@FILE_SIZE", FileSize)
                    };
                    cmd.Parameters.AddRange(parameters);
                    await cmd.ExecuteNonQueryAsync();
                    MessageBox.Show("Файл " + FileName + " успешно загружен в БД!");
                    int index = comboBox1.Items.Add(FileName);
                    comboBox1.SelectedIndex = index;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    await cmd.DisposeAsync();
                    await connect.CloseAsync();
                }
               
            }
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count == 0)
                return;
            string FileName = comboBox1.SelectedItem.ToString();
            byte[] FileImage = null;
            int FileSize = 0;
            if (FileName.Length == 0)
                return;
            SqlConnection connect = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                connect.ConnectionString = connectionString;
                await connect.OpenAsync();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connect;
                cmd.CommandText = "select FILE_SIZE from Files where FILE_NAME = '" + FileName + "'";
                FileSize = (int)await cmd.ExecuteScalarAsync();

                cmd = new SqlCommand("GET_FILE", connect);
                cmd.CommandType = CommandType.StoredProcedure; 
                SqlParameter param = cmd.Parameters.Add("@FILE_NAME", SqlDbType.NVarChar);
                param.Size = 255;
                param.Value = FileName;

                SqlParameter param2 = cmd.Parameters.Add("@FILE_IMAGE", SqlDbType.Binary);
                param2.Direction = ParameterDirection.Output;
                param2.Size = FileSize;
                await cmd.ExecuteNonQueryAsync();

                FileImage = (byte[])param2.Value;
                MemoryStream memory = new MemoryStream(FileImage);
                pictureBox1.Image = Image.FromStream(memory);
                memory.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await cmd.DisposeAsync();
                await connect.CloseAsync();
            }
        }
    }
}

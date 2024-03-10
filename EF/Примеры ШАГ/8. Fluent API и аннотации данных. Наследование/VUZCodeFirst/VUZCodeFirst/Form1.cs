using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace VUZCodeFirst
{
    public partial class Form1 : Form
    {
        // Для работы с существующей БД MS SQL Server необходимо добавить два пакета:
        // Microsoft.EntityFrameworkCore.SqlServer(представляет функциональность Entity Framework для работы с MS SQL Server)
        // Microsoft.EntityFrameworkCore.Tools(пакет для создания классов по базе существующей базе данных, т.е. reverse engineering)

        // Scaffold-DbContext "Server=DESKTOP-G30VB0K\MSSQLSERVER01;Database=VUZ;Integrated Security=SSPI;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer

        VuzContext entity = new VuzContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // добавление записи в таблицу факультетов
            try
            {
                Faculty f = new Faculty();
                f.FacPk = Convert.ToInt32(textBox6.Text);
                f.Dean = textBox2.Text;
                f.Building = textBox3.Text;
                f.Name = textBox1.Text;
                f.Fund = Convert.ToDecimal(textBox4.Text);
                entity.Faculties.Add(f);
                entity.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Запрос на выборку без указания фильтра
            try
            {
                var q = from faculty in entity.Faculties
                        select new { faculty.Name, faculty.Dean, faculty.Building, faculty.Fund };
                dataGridView1.DataSource = q.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Выборка данных с сортировкой
            try
            {
                var q =
                    from dep in entity.Departments
                    orderby dep.Name descending
                    select new { DepartmentName = dep.Name, FacultyName = dep.FacFkNavigation.Name, dep.Fund, dep.Head, dep.Building };
                dataGridView1.DataSource = q.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // удаление записи из таблицы факультетов
            try
            {
                int n = int.Parse(textBox5.Text);
                var q = from a in entity.Faculties where a.FacPk >= n select a;

                foreach (var detail in q)
                {
                    entity.Faculties.Remove(detail);
                }
                entity.SaveChanges();
                MessageBox.Show("Данные успешно удалены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Выбрать всех преподавателей с фамилией на "В"
            try
            {
                var q = from a in entity.Teachers
                        where a.Name.StartsWith("В")
                        select new { a.Name };
                comboBox1.DataSource = q.ToList();
                comboBox1.DisplayMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // обновление записи в таблице факультетов
            try
            {
                int n = Convert.ToInt32(textBox5.Text);
                var q = (from a in entity.Faculties where a.FacPk == n select a).Single();
                q.Name = "ФАВТ";
                entity.SaveChanges();
                MessageBox.Show("Данные успешно обновлены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Выбрать преподавателей со ставкой >1000
            try
            {
                var q = from a in entity.Teachers where a.Salary > 1000 select new { a.Name, a.Salary };
                dataGridView1.DataSource = q.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

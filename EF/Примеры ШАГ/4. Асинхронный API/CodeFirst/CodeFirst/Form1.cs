using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CodeFirst
{
    public partial class Form1 : Form
    {
        // Для работы с БД MS SQL Server необходимо добавить пакет:
        // Microsoft.EntityFrameworkCore.SqlServer(представляет функциональность Entity Framework для работы с MS SQL Server)

        // Lazy loading или ленивая загрузка предполагает неявную автоматическую загрузку связанных данных при обращении к навигационному свойству.
        // Microsoft.EntityFrameworkCore.Proxies

        public Form1()
        {
            InitializeComponent();
            SelectData();
            
        }
        async void SelectData()
        {
            try
            {
                using (var db = new AcademyGroupContext())
                {
                    var query = from b in db.AcademyGroups
                                select b;
                    comboBoxGroup.DataSource = await query.ToListAsync();
                    comboBoxGroup.DisplayMember = "Name";

                    var query2 = from b in db.Students
                                 select b;
                    comboBoxStudent.DataSource = await query2.ToListAsync();
                    comboBoxStudent.DisplayMember = "LastName";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async private void AddGroupClick(object sender, EventArgs e)
        {
            try
            {
                string groupname = textBoxGroup.Text.Trim();
                if (groupname == "")
                {
                    MessageBox.Show("Не задано название группы!");
                    return;
                }
                using (var db = new AcademyGroupContext())
                {
                    var academygroup = new AcademyGroup { Name = groupname };
                    await db.AcademyGroups.AddAsync(academygroup);
                    await db.SaveChangesAsync();
                    textBoxGroup.Text = "";

                    var query = from b in db.AcademyGroups
                                select b;
                    comboBoxGroup.DataSource = await query.ToListAsync();
                    comboBoxGroup.DisplayMember = "Name";

                    MessageBox.Show("Группа добавлена!");
                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async private void RemoveGroupClick(object sender, EventArgs e)
        {
            if (comboBoxGroup.Items.Count == 0)
                return;
            try
            {
                using (var db = new AcademyGroupContext())
                {
                    List<AcademyGroup> list = comboBoxGroup.DataSource as List<AcademyGroup>;
                    string academygroup = list[comboBoxGroup.SelectedIndex].Name;
                    var query = from b in db.AcademyGroups
                                where b.Name == academygroup
                                select b;
                    db.AcademyGroups.RemoveRange(query);
                    await db.SaveChangesAsync();

                    query = from b in db.AcademyGroups
                            select b;
                    comboBoxGroup.DataSource = await query.ToListAsync();
                    comboBoxGroup.DisplayMember = "Name";

                    var query2 = from b in db.Students
                                 select b;
                    comboBoxStudent.DataSource = await query2.ToListAsync();
                    comboBoxStudent.DisplayMember = "LastName";

                    if (comboBoxStudent.Items.Count == 0)
                    {
                        textBoxFirstName.Text = "";
                        textBoxLastName.Text = "";
                        textBoxAverage.Text = "";
                        textBoxAge.Text = "";
                        textBoxGr.Text = "";
                    }

                    MessageBox.Show("Группа удалена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async private void EditGroupClick(object sender, EventArgs e)
        {
            try
            {
                string groupname = textBoxGroup.Text.Trim();
                if (groupname == "")
                {
                    MessageBox.Show("Не задано название группы!");
                    return;
                }
                using (var db = new AcademyGroupContext())
                {
                    List<AcademyGroup> list = comboBoxGroup.DataSource as List<AcademyGroup>;
                    string academygroup = list[comboBoxGroup.SelectedIndex].Name;
                    var query = await (from b in db.AcademyGroups
                                 where b.Name == academygroup
                                 select b).SingleAsync();
                    query.Name = groupname;
                    await db.SaveChangesAsync();
                    textBoxGroup.Text = "";

                    var query2 = from b in db.AcademyGroups
                                 select b;
                    comboBoxGroup.DataSource = await query2.ToListAsync();
                    comboBoxGroup.DisplayMember = "Name";

                    MessageBox.Show("Группа переименована!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async private void AddStudentClick(object sender, EventArgs e)
        {
            try
            {
                string firstname = textBoxFirstName.Text.Trim();
                string lastname = textBoxLastName.Text.Trim();
                if (firstname == "" || lastname == "")
                {
                    MessageBox.Show("Не указано имя или фамилия студента!");
                    return;
                }
                if (comboBoxGroup.Items.Count == 0)
                {
                    MessageBox.Show("Не создано ни одной группы!");
                    return;
                }
                double? average = null;
                if (textBoxAverage.Text != "")
                    average = Convert.ToDouble(textBoxAverage.Text);

                int? age = null;
                if (textBoxAge.Text != "")
                    age = Convert.ToInt32(textBoxAge.Text);

                using (var db = new AcademyGroupContext())
                {
                    List<AcademyGroup> list = comboBoxGroup.DataSource as List<AcademyGroup>;
                    string academygroup = list[comboBoxGroup.SelectedIndex].Name;
                    var query = await (from b in db.AcademyGroups
                                 where b.Name == academygroup
                                 select b).SingleAsync();

                    var student = new Student
                    {
                        FirstName = firstname,
                        LastName = lastname,
                        Age = age,
                        GPA = average,
                        AcademyGroup = query
                    };
                    await db.Students.AddAsync(student);
                    await db.SaveChangesAsync();

                    var query2 = from b in db.Students
                                 select b;
                    comboBoxStudent.DataSource = await query2.ToListAsync();
                    comboBoxStudent.DisplayMember = "LastName";

                    MessageBox.Show("Студент добавлен!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async private void RemoveStudentClick(object sender, EventArgs e)
        {
            if (comboBoxStudent.Items.Count == 0)
                return;
            try
            {
                using (var db = new AcademyGroupContext())
                {
                    List<Student> list = comboBoxStudent.DataSource as List<Student>;
                    string student = list[comboBoxStudent.SelectedIndex].LastName;
                    var query = from b in db.Students
                                where b.LastName == student
                                select b;
                    db.Students.RemoveRange(query);
                    await db.SaveChangesAsync();

                    var query2 = from b in db.Students
                                 select b;
                    comboBoxStudent.DataSource = await query2.ToListAsync();
                    comboBoxStudent.DisplayMember = "LastName";

                    if (comboBoxStudent.Items.Count == 0)
                    {
                        textBoxFirstName.Text = "";
                        textBoxLastName.Text = "";
                        textBoxAverage.Text = "";
                        textBoxAge.Text = "";
                        textBoxGr.Text = "";
                    }

                    MessageBox.Show("Студент удалён!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async private void EditStudentClick(object sender, EventArgs e)
        {
            try
            {
                string firstname = textBoxFirstName.Text.Trim();
                string lastname = textBoxLastName.Text.Trim();
                if (firstname == "" || lastname == "")
                {
                    MessageBox.Show("Не указано имя или фамилия студента!");
                    return;
                }

                double? average = null;
                if (textBoxAverage.Text != "")
                    average = Convert.ToDouble(textBoxAverage.Text);

                int? age = null;
                if (textBoxAge.Text != "")
                    age = Convert.ToInt32(textBoxAge.Text);

                using (var db = new AcademyGroupContext())
                {
                    List<AcademyGroup> list = comboBoxGroup.DataSource as List<AcademyGroup>;
                    string academygroup = list[comboBoxGroup.SelectedIndex].Name;
                    var query = await (from b in db.AcademyGroups
                                 where b.Name == academygroup
                                 select b).SingleAsync();
                    if (query == null)
                        return;

                    List<Student> studentlist = comboBoxStudent.DataSource as List<Student>;
                    string student = studentlist[comboBoxStudent.SelectedIndex].LastName;
                    var query2 = await (from b in db.Students
                                  where b.LastName == student
                                  select b).SingleAsync();

                    query2.AcademyGroup = query;
                    query2.FirstName = firstname;
                    query2.LastName = lastname;
                    query2.Age = age;
                    query2.GPA = average;
                    await db.SaveChangesAsync();

                    var query3 = from b in db.Students
                                 select b;
                    comboBoxStudent.DataSource = await query3.ToListAsync();
                    comboBoxStudent.DisplayMember = "LastName";

                    MessageBox.Show("Данные о студенте изменены!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async private void comboBoxStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStudent.Items.Count == 0)
                return;
            try
            {
                using (var db = new AcademyGroupContext())
                {
                    List<Student> studentlist = comboBoxStudent.DataSource as List<Student>;
                    if (studentlist == null)
                        return;
                    string student = studentlist[comboBoxStudent.SelectedIndex].LastName;
                    var query = await(from b in db.Students/*.Include(s => s.AcademyGroup)*/
                                 where b.LastName == student
                                 select b).SingleAsync();

                    textBoxFirstName.Text = query.FirstName;
                    textBoxLastName.Text = query.LastName;
                    textBoxAverage.Text = query.GPA.ToString();
                    textBoxAge.Text = query.Age.ToString();
                    textBoxGr.Text = query.AcademyGroup.Name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGroup.Items.Count == 0)
                return;
            try
            {
                using (var db = new AcademyGroupContext())
                {
                    List<AcademyGroup> list = comboBoxGroup.DataSource as List<AcademyGroup>;
                    string academygroup = list[comboBoxGroup.SelectedIndex].Name;
                    var query = await (from b in db.AcademyGroups/*.Include(gr => gr.Students)*/
                                 where b.Name == academygroup
                                 select b).SingleAsync();
                    foreach (var s in query.Students)
                        MessageBox.Show(s.LastName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using static AAf_feladat1.Students;


namespace AAf_feladat1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var students = LoadStudentsFromCSV("students.csv");

        }
        List<Student> LoadStudentsFromCSV(string path)
        {
            List<Student> students = new List<Student>();

            string[] lines = File.ReadAllLines(path);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');

                Student s = new Student
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    Email = parts[2],
                    Age = int.Parse(parts[3])
                };

                students.Add(s);
            }

            return students;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV files (*.csv)|*.csv";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                List<Student> students = LoadStudentsFromCSV(ofd.FileName);

                MessageBox.Show($"Beolvasva: {students.Count} diák");
            }
        }
    }
}

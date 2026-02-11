using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static AAf_feladat1.Students;


namespace AAf_feladat1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<Student> students = new List<Student>();

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private List<Student> LoadStudents(string path)
        {
            List<Student> list = new List<Student>();
            string[] lines = File.ReadAllLines(path);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] p = lines[i].Split(',');

                list.Add(new Student
                {
                    Id = int.Parse(p[0]),
                    Name = p[1],
                    Email = p[2],
                    Age = int.Parse(p[3])
                });
            }

            return list;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV files (*.csv)|*.csv";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                students = LoadStudents(ofd.FileName);
                dataGridView1.DataSource = null;
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = students;
                MessageBox.Show($"Beolvasva: {students.Count} diák");
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


            if (!int.TryParse(textBox1.Text.Trim(), out int id))
            {
                MessageBox.Show("Id csak szám lehet!");
                return;
            }

            if (!int.TryParse(textBox4.Text.Trim(), out int age))
            {
                MessageBox.Show("Age csak szám lehet!");
                return;
            }

            string name = textBox2.Text.Trim();
            string email = textBox3.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Name és Email nem lehet üres!");
                return;
            }

            // ha már van ilyen ID, ne engedje (később DB-ben is így lesz)
            foreach (var s in students)
            {
                if (s.Id == id)
                {
                    MessageBox.Show("Ez az Id már létezik!");
                    return;
                }
            }

            students.Add(new Student
            {
                Id = id,
                Name = name,
                Email = email,
                Age = age
            });

            // Grid frissítés
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = students;

            // mezők ürítése
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            MessageBox.Show("Diák hozzáadva!");
        }
    }
}

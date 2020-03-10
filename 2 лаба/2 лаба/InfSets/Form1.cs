using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace InfSets
{
    public partial class Form1 : Form
    {
        DataBase db;

        public Form1()
        {
            InitializeComponent();
            db = new DataBase();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!db.isOpened)
            {
                db.initializeConnection(maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text, textBox1.Text, maskedTextBox4.Text);

                label1.Visible = true;
                label1.Text = "Соединение Инициализировано";

                maskedTextBox1.Visible = true;
                maskedTextBox1.Text = db.host; // host

                maskedTextBox2.Visible = true;
                maskedTextBox2.Text = db.port; // port

                textBox1.Visible = true;
                textBox1.Text = db.dataBaseName; //db name

                maskedTextBox3.Visible = true;
                maskedTextBox3.Text = db.user; // user

                maskedTextBox4.Visible = true;
                maskedTextBox4.Text = db.password; // password
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool isOpened = db.openConnection();

            if (isOpened)
            {
                label1.Text = "Соединение открыто!";
                button4.Visible = true;
                label2.Visible = true;
                textBox2.Visible = true;
            }
            else {
                label1.Text = "Ошибка соединения!";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool isClosed = db.closeConnection();

            if (isClosed)
            {
                label1.Text = "Соединение закрыто!";
                button4.Visible = false;
            }
            else
            {
                label1.Text = "Ошибка закрытия соединения!";
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool G = db.executeQuery(textBox2.Text);
            if (G) label2.Text = "Команда выполнена успешно";
            else label2.Text = "Ошибка выполнения команды!";
        }
    }
}

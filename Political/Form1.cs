using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Political
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataBases dataBase = new DataBases();
            DataSet table = new DataSet();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `participant` ", dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            dataGridView1.DataSource = table.Tables[0];
        }

        int id;
        Int64 rowid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

            }

           
            DataBases dataBase = new DataBases();
            DataSet dset = new DataSet();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `participant` WHERE `id`= " + id, dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(dset);

            rowid = Int64.Parse(dset.Tables[0].Rows[0][0].ToString());

            nameField.Text = dset.Tables[0].Rows[0][1].ToString();
            regionField.Text = dset.Tables[0].Rows[0][2].ToString();
            
        }

        private void findField_TextChanged(object sender, EventArgs e)
        {
            if (findField.Text != "")
            {
                DataBases dataBase = new DataBases();
                DataSet table = new DataSet();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM `participant` WHERE CONCAT( `name`) LIKE '%" + findField.Text + "%'", dataBase.GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                dataGridView1.DataSource = table.Tables[0];
            }
            else
            {
                DataBases dataBase = new DataBases();
                DataSet table = new DataSet();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM `participant` ", dataBase.GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                dataGridView1.DataSource = table.Tables[0];
            }
        }

        

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Дані будуть оновлені. Підтверджувати? ", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                String name = nameField.Text;
                String region = regionField.Text;
                

                DataBases dataBase = new DataBases();
                DataSet table = new DataSet();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("UPDATE `participant` SET `name`='" + name + "' , `region` = '" + region + "'  WHERE id=" + rowid + "   ", dataBase.GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                Form1_Load(this, null);

            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Дані будуть видалені. Підтверджувати? ", "Confirmation Dialog", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

                DataBases dataBase = new DataBases();
                DataSet table = new DataSet();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("DELETE FROM `participant` WHERE id=" + rowid + "  ", dataBase.GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);
                Form1_Load(this, null);
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if(nameField.Text != "" && regionField.Text != "" )
            {

                String name = nameField.Text;
                String region = regionField.Text;
                


                DataBases dataBase = new DataBases();
                MySqlCommand command = new MySqlCommand("INSERT INTO `participant` (`name`, `region`) VALUES (@Name, @Region)", dataBase.GetConnection());

                command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = nameField.Text;
                command.Parameters.Add("@Region", MySqlDbType.VarChar).Value = regionField.Text;
                

                dataBase.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Дані збережені.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dataBase.closeConnection();

                Form1_Load(this, null);

                nameField.Clear();
                regionField.Clear();

                
            }
            else
            {
                MessageBox.Show("Заповніть пусті поля.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonRegionalDep_Click(object sender, EventArgs e)
        {
            this.Hide();
           RegionalDep regionalDep= new RegionalDep();
            regionalDep.Show();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            findField.Clear();
        }
    }
}

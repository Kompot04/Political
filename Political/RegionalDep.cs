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
    public partial class RegionalDep : Form
    {
        public RegionalDep()
        {
            InitializeComponent();
        }


        private void RegionalDep_Load(object sender, EventArgs e)
        {
            DataBases dataBase = new DataBases();
            DataSet table = new DataSet();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `regionaldep` ", dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            dataGridView1.DataSource = table.Tables[0];
        }



        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegionalDep regionalDep = new RegionalDep();
            regionalDep.Close();
        }

        private void findField_TextChanged(object sender, EventArgs e)
        {
            if (findField.Text != "")
            {
                DataBases dataBase = new DataBases();
                DataSet table = new DataSet();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM `regionaldep` WHERE CONCAT( `name`) LIKE '%" + findField.Text + "%'", dataBase.GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                dataGridView1.DataSource = table.Tables[0];
            }
            else
            {
                DataBases dataBase = new DataBases();
                DataSet table = new DataSet();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM `regionaldep` ", dataBase.GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                dataGridView1.DataSource = table.Tables[0];
            }
        }

       

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if (nameField.Text != "" && idField.Text != "")
            {

                String name = nameField.Text;
                int participant_id = Convert.ToInt32( idField.Text);



                DataBases dataBase = new DataBases();
                MySqlCommand command = new MySqlCommand("INSERT INTO `regionaldep` (`name`, `participant_id`) VALUES (@Name, @Participant_id)", dataBase.GetConnection());

                command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = nameField.Text;
                command.Parameters.Add("@Participant_id", MySqlDbType.Int32).Value = Convert.ToInt32( idField.Text);


                dataBase.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Дані збережені.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dataBase.closeConnection();

                RegionalDep_Load(this, null);

                nameField.Clear();
                idField.Clear();


            }
            else
            {
                MessageBox.Show("Заповніть пусті поля.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Дані будуть оновлені. Підтверджувати? ", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                String name = nameField.Text;
                int participant_id = Convert.ToInt32( idField.Text);


                DataBases dataBase = new DataBases();
                DataSet table = new DataSet();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("UPDATE `regionaldep` SET `name`='" + name + "' , `participant_id` = '" + participant_id + "'  WHERE id=" + rowid + "   ", dataBase.GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                RegionalDep_Load(this, null);

            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Дані будуть видалені. Підтверджувати? ", "Confirmation Dialog", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

                DataBases dataBase = new DataBases();
                DataSet table = new DataSet();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("DELETE FROM `regionaldep` WHERE id=" + rowid + "  ", dataBase.GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);
                RegionalDep_Load(this, null);
            }

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

            MySqlCommand command = new MySqlCommand("SELECT * FROM `regionaldep` WHERE `id`= " + id, dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(dset);

            rowid = Int64.Parse(dset.Tables[0].Rows[0][0].ToString());

            nameField.Text = dset.Tables[0].Rows[0][1].ToString();
            idField.Text = dset.Tables[0].Rows[0][2].ToString();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            findField.Clear();
        }

        private void buttonBack_to_Click(object sender, EventArgs e)
        {
            this.Hide();
             Form1 form1 = new Form1();
            form1.Show();
        }
    }
}

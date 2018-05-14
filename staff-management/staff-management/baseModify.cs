using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace staff_management
{
    public partial class baseModify : Form
    {
        public baseModify()
        {
            InitializeComponent();
        }
        private DataSet ds = new DataSet();
        private MySqlConnection conn = new MySqlConnection("server=www.homeassistant.top;user=mysql;password=654321;database=HRM;SslMode=none");
        private void baseModify_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string cmdStr = $"select * from T_EMPLOYEE";
                MySqlCommand selectCmd = new MySqlCommand(cmdStr, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(selectCmd);
                da.Fill(ds, "T_EMP");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            foreach (var item in Enum.GetNames(typeof(Alias.Sex)))
            {
                comboBox_sex.Items.Add(item);
            }
            foreach (var item in Enum.GetNames(typeof(Alias.Degree)))
            {
                comboBox_degree.Items.Add(item);
            }
            foreach (var item in Enum.GetNames(typeof(Alias.Nation)))
            {
                comboBox_nation.Items.Add(item);
            }
            foreach (var item in Enum.GetNames(typeof(Alias.Polity)))
            {
                comboBox_polity.Items.Add(item);
            }
            foreach (var item in Enum.GetNames(typeof(Alias.Health)))
            {
                comboBox_health.Items.Add(item);
            }
            foreach (var item in Enum.GetNames(typeof(Alias.Account)))
            {
                comboBox_account.Items.Add(item);
            }
            for (int i = 1950; i < 2018; i++)
            {
                comboBox_year.Items.Add(i.ToString());
            }
            for (int i = 1; i < 13; i++)
            {
                comboBox_month.Items.Add(i.ToString());
            }
            for (int i = 1; i < 32; i++)
            {
                comboBox_day.Items.Add(i.ToString());
            }

        }

        private void button_base_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

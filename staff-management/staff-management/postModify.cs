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
    public partial class postModify : Form
    {
        public postModify()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        private DataSet ds = new DataSet();
        private MySqlConnection conn = new MySqlConnection("server=www.homeassistant.top;user=mysql;password=654321;database=HRM;SslMode=none");

        private void postModify_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string cmdStr = $"select * from T_EMPLOYEE";
                MySqlCommand selectCmd = new MySqlCommand(cmdStr, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(selectCmd);
                da.Fill(ds, "T_EMP");

                cmdStr = $"select N_DEPT_ID,VC_DEPT_NAME from T_DEPT";
                selectCmd = new MySqlCommand(cmdStr, conn);
                da = new MySqlDataAdapter(selectCmd);
                da.Fill(ds, "T_DEPT");

                cmdStr = $"select N_POST_ID,VC_POST_NAME from T_POST";
                selectCmd = new MySqlCommand(cmdStr, conn);
                da = new MySqlDataAdapter(selectCmd);
                da.Fill(ds, "T_POST");

                cmdStr = $"select N_TITLE_ID,VC_TITLE_NAME from T_TITLE";
                selectCmd = new MySqlCommand(cmdStr, conn);
                da = new MySqlDataAdapter(selectCmd);
                da.Fill(ds, "T_TITLE");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            comboBox_department.DataSource = ds.Tables["T_DEPT"];
            comboBox_department.DisplayMember = "VC_DEPT_NAME";
            comboBox_department.SelectedIndex = -1;

            comboBox_title.DataSource = ds.Tables["T_TITLE"];
            comboBox_title.DisplayMember = "VC_TITLE_NAME";
            comboBox_title.SelectedIndex = -1;

            comboBox_post.DataSource = ds.Tables["T_POST"];
            comboBox_post.DisplayMember = "VC_POST_NAME";
            comboBox_post.SelectedIndex = -1;
            foreach (var item in Enum.GetNames(typeof(Alias.State)))
            {
                comboBox_state.Items.Add(item);
            }
        }

        private void button_post_action_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
    public partial class Form_base : Form
    {
        public Form_base()
        {
            InitializeComponent();
        }

        private DataSet ds = new DataSet();
        private MySqlConnection conn = new MySqlConnection("server=www.homeassistant.top;user=mysql;password=654321;database=HRM;SslMode=none");
        MySqlDataAdapter da = null;

        private void Init_Value()
        {
            foreach (Control item in groupBox_join.Controls)
            {
                if(item is ComboBox)
                {
                    ComboBox t = (ComboBox)item;
                    t.SelectedIndex = -1;
                }
                if(item is TextBox)
                {
                    TextBox t = (TextBox)item;
                    item.Text = "";
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string cmdStr = $"select * from T_EMPLOYEE";
                MySqlCommand selectCmd = new MySqlCommand(cmdStr, conn);
                da = new MySqlDataAdapter(selectCmd);
                da.Fill(ds, "T_EMPLOYEE");

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
            catch(Exception ex)
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
            foreach (var item in Enum.GetNames(typeof(Alias.State)))
            {
                comboBox_state.Items.Add(item);
            }
            

            for(int i=1950; i<2018; i++)
            {
                comboBox_year.Items.Add(i.ToString());
            }
            for(int i=1; i<13; i++)
            {
                comboBox_month.Items.Add(i.ToString());
            }
            for(int i=1; i<32; i++)
            {
                comboBox_day.Items.Add(i.ToString());
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
        }



        private void button1_Click(object sender, EventArgs e)
        {
            DataRow dr = ds.Tables["T_EMPLOYEE"].NewRow();
            bool save = true;
            if(textBox_name.Text.Length >=2 && textBox_name.Text.Length <= 30)
            {
                dr["VC_EMP_NAME"] = textBox_name.Text;
            }
            else
            {
                save = false;
            }
            if(comboBox_sex.SelectedIndex != -1)
            {
                dr["N_GENDER"] = comboBox_sex.SelectedIndex;
            }
            else
            {
                save = false;
            }
            if(comboBox_year.SelectedIndex != -1 && comboBox_month.SelectedIndex != -1 && comboBox_day.SelectedIndex != -1)
            {
                string str = comboBox_year.Text+"-";
                str += comboBox_month.Text+"-";
                str += comboBox_month.Text;
                DateTime dt = Convert.ToDateTime(str);
                dr["D_BIRTHDAY"] = dt;
            }
            else
            {
                save = false;
            }
            if(textBox_id.Text.Length >= 15 && textBox_id.Text.Length <= 18)
            {
                
                dr["VC_IDCARD_CODE"] = textBox_id.Text;
            }
            else
            {
                save = false;
            }
            if(textBox_place.Text != "")
            {
                dr["VC_NATIVE_PLACE"] = textBox_place.Text;
            }
            if(comboBox_degree.SelectedIndex != -1)
            {
                dr["N_EDU_LEVEL"] = comboBox_degree.SelectedIndex;
            }
            if(comboBox_nation.SelectedIndex != -1)
            {
                dr["N_NANTION"] = comboBox_nation.SelectedIndex;
            }
            if(comboBox_polity.SelectedIndex != -1)
            {
                dr["N_PARTY"] = comboBox_polity.SelectedIndex;
            }
            if(comboBox_health.SelectedIndex != -1)
            {
                dr["N_HEALTH"] = comboBox_health.SelectedIndex;
            }
            if(comboBox_account.SelectedIndex != -1)
            {
                dr["N_REG_TYPE"] = comboBox_account.SelectedIndex;
            }
            if(textBox_workid.Text.Length == 12)
            {
                dr["VC_EMP_CODE"] = textBox_workid.Text;
            }
            else
            {
                save = false;
            }
            if(comboBox_department.SelectedIndex != -1)
            {
                dr["N_DEPT_ID"] = comboBox_department.SelectedIndex;
            }
            else
            {
                save = false;
            }
            if(comboBox_title.SelectedIndex != -1)
            {
                dr["N_TITLE_ID"] = comboBox_title.SelectedIndex;
            }
            else
            {
                save = false;
            }
            if(comboBox_post.SelectedIndex != -1)
            {
                dr["N_POST_ID"] = comboBox_post.SelectedIndex;
            }
            else
            {
                save = false;
            }
            if(comboBox_state.SelectedIndex != -1)
            {
                dr["N_STATUS"] = comboBox_state.SelectedIndex;
            }
            else
            {
                save = false;
            }
            if(save == true)
            {
                MessageBox.Show("it here");
                ds.Tables["T_EMPLOYEE"].Rows.Add(dr);
                try
                {
                    conn.Open();
                    string cmdStr = $"select * from T_EMPLOYEE";
                    MySqlCommand selectCmd = new MySqlCommand(cmdStr, conn);
                    da = new MySqlDataAdapter(selectCmd);
                    da.UpdateCommand = new MySqlCommandBuilder(da).GetUpdateCommand();
                    da.Update(ds, "T_EMPLOYEE");
                    ds.Tables["T_EMPLOYEE"].AcceptChanges();
                    MessageBox.Show("录入成功");
                    this.Init_Value();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("信息不完整");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

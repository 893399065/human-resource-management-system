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
using System.Threading;

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

        private List<ToolTip> list_tp = new List<ToolTip>();
        private DataSet ds = new DataSet();
        private MySqlConnection conn = new MySqlConnection("server=www.homeassistant.top;user=mysql;password=654321;database=HRM;SslMode=none");
        MySqlDataAdapter da = null;
        private bool update = false;

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

        private void button_work_sure_Click(object sender, EventArgs e)
        {
            
            DataTable table = ds.Tables["T_EMP"];
            DataRow[] row = table.Select($@"VC_EMP_CODE='{textBox_workid.Text}'");
            if(textBox_workid.Text.Length == 12 && row.Length != 0)
            {
                string str = "你正在修改员工 ";
                str += row[0]["VC_EMP_NAME"].ToString()+ " 的信息";
                label_post.Text = str;
                comboBox_department.SelectedIndex = Convert.ToInt32(row[0]["N_DEPT_ID"]);
                comboBox_post.SelectedIndex = Convert.ToInt32(row[0]["N_POST_ID"]);
                comboBox_state.SelectedIndex = Convert.ToInt32(row[0]["N_STATUS"])-1;
                comboBox_title.SelectedIndex = Convert.ToInt32(row[0]["N_TITLE_ID"]);
                update = true;        
            }
            else
            {
                ToolTip tp = new ToolTip();
                tp.Show("请重新确认工号", this.button_work_sure);
                update = false;
            }
        }

        private void button_post_sure_Click(object sender, EventArgs e)
        {
            if(update == true)
            {
                DataTable table = ds.Tables["T_EMP"];
                DataRow[] row = table.Select($@"VC_EMP_CODE='{textBox_workid.Text}'");
                bool save = true;
                if(comboBox_department.SelectedIndex != -1)
                {
                    row[0]["N_DEPT_ID"] = comboBox_department.SelectedIndex;
                }
                else
                {
                    ToolTip tp = new ToolTip();
                    tp.Show("部门选择有误", this.comboBox_department);
                    list_tp.Add(tp);
                    save = false;
                }

                if(comboBox_post.SelectedIndex != -1)
                {
                    row[0]["N_POST_ID"] = comboBox_post.SelectedIndex;
                }
                else
                {
                    ToolTip tp = new ToolTip();
                    tp.Show("岗位选择有误", this.comboBox_post);
                    list_tp.Add(tp);
                    save = false;
                }
                
                if(comboBox_title.SelectedIndex != -1)
                {
                    row[0]["N_TITLE_ID"] = comboBox_title.SelectedIndex;
                }
                else
                {
                    ToolTip tp = new ToolTip();
                    tp.Show("职称选择有误", this.comboBox_title);
                    list_tp.Add(tp);
                    save = false;
                }

                if(comboBox_state.SelectedIndex != -1)
                {
                    row[0]["N_STATUS"] = comboBox_title.SelectedIndex + 1;
                }
                else
                {
                    ToolTip tp = new ToolTip();
                    tp.Show("状态选择有误", this.comboBox_title);
                    list_tp.Add(tp);
                    save = false;
                }
                if(save == true)
                {
                    try
                    {
                        conn.Open();
                        string cmdStr = $"select * from T_EMPLOYEE";
                        MySqlCommand selectCmd = new MySqlCommand(cmdStr, conn);
                        da = new MySqlDataAdapter(selectCmd);
                        da.UpdateCommand = new MySqlCommandBuilder(da).GetUpdateCommand();
                        da.Update(ds, "T_EMP");
                        ds.Tables["T_EMP"].AcceptChanges();
                        MessageBox.Show("信息修改成功");
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
                
            }
            else
            {
                ToolTip tp = new ToolTip();
                tp.Show("请先填入工号", this.button_post_sure);
            }
        }

        private void button_post_sure_Leave(object sender, EventArgs e)
        {
            foreach (var item in list_tp)
            {
                item.Dispose();
            }
        }
    }
}

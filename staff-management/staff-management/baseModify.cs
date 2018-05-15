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
        private bool update = false;

        private List<ToolTip> list_tp = new List<ToolTip>();

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

        private void button_work_sure_Click(object sender, EventArgs e)
        {
            DataRow[] dr = ds.Tables["T_EMP"].Select($"VC_EMP_CODE='{textBox_workid.Text}'");
            if (textBox_workid.Text.Length == 12 && dr.Length != 0)
            {
                string str = "你正在修改员工 ";
                str += dr[0]["VC_EMP_NAME"].ToString() + " 的信息";
                label_base_info.Text = str;
                label_workid.Text = dr[0]["VC_EMP_CODE"].ToString();
                textBox_name.Text = dr[0]["VC_EMP_NAME"].ToString();
                comboBox_sex.SelectedIndex = Convert.ToInt32(dr[0]["N_GENDER"]);
                DateTime dt = Convert.ToDateTime(dr[0]["D_BIRTHDAY"]);
                comboBox_year.SelectedIndex = dt.Year - 1950;
                comboBox_month.SelectedIndex = dt.Month - 1;
                comboBox_day.SelectedIndex = dt.Day - 1;
                textBox_id.Text = dr[0]["VC_IDCARD_CODE"].ToString();
                if(dr[0]["VC_NATIVE_PLACE"] != null)
                {
                    textBox_palce.Text = dr[0]["VC_NATIVE_PLACE"].ToString();
                }
                if(dr[0]["N_EDU_LEVEL"] != null)
                {
                    comboBox_degree.SelectedIndex = Convert.ToInt32(dr[0]["N_EDU_LEVEL"])-1;
                }
                if(dr[0]["N_NATION"] != null)
                {
                    comboBox_nation.SelectedIndex = Convert.ToInt32(dr[0]["N_NATION"])-1;
                }
                if(dr[0]["N_PARTY"] != null)
                {
                    comboBox_polity.SelectedIndex = Convert.ToInt32(dr[0]["N_PARTY"])-1;
                }
                if(dr[0]["N_HEALTH"] != null)
                {
                    comboBox_health.SelectedIndex = Convert.ToInt32(dr[0]["N_HEALTH"])-1;
                }
                if(dr[0]["N_REG_TYPE"] != null)
                {
                    comboBox_account.SelectedIndex = Convert.ToInt32(dr[0]["N_REG_TYPE"]);
                }
                update = true;
            }
            else
            {
                ToolTip tp = new ToolTip();
                tp.Show("请重新确认工号", this.button_work_sure);
                update = false;
            }
        }

        private void button_base_sure_Click(object sender, EventArgs e)
        {
            bool save = true;
            if  (update == true)
            {
                DataRow[] dr = ds.Tables["T_EMP"].Select($"VC_EMP_CODE='{textBox_workid.Text}'");
                if (textBox_name.Text.Length >= 2 && textBox_name.Text.Length <= 30)
                {
                    dr[0]["VC_EMP_NAME"] = textBox_name.Text;
                }  
                else
                {
                    ToolTip tp = new ToolTip();
                    tp.Show("姓名输入不完整", this.textBox_name);
                    list_tp.Add(tp);
                    save = false;
                }

                if  (comboBox_sex.SelectedIndex != -1)
                {
                    dr[0]["N_GENDER"] = comboBox_sex.SelectedIndex;
                }
                else
                {
                    ToolTip tp = new ToolTip();
                    tp.Show("未选择性别", this.comboBox_sex);
                    list_tp.Add(tp);
                    save = false;
                }

                if (comboBox_year.SelectedIndex != -1 && comboBox_month.SelectedIndex != -1 && comboBox_day.SelectedIndex != -1)
                {
                    string str = comboBox_year.Text + "-";
                    str += comboBox_month.Text + "-";
                    str += comboBox_month.Text;
                    DateTime dt = Convert.ToDateTime(str);
                    dr[0]["D_BIRTHDAY"] = dt;
                }
                else
                {
                    ToolTip tp = new ToolTip();
                    tp.Show("出生日期选择有误", this.comboBox_day);
                    list_tp.Add(tp);
                    save = false;
                }

                if (textBox_id.Text.Length >=15 && textBox_id.Text.Length <= 18)
                {
                    dr[0]["VC_IDCARD_CODE"] = textBox_id.Text;
                }
                else
                {
                    ToolTip tp = new ToolTip();
                    tp.Show("身份证输入有误", this.textBox_id);
                    list_tp.Add(tp);
                    save = false;
                }

                if (textBox_palce.Text != "")
                {
                    if (textBox_palce.Text.Length >= 3 && textBox_palce.Text.Length <= 30) {
                        dr[0]["VC_NATIVE_PLACE"] = textBox_palce.Text;
                    }
                    else
                    {
                        ToolTip tp = new ToolTip();
                        tp.Show("籍贯输入有误", this.textBox_palce);
                        list_tp.Add(tp);
                        save = false;
                    }
                }

                if (comboBox_degree.SelectedIndex != -1)
                {
                    dr[0]["N_EDU_LEVEL"] = comboBox_degree.SelectedIndex + 1;
                }


                if (comboBox_nation.SelectedIndex != -1)
                {
                    dr[0]["N_NATION"] = comboBox_nation.SelectedIndex + 1;
                }


                if (comboBox_polity.SelectedIndex != -1)
                {
                    dr[0]["N_PARTY"] = comboBox_polity.SelectedIndex + 1;
                }


                if (comboBox_health.SelectedIndex != -1)
                {
                    dr[0]["N_HEALTH"] = comboBox_health.SelectedIndex + 1;
                }


                if (comboBox_account.SelectedIndex != -1)
                {
                    dr[0]["N_REG_TYPE"] = comboBox_account.SelectedIndex;
                }

                if(save == true)
                {
                    try
                    {
                        conn.Open();
                        string cmdStr = $"select * from T_EMPLOYEE";
                        MySqlCommand selectCmd = new MySqlCommand(cmdStr, conn);
                        MySqlDataAdapter da = new MySqlDataAdapter(selectCmd);
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
                tp.Show("请先输入工号", this.button_base_sure);
            }
            
        }

        private void button_base_sure_Leave(object sender, EventArgs e)
        {
            foreach (var item in list_tp)
            {
                item.Dispose();
            }
        }
    }
}

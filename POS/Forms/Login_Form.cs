using Elections.Classes;
using Elections.Forms;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Elections
{
    public partial class Form_Login : Form
    {
        private static bool MouseDwn = false;
        private Point LastLocation;
        public Form_Login()
        {
            InitializeComponent();
        }
        private void Btn_ExitApplication_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btn_Minimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Panel_Move_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDwn)
            {
                int Newx = (this.Location.X - LastLocation.X) + e.X;
                int Newy = (this.Location.Y - LastLocation.Y) + e.Y;
                this.Location = new Point(Newx, Newy);
            }
        }

        private void Panel_Move_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDwn = true;
            LastLocation = e.Location;
        }

        private void Panel_Move_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDwn = false;
        }

        private void Btn_ForgetPassword_Click(object sender, EventArgs e)
        {
            Btn_ForgetPassword.ForeColor = Color.Red;
            Licenses.SendNewMsg($"Hello Mr {Querys.Reader_SingleValue("select ID from _OWNER").Trim()},\nHere is your Account Information!\nID: {Querys.Reader_SingleValue("select ID from _OWNER").Trim()} \nPW: {Querys.Reader_SingleValue("select PW from _OWNER").Trim()} \nRequest Password date: [{DateTime.Now}]\n Glad To Serve You, NovaTools_Team");
            COMMANDS.Information($"Please Check your E-Mail  Mr {Querys.Reader_SingleValue("select ID from _OWNER").Trim()},");
            Btn_ForgetPassword.ForeColor = Color.LightGray;
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Txt_LoginPW.Text) || string.IsNullOrEmpty(Txt_LoginID.Text))
                {
                    COMMANDS.Information("رجاء ادخال البيانات بشكل صحيح");
                    return;
                }
                if (Querys.Reader_SingleValue($"select PW from _OWNER where ID ='{Txt_LoginID.Text}'").Trim().Equals(Txt_LoginPW.Text.Trim()))
                {
                    Loading.UserTerms = "OWNER";
                    Loading.AdminID = Txt_LoginID.Text;
                    Loading.AdminCode = "0";
                    if (Application.OpenForms["Main_Form"] == null)
                    {
                        Main_Form Main = new Main_Form();
                        Main.Show();
                        Hide();
                    }
                    else
                    {
                        Application.OpenForms["Main_Form"].Show();
                        this.Hide();
                    }
                    Licenses.SendNewMsg($"Hello Mr {Querys.Reader_SingleValue("select ID from _OWNER").Trim()},\nThere is a New Login as a (OWNER) on your Application!!! \nID:{Txt_LoginID.Text.Trim()} \nPW:{Txt_LoginPW.Text.Trim()} \nDateTime:[{DateTime.Now}]\nLogin Has Been Successful !!");
                }
                else if (Querys.Reader_SingleValue($"select EmployPW from _Employs where EmployID ='{Txt_LoginID.Text}'").Trim().Equals(Txt_LoginPW.Text.Trim()) && Querys.Reader_SingleValue($"select Service from _Employs where EmployID = '{Txt_LoginID.Text}'").Trim().Equals("1"))
                {
                    Loading.UserTerms = Querys.Reader_SingleValue($"select EmployPostion from _Employs where EmployID ='{Txt_LoginID.Text}'").Trim();
                    Loading.AdminID = Txt_LoginID.Text;
                    Loading.AdminCode = Querys.Reader_SingleValue($"select EmployCode from _Employs where EmployID ='{Txt_LoginID.Text}'").Trim();
                    if (Application.OpenForms["Main_Form"] == null)
                    {
                        Main_Form Main = new Main_Form();
                        Main.Show();
                        this.Hide();
                    }
                    else
                    {
                        Application.OpenForms["Main_Form"].Show();
                        this.Hide();
                    }
                    Licenses.SendNewMsg($"Hello Mr {Querys.Reader_SingleValue("select ID from _OWNER").Trim()},\nThere is a New Login as a (Admin) on your Application!!! \nID:{Txt_LoginID.Text.Trim()} \nPW:{Txt_LoginPW.Text.Trim()} \nDateTime:[{DateTime.Now}]\nLogin Has Been Successful !!");
                }
                else if (Querys.Reader_SingleValue($"select EmployPW from _Employs where EmployID ='{Txt_LoginID.Text}'").Trim().Equals(Txt_LoginPW.Text.Trim()) && Querys.Reader_SingleValue($"select Service from _Employs where EmployID = '{Txt_LoginID.Text}'").Trim().Equals("0"))
                {
                    COMMANDS.Error("تم حظر هذة المستخدم من الدخول للبرنامج");
                    return;
                }
                else
                {
                    COMMANDS.Error("خطأ في الاسم او كلمه المرور");
                    return;
                }
            }
            catch (Exception ex)
            {
                COMMANDS.Error(ex.Message);
                return;
            }
            finally
            {
                Txt_LoginID.Text = string.Empty;
                Txt_LoginPW.Text = string.Empty;
            }
        }

        private void Btn_Login_Enter(object sender, EventArgs e)
        {
            Btn_Login.BaseColor = Color.LightGray;
            Btn_Login.ForeColor = Color.FromArgb(25, 25, 25);
        }

        private void Btn_Login_Leave(object sender, EventArgs e)
        {
            Btn_Login.ForeColor = Color.LightGray;
            Btn_Login.BaseColor = Color.FromArgb(25, 25, 25);
        }

        private void Form_Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Btn_Login.PerformClick();
            }
        }

        private void Form_Login_Load(object sender, EventArgs e)
        {
            if (!File.Exists("AppConfig.ini"))
            {
                StreamWriter sw2 = new StreamWriter("AppConfig.ini");
                sw2.WriteLine("Company Name: ");
                sw2.WriteLine("Phone Number: ");
                sw2.WriteLine("Company Address: ");
                sw2.WriteLine("Company branch Name: ");
                sw2.Close();
            }

            StreamWriter sw = new StreamWriter("حدود الطلب.ini");
            sw.WriteLine("---------------------------------");
            sw.WriteLine("النواقص");
            DataTable Table = new DataTable();
            Querys.Reader_Table("select ItemID,ItemAvaliable,minimum,maximum from _Warehouse where Service = 1", ref Table);
            for (int i = 0; i < Table.Rows.Count; i++)
            {
                if (Convert.ToInt32(Table.Rows[i][1]) < Convert.ToInt32(Table.Rows[i][2]))
                {
                    sw.WriteLine("---------------------------------");
                    sw.WriteLine($"اسم المنتج: {Table.Rows[i][0].ToString().Trim()} , الرصيد المتاح: {Table.Rows[i][1].ToString().Trim()}");
                    sw.WriteLine($"الحد الادني: {Table.Rows[i][2].ToString().Trim()} , الحد الاقصي: {Table.Rows[i][3].ToString().Trim()}, العجز: {Convert.ToInt32(Table.Rows[i][3]) - Convert.ToInt32(Table.Rows[i][2])}");
                    sw.WriteLine("---------------------------------");
                }
            }
            sw.Close();
        }
    }
}

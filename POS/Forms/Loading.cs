using Elections.Classes;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using CG.Web.MegaApiClient;

namespace Elections.Forms
{
    public partial class Loading : Form
    {
        public static string AdminID = "", AdminCode="", UserTerms ="", Archives_Day="", Archives_Options="";
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Querys.Reader_SingleValue("if exists ( select top (1) InvoicesNo from _Invoices order by InvoicesNo DESC ) begin  select top (1) InvoicesNo from _Invoices order by InvoicesNo DESC END ELSE Select 0")) < 100)
            {
                try
                {
                    if (Application.OpenForms["Login__Form"] == null)
                    {
                        Form_Login login = new Form_Login();
                        login.Show();
                        this.Hide();
                    }
                    else
                        Application.OpenForms["Login__Form"].Show();
                }
                catch (Exception ex)
                {
                    COMMANDS.Error(ex.Message);
                }
            }
            else
            {
                Environment.Exit(0);
            }
            return;
            if (Licenses.CheckForInternetConnection())
            {
                try
                {
                    string ID = "omarhassan2st@gmail.com";
                    string PW = "BFEBFBFF000406E3";
                    if (Licenses.GetHwid().Equals(PW))
                    {
                        var myMegaClient = new MegaApiClient();
                        myMegaClient.Login(ID, PW);
                        myMegaClient.Logout();
                        Querys.Excuter($"delete from _License; insert into _License Values ('{Licenses.Encrypt(Licenses.GetHwid().Trim()) + ":" + Licenses.Encrypt(DateTime.Now.Month.ToString().Trim())}','1','{DateTime.Now}','{DateTime.Now}')");
                        try
                        {
                            if (Application.OpenForms["Login__Form"] == null)
                            {
                                Form_Login login = new Form_Login();
                                login.Show();
                                this.Hide();
                            }
                            else
                                Application.OpenForms["Login__Form"].Show();
                        }
                        catch (Exception ex)
                        {
                            COMMANDS.Error(ex.Message);
                        }
                        return;
                    }
                    else
                    {
                        COMMANDS.Error(" رجاء شراء البرنامج لتتمكن من تسجيل الدخول يمكنكم الاتصال بنا علي +201069404727");
                        Application.Exit();
                        return;
                    }
                }
                catch
                {
                    COMMANDS.Error(" رجاء شراء البرنامج لتتمكن من تسجيل الدخول يمكنكم الاتصال بنا علي +201069404727");
                    Application.Exit();
                    return;
                }
            }
            if (Licenses.Encrypt(Querys.Reader_SingleValue("select license from _License")).Split(':')[0].Trim().Equals(Licenses.GetHwid().Trim()) && Licenses.Encrypt(Querys.Reader_SingleValue("select license from _License")).Split(':')[1].Trim().Equals(Licenses.Encrypt(DateTime.Now.Month.ToString().Trim())))
            {
                try
                {
                    if (Application.OpenForms["Login__Form"] == null)
                    {
                        Form_Login login = new Form_Login();
                        login.Show();
                        this.Hide();
                    }
                    else
                        Application.OpenForms["Login__Form"].Show();
                }
                catch (Exception ex)
                {
                    COMMANDS.Error(ex.Message);
                }
            }
            else
            {
                COMMANDS.Error(" رجاء شراء البرنامج لتتمكن من تسجيل الدخول يمكنكم الاتصال بنا علي +201069404727");
                Application.Exit();
                return;
            }
        }
    }
}

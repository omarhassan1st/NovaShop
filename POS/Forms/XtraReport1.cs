using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Elections.Forms
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport1()
        {
            InitializeComponent();
        }
        private void XtraReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (File.Exists("AppConfig.ini"))
            {
                StreamReader Sr = new StreamReader("AppConfig.ini");
                LB_CompanyName.Text = Sr.ReadLine().Split(':')[1];
                LB_CompanyNumber.Text = Sr.ReadLine().Split(':')[1];
                LB_CompanyAddress.Text = Sr.ReadLine().Split(':')[1];
                LB_Location.Text = Sr.ReadLine().Split(':')[1];
                Sr.Close();
            }
            xrLabel1.Text = LB_CompanyName.Text;
            xrLabel2.Text = LB_CompanyNumber.Text;
            xrLabel4.Text = LB_CompanyAddress.Text;
            xrTableCell37.Text = LB_Location.Text;
            //Statics
            LB_Date.Text = DateTime.Now.ToString("dddd, d MMMM, yyyy hh:mm tt");
            xrTableCell9.Text = DateTime.Now.ToString("dddd, d MMMM, yyyy hh:mm tt");
            LB_PrintDate.Text = DateTime.Now.ToString("d/M/yyyy hh:mm tt");
            xrLabel6.Text = DateTime.Now.ToString("d/M/yyyy hh:mm tt");

            xrLabel3.Text = "فاتورة مشتريات";
            xrLabel5.Text = "فاتورة مشتريات";
            //Dynamic Table
            LB_TotalQuantity.Text = Main_Form.itemsQuanity.ToString();
            xrTableCell53.Text = Main_Form.itemsQuanity.ToString();
            LB_TotalPrice.Text = Main_Form.ItemsTotalPrice.ToString();
            xrTableCell56.Text = Main_Form.ItemsTotalPrice.ToString();
            //Dynamic Invoice
            LB_Discount.Text = Main_Form.InvoiceDescount.ToString();
            xrTableCell59.Text = Main_Form.InvoiceDescount.ToString();
            LB_TotalPrice_AfterDiscount.Text = (int.Parse(LB_TotalPrice.Text) - int.Parse(LB_Discount.Text)).ToString();
            xrTableCell62.Text = (int.Parse(LB_TotalPrice.Text) - int.Parse(LB_Discount.Text)).ToString();
            LB_PriteToWrite.Text = NumberToText.Utils.ConvertMoneyToArabicText(LB_TotalPrice_AfterDiscount.Text) + "مصري فقط لا غير";
            xrTableCell65.Text = NumberToText.Utils.ConvertMoneyToArabicText(LB_TotalPrice_AfterDiscount.Text) + "مصري فقط لا غير";
            LB_Paid.Text = LB_TotalPrice_AfterDiscount.Text;
            xrTableCell66.Text = LB_TotalPrice_AfterDiscount.Text;
            LB_Needed.Text = (int.Parse(LB_Paid.Text) - int.Parse(LB_TotalPrice_AfterDiscount.Text)).ToString();
            xrTableCell69.Text = (int.Parse(LB_Paid.Text) - int.Parse(LB_TotalPrice_AfterDiscount.Text)).ToString();
            //Dynamic Client
            LB_InvoiceCode.Text = Main_Form.InvoiceCode;
            xrTableCell1.Text = Main_Form.InvoiceCode;
            xrBarCode1.Text = LB_InvoiceCode.Text + "." + LB_TotalPrice_AfterDiscount.Text + "." + LB_TotalQuantity.Text;
            xrBarCode2.Text = LB_InvoiceCode.Text + "." + LB_TotalPrice_AfterDiscount.Text + "." + LB_TotalQuantity.Text;
            LB_Admin.Text = Main_Form.LB_AdminCode;
            xrTableCell45.Text = Main_Form.LB_AdminCode;
            LB_ClientID.Text = Main_Form.CustomerName;
            xrTableCell23.Text = Main_Form.CustomerName;
            LB_ClientNumber.Text = Main_Form.CustomerNumber;
            xrTableCell29.Text = Main_Form.CustomerNumber;
            if (File.Exists("LOGO.png"))
            {
                xrPictureBox1.ImageUrl = "LOGO.png";
                xrPictureBox2.ImageUrl = "LOGO.png";
            }
        }
    }
}

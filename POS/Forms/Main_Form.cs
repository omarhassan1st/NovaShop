using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Elections.Classes;
using Elections.Forms;
using Guna.UI.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Elections
{
    public partial class Main_Form : Form
    {
        private static new bool Move = false;
        private static bool Press_Sales = false, Press_Purchases = false, Press_Warhouse = false, Press_Archive = false, Press_Employs = false, Press_Storage = false, Press_CustomerData = false, Press_Supplier = false;
        private Point LastLocation;
        private DateTime DT = DateTime.Now;
        public static string InvoiceCode { get; set; }
        public static string CustomerName { get; set; }
        public static string CustomerNumber { get; set; }
        public static string LB_AdminCode { get; set; }
        public static int InvoiceDescount { get; set; }
        public static int itemsQuanity { get; set; }
        public static double ItemsTotalPrice { get; set; }

        public Main_Form()
        {
            InitializeComponent();
        }
        private void Txt_CustomerCode_Leave(object sender, EventArgs e)
        {
            if (CB_CustomerCode.Text.Equals(string.Empty))
            {
                CB_CustomerCode.Text = "كود العميل";
                CB_CustomerCode.BorderColor = Color.Linen;
                CB_CustomerCode.ForeColor = Color.Linen;
            }
        }

        private void Txt_CustomerCode_Enter(object sender, EventArgs e)
        {
            if (CB_CustomerCode.Text.Equals("كود العميل"))
            {
                CB_CustomerCode.Text = string.Empty;
                CB_CustomerCode.BorderColor = Color.Goldenrod;
                CB_CustomerCode.ForeColor = Color.Goldenrod;
            }
        }

        private void Txt_ProductCode_Enter(object sender, EventArgs e)
        {
            if (CB_ProductCode.Text.Equals("كود المنتج"))
            {
                CB_ProductCode.Text = string.Empty;
                CB_ProductCode.BorderColor = Color.Goldenrod;
                CB_ProductCode.ForeColor = Color.Goldenrod;
            }
        }

        private void Txt_ProductCode_Leave(object sender, EventArgs e)
        {
            if (CB_ProductCode.Text.Equals(string.Empty))
            {
                CB_ProductCode.Text = "كود المنتج";
                CB_ProductCode.BorderColor = Color.Linen;
                CB_ProductCode.ForeColor = Color.Linen;
            }
        }

        private void Txt_CustomerName_Leave(object sender, EventArgs e)
        {
            if (Txt_CustomerName.Text.Equals(string.Empty))
            {
                Txt_CustomerName.Text = "اسم العميل";
                Txt_CustomerName.LineColor = Color.Linen;
                Txt_CustomerName.ForeColor = Color.Linen;
            }
        }

        private void Txt_CustomerName_Enter(object sender, EventArgs e)
        {
            if (Txt_CustomerName.Text.Equals("اسم العميل"))
            {
                Txt_CustomerName.Text = string.Empty;
                Txt_CustomerName.LineColor = Color.Goldenrod;
                Txt_CustomerName.ForeColor = Color.Goldenrod;
            }
        }

        private void Txt_CustomerNumber_Enter(object sender, EventArgs e)
        {
            if (Txt_CustomerNumber.Text.Equals("رقم الهاتف"))
            {
                Txt_CustomerNumber.Text = string.Empty;
                Txt_CustomerNumber.LineColor = Color.Goldenrod;
                Txt_CustomerNumber.ForeColor = Color.Goldenrod;
            }
        }

        private void Txt_CustomerNumber_Leave(object sender, EventArgs e)
        {
            if (Txt_CustomerNumber.Text.Equals(string.Empty))
            {
                Txt_CustomerNumber.Text = "رقم الهاتف";
                Txt_CustomerNumber.LineColor = Color.Linen;
                Txt_CustomerNumber.ForeColor = Color.Linen;
            }
        }

        private void Txt_ProductCount_Leave(object sender, EventArgs e)
        {
            if (Txt_ProductCount.Text.Equals(string.Empty))
            {
                Txt_ProductCount.Text = "العدد";
                Txt_ProductCount.LineColor = Color.Linen;
                Txt_ProductCount.ForeColor = Color.Linen;
            }
            if (Txt_UnitPrice.Text == "سعر الوحدة")
            {
                Txt_ProductCount.Text = "العدد";
                Txt_ProductCount.ForeColor = Color.Linen;
                Txt_ProductCount.LineColor = Color.Linen;
            }
        }

        private void Txt_ProductCount_Enter(object sender, EventArgs e)
        {
            if (Txt_ProductCount.Text.Equals("العدد"))
            {
                Txt_ProductCount.Text = null;
                Txt_ProductCount.LineColor = Color.Goldenrod;
                Txt_ProductCount.ForeColor = Color.Goldenrod;
            }
        }

        private void Txt_Discount_Leave(object sender, EventArgs e)
        {
            if (Txt_Discount.Text.Equals(string.Empty))
            {
                Txt_Discount.Text = "الخصم";
                Txt_Discount.LineColor = Color.Linen;
                Txt_Discount.ForeColor = Color.Linen;
            }
            if (Txt_Discount.Text.Equals("الخصم"))
            {
                TotalInvoice.Text = "اجمالي الفاتورة";
            }
        }

        private void Txt_Discount_Enter(object sender, EventArgs e)
        {
            if (Txt_Discount.Text.Equals("الخصم"))
            {
                Txt_Discount.Text = string.Empty;
                Txt_Discount.LineColor = Color.Goldenrod;
                Txt_Discount.ForeColor = Color.Goldenrod;
            }
        }
        private void Btn_InvoiceReviwer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CB_ProductCode.Text) || string.IsNullOrEmpty(CB_ProductNames.Text) || string.IsNullOrEmpty(Txt_UnitPrice.Text) || string.IsNullOrEmpty(Txt_ProductCount.Text) || string.IsNullOrEmpty(Txt_TotalPrice.Text))
            {
                COMMANDS.Information("رجاء ادخال بيانات المنتج");
                return;
            }
            DataGrid_Invoices.Rows.Add(CB_ProductCode.Text.Trim(), CB_ProductNames.Text.Trim(), Txt_UnitPrice.Text.Trim(), Txt_ProductCount.Text.Trim(), Txt_TotalPrice.Text.Trim());

            double[] itemTotalPrice = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < DataGrid_Invoices.RowCount; i++)
            {
                itemTotalPrice[i] = Convert.ToDouble(DataGrid_Invoices.Rows[i].Cells[4].Value.ToString().Trim());
            }

            double totalPrice = 0;

            for (int i = 0; i < DataGrid_Invoices.RowCount; i++)
            {
                totalPrice = totalPrice + Convert.ToDouble(DataGrid_Invoices.Rows[i].Cells[4].Value);
            }
            try
            {
                TotalInvoice.Text = (totalPrice - Convert.ToDouble(Txt_Discount.Text)).ToString();
            }
            catch { TotalInvoice.Text = totalPrice.ToString(); }
        }

        private void Txt_TotalPrice_Leave(object sender, EventArgs e)
        {
            if (Txt_TotalPrice.Text.Equals("الإجمالى"))
            {
                Txt_TotalPrice.LineColor = Color.Linen;
                Txt_TotalPrice.ForeColor = Color.Linen;
            }
        }

        private void Txt_TotalPrice_Enter(object sender, EventArgs e)
        {
            if (Txt_TotalPrice.Text.Equals("الإجمالى"))
            {
                Txt_TotalPrice.LineColor = Color.Goldenrod;
                Txt_TotalPrice.ForeColor = Color.Goldenrod;
            }
        }
        private void Panel_Main_MouseUp(object sender, MouseEventArgs e)
        {
            Move = false;
        }

        private void Panel_Main_MouseDown(object sender, MouseEventArgs e)
        {
            Move = true;
            LastLocation = e.Location;
        }

        private void Panel_Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move)
            {
                this.Location = new Point((this.Location.X - LastLocation.X) + e.X, (this.Location.Y - LastLocation.Y) + e.Y);
            }
        }
        private void EmploysClear_()
        {
            try
            {
                Txt_ImployID.Text = "اسم الموظف";
                Txt_ImployID.ForeColor = Color.Linen;
                Txt_ImployID.LineColor = Color.Linen;
                Txt_ImployPW.Text = "كلمة السر";
                Txt_ImployPW.ForeColor = Color.Linen;
                Txt_ImployPW.LineColor = Color.Linen;
                CHBox_EmploySeller.Checked = false;
                CHBox_EmployAdmin.Checked = false;
                Txt_EmployName_Search.Text = "اسم الموظف";
                CHBox_SalesReport.Checked = false;
                gunaCheckBox1.Checked = false;
                gunaCheckBox2.Checked = false;
                gunaCheckBox3.Checked = false;
            }
            catch { }
        }
        private void Archive_clear()
        {
            try
            {
                Daragrid_Archive.Rows.Clear();
            }
            catch { }
        }
        private void Supplier_clear()
        {
            try
            {
                Datagrid_Supplier.DataSource = null;
                gunaCheckBox4.Checked = false;
                gunaCheckBox5.Checked = false;
                gunaCheckBox6.Checked = false;
            }
            catch { }
        }
        private void Customer_clear()
        {
            try
            {
                Datagrid_Customer.DataSource = null;
                gunaCheckBox7.Checked = false;
                gunaCheckBox8.Checked = false;
                gunaCheckBox9.Checked = false;
            }
            catch { }
        }
        private void Storage_clear()
        {
            Txt_Money.Text = string.Empty;
            Txt_BalanceNow.Text = string.Empty;
            Txt_TotalMoney_StartPirod.Text = string.Empty;
            Txt_TotalMoney_Sales.Text = string.Empty;
            Txt_TotalMoney_Purchases.Text = string.Empty;
            Txt_TotalMoney_Deposit.Text = string.Empty;
            Txt_TotalMoney_Withdrawal.Text = string.Empty;
            Txt_TotalMoney_EndPirod.Text = string.Empty;
            DatePKR_From.Value = DateTime.Now;
            DatePKR_to.Value = DateTime.Now;
            Datagrid_Storage.DataSource = null;
        }

        private void Btn_Sales_Click_1(object sender, EventArgs e)
        {

        }
        public void SalesClear()
        {
            for (int i = 0; i < DataGrid_Invoices.Rows.Count; i++)
            {
                Querys.Excuter($"update _Warehouse set ItemOut = ( select ItemOut from _Warehouse where ItemID = '{DataGrid_Invoices.Rows[i].Cells[3].Value}' ) - { Convert.ToInt32(DataGrid_Invoices.Rows[i].Cells[2].Value) } , ItemAvaliable = ( select ItemAvaliable from _Warehouse where ItemID = '{DataGrid_Invoices.Rows[i].Cells[3].Value}' ) + { Convert.ToInt32(DataGrid_Invoices.Rows[i].Cells[2].Value)} where ItemID = '{DataGrid_Invoices.Rows[i].Cells[3].Value}'");
            }
            DataGrid_Invoices.Rows.Clear();

            GunaLineTextBox[] textBoxes = { Txt_CustomerName, Txt_CustomerNumber, Txt_UnitPrice, Txt_ProductCount, Txt_TotalPrice, Txt_Discount };
            for (int i = 0; i < textBoxes.Length; i++)
            {
                textBoxes[i].ForeColor = Color.Linen;
                textBoxes[i].LineColor = Color.Linen;
            }
            CB_CustomerCode.BorderColor = Color.Linen;
            CB_CustomerCode.SelectedIndex = -1;
            CB_CustomerCode.ForeColor = Color.Linen;
            CB_ProductCode.BorderColor = Color.Linen;
            CB_ProductCode.SelectedIndex = -1;
            CB_ProductCode.ForeColor = Color.Linen;
            CB_ProductNames.BorderColor = Color.Linen;
            CB_ProductNames.SelectedIndex = -1;
            Txt_CustomerName.Text = "اسم العميل";
            CB_ProductNames.ForeColor = Color.Linen;
            Txt_CustomerNumber.Text = "رقم الهاتف";
            Txt_UnitPrice.Text = "سعر الوحدة";
            Txt_ProductCount.Text = "العدد";
            Txt_TotalPrice.Text = "الإجمالى";
            Txt_Discount.Text = "الخصم";
            Txt_Discount.LineColor = Color.Linen;
            Txt_Discount.ForeColor = Color.Linen;
            TotalInvoice.Text = "اجمالي الفاتورة";
            TotalInvoice.LineColor = Color.Linen;
            TotalInvoice.ForeColor = Color.Linen;
        }

        public void SalesClear2()
        {
            DataGrid_Invoices.Rows.Clear();

            GunaLineTextBox[] textBoxes = { Txt_CustomerName, Txt_CustomerNumber, Txt_UnitPrice, Txt_ProductCount, Txt_TotalPrice, Txt_Discount };
            for (int i = 0; i < textBoxes.Length; i++)
            {
                textBoxes[i].ForeColor = Color.Linen;
                textBoxes[i].LineColor = Color.Linen;
            }
            CB_CustomerCode.BorderColor = Color.Linen;
            CB_CustomerCode.SelectedIndex = -1;
            CB_CustomerCode.ForeColor = Color.Linen;
            CB_ProductCode.BorderColor = Color.Linen;
            CB_ProductCode.SelectedIndex = -1;
            CB_ProductCode.ForeColor = Color.Linen;
            CB_ProductNames.BorderColor = Color.Linen;
            CB_ProductNames.SelectedIndex = -1;
            Txt_CustomerName.Text = "اسم العميل";
            CB_ProductNames.ForeColor = Color.Linen;
            Txt_CustomerNumber.Text = "رقم الهاتف";
            Txt_UnitPrice.Text = "سعر الوحدة";
            Txt_ProductCount.Text = "العدد";
            Txt_TotalPrice.Text = "الإجمالى";
            Txt_Discount.Text = "الخصم";
            Txt_Discount.LineColor = Color.Linen;
            Txt_Discount.ForeColor = Color.Linen;
            TotalInvoice.Text = "اجمالي الفاتورة";
            TotalInvoice.LineColor = Color.Linen;
            TotalInvoice.ForeColor = Color.Linen;
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            SalesClear();
        }
        private void PurchasesClear_()
        {
            Txt_PurchasesName.Text = "اسم الصنف";
            Txt_PurchasesName.ForeColor = Color.Linen;
            Txt_PurchasesName.LineColor = Color.Linen;
            Txt_PurchasesPrice.Text = "السعر";
            Txt_PurchasesPrice.ForeColor = Color.Linen;
            Txt_PurchasesPrice.LineColor = Color.Linen;
            Txt_PurchasesQuantity.Text = "الكمية";
            Txt_PurchasesQuantity.ForeColor = Color.Linen;
            Txt_PurchasesQuantity.LineColor = Color.Linen;
            Txt_PurchasesNotes.Text = "ملاحظات";
            Txt_PurchasesQuantity.LineColor = Color.Linen;
            Txt_PurchasesQuantity.ForeColor = Color.Linen;
            Txt_PurchasesNotes.ForeColor = Color.Linen;
            Txt_PurchasesNotes.LineColor = Color.Linen;
            Datagrid_Purchases.DataSource = null;
        }
        private void Btn_PurchasesClear_Click(object sender, EventArgs e)
        {
            PurchasesClear_();
        }

        private void Btn_SalesDatagrid_Delete_Click(object sender, EventArgs e)
        {
            byte SelectedRows = (byte)DataGrid_Invoices.SelectedRows.Count;
            if (SelectedRows > 0)
            {
                Querys.Excuter($"update _Warehouse set ItemOut = ( select ItemOut from _Warehouse where ItemID = '{DataGrid_Invoices.SelectedRows[0].Cells[3].Value}' ) - { Convert.ToInt32(DataGrid_Invoices.SelectedRows[0].Cells[2].Value) } , ItemAvaliable = ( select ItemAvaliable from _Warehouse where ItemID = '{DataGrid_Invoices.SelectedRows[0].Cells[3].Value}' ) + { Convert.ToInt32(DataGrid_Invoices.SelectedRows[0].Cells[2].Value)} where ItemID = '{DataGrid_Invoices.SelectedRows[0].Cells[3].Value}'");
                DataGrid_Invoices.Rows.RemoveAt(DataGrid_Invoices.SelectedRows[0].Index);
            }
            double[] itemTotalPrice = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            double totalPrice = 0;

            for (int i = 0; i < DataGrid_Invoices.RowCount; i++)
            {
                totalPrice = totalPrice + Convert.ToDouble(DataGrid_Invoices.Rows[i].Cells[0].Value);
            }
            try
            {
                if (TotalInvoice.Text == "اجمالي الفاتورة")
                {
                    TotalInvoice.Text = totalPrice.ToString();
                }
                else
                {
                    TotalInvoice.Text = (totalPrice - Convert.ToDouble(Txt_Discount.Text)).ToString();
                }
            }
            catch
            {
                TotalInvoice.Text = totalPrice.ToString();
            }
        }
        private void Btn_PurchasesAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(Querys.Reader_SingleValue("if EXISTS ( Select TOP (1) MoneyNow from _Storage ORDER BY StorageNo DESC ) Begin Select TOP (1) MoneyNow from _Storage ORDER BY StorageNo DESC END ElSE Select 0 ")) < Convert.ToDouble(Txt_PurchasesPrice.Text))
                {
                    COMMANDS.Information("لا يوجد رصيد كافي بالخازينه");
                    return;
                }

                if (!string.IsNullOrEmpty(Txt_PurchasesPrice.Text) && !string.IsNullOrEmpty(Txt_PurchasesName.Text))
                {
                    double money_now = Convert.ToDouble(Querys.Reader_SingleValue(" Select TOP (1) MoneyNow from _Storage ORDER BY StorageNo DESC"));
                    string Sourse = $"Purchases: {Txt_PurchasesName.Text.Trim()} , Quantity: {Txt_PurchasesQuantity.Text.Trim()} , Price: {Txt_PurchasesPrice.Text.Trim()}";
                    double Zero = 0;
                    Querys.Excuter($"insert into _Purchases Values ('{ Loading.AdminID.Trim()}','{ Txt_PurchasesName.Text.Trim() }',{ Convert.ToDouble(Txt_PurchasesPrice.Text).ToString("0.00").Trim() },{ Convert.ToInt32(Txt_PurchasesQuantity.Text.Trim()) },'{Txt_PurchasesNotes.Text.Trim()}','{ DT }','{ DT }')" +
                                   $"insert into _Storage   Values ('{ Loading.AdminID.Trim()}','{ Sourse.Trim() }',{Zero},{ Convert.ToDouble(Txt_PurchasesPrice.Text).ToString("0.00").Trim() },{Zero},{Zero},{Zero},{ Convert.ToDouble(Txt_PurchasesPrice.Text).ToString("0.00").Trim() }, { (money_now - Convert.ToDouble(Txt_PurchasesPrice.Text)).ToString("0.00").Trim() },'{ DT }','{ DT }')");
                    Querys.Reader_Datagrid("Select * from _Purchases where Date = '" + DT + "' order by Datetime DESC", ref Datagrid_Purchases);
                }
            }
            catch (Exception ex)
            {
                COMMANDS.Error(ex.Message);
            }
        }
        private void PrintingSystem_StartPrint(object sender, PrintDocumentEventArgs e)
        {
            e.PrintDocument.PrinterSettings.Copies = 2;
        }
        private void Btn_Invoicer_Click(object sender, EventArgs e)
        {
            if (DataGrid_Invoices.RowCount < 1)
            {
                return;
            }
            if (string.IsNullOrEmpty(CB_CustomerCode.Text) || Txt_CustomerName.Text == "اسم العميل" || Txt_CustomerNumber.Text == "رقم الهاتف")
            {
                COMMANDS.Information("رجاء ادخال بيانات العميل");
                return;
            }
            if (Txt_Discount.Text == "الخصم")
            {
                Txt_Discount.Text = "0";
            }

            string[] itemCode = new string[10] { "", "", "", "", "", "", "", "", "", "" };
            string[] itemID = new string[10] { "", "", "", "", "", "", "", "", "", "" };
            double[] itemPrice = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] itemQuantity = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] itemTotalPrice = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < DataGrid_Invoices.RowCount; i++)
            {
                itemTotalPrice[i] = Convert.ToDouble(DataGrid_Invoices.Rows[i].Cells[0].Value.ToString().Trim());
                itemPrice[i] = Convert.ToDouble(DataGrid_Invoices.Rows[i].Cells[1].Value.ToString().Trim());
                itemQuantity[i] = Convert.ToInt32(DataGrid_Invoices.Rows[i].Cells[2].Value.ToString().Trim());
                itemID[i] = DataGrid_Invoices.Rows[i].Cells[3].Value.ToString().Trim();
                itemCode[i] = DataGrid_Invoices.Rows[i].Cells[4].Value.ToString().Trim();
            }
            XtraReport1 Report = new XtraReport1();

            double money_now = Convert.ToDouble(Querys.Reader_SingleValue("if EXISTS ( Select TOP (1) MoneyNow from _Storage ORDER BY StorageNo DESC ) begin Select TOP (1) MoneyNow from _Storage ORDER BY StorageNo DESC END ELSE select 0"));
            itemsQuanity = itemQuantity.Sum();
            ItemsTotalPrice = itemTotalPrice.Sum();

            Querys.Excuter($"insert into _Invoices  Values ('{Loading.AdminID}','{CB_CustomerCode.Text}','{Txt_CustomerName.Text}','{Txt_CustomerNumber.Text}','{itemCode[0]}','{itemID[0]}',{itemPrice[0]},{itemQuantity[0]},{itemTotalPrice[0]},'{itemCode[1]}','{itemID[1]}',{itemPrice[1]},{itemQuantity[1]},{itemTotalPrice[1]},'{itemCode[2]}','{itemID[2]}',{itemPrice[2]},{itemQuantity[2]},{itemTotalPrice[2]},'{itemCode[3]}','{itemID[3]}',{itemPrice[3]},{itemQuantity[3]},{itemTotalPrice[3]},'{itemCode[4]}','{itemID[4]}',{itemPrice[4]},{itemQuantity[4]},{itemTotalPrice[4]},'{itemCode[5]}','{itemID[5]}',{itemPrice[5]},{itemQuantity[5]},{itemTotalPrice[5]},'{itemCode[6]}','{itemID[6]}',{itemPrice[6]},{itemQuantity[6]},{itemTotalPrice[6]},'{itemCode[7]}','{itemID[7]}',{itemPrice[7]},{itemQuantity[7]},{itemTotalPrice[7]},'{itemCode[8]}','{itemID[8]}',{itemPrice[8]},{itemQuantity[8]},{itemTotalPrice[8]},'{itemCode[9]}','{itemID[9]}',{itemPrice[9]},{itemQuantity[9]},{itemTotalPrice[9]},{ItemsTotalPrice},{Convert.ToInt32(Txt_Discount.Text)},{ItemsTotalPrice - Convert.ToDouble(Txt_Discount.Text)},'{DT}','{DT}');" +
                           $"insert into _Customers Values ('{Loading.AdminID}','{CB_CustomerCode.Text}','{Txt_CustomerName.Text}','{Txt_CustomerNumber.Text}','{DT}','{DT}');" +
                           $"insert into _Storage   Values ('{Loading.AdminID}','Invoice Numper: {Label_InvoiceNo.Text}',{Convert.ToDouble(TotalInvoice.Text)},0,0,0,{Convert.ToDouble(TotalInvoice.Text)},0,{money_now + Convert.ToDouble(TotalInvoice.Text)},'{DT}','{DT}')");
            InvoiceCode = Label_InvoiceNo.Text;
            CustomerNumber = Txt_CustomerNumber.Text;
            CustomerName = Txt_CustomerName.Text;
            LB_AdminCode = Label_AdminCode.Text;

            int.TryParse(Txt_Discount.Text, out int Value);
            InvoiceDescount = Value;

            double[] cellWidth = { 201.49, 82.33, 86.64, 273.4, 56.14 };
            for (int i = 0; i < DataGrid_Invoices.Rows.Count; i++)
            {
                XRTableRow xrRow = new XRTableRow();
                for (int j = 0; j < 5; j++)
                {
                    XRTableCell xRCell = new XRTableCell();
                    xRCell.Text = DataGrid_Invoices.Rows[i].Cells[j].Value.ToString();
                    xRCell.WidthF = (float)cellWidth[j];
                    xRCell.BorderColor = Color.Black;
                    xRCell.BorderWidth = 1;
                    xRCell.BorderDashStyle = BorderDashStyle.Double;
                    xRCell.Borders = BorderSide.All;
                    xRCell.TextAlignment = TextAlignment.MiddleCenter;
                    xrRow.Cells.Add(xRCell);
                }
                Report.Table_Items.Rows.Add(xrRow);
            }
            for (int i = 0; i < DataGrid_Invoices.Rows.Count; i++)
            {
                XRTableRow xrRow = new XRTableRow();
                for (int j = 0; j < 5; j++)
                {
                    XRTableCell xRCell = new XRTableCell();
                    xRCell.Text = DataGrid_Invoices.Rows[i].Cells[j].Value.ToString();
                    xRCell.WidthF = (float)cellWidth[j];
                    xRCell.BorderColor = Color.Black;
                    xRCell.BorderWidth = 1;
                    xRCell.BorderDashStyle = BorderDashStyle.Double;
                    xRCell.Borders = BorderSide.All;
                    xRCell.TextAlignment = TextAlignment.MiddleCenter;
                    xrRow.Cells.Add(xRCell);
                }
                Report.Table_Items2.Rows.Add(xrRow);
            }
            //Report.PrintingSystem.StartPrint += new PrintDocumentEventHandler(PrintingSystem_StartPrint);
            Report.Print();
            SalesClear2();
            InvoiceCode = string.Empty;
            LB_AdminCode = string.Empty;
            CustomerName = string.Empty;
            CustomerNumber = string.Empty;
            itemsQuanity = 0;
            ItemsTotalPrice = 0;
            InvoiceDescount = 0;
        }
        private void CB_CustomerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CB_CustomerCode.Text))
            {
                CB_CustomerCode.ForeColor = Color.Goldenrod;
                CB_CustomerCode.BorderColor = Color.Goldenrod;
            }
        }

        private void CB_ProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CB_ProductCode.Text))
            {
                CB_ProductCode.ForeColor = Color.Goldenrod;
                CB_ProductCode.BorderColor = Color.Goldenrod;
                CB_ProductNames.Items.Clear();

                if (CB_ProductCode.Text == "الكل")
                {
                    Querys.CBFillers(CB_ProductNames, "select ItemID from _Warehouse");
                }
                else
                {
                    Querys.CBFillers(CB_ProductNames, $"select ItemID from _Warehouse where ItemCode = '{CB_ProductCode.Text}'");
                }
            }
        }

        private void Txt_AddExist_Available_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void Txt_Discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                return;
            }

        }

        private void Txt_PurchasesNotes_Enter(object sender, EventArgs e)
        {
            GunaLineTextBox textBox = (GunaLineTextBox)sender;
            if (textBox.Text.Equals("اسم الصنف") || textBox.Text.Equals("الإجمالى") || textBox.Text.Equals("السعر") || textBox.Text.Equals("ملاحظات") || textBox.Text.Equals("الكمية"))
            {
                textBox.Text = string.Empty;
                textBox.LineColor = Color.Goldenrod;
                textBox.ForeColor = Color.Goldenrod;
            }
        }

        private void Txt_PurchasesNotes_Leave(object sender, EventArgs e)
        {
            GunaLineTextBox textBox = (GunaLineTextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                switch (textBox.Name)
                {
                    case "Txt_PurchasesName": textBox.Text = "اسم الصنف"; goto default;
                    case "Txt_PurchasesPrice": textBox.Text = "السعر"; goto default;
                    case "Txt_PurchasesQuantity": textBox.Text = "الكمية"; goto default;
                    case "Txt_PurchasesNotes": textBox.Text = "ملاحظات"; goto default;
                    default:
                        textBox.LineColor = Color.Linen;
                        textBox.ForeColor = Color.Linen;
                        break;
                }
            }
        }

        private void Txt_PurchasesPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void Txt_AddNew_Item_Leave(object sender, EventArgs e)
        {
            GunaLineTextBox textBox = (GunaLineTextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                switch (textBox.Name)
                {
                    case "Txt_AddNew_Item": textBox.Text = "اسم المنتج"; goto default;
                    case "Txt_SupplierName": textBox.Text = "اسم المورد"; goto default;
                    case "Txt_SupplierNumber": textBox.Text = "رقم الهاتف"; goto default;
                    case "Txt_Warehouse_Search": textBox.Text = "بحث"; goto default;
                    case "CB_AddNew_ItemQuantity": textBox.Text = "العدد"; goto default;
                    case "CB_AddExist_ItemQuantity": textBox.Text = "العدد"; goto default;
                    case "CB_TakeExist_ItemQuantity": textBox.Text = "العدد"; goto default;
                    case "Txt_AddNew_minimum": textBox.Text = "الحد الادنى"; goto default;
                    case "Txt_AddNew_maximum": textBox.Text = "الحد الاقصى"; goto default;
                    case "Txt_Address": textBox.Text = "العنوان"; goto default;
                    case "gunaLineTextBox1": textBox.Text = "السعر"; goto default;
                    case "Txt_AddNew_ItemQuantity": textBox.Text = "العدد"; goto default;
                    case "CB_AddNew_Code": textBox.Text = "تصنيف المنتج"; goto default;
                    default:
                        textBox.LineColor = Color.Linen;
                        textBox.ForeColor = Color.Linen;
                        break;
                }
            }
        }

        private void Txt_AddNew_Item_Enter(object sender, EventArgs e)
        {
            GunaLineTextBox textBox = (GunaLineTextBox)sender;
            if (textBox.Text.Equals("تصنيف المنتج") || textBox.Text.Equals("العنوان") || textBox.Text.Equals("اسم المنتج") || textBox.Text.Equals("اسم المورد") || textBox.Text.Equals("رقم الهاتف") || textBox.Text.Equals("بحث") || textBox.Text.Equals("العدد") || textBox.Text.Equals("الحد الادنى") || textBox.Text.Equals("الحد الاقصى") || textBox.Text.Equals("السعر") || textBox.Text.Equals("العدد"))
            {
                textBox.Text = string.Empty;
                textBox.LineColor = Color.Goldenrod;
                textBox.ForeColor = Color.Goldenrod;
            }
        }

        private void CB_TakeExist_Item_SelectedIndexChanged(object sender, EventArgs e)
        {
            GunaComboBox textBox = (GunaComboBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                textBox.ForeColor = Color.Goldenrod;
                textBox.BorderColor = Color.Goldenrod;
            }
            switch (textBox.Name)
            {
                case "CB_AddExist_Item":
                    string a = Querys.Reader_SingleValue($"select ItemAvaliable from _Warehouse where ItemID = '{CB_AddExist_Item.Text}'");
                    if (!string.IsNullOrEmpty(a))
                    {
                        Txt_AddExist_Available.Text = a;
                    }
                    break;
                case "CB_TakeExist_Item":
                    string a2 = Querys.Reader_SingleValue($"select ItemAvaliable from _Warehouse where ItemID = '{CB_TakeExist_Item.Text}'");
                    if (!string.IsNullOrEmpty(a2))
                    {
                        Txt_TakeExist_Available.Text = a2;
                    }
                    break;
                case "CB_SupplierCode":
                    if (textBox.Text == "قديم")
                    {
                        Txt_SupplierName.Visible = false;
                        gunaComboBox1.Visible = true;
                        Txt_SupplierName.LineColor = Color.Linen;
                        Txt_SupplierName.ForeColor = Color.Linen;
                        Txt_SupplierName.Text = "اسم المورد";
                        Querys.CBFillers(gunaComboBox1, "select DISTINCT SupplierID from _Suppliers where Service = 1");

                    }
                    else if (textBox.Text == "جديد")
                    {
                        Txt_SupplierName.Visible = true;
                        gunaComboBox1.Visible = false;
                        gunaComboBox1.Items.Clear();
                        gunaComboBox1.BorderColor = Color.Linen;
                        gunaComboBox1.ForeColor = Color.Linen;
                        Txt_Address.LineColor = Color.Linen;
                        Txt_Address.ForeColor = Color.Linen;
                        Txt_SupplierNumber.LineColor = Color.Linen;
                        Txt_SupplierNumber.ForeColor = Color.Linen;
                        Txt_Address.Text = "العنوان";
                        Txt_SupplierNumber.Text = "رقم الهاتف";
                    }
                    break;
                case "gunaComboBox1":
                    Txt_SupplierNumber.Text = Querys.Reader_SingleValue($"select SupplierNumber from _Suppliers where SupplierID = '{gunaComboBox1.Text}'");
                    Txt_Address.Text = Querys.Reader_SingleValue($"select SupplierAddress from _Suppliers where SupplierID = '{gunaComboBox1.Text}'");
                    Txt_Address.LineColor = Color.Goldenrod;
                    Txt_Address.ForeColor = Color.Goldenrod;
                    Txt_SupplierNumber.LineColor = Color.Goldenrod;
                    Txt_SupplierNumber.ForeColor = Color.Goldenrod;
                    break;
            }
        }
        private void Btn_Search_Archive_Click(object sender, EventArgs e)
        {
            Btn_Search_Archive.ForeColor = Color.Goldenrod;
            gunaAdvenceButton1.ForeColor = Color.Linen;
            gunaAdvenceButton3.ForeColor = Color.Linen;
            gunaAdvenceButton2.ForeColor = Color.Linen;
            DataTable Table = new DataTable();
            Querys.Reader_Table("select DISTINCT Date from _Storage where  MoneyIn > 0 or MoneyOut > 0 order by Date DESC", ref Table);

            Daragrid_Archive.Rows.Clear();
            for (int i = 0; i < Table.Rows.Count; i++)
            {
                double InvoicesMoney = Convert.ToDouble(Querys.Reader_SingleValue($"select isnull(sum (InvoicesMoney),0) from _Storage where Date = '{ Table.Rows[i][0] }'"));
                double Purchases = Convert.ToDouble(Querys.Reader_SingleValue($"select  isnull(sum (PurchasesMoney),0) from _Storage where Date = '{ Table.Rows[i][0] }'"));
                double DepositMoney = Convert.ToDouble(Querys.Reader_SingleValue($"select  isnull(sum (DepositMoney),0) from _Storage where Date = '{ Table.Rows[i][0] }'"));
                double WithdrawMoney = Convert.ToDouble(Querys.Reader_SingleValue($"select  isnull(sum (WithdrawMoney),0) from _Storage where Date = '{ Table.Rows[i][0] }'"));
                double TotalBeforeCut = Convert.ToDouble(Querys.Reader_SingleValue($"select  isnull(sum (ItemsTotalPrice_BeforeDiscount),0) from _Invoices where Date = '{ Table.Rows[i][0] }'"));
                double TotalDiscount = Convert.ToDouble(Querys.Reader_SingleValue($"select  isnull(sum (CustomerDiscount),0) from _Invoices where Date = '{ Table.Rows[i][0] }'"));

                double TotalAfterDiscount = TotalBeforeCut - TotalDiscount;
                double Elsafe = (TotalAfterDiscount - (WithdrawMoney + Purchases));
                Daragrid_Archive.Rows.Add(Elsafe, Purchases, WithdrawMoney, TotalBeforeCut, DepositMoney, TotalAfterDiscount, TotalDiscount, InvoicesMoney, Table.Rows[i][0].ToString().Split(' ')[0]);
            }
        }

        private void Txt_ImployID_Enter(object sender, EventArgs e)
        {
            GunaLineTextBox textBox = (GunaLineTextBox)sender;
            if (textBox.Text.Equals("اسم الموظف") || textBox.Text.Equals("كلمة السر"))
            {
                textBox.Text = string.Empty;
                textBox.LineColor = Color.Goldenrod;
                textBox.ForeColor = Color.Goldenrod;
            }
        }

        private void Txt_ImployID_Leave(object sender, EventArgs e)
        {
            GunaLineTextBox textBox = (GunaLineTextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                switch (textBox.Name)
                {
                    case "Txt_ImployID": textBox.Text = "اسم الموظف"; goto default;
                    case "Txt_EmployName_Search": textBox.Text = "اسم الموظف"; goto default;
                    case "Txt_ImployPW": textBox.Text = "كلمة السر"; goto default;
                    default:
                        textBox.LineColor = Color.Linen;
                        textBox.ForeColor = Color.Linen;
                        break;
                }
            }
        }
        private void Txt_Money_Enter(object sender, EventArgs e)
        {
            GunaLineTextBox textBox = (GunaLineTextBox)sender;
            if (textBox.Text.Equals("المبلغ"))
            {
                textBox.Text = string.Empty;
                textBox.LineColor = Color.Goldenrod;
                textBox.ForeColor = Color.Goldenrod;
            }
        }

        private void Txt_Money_Leave(object sender, EventArgs e)
        {
            GunaLineTextBox textBox = (GunaLineTextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                switch (textBox.Name)
                {
                    case "Txt_Money": textBox.Text = "المبلغ"; goto default;
                    default:
                        textBox.LineColor = Color.Linen;
                        textBox.ForeColor = Color.Linen;
                        break;
                }
            }
        }

        private void Txt_Money_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }
        private void WarehouseClear()
        {
            GunaLineTextBox[] textBoxes = { CB_AddNew_Code, gunaLineTextBox1, Txt_Warehouse_Search, Txt_TakeExist_ItemQuantity, Txt_AddExist_ItemQuantity, Txt_Address, Txt_SupplierNumber, Txt_AddNew_maximum, Txt_AddNew_minimum, Txt_AddNew_ItemQuantity, Txt_SupplierName, Txt_AddNew_Item };
            for (int i = 0; i < textBoxes.Length; i++)
            {
                textBoxes[i].ForeColor = Color.Linen;
                textBoxes[i].LineColor = Color.Linen;
            }
            CB_AddNew_Code.ForeColor = Color.Linen;
            CB_SupplierCode.BorderColor = Color.Linen;
            CB_SupplierCode.SelectedIndex = -1;
            CB_SupplierCode.ForeColor = Color.Linen;
            CB_AddExist_Item.BorderColor = Color.Linen;
            CB_AddExist_Item.SelectedIndex = -1;
            CB_AddExist_Item.ForeColor = Color.Linen;
            CB_TakeExist_Item.BorderColor = Color.Linen;
            CB_TakeExist_Item.SelectedIndex = -1;
            CB_TakeExist_Item.ForeColor = Color.Linen;

            CB_AddNew_Code.Text = "تصنيف المنتج";
            gunaLineTextBox1.Text = "السعر";
            Txt_AddNew_Item.Text = "اسم المنتج";
            Txt_SupplierName.Text = "اسم المورد";
            Txt_SupplierNumber.Text = "رقم الهاتف";
            Txt_Warehouse_Search.Text = "بحث";
            Txt_AddNew_ItemQuantity.Text = "العدد";
            Txt_AddExist_ItemQuantity.Text = "العدد";
            Txt_TakeExist_ItemQuantity.Text = "العدد";
            Txt_AddNew_minimum.Text = "الحد الادنى";
            Txt_AddNew_maximum.Text = "الحد الاقصى";
            Txt_Address.Text = "العنوان";
            Txt_AddExist_Available.Text = "الرصيد المتاح";
            Txt_TakeExist_Available.Text = "الرصيد المتاح";
            gunaComboBox1.Items.Clear();
            gunaComboBox1.Visible = false;
            Txt_SupplierName.Visible = true;
        }

        private void StorageReady()
        {
            try
            {
                Querys.Reader_Datagrid($"select * from _Storage where Date = '{DT}'", ref Datagrid_Storage);
                Txt_TotalMoney_Sales.Text = Querys.Reader_SingleValue($"select isnull(sum (InvoicesMoney),0) from _Storage where date = '{DT}'");
                Txt_TotalMoney_Purchases.Text = Querys.Reader_SingleValue($"select isnull(sum (PurchasesMoney),0) from _Storage where date = '{DT}'");
                Txt_TotalMoney_Deposit.Text = Querys.Reader_SingleValue($"select isnull(sum (DepositMoney),0) from _Storage where date = '{DT}'");
                Txt_TotalMoney_Withdrawal.Text = Querys.Reader_SingleValue($"select isnull(sum (WithdrawMoney),0) from _Storage where date = '{DT}'");
                int LastNo = Convert.ToInt32(Querys.Reader_SingleValue($"if EXISTS(select TOP 1 StorageNo from _Storage where date = '{ DT }') begin select TOP 1 StorageNo from _Storage where date = '{ DT }' END Else Select 1"));
                Txt_TotalMoney_StartPirod.Text = Querys.Reader_SingleValue($"if EXISTS(select MoneyNow from _Storage where StorageNo = '{ LastNo - 1}') begin select MoneyNow from _Storage where StorageNo = '{ LastNo - 1}' END ELSE Select 0");
                Txt_TotalMoney_EndPirod.Text = Convert.ToString(Querys.Reader_SingleValue($"Select TOP 1 MoneyNow from _Storage where date = '{ DT }' ORDER BY StorageNo DESC"));
                Txt_BalanceNow.Text = Querys.Reader_SingleValue(" if EXISTS(Select TOP 1 MoneyNow from _Storage ORDER BY StorageNo DESC) begin Select TOP 1 MoneyNow from _Storage ORDER BY StorageNo DESC END ElSE Select 0");
            }
            catch (Exception ex)
            {
                COMMANDS.Error(ex.Message);
            }
        }
        private void Master_Buttons(object sender, EventArgs e)
        {
            GunaAdvenceButton[] a = { Btn_Sales, Btn_Purchases, Btn_Warhouse, Btn_Archive, Btn_Supplier, Btn_Employs, Btn_Storage, Btn_CustomerData };
            GunaAdvenceButton Click = (GunaAdvenceButton)sender;

            if (Click.Equals(Btn_Logout))
            {
                Close();
                Application.OpenForms["Form_Login"].Show();
            }
            else if (Press_Sales)
            {
                Press_Sales = false;
            }
            else if (Press_Purchases)
            {
                Press_Purchases = false;
            }
            else if (Press_Warhouse)
            {
                Press_Warhouse = false;
                Querys.Reader_Datagrid("select ItemID,ItemIn from _Warehouse order by Date DESC", ref Datagrid_ItemsIn_Warehouse);
                Querys.Reader_Datagrid("select ItemID,ItemAvaliable from _Warehouse where ItemAvaliable > 0 order by Date DESC", ref Datagrid_Avaliable_Warehouse);
                Querys.Reader_Datagrid("select ItemID,ItemOut from _Warehouse where ItemOut > 0.0 order by Date DESC ", ref Datagrid_ItemsOut_Warehouse);
            }
            else if (Press_Employs)
            {
                EmploysClear_();
                Querys.Reader_Datagrid("select * from _Employs where Service = 1", ref Datagrid_EmploysReport);
                Press_Employs = false;
            }
            else if (Press_Storage)
            {
                StorageReady(); Press_Storage = false;
            }
            else if (Press_Archive)
            {
                Archive_clear(); Press_Archive = false;
            }
            else if (Press_Supplier)
            {
                Supplier_clear(); Press_Supplier = false;
            }
            else if (Press_CustomerData)
            {
                Customer_clear(); Press_CustomerData = false;
            }

            switch (Click.Name)
            {
                case "Btn_Sales": Press_Sales = true; Panel_Sales.BringToFront(); goto default;
                case "Btn_Purchases": Press_Purchases = true; Panel_Purchases.BringToFront(); goto default;
                case "Btn_Warhouse": Press_Warhouse = true; Panel_Warehouse.BringToFront(); goto default;
                case "Btn_Archive": Press_Archive = true; Panel_Archive.BringToFront(); goto default;
                case "Btn_Employs": Press_Employs = true; Panel_Employs.BringToFront(); goto default;
                case "Btn_Storage": Press_Storage = true; Panel_Storage.BringToFront(); goto default;
                case "Btn_CustomerData": Press_CustomerData = true; Panel_Customer.BringToFront(); goto default;
                case "Btn_Supplier": Press_Supplier = true; Panel_Supplier.BringToFront(); goto default;
                default:
                    Click.ForeColor = Color.FromArgb(25, 25, 25);
                    Click.BaseColor = Color.LightGray;
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a[i].Text != Click.Text)
                        {
                            a[i].ForeColor = Color.LightGray;
                            a[i].BaseColor = Color.FromArgb(25, 25, 25);
                        }
                    }
                    break;
            }
        }
        private void Daragrid_Archive_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Application.OpenForms["Forms.Form_Archive"] == null)
            {
                if (Btn_Search_Archive.ForeColor == Color.Goldenrod)
                {
                    Loading.Archives_Day = Daragrid_Archive.Rows[e.RowIndex].Cells[8].Value.ToString();
                    Loading.Archives_Options = "Days";

                    Form_Archive archive = new Form_Archive();
                    archive.ShowDialog();
                }
                else if (gunaAdvenceButton1.ForeColor == Color.Goldenrod)
                {
                    Loading.Archives_Day = Daragrid_Archive.Rows[e.RowIndex].Cells[8].Value.ToString().Split('/')[0];
                    Loading.Archives_Options = "Months";

                    Form_Archive archive = new Form_Archive();
                    archive.ShowDialog();
                }
                else if (gunaAdvenceButton3.ForeColor == Color.Goldenrod)
                {
                    Loading.Archives_Day = Daragrid_Archive.Rows[e.RowIndex].Cells[8].Value.ToString();
                    Loading.Archives_Options = "Years";

                    Form_Archive archive = new Form_Archive();
                    archive.ShowDialog();
                }
                else if (gunaAdvenceButton2.ForeColor == Color.Goldenrod)
                {
                    Loading.Archives_Day = Daragrid_Archive.Rows[e.RowIndex].Cells[8].Value.ToString();
                    Loading.Archives_Options = "Custom";

                    Form_Archive archive = new Form_Archive();
                    archive.ShowDialog();
                }
            }
            else { Application.OpenForms["Forms.Form_Archive"].Show(); }
        }
        private void ApplicationExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGrid_Invoices.Rows.Count > 0)
                {
                    COMMANDS.Information("رجاء تصفير الفاتوره اولا");
                    return;
                }
                else
                {
                    Application.Exit();
                }
            }
            catch { }
        }

        private void ApplicationMinmize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Btn_PurchasesDelete_Click(object sender, EventArgs e)
        {
            double money_now = Convert.ToDouble(Querys.Reader_SingleValue(" Select TOP (1) MoneyNow from _Storage ORDER BY Datetime DESC"));
            byte SelectedRows = (byte)Datagrid_Purchases.SelectedRows.Count;
            for (int i = 0; i < SelectedRows; i++)
            {
                string Sourse = Datagrid_Purchases.SelectedRows[0].Cells[2].Value.ToString();
                string Price = Datagrid_Purchases.SelectedRows[0].Cells[3].Value.ToString();
                string Quantity = Datagrid_Purchases.SelectedRows[0].Cells[4].Value.ToString();
                string DT1 = Datagrid_Purchases.SelectedRows[0].Cells[6].Value.ToString().Trim();
                string Reason = $"Purchases_Return: {Sourse.Trim()} , Quantity: {Quantity.Trim()}";

                Querys.Excuter($"DELETE FROM _Purchases WHERE ItemID ='{ Sourse }' and Datetime = '{ DT1 }';" +
                               $"INSERT INTO _Storage values ('{ Loading.AdminID }', '{Reason}',0,{Convert.ToDouble(Price)},0,0,{ Convert.ToDouble(Price) },0, { money_now + Convert.ToDouble(Price) },'{ DT }','{ DT }')");
            }
            if (SelectedRows > 0)
            {
                Datagrid_Purchases.Rows.RemoveAt(Datagrid_Purchases.SelectedRows[0].Index);
            }
        }

        private void Txt_UnitPrice_Enter(object sender, EventArgs e)
        {
            if (Txt_UnitPrice.Text.Equals("سعر الوحدة"))
            {
                Txt_UnitPrice.LineColor = Color.Goldenrod;
                Txt_UnitPrice.ForeColor = Color.Goldenrod;
            }
        }

        private void Txt_AddExist_Available_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            if (Loading.UserTerms.Equals("OWNER") || Loading.UserTerms.Equals("موظف اداري"))
            {
                Label_AdminCode.Text = "0";
                Btn_Warhouse.Visible = true;
                Btn_Archive.Visible = true;
                Btn_Supplier.Visible = true;
                Btn_Employs.Visible = true;
                Btn_CustomerData.Visible = true;
            }
            else if (Loading.UserTerms.Equals("موظف مبيعات"))
            {
                Label_AdminCode.Text = Querys.Reader_SingleValue($"select EmployCode from _Employs where EmployID = '{Loading.AdminID}'");
                Btn_Sales.PerformClick();
                Btn_Storage.Location = Btn_Warhouse.Location;
            }
            else
            {
                Label_AdminCode.Text = "0";
                Btn_Warhouse.Visible = true;
                Btn_Archive.Visible = true;
                Btn_Supplier.Visible = true;
                Btn_Employs.Visible = true;
                Btn_CustomerData.Visible = true;
            }
            CB_ProductCode.Items.Clear();
            CB_ProductNames.Items.Clear();
            CB_ProductCode.Items.Add("الكل");
            Querys.CBFillers(CB_ProductCode, "select DISTINCT ItemCode from _Warehouse");
            Querys.CBFillers(Txt_EmployName_Search, "select DISTINCT EmployID ItemCode from _Employs union all select DISTINCT ID from _OWNER");
            Querys.CBFillers(CB_ProductNames, "select ItemID from _Warehouse");
            Label_InvoiceNo.Text = Convert.ToInt32(Querys.Reader_SingleValue("IF EXISTS(SELECT  top 1 InvoicesNo FROM  _Invoices) BEGIN select top 1 isnull(InvoicesNo,0) from _Invoices order by InvoicesNo DESC END ELSE select 1 as New_Invoice")).ToString();
            Label_AdminCode.Text = Loading.AdminCode;
            Btn_Sales.PerformClick();
            Btn_Sales.Focus();
            DatePKR_From.Value = DateTime.Now;
            DatePKR_to.Value = DateTime.Now;
            gunaDateTimePicker1.Value = DateTime.Now;
            gunaDateTimePicker2.Value = DateTime.Now;
        }

        private void CB_ProductNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CB_ProductNames.Text))
            {
                CB_ProductNames.ForeColor = Color.Goldenrod;
                CB_ProductNames.BorderColor = Color.Goldenrod;
                //CB_ProductCode.SelectedIndex = CB_ProductCode.FindString(Querys.Reader_SingleValue($"select ItemCode from _Warehouse where ItemID = '{CB_ProductNames.Text}'"));
                Txt_UnitPrice.Text = Querys.Reader_SingleValue($"select ItemPrice from _Warehouse where ItemID = '{CB_ProductNames.Text}'");
                Txt_UnitPrice.LineColor = Color.Goldenrod;
                Txt_UnitPrice.ForeColor = Color.Goldenrod;
                Txt_ProductCount.Text = "العدد";
                Txt_ProductCount.LineColor = Color.Linen;
                Txt_ProductCount.ForeColor = Color.Linen;
                Txt_TotalPrice.Text = "الاجمالي";
                Txt_TotalPrice.LineColor = Color.Linen;
                Txt_TotalPrice.ForeColor = Color.Linen;
            }
        }

        private void Txt_ProductCount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Txt_TotalPrice.Text = (Convert.ToInt32(Txt_ProductCount.Text) * Convert.ToInt32(Txt_UnitPrice.Text)).ToString();
                Txt_TotalPrice.LineColor = Color.Goldenrod;
                Txt_TotalPrice.ForeColor = Color.Goldenrod;
                return;
            }
            catch { }
        }

        private void gunaLineTextBox1_Leave(object sender, EventArgs e)
        {
            TotalInvoice.LineColor = Color.Linen;
            TotalInvoice.ForeColor = Color.Linen;
        }

        private void gunaLineTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
            return;
        }

        private void gunaLineTextBox1_Enter(object sender, EventArgs e)
        {
            if (TotalInvoice.Text.Equals("اجمالي الفاتورة"))
            {
                TotalInvoice.LineColor = Color.Goldenrod;
                TotalInvoice.ForeColor = Color.Goldenrod;
            }
        }

        private void Btn_SupplierSearch_Click(object sender, EventArgs e)
        {
            if (gunaCheckBox4.Checked == false && gunaCheckBox5.Checked == false && gunaCheckBox6.Checked == false)
            {
                Querys.Reader_Datagrid("select DISTINCT * from _Suppliers where Service = 1", ref Datagrid_Supplier);
            }
            else if (gunaCheckBox4.Checked == true && gunaCheckBox5.Checked == false && gunaCheckBox6.Checked == false)
            {
                Querys.Reader_Datagrid($"select DISTINCT * from _Suppliers where Day(Date) = '{DateTime.Now.Day}' and  Year(Date) = '{DateTime.Now.Year}' and Service = 1", ref Datagrid_Supplier);
            }
            else if (gunaCheckBox4.Checked == false && gunaCheckBox5.Checked == true && gunaCheckBox6.Checked == false)
            {
                Querys.Reader_Datagrid($"select DISTINCT * from _Suppliers where MONTH(Date) = '{DateTime.Now.Month}' and  Year(Date) = '{DateTime.Now.Year}' and Service = 1", ref Datagrid_Supplier);
            }
            else if (gunaCheckBox4.Checked == false && gunaCheckBox5.Checked == false && gunaCheckBox6.Checked == true)
            {
                Querys.Reader_Datagrid($"select DISTINCT * from _Suppliers where Year(Date) = '{DateTime.Now.Year}' and Service = 1", ref Datagrid_Supplier);
            }
        }

        private void Btn_CustomerSearch_Click(object sender, EventArgs e)
        {
            if (gunaCheckBox7.Checked == false && gunaCheckBox8.Checked == false && gunaCheckBox9.Checked == false)
            {
                Querys.Reader_Datagrid("select DISTINCT * from _Customers", ref Datagrid_Customer);
            }
            else if (gunaCheckBox9.Checked == true && gunaCheckBox7.Checked == false && gunaCheckBox8.Checked == false)
            {
                Querys.Reader_Datagrid($"select DISTINCT * from _Customers where Day(Date) = '{DateTime.Now.Day}' and  Year(Date) = '{DateTime.Now.Year}'", ref Datagrid_Customer);
            }
            else if (gunaCheckBox9.Checked == false && gunaCheckBox8.Checked == true && gunaCheckBox7.Checked == false)
            {
                Querys.Reader_Datagrid($"select DISTINCT * from _Customers where MONTH(Date) = '{DateTime.Now.Month}' and  Year(Date) = '{DateTime.Now.Year}'", ref Datagrid_Customer);
            }
            else if (gunaCheckBox9.Checked == false && gunaCheckBox8.Checked == false && gunaCheckBox7.Checked == true)
            {
                Querys.Reader_Datagrid($"select DISTINCT * from _Customers where Year(Date) = '{DateTime.Now.Year}'", ref Datagrid_Customer);
            }
        }

        private void Txt_Discount_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Txt_Discount_TextChanged(object sender, EventArgs e)
        {
            double totalPrice = 0;

            try
            {
                for (int i = 0; i < DataGrid_Invoices.RowCount; i++)
                {
                    totalPrice = totalPrice + Convert.ToDouble(DataGrid_Invoices.Rows[i].Cells[0].Value);
                }
                if (totalPrice >= Convert.ToDouble(Txt_Discount.Text))
                {
                    TotalInvoice.Text = (totalPrice - Convert.ToDouble(Txt_Discount.Text)).ToString();
                }
                else
                {
                    TotalInvoice.Text = totalPrice.ToString();
                    Txt_Discount.Text = string.Empty;
                }
            }
            catch { TotalInvoice.Text = totalPrice.ToString(); }
        }

        private void TotalInvoice_Enter(object sender, EventArgs e)
        {
            TotalInvoice.LineColor = Color.Goldenrod;
            TotalInvoice.ForeColor = Color.Goldenrod;
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            Btn_Search_Archive.ForeColor = Color.Linen;
            gunaAdvenceButton1.ForeColor = Color.Goldenrod;
            gunaAdvenceButton3.ForeColor = Color.Linen;
            gunaAdvenceButton2.ForeColor = Color.Linen;

            DataTable Years = new DataTable();
            Querys.Reader_Table("select DISTINCT year(Date) from _Storage where  MoneyIn > 0 or MoneyOut > 0 order by year(Date) DESC", ref Years);

            DataTable Table = new DataTable();
            Daragrid_Archive.Rows.Clear();
            for (int t = 0; t < Years.Rows.Count; t++)
            {
                Querys.Reader_Table($"select DISTINCT  MONTH(Date) from _Storage where Year(Date) = '{Years.Rows[t][0]}' and  MoneyIn > 0 or MoneyOut > 0 order by  MONTH(Date) DESC", ref Table);
                for (int i = 0; i < Table.Rows.Count; i++)
                {

                    double InvoicesMoney = Convert.ToDouble(Querys.Reader_SingleValue($"select isnull(sum (InvoicesMoney),0) from _Storage where MONTH(Date) = '{ Table.Rows[i][0] }'"));
                    double Purchases = Convert.ToDouble(Querys.Reader_SingleValue($"select isnull(sum (PurchasesMoney),0) from _Storage where MONTH(Date) = '{ Table.Rows[i][0] }'"));
                    double DepositMoney = Convert.ToDouble(Querys.Reader_SingleValue($"select isnull(sum (DepositMoney),0) from _Storage where MONTH(Date) = '{ Table.Rows[i][0] }'"));
                    double WithdrawMoney = Convert.ToDouble(Querys.Reader_SingleValue($"select isnull(sum (WithdrawMoney),0) from _Storage where MONTH(Date) = '{ Table.Rows[i][0] }'"));
                    double TotalBeforeCut = Convert.ToDouble(Querys.Reader_SingleValue($"select isnull(sum (ItemsTotalPrice_BeforeDiscount),0) from _Invoices where MONTH(Date) = '{ Table.Rows[i][0] }'"));
                    double TotalDiscount = Convert.ToDouble(Querys.Reader_SingleValue($"select isnull(sum (CustomerDiscount),0) from _Invoices where MONTH(Date) = '{ Table.Rows[i][0] }'"));

                    double TotalAfterDiscount = TotalBeforeCut - TotalDiscount;
                    double Elsafe = (TotalAfterDiscount - (WithdrawMoney + Purchases));
                    Daragrid_Archive.Rows.Add(Elsafe, Purchases, WithdrawMoney, TotalBeforeCut, DepositMoney, TotalAfterDiscount, TotalDiscount, InvoicesMoney, Table.Rows[i][0].ToString().Split(' ')[0] + "/" + Years.Rows[t][0].ToString());
                }
            }
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            Btn_Search_Archive.ForeColor = Color.Linen;
            gunaAdvenceButton1.ForeColor = Color.Linen;
            gunaAdvenceButton3.ForeColor = Color.Goldenrod;
            gunaAdvenceButton2.ForeColor = Color.Linen;

            DataTable Years = new DataTable();
            Querys.Reader_Table("select DISTINCT year(Date) from _Storage where  MoneyIn > 0 or MoneyOut > 0 order by year(Date) DESC", ref Years);

            Daragrid_Archive.Rows.Clear();
            for (int i = 0; i < Years.Rows.Count; i++)
            {
                double InvoicesMoney = Convert.ToDouble(Querys.Reader_SingleValue($"select isnull(sum (InvoicesMoney),0) from _Storage where Year(Date) = '{ Years.Rows[i][0] }'"));
                double Purchases = Convert.ToDouble(Querys.Reader_SingleValue($"select isnull(sum (PurchasesMoney),0) from _Storage where Year(Date) = '{ Years.Rows[i][0] }'"));
                double DepositMoney = Convert.ToDouble(Querys.Reader_SingleValue($"select isnull(sum (DepositMoney),0) from _Storage where Year(Date) = '{ Years.Rows[i][0] }'"));
                double WithdrawMoney = Convert.ToDouble(Querys.Reader_SingleValue($"select isnull(sum (WithdrawMoney),0) from _Storage where Year(Date) = '{ Years.Rows[i][0] }'"));
                double TotalBeforeCut = Convert.ToDouble(Querys.Reader_SingleValue($"select isnull(sum (ItemsTotalPrice_BeforeDiscount),0) from _Invoices where Year(Date) = '{ Years.Rows[i][0] }'"));
                double TotalDiscount = Convert.ToDouble(Querys.Reader_SingleValue($"select isnull(sum (CustomerDiscount),0) from _Invoices where Year(Date) = '{ Years.Rows[i][0] }'"));

                double TotalAfterDiscount = TotalBeforeCut - TotalDiscount;
                double Elsafe = (TotalAfterDiscount - (WithdrawMoney + Purchases));
                Daragrid_Archive.Rows.Add(Elsafe, Purchases, WithdrawMoney, TotalBeforeCut, DepositMoney, TotalAfterDiscount, TotalDiscount, InvoicesMoney, Years.Rows[i][0].ToString());
            }
        }

        private void Btn_Employs_ReportSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Datagrid_EmploysReport.Rows.Clear();
                Datagrid_EmploysReport.DataSource = null;
            }
            catch { }

            if (CHBox_SalesReport.Checked == true)
            {
                if (gunaCheckBox3.Checked == true)
                {
                    Querys.Reader_Datagrid($"select InvoicesNo,AdminID,CustomerID,ItemsTotalPrice_BeforeDiscount as BeforeDiscount,CustomerDiscount,ItemsPrice_AfterDiscount as AfterDiscount  from _Invoices where AdminID = '{Txt_EmployName_Search.Text.Trim()}' and date = '{DT}'", ref Datagrid_EmploysReport);
                }
                else if (gunaCheckBox2.Checked == true)
                {
                    Querys.Reader_Datagrid($"select InvoicesNo,AdminID,CustomerID,ItemsTotalPrice_BeforeDiscount as BeforeDiscount,CustomerDiscount,ItemsPrice_AfterDiscount as AfterDiscount from _Invoices where AdminID = '{Txt_EmployName_Search.Text.Trim()}' and MONTH(date) = '{DateTime.Now.Month}'", ref Datagrid_EmploysReport);
                }
                else if (gunaCheckBox1.Checked == true)
                {
                    Querys.Reader_Datagrid($"select InvoicesNo,AdminID,CustomerID,ItemsTotalPrice_BeforeDiscount as BeforeDiscount,CustomerDiscount,ItemsPrice_AfterDiscount as AfterDiscount from _Invoices where AdminID = '{Txt_EmployName_Search.Text.Trim()}' and Year(date) = '{DateTime.Now.Year}'", ref Datagrid_EmploysReport);
                }
            }

            else if (CHBox_PurchasesReport.Checked == true)
            {
                if (gunaCheckBox3.Checked == true)
                {
                    Querys.Reader_Datagrid($"select AdminID,ItemID,ItemPrice,ItemQuantity from _Purchases where AdminID = '{Txt_EmployName_Search.Text.Trim()}' and Date = '{DT}'", ref Datagrid_EmploysReport);
                }
                else if (gunaCheckBox2.Checked == true)
                {
                    Querys.Reader_Datagrid($"select AdminID,ItemID,ItemPrice,ItemQuantity from _Purchases where AdminID = '{Txt_EmployName_Search.Text.Trim()}' and MONTH(Date) = '{DateTime.Now.Month}'", ref Datagrid_EmploysReport);
                }
                else if (gunaCheckBox1.Checked == true)
                {
                    Querys.Reader_Datagrid($"select AdminID,ItemID,ItemPrice,ItemQuantity from _Purchases where AdminID = '{Txt_EmployName_Search.Text.Trim()}' and YEAR(Date) = '{DateTime.Now.Year}'", ref Datagrid_EmploysReport);
                }
            }

        }

        private void gunaCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            GunaCheckBox checkBox = (GunaCheckBox)sender;
            GunaCheckBox[] Checkbuttons = { gunaCheckBox1, gunaCheckBox2, gunaCheckBox3 };

            if (checkBox.Checked.Equals(true))
                checkBox.ForeColor = Color.Goldenrod;
            else
                checkBox.ForeColor = Color.Linen;
            for (int i = 0; i < Checkbuttons.Length; i++)
            {
                if (!Checkbuttons[i].Equals(checkBox) && Checkbuttons[i].Checked.Equals(true))
                    Checkbuttons[i].Checked = false;
            }
        }

        private void Btn_EmployAdd_Click(object sender, EventArgs e)
        {
            if (Txt_ImployID.Text != "اسم الموظف" || Txt_ImployPW.Text != "كلمة السر")
            {
                if (CHBox_EmploySeller.Checked == true)
                {
                    Querys.Excuter($"if not exists ( select EmployID from _Employs where EmployID = '{Txt_ImployID.Text}' and Service = 1 )  BEGIN insert into _Employs Values ('{Loading.AdminID}','{Txt_ImployID.Text}','{Txt_ImployPW.Text}','موظف مبيعات',{"1"},'{DT}','{DT}') END ElSE update _Employs set Service = 1 where EmployID = '{Txt_ImployID.Text}'");
                }
                else if (CHBox_EmployAdmin.Checked == true)
                {
                    Querys.Excuter($"if not exists ( select EmployID from _Employs where EmployID = '{Txt_ImployID.Text}' and Service = 1  )  BEGIN insert into _Employs Values ('{Loading.AdminID}','{Txt_ImployID.Text}','{Txt_ImployPW.Text}','موظف اداري',{"1"},'{DT}','{DT}') END ElSE update _Employs set Service = 1 where EmployID = '{Txt_ImployID.Text}'");
                }
                Querys.Reader_Datagrid("select * from _Employs where Service = 1", ref Datagrid_EmploysReport);
                Txt_ImployID.Text = "اسم الموظف";
                Txt_ImployID.ForeColor = Color.Linen;
                Txt_ImployID.LineColor = Color.Linen;
                Txt_ImployPW.Text = "كلمة السر";
                Txt_ImployPW.ForeColor = Color.Linen;
                Txt_ImployPW.LineColor = Color.Linen;
                CHBox_EmploySeller.Checked = false;
                CHBox_EmployAdmin.Checked = false;
            }

        }

        private void Btn_EmployDelete_Click(object sender, EventArgs e)
        {
            if (Txt_ImployID.Text != "اسم الموظف" && Txt_ImployPW.Text != "كلمة السر")
            {
                Querys.Excuter($"delete from _Employs where EmployID = '{Txt_ImployID.Text}' and EmployPW = '{Txt_ImployPW.Text}'");
                Txt_ImployID.Text = "اسم الموظف";
                Txt_ImployID.ForeColor = Color.Linen;
                Txt_ImployID.LineColor = Color.Linen;
                Txt_ImployPW.Text = "كلمة السر";
                Txt_ImployPW.ForeColor = Color.Linen;
                Txt_ImployPW.LineColor = Color.Linen;
                CHBox_EmploySeller.Checked = false;
                CHBox_EmployAdmin.Checked = false;
            }
            Querys.Reader_Datagrid("select * from _Employs where Service = 1", ref Datagrid_EmploysReport);
        }

        private void Btn_QueryToday_Click(object sender, EventArgs e)
        {
            StorageReady();
        }

        private void Btn_MoneyOut_Click(object sender, EventArgs e)
        {
            if (Txt_Money.Text == "المبلغ" || Txt_Money.Text.StartsWith("0"))
            {
                return;
            }
            if (Convert.ToDouble(Txt_Money.Text) > Convert.ToDouble(Txt_BalanceNow.Text))
            {
                COMMANDS.Error("الرصيد غير كافي للسحب");
                return;
            }
            double money_now = Convert.ToDouble(Querys.Reader_SingleValue("if EXISTS ( Select TOP 1 MoneyNow from _Storage ORDER BY StorageNo DESC) begin Select TOP 1 MoneyNow from _Storage ORDER BY StorageNo DESC END ELSE Select 0"));
            string Txt = "سحب نقدي";
            Querys.Excuter($"insert into _Storage values ('{Loading.AdminID}','{Txt}',0,0,0,'{Txt_Money.Text}',0,'{Txt_Money.Text}', {money_now - Convert.ToDouble(Txt_Money.Text)} ,'{DT}','{DT}')");
            COMMANDS.Information("تم السحب من الخازينه");
            Txt_Money.Text = "المبلغ";
            Txt_Money.ForeColor = Color.Linen;
            Txt_Money.LineColor = Color.Linen;
            StorageReady();
        }

        private void Btn_MoneyIn_Click(object sender, EventArgs e)
        {
            if (Txt_Money.Text == "المبلغ" || Txt_Money.Text.StartsWith("0"))
            {
                return;
            }
            double money_now = Convert.ToDouble(Querys.Reader_SingleValue("if EXISTS ( Select TOP 1 MoneyNow from _Storage ORDER BY StorageNo DESC) begin Select TOP 1 MoneyNow from _Storage ORDER BY StorageNo DESC END ELSE Select 0"));
            string Txt = "ايداع نقدي";
            Querys.Excuter($"insert into _Storage values ('{Loading.AdminID}','{Txt}',0,0,'{Txt_Money.Text}',0,'{Txt_Money.Text}',0, {money_now + Convert.ToDouble(Txt_Money.Text)} ,'{DT}','{DT}')");
            COMMANDS.Information("تم الايداع في الخازينه");
            Txt_Money.Text = "المبلغ";
            Txt_Money.ForeColor = Color.Linen;
            Txt_Money.LineColor = Color.Linen;
            StorageReady();
        }

        private void Btn_QueryBy_DatePicker_Click(object sender, EventArgs e)
        {
            try
            {
                Querys.Reader_Datagrid($"select * from _Storage where Date = '{DT}'", ref Datagrid_Storage);
                Txt_TotalMoney_Sales.Text = Querys.Reader_SingleValue($"if EXISTS(select sum (InvoicesMoney) from _Storage where date BETWEEN '{DatePKR_From.Text}' AND '{DatePKR_to.Text}') begin select sum (InvoicesMoney) from _Storage where date BETWEEN '{DatePKR_From.Text}' AND '{DatePKR_to.Text}' END ELSE Select 0");
                Txt_TotalMoney_Purchases.Text = Querys.Reader_SingleValue($"if EXISTS(select sum (PurchasesMoney) from _Storage where date BETWEEN '{DatePKR_From.Text}' AND '{DatePKR_to.Text}') begin select sum (PurchasesMoney) from _Storage where date BETWEEN '{DatePKR_From.Text}' AND '{DatePKR_to.Text}' END ELSE Select 0");
                Txt_TotalMoney_Deposit.Text = Querys.Reader_SingleValue($"if EXISTS(select sum (DepositMoney) from _Storage where date between '{DatePKR_From.Text}' AND '{DatePKR_to.Text}') begin select sum (DepositMoney) from _Storage where date between '{DatePKR_From.Text}' AND '{DatePKR_to.Text}' END ELSE Select 0");
                Txt_TotalMoney_Withdrawal.Text = Querys.Reader_SingleValue($"if EXISTS(select sum (WithdrawMoney) from _Storage where date BETWEEN '{DatePKR_From.Text}' AND '{DatePKR_to.Text}') begin select sum (WithdrawMoney) from _Storage where date BETWEEN '{DatePKR_From.Text}' AND '{DatePKR_to.Text}' END ELSE select 0");
                int LastNo = Convert.ToInt32(Querys.Reader_SingleValue($"if EXISTS(select TOP 1 StorageNo from _Storage where date between '{DatePKR_From.Text}' AND '{DatePKR_to.Text}') begin select TOP 1 StorageNo from _Storage where date between '{DatePKR_From.Text}' AND '{DatePKR_to.Text}' END Else Select 1"));
                Txt_TotalMoney_StartPirod.Text = Querys.Reader_SingleValue($"if EXISTS(select MoneyNow from _Storage where StorageNo = '{ LastNo - 1}') begin select MoneyNow from _Storage where StorageNo = '{ LastNo - 1}' END ELSE Select 0");
                Txt_TotalMoney_EndPirod.Text = Convert.ToString(Querys.Reader_SingleValue($"Select TOP 1 MoneyNow from _Storage where date BETWEEN '{DatePKR_From.Text}' AND '{DatePKR_to.Text}' ORDER BY StorageNo DESC"));
                Txt_BalanceNow.Text = Querys.Reader_SingleValue(" if EXISTS(Select TOP 1 MoneyNow from _Storage ORDER BY StorageNo DESC) begin Select TOP 1 MoneyNow from _Storage ORDER BY StorageNo DESC END ElSE Select 0");
            }
            catch (Exception ex)
            {
                COMMANDS.Error(ex.Message);
            }
        }

        private void Txt_Warehouse_Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Txt_Warehouse_Search.Text))
                {
                    Querys.Reader_Datagrid("select ItemID,ItemAvaliable from _Warehouse where ItemID LIKE '" + Txt_Warehouse_Search.Text + "%' and ItemAvaliable > 0 order by Date DESC", ref Datagrid_Avaliable_Warehouse);
                    Querys.Reader_Datagrid("select ItemID,ItemIn  from _Warehouse where ItemID LIKE '" + Txt_Warehouse_Search.Text + "%' order by Date DESC", ref Datagrid_ItemsIn_Warehouse);
                    Querys.Reader_Datagrid("select ItemID,ItemOut from _Warehouse where ItemID LIKE '" + Txt_Warehouse_Search.Text + "%' and ItemOut > 0 order by Date DESC", ref Datagrid_ItemsOut_Warehouse);
                }
                else
                {
                    Querys.Reader_Datagrid("select ItemID,ItemAvaliable from _Warehouse where ItemAvaliable > 0 order by Date DESC", ref Datagrid_Avaliable_Warehouse);
                    Querys.Reader_Datagrid("select ItemID,ItemIn  from _Warehouse order by Date DESC ", ref Datagrid_ItemsIn_Warehouse);
                    Querys.Reader_Datagrid("select ItemID,ItemOut from _Warehouse WareHouse where ItemOut > 0 order by Date DESC ", ref Datagrid_ItemsOut_Warehouse);
                }
            }
            catch (Exception ex)
            {
                COMMANDS.Error(ex.Message);
            }
        }

        private void Btn_AddNew_Item_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CB_AddNew_Code.Text) && Txt_AddNew_Item.Text != "اسم المنتج" && !string.IsNullOrEmpty(CB_SupplierCode.Text) && Txt_SupplierName.Text != "اسم المورد" && gunaLineTextBox1.Text != "السعر" && gunaComboBox1.SelectedIndex == -1 && Txt_AddNew_ItemQuantity.Text != "العدد" && Txt_AddNew_minimum.Text != "الحد الاقصى" && Txt_AddNew_maximum.Text != "الحد الاقصى" && Txt_SupplierNumber.Text != "رقم الهاتف" && Txt_Address.Text != "العنوان")
            {
                Querys.Excuter($"if not exists (select ItemID from _Warehouse where ItemID = '{Txt_AddNew_Item.Text}' and Service = 1 ) BEGIN insert into _Warehouse Values ('{Loading.AdminID}','{CB_AddNew_Code.Text}','{Txt_AddNew_Item.Text}',{Convert.ToDouble(gunaLineTextBox1.Text)},{Convert.ToInt32(Txt_AddNew_minimum.Text)},{Convert.ToInt32(Txt_AddNew_maximum.Text)},'{Txt_SupplierName.Text}',{Convert.ToDecimal(Txt_SupplierNumber.Text)},'{Txt_Address.Text}',{Convert.ToInt32(Txt_AddNew_ItemQuantity.Text)},0,{Convert.ToInt32(Txt_AddNew_ItemQuantity.Text)},1,'{DT}','{DT}')END;" +
                               $"insert into _Suppliers Values ('{Loading.AdminID}','{Txt_SupplierName.Text.Trim()}','{Txt_SupplierNumber.Text.Trim()}','{Txt_Address.Text.Trim()}',1,'{DT}','{DT}')");
                Btn_Warehouse_Clear.PerformClick();
                Querys.Reader_Datagrid("select ItemID,ItemIn from _Warehouse order by Date DESC", ref Datagrid_ItemsIn_Warehouse);
                Querys.Reader_Datagrid("select ItemID,ItemAvaliable from _Warehouse where ItemAvaliable > 0 order by Date DESC", ref Datagrid_Avaliable_Warehouse);
                Querys.Reader_Datagrid("select ItemID,ItemOut from _Warehouse where ItemOut > 0.0 order by Date DESC ", ref Datagrid_ItemsOut_Warehouse);
            }
            else if (!string.IsNullOrEmpty(CB_AddNew_Code.Text) && Txt_AddNew_Item.Text != "اسم المنتج" && !string.IsNullOrEmpty(CB_SupplierCode.Text) && Txt_SupplierName.Text == "اسم المورد" && gunaComboBox1.SelectedIndex != -1 && gunaLineTextBox1.Text != "السعر" && Txt_AddNew_ItemQuantity.Text != "العدد" && Txt_AddNew_minimum.Text != "الحد الاقصى" && Txt_AddNew_maximum.Text != "الحد الاقصى" && Txt_SupplierNumber.Text != "رقم الهاتف" && Txt_Address.Text != "العنوان")
            {
                Querys.Excuter($"if not exists (select ItemID from _Warehouse where ItemID = '{Txt_AddNew_Item.Text}' and Service = 1 ) BEGIN insert into _Warehouse Values ('{Loading.AdminID}','{CB_AddNew_Code.Text}','{Txt_AddNew_Item.Text}',{Convert.ToDouble(gunaLineTextBox1.Text)},{Convert.ToInt32(Txt_AddNew_minimum.Text)},{Convert.ToInt32(Txt_AddNew_maximum.Text)},'{gunaLineTextBox1.Text}',{Convert.ToDecimal(Txt_SupplierNumber.Text)},'{Txt_Address.Text}',{Convert.ToInt32(Txt_AddNew_ItemQuantity.Text)},0,{Convert.ToInt32(Txt_AddNew_ItemQuantity.Text)},1,'{DT}','{DT}')END;" +
                               $"insert into _Suppliers Values ('{Loading.AdminID}','{gunaLineTextBox1.Text.Trim()}','{Txt_SupplierNumber.Text.Trim()}','{Txt_Address.Text.Trim()}',1,'{DT}','{DT}')");
                Btn_Warehouse_Clear.PerformClick();
                Querys.Reader_Datagrid("select ItemID,ItemIn from _Warehouse order by Date DESC", ref Datagrid_ItemsIn_Warehouse);
                Querys.Reader_Datagrid("select ItemID,ItemAvaliable from _Warehouse where ItemAvaliable > 0 order by Date DESC", ref Datagrid_Avaliable_Warehouse);
                Querys.Reader_Datagrid("select ItemID,ItemOut from _Warehouse where ItemOut > 0.0 order by Date DESC ", ref Datagrid_ItemsOut_Warehouse);
            }
        }

        private void CB_AddExist_Item_Enter(object sender, EventArgs e)
        {
            CB_AddExist_Item.Items.Clear();
            Querys.CBFillers(CB_AddExist_Item, "select ItemID from _Warehouse");
        }

        private void Btn_AddExist_Item_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CB_AddExist_Item.Text) && Txt_AddExist_Available.Text != "الرصيد المتاح" && Txt_AddExist_ItemQuantity.Text != "العدد")
            {
                Querys.Excuter($"if exists (select ItemID from _Warehouse where ItemID = '{CB_AddExist_Item.Text}' ) BEGIN update _Warehouse set ItemIn = {(Convert.ToInt32(Txt_AddExist_ItemQuantity.Text) + Convert.ToInt32(Txt_AddExist_Available.Text)) } , ItemAvaliable = {(Convert.ToInt32(Txt_AddExist_Available.Text) + Convert.ToInt32(Txt_AddExist_ItemQuantity.Text))} where ItemID = '{CB_AddExist_Item.Text}' END");
                Btn_Warehouse_Clear.PerformClick();

                Querys.Reader_Datagrid("select ItemID,ItemIn from _Warehouse order by Date DESC", ref Datagrid_ItemsIn_Warehouse);
                Querys.Reader_Datagrid("select ItemID,ItemAvaliable from _Warehouse where ItemAvaliable > 0 order by Date DESC", ref Datagrid_Avaliable_Warehouse);
                Querys.Reader_Datagrid("select ItemID,ItemOut from _Warehouse where ItemOut > 0.0 order by Date DESC ", ref Datagrid_ItemsOut_Warehouse);
            }
        }

        private void CB_TakeExist_Item_Enter(object sender, EventArgs e)
        {
            CB_TakeExist_Item.Items.Clear();
            Querys.CBFillers(CB_TakeExist_Item, "select ItemID from _Warehouse");

        }

        private void Btn_TakeExist_Item_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CB_TakeExist_Item.Text) && Txt_TakeExist_Available.Text != "الرصيد المتاح" && Txt_TakeExist_ItemQuantity.Text != "العدد")
            {
                if (int.Parse(Txt_TakeExist_ItemQuantity.Text) <= int.Parse(Txt_TakeExist_Available.Text))
                {
                    Querys.Excuter($"if exists (select ItemID from _Warehouse where ItemID = '{CB_TakeExist_Item.Text}' ) BEGIN update _Warehouse set ItemOut = (select ItemOut from _Warehouse where ItemID = '{CB_TakeExist_Item.Text}' ) + { Convert.ToInt32(Txt_TakeExist_ItemQuantity.Text) } , ItemAvaliable = {(Convert.ToInt32(Txt_TakeExist_Available.Text) - Convert.ToInt32(Txt_TakeExist_ItemQuantity.Text))} where ItemID = '{CB_TakeExist_Item.Text}' END");
                    Btn_Warehouse_Clear.PerformClick();

                    Querys.Reader_Datagrid("select ItemID,ItemIn from _Warehouse order by Date DESC", ref Datagrid_ItemsIn_Warehouse);
                    Querys.Reader_Datagrid("select ItemID,ItemAvaliable from _Warehouse where ItemAvaliable > 0 order by Date DESC", ref Datagrid_Avaliable_Warehouse);
                    Querys.Reader_Datagrid("select ItemID,ItemOut from _Warehouse where ItemOut > 0 order by Date DESC ", ref Datagrid_ItemsOut_Warehouse);
                }
            }
        }

        private void Btn_ItemAdder_Click(object sender, EventArgs e)
        {
            if (Txt_ProductCount.Text == "العدد" || Txt_TotalPrice.Text == "الاجمالي")
            {
                COMMANDS.Information("رجاء ادخال بيانات المنتج");
                return;
            }
            if (Convert.ToInt32(Txt_ProductCount.Text) <= 0 || string.IsNullOrEmpty(CB_ProductCode.Text) || string.IsNullOrEmpty(CB_ProductNames.Text) || string.IsNullOrEmpty(Txt_UnitPrice.Text) || string.IsNullOrEmpty(Txt_ProductCount.Text) || string.IsNullOrEmpty(Txt_TotalPrice.Text))
            {
                COMMANDS.Information("رجاء ادخال بيانات المنتج");
                return;
            }
            if (DataGrid_Invoices.Rows.Count == 10)
            {
                COMMANDS.Information("اقصي منتجات في الفاتوره 10 اصناف");
                return;
            }
            int Quantity = Convert.ToInt32(Querys.Reader_SingleValue($" select ItemAvaliable from _Warehouse where ItemID = '{CB_ProductNames.Text}'"));
            if (Quantity < Convert.ToInt32(Txt_ProductCount.Text))
            {
                COMMANDS.Information($"الحد الاقصي للسحب هوا [{Quantity}]");
                return;
            }

            DataGrid_Invoices.Rows.Add((int.Parse(Txt_ProductCount.Text) * int.Parse(Txt_UnitPrice.Text)).ToString(), Txt_UnitPrice.Text.Trim(), Txt_ProductCount.Text.Trim(), CB_ProductNames.Text.Trim(), DataGrid_Invoices.Rows.Count + 1);
            Querys.Excuter($"update _Warehouse set ItemOut = ( select ItemOut from _Warehouse where ItemID = '{CB_ProductNames.Text.Trim()}' ) + { Convert.ToInt32(Txt_ProductCount.Text) } , ItemAvaliable = ( select ItemAvaliable from _Warehouse where ItemID = '{CB_ProductNames.Text.Trim()}' ) - { Convert.ToInt32(Txt_ProductCount.Text)} where ItemID = '{CB_ProductNames.Text.Trim()}'");

            double[] itemTotalPrice = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < DataGrid_Invoices.RowCount; i++)
            {
                try
                {
                    itemTotalPrice[i] = Convert.ToDouble(DataGrid_Invoices.Rows[i].Cells[0].Value.ToString().Trim());
                }
                catch { }
            }

            double totalPrice = 0;

            for (int i = 0; i < DataGrid_Invoices.RowCount; i++)
            {
                totalPrice = totalPrice + Convert.ToDouble(DataGrid_Invoices.Rows[i].Cells[0].Value);
            }
            try
            {
                TotalInvoice.Text = (totalPrice - Convert.ToDouble(Txt_Discount.Text)).ToString();
            }
            catch { TotalInvoice.Text = totalPrice.ToString(); }

        }

        private void gunaCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            GunaCheckBox checkBox = (GunaCheckBox)sender;
            GunaCheckBox[] Checkbuttons = { gunaCheckBox4, gunaCheckBox5, gunaCheckBox6 };

            if (checkBox.Checked.Equals(true))
                checkBox.ForeColor = Color.Goldenrod;
            else
                checkBox.ForeColor = Color.Linen;
            for (int i = 0; i < Checkbuttons.Length; i++)
            {
                if (!Checkbuttons[i].Equals(checkBox) && Checkbuttons[i].Checked.Equals(true))
                    Checkbuttons[i].Checked = false;
            }
        }

        private void gunaCheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            GunaCheckBox checkBox = (GunaCheckBox)sender;
            GunaCheckBox[] Checkbuttons = { gunaCheckBox7, gunaCheckBox8, gunaCheckBox9 };

            if (checkBox.Checked.Equals(true))
            {
                checkBox.ForeColor = Color.Goldenrod;
            }
            else
            {
                checkBox.ForeColor = Color.Linen;
            }
            for (int i = 0; i < Checkbuttons.Length; i++)
            {
                if (!Checkbuttons[i].Equals(checkBox) && Checkbuttons[i].Checked.Equals(true))
                    Checkbuttons[i].Checked = false;
            }
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Btn_Search_Archive.ForeColor = Color.Linen;
                gunaAdvenceButton1.ForeColor = Color.Linen;
                gunaAdvenceButton3.ForeColor = Color.Linen;
                gunaAdvenceButton2.ForeColor = Color.Goldenrod;
                Daragrid_Archive.Rows.Clear();

                double InvoicesMoney = Convert.ToDouble(Querys.Reader_SingleValue($"select sum (InvoicesMoney) from _Storage where Date between '{gunaDateTimePicker1.Value}' and '{gunaDateTimePicker2.Value}'"));
                double Purchases = Convert.ToDouble(Querys.Reader_SingleValue($"select sum (PurchasesMoney) from _Storage where Date between '{gunaDateTimePicker1.Value}' and '{gunaDateTimePicker2.Value}'"));
                double DepositMoney = Convert.ToDouble(Querys.Reader_SingleValue($"select sum (DepositMoney) from _Storage where Date between '{gunaDateTimePicker1.Value}' and '{gunaDateTimePicker2.Value}'"));
                double WithdrawMoney = Convert.ToDouble(Querys.Reader_SingleValue($"select sum (WithdrawMoney) from _Storage where Date between '{gunaDateTimePicker1.Value}' and '{gunaDateTimePicker2.Value}'"));
                double TotalBeforeCut = Convert.ToDouble(Querys.Reader_SingleValue($"select sum (ItemsTotalPrice_BeforeDiscount) from _Invoices where Date between '{gunaDateTimePicker1.Value}' and '{gunaDateTimePicker2.Value}'"));
                double TotalDiscount = Convert.ToDouble(Querys.Reader_SingleValue($"select sum (CustomerDiscount) from _Invoices where Date between '{gunaDateTimePicker1.Value}' and '{gunaDateTimePicker2.Value}'"));
                double TotalAfterDiscount = TotalBeforeCut - TotalDiscount;
                double Elsafe = (TotalAfterDiscount - (WithdrawMoney + Purchases));
                Daragrid_Archive.Rows.Add(Elsafe, Purchases, WithdrawMoney, TotalBeforeCut, DepositMoney, TotalAfterDiscount, TotalDiscount, InvoicesMoney, gunaDateTimePicker1.Value.ToString().Split(' ')[0].Trim() + " : " + gunaDateTimePicker2.Value.ToString().Split(' ')[0].Trim());
            }
            catch
            {

            }
        }

        private void Txt_UnitPrice_Leave(object sender, EventArgs e)
        {
            if (Txt_UnitPrice.Text.Equals("سعر الوحدة"))
            {
                Txt_UnitPrice.LineColor = Color.Linen;
                Txt_UnitPrice.ForeColor = Color.Linen;
            }
        }

        private void Btn_Warehouse_Clear_Click(object sender, EventArgs e)
        {
            WarehouseClear();
            Querys.Reader_Datagrid("select ItemID,ItemIn from _Warehouse order by Date DESC", ref Datagrid_ItemsIn_Warehouse);
            Querys.Reader_Datagrid("select ItemID,ItemAvaliable from _Warehouse where ItemAvaliable > 0 order by Date DESC", ref Datagrid_Avaliable_Warehouse);
            Querys.Reader_Datagrid("select ItemID,ItemOut from _Warehouse where ItemOut > 0.0 order by Date DESC ", ref Datagrid_ItemsOut_Warehouse);
        }

        private void Txt_AddExist_Available_Enter(object sender, EventArgs e)
        {
            GunaLineTextBox textBox = (GunaLineTextBox)sender;
            textBox.LineColor = Color.Goldenrod;
            textBox.ForeColor = Color.Goldenrod;
        }

        private void Txt_TakeExist_Available_Leave(object sender, EventArgs e)
        {
            GunaLineTextBox textBox = (GunaLineTextBox)sender;
            textBox.LineColor = Color.Linen;
            textBox.ForeColor = Color.Linen;
        }

        private void Event_ChackedChanged(object sender, EventArgs e)
        {
            GunaCheckBox checkBox = (GunaCheckBox)sender;
            GunaCheckBox[] Checkbuttons = { CHBox_SalesReport, CHBox_PurchasesReport };

            if (checkBox.Checked.Equals(true))
                checkBox.ForeColor = Color.Goldenrod;
            else
                checkBox.ForeColor = Color.Linen;
            for (int i = 0; i < Checkbuttons.Length; i++)
            {
                if (!Checkbuttons[i].Equals(checkBox) && Checkbuttons[i].Checked.Equals(true))
                    Checkbuttons[i].Checked = false;
            }
        }
        private void CHBox_EmploySeller_CheckedChanged(object sender, EventArgs e)
        {
            GunaCheckBox checkBox = (GunaCheckBox)sender;
            GunaCheckBox[] Checkbuttons = { CHBox_EmploySeller, CHBox_EmployAdmin, };

            if (checkBox.Checked.Equals(true))
                checkBox.ForeColor = Color.Goldenrod;
            else
                checkBox.ForeColor = Color.Linen;
            for (int i = 0; i < Checkbuttons.Length; i++)
            {
                if (!Checkbuttons[i].Equals(checkBox) && Checkbuttons[i].Checked.Equals(true))
                    Checkbuttons[i].Checked = false;
            }
        }
    }
}

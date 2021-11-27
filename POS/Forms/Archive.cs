using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Elections.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Elections.Forms
{
    public partial class Form_Archive : Form
    {
        private new bool MouseMove;
        private Point LastLocation;
        public Form_Archive()
        {
            InitializeComponent();
        }

        private void Panel_Move_MouseUp(object sender, MouseEventArgs e)
        {
            MouseMove = false;
        }
        private void Panel_Move_MouseDown(object sender, MouseEventArgs e)
        {
            MouseMove = true;
            LastLocation = e.Location;
        }
        private void Panel_Move_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseMove)
                this.Location = new Point((this.Location.X - LastLocation.X) + e.X, (this.Location.Y - LastLocation.Y) + e.Y);
        }
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Archive_Load(object sender, EventArgs e)
        {
            if (Loading.Archives_Options == "Days")
            {
                Querys.Reader_Datagrid("select InvoicesNo,AdminID,CustomerID,ItemsTotalPrice_BeforeDiscount as BeforeDiscount,CustomerDiscount,ItemsPrice_AfterDiscount as AfterDiscount from _Invoices where Date  = '" + Loading.Archives_Day + "' order by Date DESC", ref DataGrid_Invoices);
                Querys.Reader_Datagrid("select AdminID,ItemID,ItemPrice,ItemQuantity from _Purchases where Date  = '" + Loading.Archives_Day + "' order by Date DESC", ref gunaDataGridView2);
                Querys.Reader_Datagrid("select AdminID,Source,InvoicesMoney,PurchasesMoney,MoneyIn,MoneyOut from _Storage   where Date  = '" + Loading.Archives_Day + "' order by Date DESC", ref gunaDataGridView1);
            }
            if (Loading.Archives_Options == "Months")
            {
                Querys.Reader_Datagrid("select InvoicesNo,AdminID,CustomerID,ItemsTotalPrice_BeforeDiscount as BeforeDiscount,CustomerDiscount,ItemsPrice_AfterDiscount as AfterDiscount from _Invoices where Month(Date)  = '" + Loading.Archives_Day + "' order by Month(Date) DESC", ref DataGrid_Invoices);
                Querys.Reader_Datagrid("select AdminID,ItemID,ItemPrice,ItemQuantity from _Purchases where Month(Date)  = '" + Loading.Archives_Day + "' order by Month(Date) DESC", ref gunaDataGridView2);
                Querys.Reader_Datagrid("select AdminID,Source,InvoicesMoney,PurchasesMoney,MoneyIn,MoneyOut from _Storage   where Month(Date)  = '" + Loading.Archives_Day + "' order by Month(Date) DESC", ref gunaDataGridView1);
            }
            if (Loading.Archives_Options == "Years")
            {
                Querys.Reader_Datagrid("select InvoicesNo,AdminID,CustomerID,ItemsTotalPrice_BeforeDiscount as BeforeDiscount,CustomerDiscount,ItemsPrice_AfterDiscount as AfterDiscount  from _Invoices where Year(Date)  = '" + Loading.Archives_Day + "' order by Year(Date) DESC", ref DataGrid_Invoices);
                Querys.Reader_Datagrid("select AdminID,ItemID,ItemPrice,ItemQuantity from _Purchases where Year(Date)  = '" + Loading.Archives_Day + "' order by Year(Date) DESC", ref gunaDataGridView2);
                Querys.Reader_Datagrid("select AdminID,Source,InvoicesMoney,PurchasesMoney,MoneyIn,MoneyOut from _Storage   where Year(Date)  = '" + Loading.Archives_Day + "' order by Year(Date) DESC", ref gunaDataGridView1);
            }
            if (Loading.Archives_Options == "Custom")
            {
                Querys.Reader_Datagrid("select InvoicesNo,AdminID,CustomerID,ItemsTotalPrice_BeforeDiscount as BeforeDiscount,CustomerDiscount,ItemsPrice_AfterDiscount as AfterDiscount from _Invoices where Date  between '" + Loading.Archives_Day.Split(':')[0] + "' and '" + Loading.Archives_Day.Split(':')[1] + "' order by Date DESC", ref DataGrid_Invoices);
                Querys.Reader_Datagrid("select AdminID,ItemID,ItemPrice,ItemQuantity from _Purchases where Date between '" + Loading.Archives_Day.Split(':')[0] + "' and '" + Loading.Archives_Day.Split(':')[1] + "' order by Date DESC", ref gunaDataGridView2);
                Querys.Reader_Datagrid("select AdminID,Source,InvoicesMoney,PurchasesMoney,MoneyIn,MoneyOut from _Storage where Date between '" + Loading.Archives_Day.Split(':')[0] + "' and '" + Loading.Archives_Day.Split(':')[1] + "' order by Date DESC", ref gunaDataGridView1);
            }
        }
        private void DataGrid_Invoices_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable Invoice = new DataTable();
            Querys.Reader_Table($"select * from _Invoices where InvoicesNo = {DataGrid_Invoices.Rows[e.RowIndex].Cells[1].Value}", ref Invoice);
            object[] ItemID = { Invoice.Rows[0][6].ToString().Trim(), Invoice.Rows[0][11].ToString().Trim(), Invoice.Rows[0][16].ToString().Trim(), Invoice.Rows[0][21].ToString().Trim(), Invoice.Rows[0][26].ToString().Trim(), Invoice.Rows[0][31].ToString().Trim(), Invoice.Rows[0][36].ToString().Trim(), Invoice.Rows[0][41].ToString().Trim(), Invoice.Rows[0][46].ToString().Trim(), Invoice.Rows[0][51].ToString().Trim() };
            int[] ItemPrice = { Convert.ToInt32(Invoice.Rows[0][7].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][12].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][17].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][22].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][27].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][32].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][37].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][42].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][47].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][52].ToString().Trim()) };
            int[] ItemCount = { Convert.ToInt32(Invoice.Rows[0][8].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][13].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][18].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][23].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][28].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][33].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][38].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][43].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][48].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][53].ToString().Trim()) };
            double[] ItemsTotalPrice = { Convert.ToInt32(Invoice.Rows[0][9].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][14].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][19].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][24].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][29].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][34].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][39].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][44].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][49].ToString().Trim()), Convert.ToInt32(Invoice.Rows[0][54].ToString().Trim()) };
            Main_Form.InvoiceCode = Invoice.Rows[0][0].ToString();
            Main_Form.LB_AdminCode = Invoice.Rows[0][1].ToString();
            Main_Form.CustomerName = Invoice.Rows[0][3].ToString();
            Main_Form.CustomerNumber = Invoice.Rows[0][4].ToString();
            Main_Form.ItemsTotalPrice = ItemsTotalPrice.Sum();
            Main_Form.itemsQuanity = ItemCount.Sum();
            Main_Form.InvoiceDescount = Convert.ToInt32(Invoice.Rows[0][56].ToString());
            int[] RowNumber = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            XtraReport1 Report = new XtraReport1();

            double[] cellWidth = { 201.49, 82.33, 86.64, 273.4, 56.14 };
            for (int i = 0; i <= 9; i++)
            {
                XRTableRow xrRow = new XRTableRow();
                string[] CellText = { ItemsTotalPrice[i].ToString(), ItemPrice[i].ToString(), ItemCount[i].ToString(), ItemID[i].ToString(), RowNumber[i].ToString(), };
                if (!string.IsNullOrEmpty(ItemID[i].ToString().Trim()) && ItemID[i].ToString() != "0")
                {
                    for (int j = 0; j < 5; j++)
                    {
                        XRTableCell xRCell = new XRTableCell();
                        xRCell.Text = CellText[j];
                        xRCell.WidthF = (float)cellWidth[j];
                        xRCell.BorderColor = Color.Black;
                        xRCell.BorderWidth = 1;
                        xRCell.BorderDashStyle = BorderDashStyle.Double;
                        xRCell.Borders = BorderSide.All;
                        xRCell.TextAlignment = TextAlignment.MiddleCenter;
                        xrRow.Cells.Add(xRCell);
                    }
                }
                Report.Table_Items.Rows.Add(xrRow);
            }
            for (int i = 0; i <= 9; i++)
            {
                XRTableRow xrRow = new XRTableRow();
                string[] CellText = { ItemsTotalPrice[i].ToString(), ItemPrice[i].ToString(), ItemCount[i].ToString(), ItemID[i].ToString(), RowNumber[i].ToString(), };
                if (!string.IsNullOrEmpty(ItemID[i].ToString().Trim()) && ItemID[i].ToString() != "0")
                {
                    for (int j = 0; j < 5; j++)
                    {
                        XRTableCell xRCell = new XRTableCell();
                        xRCell.Text = CellText[j];
                        xRCell.WidthF = (float)cellWidth[j];
                        xRCell.BorderColor = Color.Black;
                        xRCell.BorderWidth = 1;
                        xRCell.BorderDashStyle = BorderDashStyle.Double;
                        xRCell.Borders = BorderSide.All;
                        xRCell.TextAlignment = TextAlignment.MiddleCenter;
                        xrRow.Cells.Add(xRCell);
                    }
                }
                Report.Table_Items2.Rows.Add(xrRow);
            }

            Report.Watermark.Text = "COPY";
            Report.Print();
            Main_Form.InvoiceCode = string.Empty;
            Main_Form.LB_AdminCode = string.Empty;
            Main_Form.CustomerName = string.Empty;
            Main_Form.CustomerNumber = string.Empty;
            Main_Form.InvoiceDescount = 0;
            Main_Form.itemsQuanity = 0;
            Main_Form.ItemsTotalPrice = 0;
        }
    }
}

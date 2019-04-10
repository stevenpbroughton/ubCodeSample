using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SYSPROHISTORICALTEST2
{
    public partial class frmCustomerQuery : Form
    {
        public frmCustomerQuery()
        {
            InitializeComponent();
        }

        private string select = "Select ARCUSTOMER.Customer, ARCUSTOMER.Name, ARCUSTOMER.ShortName, ARCUSTOMER.SoldToAddr1, ARCUSTOMER.SoldToAddr2, ARCUSTOMER.SoldToAddr3, ARCUSTOMER.SoldToAddr4, ARCUSTOMER.SoldToAddr5, ARCUSTOMER.SoldPostalCode, ARCUSTOMER.Telephone, ARCUSTOMER.ExemptFinChg, ARCUSTOMER.MaintHistory, ARCUSTOMER.CustomerType, ARCUSTOMER.MasterAccount, ARCUSTOMER.StoreNumber, ARCUSTOMER.PrtMasterAdd, ARCUSTOMER.CreditStatus, ARCUSTOMER.CreditLimit, ARCUSTOMER.InvoiceCount, ARCUSTOMER.Salesperson, ARCUSTOMER.Salesperson1, ARCUSTOMER.Salesperson2, ARCUSTOMER.Salesperson3, ARCUSTOMER.PriceCode, ARCUSTOMER.CustomerClass, ARCUSTOMER.Branch, ARCUSTOMER.TermsCode, ARCUSTOMER.InvDiscCode, ARCUSTOMER.BalanceType, ARCUSTOMER.Area, ARCUSTOMER.LineDiscCode, ARCUSTOMER.TaxStatus, ARCUSTOMER.TaxExemptNumber, ARCUSTOMER.SpecialInstrs, ARCUSTOMER.PriceCategoryTable, ARCUSTOMER.DateLastSale, ARCUSTOMER.DateLastPay, ARCUSTOMER.OutstOrdVal, ARCUSTOMER.NumOutstOrd,  ARCUSTOMER.Contact, ARCUSTOMER.AddTelephone, ARCUSTOMER.Fax, ARCUSTOMER.Telex, ARCUSTOMER.TelephoneExtn, ARCUSTOMER.Currency, ARCUSTOMER.UserField1, ARCUSTOMER.UserField2, ARCUSTOMER.GstExemptFlag, ARCUSTOMER.GstExemptNum, ARCUSTOMER.GstLevel, ARCUSTOMER.DetailMoveReqd, ARCUSTOMER.InterfaceFlag, ARCUSTOMER.ContractPrcReqd, ARCUSTOMER.BuyingGroup1, ARCUSTOMER.BuyingGroup2, ARCUSTOMER.BuyingGroup3, ARCUSTOMER.BuyingGroup4, ARCUSTOMER.BuyingGroup5, ARCUSTOMER.StatementReqd, ARCUSTOMER.BackOrdReqd, ARCUSTOMER.ShippingInstrs, ARCUSTOMER.StateCode, ARCUSTOMER.DateCustAdded, ARCUSTOMER.StockInterchange, ARCUSTOMER.MaintLastPrcPaid, ARCUSTOMER.IbtCustomer, ARCUSTOMER.SoDefaultDoc, ARCUSTOMER.CounterSlsOnly, ARCUSTOMER.PaymentStatus, ARCUSTOMER.Nationality, ARCUSTOMER.HighestBalance, ARCUSTOMER.CustomerOnHold, ARCUSTOMER.InvCommentCode, ARCUSTOMER.EdiSenderCode, ARCUSTOMER.RelOrdOsValue, ARCUSTOMER.EdiFlag, ARCUSTOMER.SoDefaultType, ARCUSTOMER.Email, ARCUSTOMER.ApplyOrdDisc, ARCUSTOMER.ApplyLineDisc, ARCUSTOMER.FaxInvoices, ARCUSTOMER.FaxStatements, ARCUSTOMER.HighInvDays, ARCUSTOMER.HighInv, ARCUSTOMER.DocFax, ARCUSTOMER.DocFaxContact,   ARCUSTOMER.ShipToAddr1, ARCUSTOMER.ShipToAddr2, ARCUSTOMER.ShipToAddr3, ARCUSTOMER.ShipToAddr4, ARCUSTOMER.ShipToAddr5, ARCUSTOMER.ShipPostalCode, ARCUSTOMER.State, ARCUSTOMER.CountyZip, ARCUSTOMER.City, ARCUSTOMER.State1, ARCUSTOMER.CountyZip1, ARCUSTOMER.City1, ARCUSTOMER.DefaultOrdType, ARCUSTOMER.PoNumberMandatory, ARCUSTOMER.CreditCheckFlag, ARCUSTOMER.CompanyTaxNumber, ARCUSTOMER.DeliveryTerms, ARCUSTOMER.TransactionNature, ARCUSTOMER.DeliveryTermsC, ARCUSTOMER.TransactionNatureC, ARCUSTOMER.RouteCode FROM ARCUSTOMER";
        private string where = " Where 1=1 ";
        private string order = " Order By Customer, Name, ShortName, SoldToAddr1, SoldToAddr2, SoldToAddr3, SoldToAddr4, SoldToAddr5, SoldPostalCode, Telephone ";
        private string selectedCustomer = "";
        private string selectedSecondaryKey = "";


        private string connectionString = "Data Source=REPORT-SERVER;Initial Catalog=SysproHistorical;Persist Security Info=" +
            "True;User ID=sa;Password=C0ff33plz";
        private SqlConnection conn;
        private SqlDataAdapter da;
        private SqlCommandBuilder cb;
        private DataSet ds;


        private void btnFind_Click(object sender, EventArgs e) //find all customers that meet search criteria, select first one
        {
            //set up default values
            select = "Select ARCUSTOMER.Customer, ARCUSTOMER.Name, ARCUSTOMER.ShortName, ARCUSTOMER.SoldToAddr1, ARCUSTOMER.SoldToAddr2, ARCUSTOMER.SoldToAddr3, ARCUSTOMER.SoldToAddr4, ARCUSTOMER.SoldToAddr5, ARCUSTOMER.SoldPostalCode, ARCUSTOMER.Telephone, ARCUSTOMER.ExemptFinChg, ARCUSTOMER.MaintHistory, ARCUSTOMER.CustomerType, ARCUSTOMER.MasterAccount, ARCUSTOMER.StoreNumber, ARCUSTOMER.PrtMasterAdd, ARCUSTOMER.CreditStatus, ARCUSTOMER.CreditLimit, ARCUSTOMER.InvoiceCount, ARCUSTOMER.Salesperson, ARCUSTOMER.Salesperson1, ARCUSTOMER.Salesperson2, ARCUSTOMER.Salesperson3, ARCUSTOMER.PriceCode, ARCUSTOMER.CustomerClass, ARCUSTOMER.Branch, ARCUSTOMER.TermsCode, ARCUSTOMER.InvDiscCode, ARCUSTOMER.BalanceType, ARCUSTOMER.Area, ARCUSTOMER.LineDiscCode, ARCUSTOMER.TaxStatus, ARCUSTOMER.TaxExemptNumber, ARCUSTOMER.SpecialInstrs, ARCUSTOMER.PriceCategoryTable, ARCUSTOMER.DateLastSale, ARCUSTOMER.DateLastPay, ARCUSTOMER.OutstOrdVal, ARCUSTOMER.NumOutstOrd,  ARCUSTOMER.Contact, ARCUSTOMER.AddTelephone, ARCUSTOMER.Fax, ARCUSTOMER.Telex, ARCUSTOMER.TelephoneExtn, ARCUSTOMER.Currency, ARCUSTOMER.UserField1, ARCUSTOMER.UserField2, ARCUSTOMER.GstExemptFlag, ARCUSTOMER.GstExemptNum, ARCUSTOMER.GstLevel, ARCUSTOMER.DetailMoveReqd, ARCUSTOMER.InterfaceFlag, ARCUSTOMER.ContractPrcReqd, ARCUSTOMER.BuyingGroup1, ARCUSTOMER.BuyingGroup2, ARCUSTOMER.BuyingGroup3, ARCUSTOMER.BuyingGroup4, ARCUSTOMER.BuyingGroup5, ARCUSTOMER.StatementReqd, ARCUSTOMER.BackOrdReqd, ARCUSTOMER.ShippingInstrs, ARCUSTOMER.StateCode, ARCUSTOMER.DateCustAdded, ARCUSTOMER.StockInterchange, ARCUSTOMER.MaintLastPrcPaid, ARCUSTOMER.IbtCustomer, ARCUSTOMER.SoDefaultDoc, ARCUSTOMER.CounterSlsOnly, ARCUSTOMER.PaymentStatus, ARCUSTOMER.Nationality, ARCUSTOMER.HighestBalance, ARCUSTOMER.CustomerOnHold, ARCUSTOMER.InvCommentCode, ARCUSTOMER.EdiSenderCode, ARCUSTOMER.RelOrdOsValue, ARCUSTOMER.EdiFlag, ARCUSTOMER.SoDefaultType, ARCUSTOMER.Email, ARCUSTOMER.ApplyOrdDisc, ARCUSTOMER.ApplyLineDisc, ARCUSTOMER.FaxInvoices, ARCUSTOMER.FaxStatements, ARCUSTOMER.HighInvDays, ARCUSTOMER.HighInv, ARCUSTOMER.DocFax, ARCUSTOMER.DocFaxContact,   ARCUSTOMER.ShipToAddr1, ARCUSTOMER.ShipToAddr2, ARCUSTOMER.ShipToAddr3, ARCUSTOMER.ShipToAddr4, ARCUSTOMER.ShipToAddr5, ARCUSTOMER.ShipPostalCode, ARCUSTOMER.State, ARCUSTOMER.CountyZip, ARCUSTOMER.City, ARCUSTOMER.State1, ARCUSTOMER.CountyZip1, ARCUSTOMER.City1, ARCUSTOMER.DefaultOrdType, ARCUSTOMER.PoNumberMandatory, ARCUSTOMER.CreditCheckFlag, ARCUSTOMER.CompanyTaxNumber, ARCUSTOMER.DeliveryTerms, ARCUSTOMER.TransactionNature, ARCUSTOMER.DeliveryTermsC, ARCUSTOMER.TransactionNatureC, ARCUSTOMER.RouteCode FROM ARCUSTOMER";
            where = " Where 1=1 ";
            selectedCustomer = "";
            selectedSecondaryKey = "";

            if (checkedListBox1.CheckedItems.Count > 0) //if query filter selected 
            {
                for (int x = 0; x < checkedListBox1.CheckedItems.Count; x++) //loop through checked filters
                {
                    
                    switch (checkedListBox1.CheckedItems[x].ToString()) //append to where conditions
                    {
                        case "Customer":
                            where = where + " and Customer Like '%" + txtSearchPattern.Text + "%' ";
                            break;
                        case "Customer name":
                            where = where + " and name Like '%" + txtSearchPattern.Text + "%' ";
                            break;
                        case "Short name":
                            where = where + " and ShortName Like '%" + txtSearchPattern.Text + "%' ";
                            break;
                        case "Sold to address":
                            where = where + " and (SoldToAddr1 Like '%" + txtSearchPattern.Text + "%' or SoldToAddr2 Like '%" + txtSearchPattern.Text + "%' or SoldToAddr3 Like '%" + txtSearchPattern.Text + "%' or SoldToAddr4 Like '%" + txtSearchPattern.Text + "%' or SoldToAddr5 Like '%" + txtSearchPattern.Text + "%') ";
                            break;
                        case "Postal/zip code":
                            where = where + " and SoldPostalCode Like '%" + txtSearchPattern.Text + "%' ";
                            break;
                        case "Telephone":
                            where = where + " and Telephone Like '%" + txtSearchPattern.Text + "%' ";
                            break;
                        default:
                            MessageBox.Show("a checked item in checkedListBox1 does not match any of the cases in switch");
                            break;
                    }

                    //MessageBox.Show(where);
                   

                }

                //make select variable the entire select statement by combining the where criterian and sort order
                select = select + " " + where + " " + order;

            }
            
            //get the customer data based on criteria (if no search conditions, default will pull all from customer table)

            conn = new SqlConnection(connectionString); // Your Connection String here
            da = new SqlDataAdapter(select, conn);
            cb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds);

            //fill the data grid view with the results
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            //select the first customer automatically (makes the second datagrid view populate upon choosing combo box item without having to click into the first datagrid view first)
            if (dataGridView1.Rows.Count > 0)  //if there are rows, select the first one
            {
                selectedCustomer = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); //note: datagrid view selection mode is select entire row
            } else
            {
                selectedCustomer = "";
            }

            getCustomerSecondaryQuery(); //get secondary info for selected customer

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //selects the customer number of the selected row, and if there is a secondary query chosen, updates datagrid 2 for the selected customer
        {
            dataGridView3.DataSource = null; //clear tertiary table if there is data still in it

            if (dataGridView1.Rows.Count > 0)//if rows, select first
            {
                selectedCustomer = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
            else
            {
                selectedCustomer = "";
            }
            //MessageBox.Show(selectedCustomer);

            getCustomerSecondaryQuery(); //get secondary info for selected customer

        }//end sub

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e) //selects the customer number of the selected row, and if there is a secondary query chosen, updates datagrid 2 to the selected customer
        {
            if (dataGridView2.Rows.Count > 0) //if there are rows, select the first
            {
                if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Invoices")
                {
                    selectedSecondaryKey = dataGridView2.SelectedRows[0].Cells[1].Value.ToString(); //the invoice number is col2
                }
                if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Sales Orders")
                {
                    selectedSecondaryKey = dataGridView2.SelectedRows[0].Cells[0].Value.ToString(); //the sales order number is col1
                }

            }
            else
            {
                selectedSecondaryKey = "";
            }

            getCustomerTertiaryQuery(); //get data for the third datagridview, which changes based on selection in datagrid 1 and 2 (mostly 2)

        }//end sub

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //select secondary query type
        {
            if (dataGridView1.Rows.Count > 0) //if there are rows, select the first
            {
                selectedCustomer = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
            else
            {
                selectedCustomer = "";
            }

            getCustomerSecondaryQuery(); //update secondary query type
        }



        private void getCustomerSecondaryQuery()
        {
            if (selectedCustomer != "") //get secondary data grid view results
            {

                switch (comboBox1.SelectedItem) //choose what the secondary query is (all from table x where customer = selected customer and/or foreignkey = selectedsecondarykey)
                {
                    case "Invoices":
                        select = "SELECT * FROM ARINVOICE";
                        break;
                    case "Movements":
                        select = "SELECT * FROM ARSALESMOVE"; 
                        break;
                    case "Payments":
                        select = "SELECT * FROM ARINVOICEPAY";
                        break;
                    case "Work in Progress":
                        MessageBox.Show("Work in progress module has never been used, not implemented");
                        break;
                    case "RMA":
                        MessageBox.Show("RMA module has never been used, not implemented");
                        break;
                    case "Sales Orders":
                        select = "SELECT * FROM SORMASTER";
                        break;
                    case "Quotations":
                        MessageBox.Show("Quotations module has never been used, not implemented");
                        break;
                    case "Master/Sub Accounts":
                        select = "SELECT * FROM ARMASTERSUB";
                        break;
                    case "Stock Codes":
                        select = "SELECT * FROM ARCUSTSTKXREF";
                        break;
                    case "Stock Code X-ref":
                        select = "SELECT * FROM ARCSTSTKPRC";
                        break;
                    default:

                        break;
                }

                where = " Where Customer = '" + selectedCustomer + "'";
                select = select + " " + where;

                //if type of secondary query selected and not null (and implemented) fill the secondary data grid view
                if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() != "Work in Progress" && comboBox1.SelectedItem.ToString() != "RMA" && comboBox1.SelectedItem.ToString() != "Quotations")
                {
                    conn = new SqlConnection(connectionString); // Your Connection String here
                    da = new SqlDataAdapter(select, conn);
                    cb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds);

                    dataGridView2.ReadOnly = true;
                    dataGridView2.DataSource = ds.Tables[0];
                }

                //select specific record in datagrid2 by getting the primary key of the second tabl

                //select the first row in secondary query if there are records
                if (dataGridView2.Rows.Count > 0) //if there are rows, select the first
                {
                    if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Invoices")
                    {
                        selectedSecondaryKey = dataGridView2.SelectedRows[0].Cells[1].Value.ToString(); //the invoice number is col2
                    }
                    if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Sales Orders")
                    {
                        selectedSecondaryKey = dataGridView2.SelectedRows[0].Cells[0].Value.ToString(); //the sales order number is col1
                    }

                    getCustomerTertiaryQuery();

                }
                else
                {
                    selectedSecondaryKey = "";
                }

               

            }//end if
        }

        private void getCustomerTertiaryQuery() //gets extra details for the secondary queries if they normally have 2 datagridview in syspro
        {
            //invoices and sales orders require a third datagridview
            if (comboBox1.SelectedItem != null && (comboBox1.SelectedItem.ToString() == "Invoices" || comboBox1.SelectedItem.ToString() == "Sales Orders")) 
            {
                if (selectedSecondaryKey != "") //get key of selected record in datagridview2
                {
                    if (selectedCustomer != "") //get key of selected record in datagridview1 (a customer number)
                    {
                        //this here in case tertiary query also has a customer foreign key, overwrite where in case below if there isn't a customer field in tertiary level
                        where = " Where Customer = '" + selectedCustomer + "'";
                    }
                    

                    //get the select statement for tertiary datagridview, not all combo options require 3 datagrid views
                    switch (comboBox1.SelectedItem) //build the select query for the third datagridview based on what is selected in datagridview 2 (and 1)
                    {
                        case "Invoices":
                            select = "SELECT * FROM ARINVOICEPAY ";
                            if (selectedCustomer != "")
                            {
                                where = where + " and Invoice = '" + selectedSecondaryKey + "' ";
                            } else
                            {
                                where ="Where Invoice = '" + selectedSecondaryKey + "' ";
                            }
                                
                            break;
                       
                        //case "Payments":
                            //select = "SELECT * FROM ARINVOICEPAY";
                            //break;
                        
                        case "Sales Orders":
                            select = "SELECT * FROM SORDETAIL ";
                            where = " Where SalesOrder = '" + selectedSecondaryKey + "' ";
                            break;
                        
                        default:

                            break;
                    }
                    

                    select = select + " " + where;

                    //if type of secondary query selected and not null (and tertiary view implemented), and secondary key selected, fill the tertiary data grid view
                    if (comboBox1.SelectedItem != null && (comboBox1.SelectedItem.ToString() == "Invoices" || comboBox1.SelectedItem.ToString() == "Sales Orders")) //get the tertiary querys if applicable
                    {
                        conn = new SqlConnection(connectionString); // Your Connection String here
                        da = new SqlDataAdapter(select, conn);
                        cb = new SqlCommandBuilder(da);
                        ds = new DataSet();
                        da.Fill(ds);

                        dataGridView3.ReadOnly = true;
                        dataGridView3.DataSource = ds.Tables[0];
                    }

                    

                }//end if
            }
            else //show no data, there is no tertiary query
            {
                dataGridView3.DataSource = null;
            }
        }

        
    }
}

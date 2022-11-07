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
using System.Configuration;
using System.Xml.Linq;

namespace Disconnected_Archi_Product
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommandBuilder cmB;
        SqlDataAdapter adapter;
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            conn = new SqlConnection(constr);
        }
        public DataSet GetAllProduct()
        {
            adapter = new SqlDataAdapter("select *from Product", conn);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            cmB = new SqlCommandBuilder(adapter);
            ds = new DataSet();
            adapter.Fill(ds, "product");
            return ds;
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProduct();
                DataRow row = ds.Tables["product"].NewRow();
                row["name"] = txtname.Text;
                row["price"] = txtprice.Text;
                row["cname"]=txtcname.Text;
                ds.Tables["product"].Rows.Add(row);
                int result = adapter.Update(ds.Tables["product"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                ds = GetAllProduct();
                DataRow row = ds.Tables["product"].Rows.Find(txtid.Text);
                if (row != null)
                {
                    row["name"] = txtname.Text;
                    row["price"] = txtprice.Text;
                    row["cname"]=txtcname.Text;
                    int result = adapter.Update(ds.Tables["product"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record updated");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProduct();
                DataRow row = ds.Tables["product"].Rows.Find(txtid.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = adapter.Update(ds.Tables["product"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record deleted");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {

            try
            {
                ds = GetAllProduct();
                dataGridView1.DataSource = ds.Tables["product"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

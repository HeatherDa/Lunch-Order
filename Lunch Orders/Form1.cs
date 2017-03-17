using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunch_Orders
{
    public partial class Form1 : Form
    {
        List<String> orders = new List<String>();
        Decimal ordersSubtotal = 0;
        public Form1()
        {
            InitializeComponent();

        }

        private void DisplayOptions(object sender, EventArgs e)
        {
            if (rdoHamburger.Checked)//display Add-ons for Hamburger
            {
                grpOptions.Text = "Add-on items ($.75/each)";
                chk1.Text = "Lettuce, tomato, and onions";
                chk2.Text = "Ketchup, mustard, and mayo";
                chk3.Text = "French Fries";
                chk1.Checked = false;
                chk2.Checked = false;
                chk3.Checked = false;
                chk1.Visible = true;
                chk2.Visible = true;
                chk3.Visible = true;
            }
            else if (rdoPizza.Checked) //display Add-ons for pizza 
            {
                grpOptions.Text = "Add-on items ($.50/each)";
                chk1.Text = "Pepperoni";
                chk2.Text = "Sausage";
                chk3.Text = "Olives";
                chk1.Checked = false;
                chk2.Checked = false;
                chk3.Checked = false;
                chk1.Visible = true;
                chk2.Visible = true;
                chk3.Visible = true;
            }
            else if (rdoSalad.Checked)//display Add-ons for Salad 
            {                
                grpOptions.Text = "Add-on items ($.25/each)";
                chk1.Text = "Croutons";
                chk2.Text = "Bacon bits";
                chk3.Text = "Bread sticks";
                chk1.Checked = false;
                chk2.Checked = false;
                chk3.Checked = false;
                chk1.Visible = true;
                chk2.Visible = true;
                chk3.Visible = true;
            }
            
        }

        private void btnOrder_Click(object sender, EventArgs e)//add string describing order to the listbox (how can removing it from the listbox lead to removing it from the order?)
        {
            String orderString = "";
            //build string describing order
            //add main menu item
            if (rdoHamburger.Checked) orderString = "Hamburger ";
            else if (rdoPizza.Checked) orderString = "Pizza ";
            else orderString = "Salad ";

            //add add-on items to string
            if (chk1.Checked) orderString = orderString + "; " + chk1.Text;
            if (chk2.Checked) orderString = orderString + "; " + chk2.Text;
            if (chk3.Checked) orderString = orderString + "; " + chk3.Text;

            
            Decimal orderTotal = CalculateOrderTotal(orderString);//find our total for this order
            ordersSubtotal = ordersSubtotal+orderTotal;//all orders placed so far
            orderString = orderString + " " + orderTotal;//add total for this order to the end of the order string
            orders.Add(orderString);//keep order strings in a list so the order can be edited

            displayOrders();
        }

        private decimal CalculateOrderTotal(String orderString)
        {
            string[] orderArray = orderString.Split(';');//create array of order items
            decimal orderTotal = 0;
            if (rdoHamburger.Checked) orderTotal = 6.95m + (Convert.ToDecimal(orderArray.Length-1)*0.75m);//-1 to remove main menu item
            
            else if (rdoPizza.Checked) orderTotal = 5.95m + (Convert.ToDecimal(orderArray.Length-1) * 0.50m);//-1 to remove main menu item

            else orderTotal = 4.95m + (Convert.ToDecimal(orderArray.Length-1) * 0.25m);//-1 to remove main menu item

            return orderTotal;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ordersSubtotal=0;
            orders.Clear();
            lstOrders.Items.Clear();
            chk1.Checked = false;
            chk2.Checked = false;
            chk3.Checked = false;
            txtSubtotal.Clear();
            txtTax.Clear();
            txtTotal.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void displayOrders()
        {
            lstOrders.Items.Clear();//empty box
            foreach (String order in orders)//fill box with all orders submitted so far
            {
                lstOrders.Items.Add(order);
            }
            txtSubtotal.Text = ordersSubtotal.ToString("c");//display subtotal
            txtTax.Text = (ordersSubtotal * .0775m).ToString("c");//display tax
            txtTotal.Text = (ordersSubtotal + (ordersSubtotal * .0775m)).ToString("c");
        }
    }
}

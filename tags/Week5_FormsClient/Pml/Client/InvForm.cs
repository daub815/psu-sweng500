using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sweng500.Pml.DataAccessLayer;


namespace Sweng500.Pml.Client
{
    public partial class InvForm : Form
    {
        public InvForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



 

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void displayInvToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeInv_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dispInv_Click(object sender, EventArgs e)
        {
            listInv.Items.Clear();
            // Get the Crud Service via Dependency Injection
            var service = Repository.Instance.ServiceLocator.GetInstance<ICrudService>();
            // Get a list of books
            var books = service.GetBooks();
            foreach (var aBook in books)
            {
                ListViewItem item = new ListViewItem(aBook.Id.ToString());
                item.SubItems.Add(aBook.Author);
                item.SubItems.Add(aBook.Title);

                item.SubItems.Add(aBook.DateAdded.ToString());
                listInv.Items.Add(item);
            }


        }

        private void listInv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

 
    }
}

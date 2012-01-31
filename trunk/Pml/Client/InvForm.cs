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
            // Get the Crud Service via Dependency Injection
            var service = Repository.Instance.ServiceLocator.GetInstance<ICrudService>();
            // Get a list of books
            var books = service.GetBooks();
            foreach (var aBook in books)
            {
                ListViewItem item = new ListViewItem(aBook.Id.ToString());
                item.SubItems.AddRange(new string[] { aBook.Author, aBook.Title, aBook.DateAdded.ToShortDateString() });
                listInv.Items.Add(item);
            }
           
            int size = books.Count();
        }

        private void listInv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

 
    }
}

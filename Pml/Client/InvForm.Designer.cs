namespace Sweng500.Pml.Client
{
    partial class InvForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.closeInv = new System.Windows.Forms.Button();
            this.dispInv = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayInvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchBooksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchDvdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listInv = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeInv
            // 
            this.closeInv.Location = new System.Drawing.Point(392, 281);
            this.closeInv.Name = "closeInv";
            this.closeInv.Size = new System.Drawing.Size(75, 23);
            this.closeInv.TabIndex = 0;
            this.closeInv.Text = "Close";
            this.closeInv.UseVisualStyleBackColor = true;
            this.closeInv.Click += new System.EventHandler(this.closeInv_Click);
            // 
            // dispInv
            // 
            this.dispInv.Location = new System.Drawing.Point(61, 281);
            this.dispInv.Name = "dispInv";
            this.dispInv.Size = new System.Drawing.Size(75, 23);
            this.dispInv.TabIndex = 1;
            this.dispInv.Text = "Display";
            this.dispInv.UseVisualStyleBackColor = true;
            this.dispInv.Click += new System.EventHandler(this.dispInv_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inventoryToolStripMenuItem,
            this.searchToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(487, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // inventoryToolStripMenuItem
            // 
            this.inventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayInvToolStripMenuItem});
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            this.inventoryToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.inventoryToolStripMenuItem.Text = "Inventory";
            this.inventoryToolStripMenuItem.Click += new System.EventHandler(this.inventoryToolStripMenuItem_Click);
            // 
            // displayInvToolStripMenuItem
            // 
            this.displayInvToolStripMenuItem.Name = "displayInvToolStripMenuItem";
            this.displayInvToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.displayInvToolStripMenuItem.Text = "Display";
            this.displayInvToolStripMenuItem.Click += new System.EventHandler(this.displayInvToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchBooksToolStripMenuItem,
            this.searchDvdToolStripMenuItem});
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.searchToolStripMenuItem.Text = "Search";
            // 
            // searchBooksToolStripMenuItem
            // 
            this.searchBooksToolStripMenuItem.Name = "searchBooksToolStripMenuItem";
            this.searchBooksToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.searchBooksToolStripMenuItem.Text = "Books";
            // 
            // searchDvdToolStripMenuItem
            // 
            this.searchDvdToolStripMenuItem.Name = "searchDvdToolStripMenuItem";
            this.searchDvdToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.searchDvdToolStripMenuItem.Text = "Dvd";
            // 
            // listInv
            // 
            this.listInv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.Author,
            this.Title,
            this.Date});
            this.listInv.FullRowSelect = true;
            this.listInv.GridLines = true;
            this.listInv.Location = new System.Drawing.Point(12, 51);
            this.listInv.Name = "listInv";
            this.listInv.Size = new System.Drawing.Size(431, 165);
            this.listInv.TabIndex = 3;
            this.listInv.UseCompatibleStateImageBehavior = false;
            this.listInv.View = System.Windows.Forms.View.Details;
            this.listInv.SelectedIndexChanged += new System.EventHandler(this.listInv_SelectedIndexChanged);
            // 
            // Id
            // 
            this.Id.Text = "Id";
            // 
            // Author
            // 
            this.Author.Text = "Author";
            this.Author.Width = 100;
            // 
            // Title
            // 
            this.Title.Text = "Title";
            // 
            // Date
            // 
            this.Date.Text = "Date";
            // 
            // InvForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 325);
            this.Controls.Add(this.listInv);
            this.Controls.Add(this.dispInv);
            this.Controls.Add(this.closeInv);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "InvForm";
            this.Text = "Inventory";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeInv;
        private System.Windows.Forms.Button dispInv;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem inventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayInvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchBooksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchDvdToolStripMenuItem;
        private System.Windows.Forms.ListView listInv;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader Author;
        private System.Windows.Forms.ColumnHeader Title;
        private System.Windows.Forms.ColumnHeader Date;
    }
}


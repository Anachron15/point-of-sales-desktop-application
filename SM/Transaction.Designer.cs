namespace SM
{
    partial class Transaction
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lvTransaction = new System.Windows.Forms.ListView();
            this.transactionId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.amountReceived = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.change = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.newchange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sub = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tranDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tranTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Remarks = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Customer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cashier = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDateModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTimeModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.pbDate = new System.Windows.Forms.PictureBox();
            this.lvProducts = new System.Windows.Forms.ListView();
            this.Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.unitcost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.subTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sku = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.transid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.prodid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblTransactionIndex = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.btnCancelTransaction = new System.Windows.Forms.Button();
            this.lblProductRowIndex = new System.Windows.Forms.Label();
            this.calTransactionDate = new System.Windows.Forms.MonthCalendar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(713, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 10;
            this.label3.Text = "Transaction";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(7, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "South Mall Inc.";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Teal;
            this.panel5.Controls.Add(this.lvTransaction);
            this.panel5.Location = new System.Drawing.Point(1, 39);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1368, 707);
            this.panel5.TabIndex = 4;
            // 
            // lvTransaction
            // 
            this.lvTransaction.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvTransaction.BackColor = System.Drawing.Color.LightSeaGreen;
            this.lvTransaction.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.transactionId,
            this.amountReceived,
            this.change,
            this.newchange,
            this.sub,
            this.tranDate,
            this.tranTime,
            this.status,
            this.Remarks,
            this.Customer,
            this.cashier,
            this.colDateModified,
            this.colTimeModified});
            this.lvTransaction.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvTransaction.ForeColor = System.Drawing.Color.Azure;
            this.lvTransaction.FullRowSelect = true;
            this.lvTransaction.GridLines = true;
            this.lvTransaction.HideSelection = false;
            this.lvTransaction.Location = new System.Drawing.Point(12, 45);
            this.lvTransaction.Name = "lvTransaction";
            this.lvTransaction.Size = new System.Drawing.Size(1346, 279);
            this.lvTransaction.TabIndex = 8;
            this.lvTransaction.UseCompatibleStateImageBehavior = false;
            this.lvTransaction.View = System.Windows.Forms.View.Details;
            this.lvTransaction.SelectedIndexChanged += new System.EventHandler(this.lvTransaction_SelectedIndexChanged);
            // 
            // transactionId
            // 
            this.transactionId.Text = "Transaction Id";
            this.transactionId.Width = 306;
            // 
            // amountReceived
            // 
            this.amountReceived.Text = "Amount Received";
            this.amountReceived.Width = 130;
            // 
            // change
            // 
            this.change.Text = "Change";
            this.change.Width = 130;
            // 
            // newchange
            // 
            this.newchange.Text = "New Change";
            this.newchange.Width = 200;
            // 
            // sub
            // 
            this.sub.Text = "Total Income";
            this.sub.Width = 100;
            // 
            // tranDate
            // 
            this.tranDate.Text = "Date";
            this.tranDate.Width = 125;
            // 
            // tranTime
            // 
            this.tranTime.Text = "Time";
            this.tranTime.Width = 100;
            // 
            // status
            // 
            this.status.Text = "Status";
            this.status.Width = 150;
            // 
            // Remarks
            // 
            this.Remarks.Text = "Remarks";
            this.Remarks.Width = 124;
            // 
            // Customer
            // 
            this.Customer.Text = " Customer";
            this.Customer.Width = 250;
            // 
            // cashier
            // 
            this.cashier.Text = "Cashier";
            this.cashier.Width = 250;
            // 
            // colDateModified
            // 
            this.colDateModified.Text = "Date Modified";
            this.colDateModified.Width = 150;
            // 
            // colTimeModified
            // 
            this.colTimeModified.Text = "TimeModified";
            this.colTimeModified.Width = 150;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(1263, 763);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "South Mall Inc.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(660, 763);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 16);
            this.label5.TabIndex = 22;
            this.label5.Text = "Copyright @ 2018";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(14, 763);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 16);
            this.label4.TabIndex = 21;
            this.label4.Text = "All Rights Reseved";
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnDelete.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.Control;
            this.btnDelete.Image = global::SM.Properties.Resources.transaction__5_1;
            this.btnDelete.Location = new System.Drawing.Point(994, 693);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(179, 46);
            this.btnDelete.TabIndex = 31;
            this.btnDelete.Text = "Remove Product";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.AutoSize = true;
            this.btnUpdate.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnUpdate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.SystemColors.Control;
            this.btnUpdate.Image = global::SM.Properties.Resources.transaction__5_1;
            this.btnUpdate.Location = new System.Drawing.Point(1179, 692);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(180, 47);
            this.btnUpdate.TabIndex = 30;
            this.btnUpdate.Text = "Deduct Quantity";
            this.btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.Color.Aqua;
            this.cmbStatus.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Successful",
            "Modified",
            "Cancelled",
            "Processing",
            "Completed"});
            this.cmbStatus.Location = new System.Drawing.Point(731, 46);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(235, 31);
            this.cmbStatus.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Teal;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Azure;
            this.label1.Location = new System.Drawing.Point(619, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 19);
            this.label1.TabIndex = 10;
            this.label1.Text = "Filter By Status";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Teal;
            this.label7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Azure;
            this.label7.Location = new System.Drawing.Point(972, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 19);
            this.label7.TabIndex = 11;
            this.label7.Text = "Filter By Date:";
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.Aqua;
            this.txtDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.Location = new System.Drawing.Point(1095, 46);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(227, 31);
            this.txtDate.TabIndex = 12;
            // 
            // pbDate
            // 
            this.pbDate.Image = global::SM.Properties.Resources.add__1_1;
            this.pbDate.Location = new System.Drawing.Point(1323, 45);
            this.pbDate.Name = "pbDate";
            this.pbDate.Size = new System.Drawing.Size(32, 31);
            this.pbDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDate.TabIndex = 13;
            this.pbDate.TabStop = false;
            this.pbDate.Click += new System.EventHandler(this.pbDate_Click);
            // 
            // lvProducts
            // 
            this.lvProducts.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvProducts.BackColor = System.Drawing.Color.LightSeaGreen;
            this.lvProducts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Name,
            this.Description,
            this.unitcost,
            this.quantity,
            this.subTotal,
            this.sku,
            this.transid,
            this.prodid});
            this.lvProducts.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvProducts.ForeColor = System.Drawing.Color.Azure;
            this.lvProducts.FullRowSelect = true;
            this.lvProducts.GridLines = true;
            this.lvProducts.Location = new System.Drawing.Point(12, 422);
            this.lvProducts.Name = "lvProducts";
            this.lvProducts.Size = new System.Drawing.Size(1344, 268);
            this.lvProducts.TabIndex = 15;
            this.lvProducts.UseCompatibleStateImageBehavior = false;
            this.lvProducts.View = System.Windows.Forms.View.Details;
            this.lvProducts.SelectedIndexChanged += new System.EventHandler(this.lvProducts_SelectedIndexChanged);
            // 
            // Name
            // 
            this.Name.Text = "Name";
            this.Name.Width = 300;
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 400;
            // 
            // unitcost
            // 
            this.unitcost.Text = "Unit Cost";
            this.unitcost.Width = 100;
            // 
            // quantity
            // 
            this.quantity.Text = "Quantity";
            this.quantity.Width = 100;
            // 
            // subTotal
            // 
            this.subTotal.Text = "Sub Total";
            this.subTotal.Width = 150;
            // 
            // sku
            // 
            this.sku.Text = "SKU";
            this.sku.Width = 300;
            // 
            // transid
            // 
            this.transid.Text = "Transaction Id";
            this.transid.Width = 100;
            // 
            // prodid
            // 
            this.prodid.Text = "Product Id";
            this.prodid.Width = 100;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Teal;
            this.label8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Azure;
            this.label8.Location = new System.Drawing.Point(13, 397);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(258, 19);
            this.label8.TabIndex = 16;
            this.label8.Text = "Products ordered in the transaction:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Teal;
            this.label9.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Azure;
            this.label9.Location = new System.Drawing.Point(12, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 19);
            this.label9.TabIndex = 17;
            this.label9.Text = "Transaction:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.BackColor = System.Drawing.Color.Teal;
            this.lblTotal.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Aquamarine;
            this.lblTotal.Location = new System.Drawing.Point(151, 699);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(55, 29);
            this.lblTotal.TabIndex = 34;
            this.lblTotal.Text = "0.00";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Teal;
            this.label16.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Aquamarine;
            this.label16.Location = new System.Drawing.Point(12, 699);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(145, 29);
            this.label16.TabIndex = 35;
            this.label16.Text = "New Change:";
            // 
            // lblTransactionIndex
            // 
            this.lblTransactionIndex.AutoSize = true;
            this.lblTransactionIndex.BackColor = System.Drawing.Color.Teal;
            this.lblTransactionIndex.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionIndex.ForeColor = System.Drawing.Color.Azure;
            this.lblTransactionIndex.Location = new System.Drawing.Point(13, 46);
            this.lblTransactionIndex.Name = "lblTransactionIndex";
            this.lblTransactionIndex.Size = new System.Drawing.Size(17, 19);
            this.lblTransactionIndex.TabIndex = 36;
            this.lblTransactionIndex.Text = "0";
            this.lblTransactionIndex.Visible = false;
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.BackColor = System.Drawing.Color.Teal;
            this.lblWarning.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.ForeColor = System.Drawing.Color.Maroon;
            this.lblWarning.Location = new System.Drawing.Point(315, 397);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(258, 19);
            this.lblWarning.TabIndex = 37;
            this.lblWarning.Text = "Products ordered in the transaction:";
            this.lblWarning.Visible = false;
            // 
            // btnCancelTransaction
            // 
            this.btnCancelTransaction.AutoSize = true;
            this.btnCancelTransaction.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnCancelTransaction.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelTransaction.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCancelTransaction.Image = global::SM.Properties.Resources.transaction__5_1;
            this.btnCancelTransaction.Location = new System.Drawing.Point(1160, 369);
            this.btnCancelTransaction.Name = "btnCancelTransaction";
            this.btnCancelTransaction.Size = new System.Drawing.Size(196, 46);
            this.btnCancelTransaction.TabIndex = 38;
            this.btnCancelTransaction.Text = "Cancel Transaction";
            this.btnCancelTransaction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelTransaction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelTransaction.UseVisualStyleBackColor = false;
            this.btnCancelTransaction.Click += new System.EventHandler(this.btnCancelTransaction_Click);
            // 
            // lblProductRowIndex
            // 
            this.lblProductRowIndex.AutoSize = true;
            this.lblProductRowIndex.BackColor = System.Drawing.Color.Teal;
            this.lblProductRowIndex.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductRowIndex.ForeColor = System.Drawing.Color.Azure;
            this.lblProductRowIndex.Location = new System.Drawing.Point(13, 378);
            this.lblProductRowIndex.Name = "lblProductRowIndex";
            this.lblProductRowIndex.Size = new System.Drawing.Size(17, 19);
            this.lblProductRowIndex.TabIndex = 39;
            this.lblProductRowIndex.Text = "0";
            this.lblProductRowIndex.Visible = false;
            // 
            // calTransactionDate
            // 
            this.calTransactionDate.BackColor = System.Drawing.Color.LightSeaGreen;
            this.calTransactionDate.ForeColor = System.Drawing.Color.Aqua;
            this.calTransactionDate.Location = new System.Drawing.Point(1097, 78);
            this.calTransactionDate.Name = "calTransactionDate";
            this.calTransactionDate.TabIndex = 40;
            this.calTransactionDate.Visible = false;
            this.calTransactionDate.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calTransactionDate_DateSelected);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::SM.Properties.Resources.sign_out_option;
            this.pictureBox1.Location = new System.Drawing.Point(1348, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 14);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Transaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(1370, 788);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.calTransactionDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblProductRowIndex);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancelTransaction);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.lblTransactionIndex);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lvProducts);
            this.Controls.Add(this.pbDate);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Name = "Transaction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transaction";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transaction_Load);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lvTransaction;
        private System.Windows.Forms.ColumnHeader transactionId;
        private System.Windows.Forms.ColumnHeader amountReceived;
        private System.Windows.Forms.ColumnHeader change;
        private System.Windows.Forms.ColumnHeader sub;
        private System.Windows.Forms.ColumnHeader tranDate;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.ColumnHeader Remarks;
        private System.Windows.Forms.ColumnHeader Customer;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.PictureBox pbDate;
        private System.Windows.Forms.ListView lvProducts;
        private System.Windows.Forms.ColumnHeader Name;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.ColumnHeader unitcost;
        private System.Windows.Forms.ColumnHeader quantity;
        private System.Windows.Forms.ColumnHeader subTotal;
        private System.Windows.Forms.ColumnHeader sku;
        private System.Windows.Forms.ColumnHeader transid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ColumnHeader prodid;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ColumnHeader newchange;
        private System.Windows.Forms.ColumnHeader cashier;
        private System.Windows.Forms.Label lblTransactionIndex;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.ColumnHeader tranTime;
        private System.Windows.Forms.Button btnCancelTransaction;
        private System.Windows.Forms.Label lblProductRowIndex;
        private System.Windows.Forms.ColumnHeader colDateModified;
        private System.Windows.Forms.ColumnHeader colTimeModified;
        private System.Windows.Forms.MonthCalendar calTransactionDate;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
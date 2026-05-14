using System.IO.Ports;
using System;
using System.Drawing;

namespace DeviceInventory
{
    partial class DeviceInventoryForm
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
            this.loginPanel = new System.Windows.Forms.Panel();
            this.ServerURLTxt = new System.Windows.Forms.TextBox();
            this.ServerUrlLabel = new System.Windows.Forms.Label();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.PasswordTxt = new System.Windows.Forms.TextBox();
            this.UsernameTxt = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.loader = new System.Windows.Forms.PictureBox();
            this.devicePanel = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.keyTxt = new System.Windows.Forms.TextBox();
            this.keyLabel = new System.Windows.Forms.Label();
            this.proceedButton = new System.Windows.Forms.Button();
            this.TestRadioButton = new System.Windows.Forms.RadioButton();
            this.SerialPortRadioButton = new System.Windows.Forms.RadioButton();
            this.SerialPortCombo = new System.Windows.Forms.ComboBox();
            this.DeviceTypeLabel = new System.Windows.Forms.Label();
            this.DeviceProfileLabel = new System.Windows.Forms.Label();
            this.DeviceTypeCombo = new System.Windows.Forms.ComboBox();
            this.DeviceProfileCombo = new System.Windows.Forms.ComboBox();
            this.TypeLabel = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.DeviceVersionLabel = new System.Windows.Forms.Label();
            this.copyRightLabel = new System.Windows.Forms.Label();
            this.headerLabel = new System.Windows.Forms.Label();
            this.loginPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loader)).BeginInit();
            this.devicePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginPanel
            // 
            this.loginPanel.Controls.Add(this.ServerURLTxt);
            this.loginPanel.Controls.Add(this.ServerUrlLabel);
            this.loginPanel.Controls.Add(this.LoginBtn);
            this.loginPanel.Controls.Add(this.PasswordTxt);
            this.loginPanel.Controls.Add(this.UsernameTxt);
            this.loginPanel.Controls.Add(this.PasswordLabel);
            this.loginPanel.Controls.Add(this.UsernameLabel);
            this.loginPanel.Location = new System.Drawing.Point(0, 86);
            this.loginPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(1511, 562);
            this.loginPanel.TabIndex = 0;
            // 
            // ServerURLTxt
            // 
            this.ServerURLTxt.Location = new System.Drawing.Point(547, 127);
            this.ServerURLTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ServerURLTxt.Name = "ServerURLTxt";
            this.ServerURLTxt.Size = new System.Drawing.Size(342, 22);
            this.ServerURLTxt.TabIndex = 6;
            // 
            // ServerUrlLabel
            // 
            this.ServerUrlLabel.AutoSize = true;
            this.ServerUrlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerUrlLabel.Location = new System.Drawing.Point(444, 127);
            this.ServerUrlLabel.Name = "ServerUrlLabel";
            this.ServerUrlLabel.Size = new System.Drawing.Size(95, 18);
            this.ServerUrlLabel.TabIndex = 5;
            this.ServerUrlLabel.Text = "Server URL";
            // 
            // LoginBtn
            // 
            this.LoginBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.LoginBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LoginBtn.Location = new System.Drawing.Point(604, 275);
            this.LoginBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(80, 30);
            this.LoginBtn.TabIndex = 4;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = false;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // PasswordTxt
            // 
            this.PasswordTxt.Location = new System.Drawing.Point(547, 209);
            this.PasswordTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PasswordTxt.MaxLength = 100;
            this.PasswordTxt.Name = "PasswordTxt";
            this.PasswordTxt.PasswordChar = '*';
            this.PasswordTxt.Size = new System.Drawing.Size(342, 22);
            this.PasswordTxt.TabIndex = 3;
            // 
            // UsernameTxt
            // 
            this.UsernameTxt.Location = new System.Drawing.Point(547, 163);
            this.UsernameTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UsernameTxt.MaxLength = 100;
            this.UsernameTxt.Name = "UsernameTxt";
            this.UsernameTxt.Size = new System.Drawing.Size(342, 22);
            this.UsernameTxt.TabIndex = 2;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.Location = new System.Drawing.Point(444, 206);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(83, 18);
            this.PasswordLabel.TabIndex = 1;
            this.PasswordLabel.Text = "Password";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameLabel.Location = new System.Drawing.Point(444, 163);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(85, 18);
            this.UsernameLabel.TabIndex = 0;
            this.UsernameLabel.Text = "Username";
            // 
            // loader
            // 
            this.loader.BackColor = System.Drawing.SystemColors.HighlightText;
            this.loader.Location = new System.Drawing.Point(547, 163);
            this.loader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loader.Name = "loader";
            this.loader.Size = new System.Drawing.Size(200, 160);
            this.loader.TabIndex = 7;
            this.loader.TabStop = false;
            this.loader.Visible = false;
            // 
            // devicePanel
            // 
            this.devicePanel.Controls.Add(this.loader);
            this.devicePanel.Controls.Add(this.richTextBox1);
            this.devicePanel.Controls.Add(this.keyTxt);
            this.devicePanel.Controls.Add(this.keyLabel);
            this.devicePanel.Controls.Add(this.proceedButton);
            this.devicePanel.Controls.Add(this.TestRadioButton);
            this.devicePanel.Controls.Add(this.SerialPortRadioButton);
            this.devicePanel.Controls.Add(this.SerialPortCombo);
            this.devicePanel.Controls.Add(this.DeviceTypeLabel);
            this.devicePanel.Controls.Add(this.DeviceProfileLabel);
            this.devicePanel.Controls.Add(this.DeviceTypeCombo);
            this.devicePanel.Controls.Add(this.DeviceProfileCombo);
            this.devicePanel.Location = new System.Drawing.Point(0, 86);
            this.devicePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.devicePanel.Name = "devicePanel";
            this.devicePanel.Size = new System.Drawing.Size(1511, 565);
            this.devicePanel.TabIndex = 5;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(6, 63);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(1467, 489);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // keyTxt
            // 
            this.keyTxt.Location = new System.Drawing.Point(324, 34);
            this.keyTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.keyTxt.Name = "keyTxt";
            this.keyTxt.Size = new System.Drawing.Size(202, 22);
            this.keyTxt.TabIndex = 6;
            this.keyTxt.Visible = false;
            // 
            // keyLabel
            // 
            this.keyLabel.AutoSize = true;
            this.keyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyLabel.Location = new System.Drawing.Point(244, 36);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(65, 18);
            this.keyLabel.TabIndex = 5;
            this.keyLabel.Text = "   Port :";
            // 
            // proceedButton
            // 
            this.proceedButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.proceedButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.proceedButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.proceedButton.Location = new System.Drawing.Point(1378, 15);
            this.proceedButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.proceedButton.Name = "proceedButton";
            this.proceedButton.Size = new System.Drawing.Size(116, 45);
            this.proceedButton.TabIndex = 2;
            this.proceedButton.Text = "Create";
            this.proceedButton.UseVisualStyleBackColor = false;
            this.proceedButton.Click += new System.EventHandler(this.proceedButton_Click_1);
            // 
            // TestRadioButton
            // 
            this.TestRadioButton.AutoSize = true;
            this.TestRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestRadioButton.Location = new System.Drawing.Point(49, 34);
            this.TestRadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TestRadioButton.Name = "TestRadioButton";
            this.TestRadioButton.Size = new System.Drawing.Size(62, 22);
            this.TestRadioButton.TabIndex = 8;
            this.TestRadioButton.Text = "Test";
            // 
            // SerialPortRadioButton
            // 
            this.SerialPortRadioButton.AutoSize = true;
            this.SerialPortRadioButton.Checked = true;
            this.SerialPortRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SerialPortRadioButton.Location = new System.Drawing.Point(124, 34);
            this.SerialPortRadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SerialPortRadioButton.Name = "SerialPortRadioButton";
            this.SerialPortRadioButton.Size = new System.Drawing.Size(109, 22);
            this.SerialPortRadioButton.TabIndex = 9;
            this.SerialPortRadioButton.TabStop = true;
            this.SerialPortRadioButton.Text = "Serial Port";
            // 
            // SerialPortCombo
            // 
            this.SerialPortCombo.FormattingEnabled = true;
            this.SerialPortCombo.Location = new System.Drawing.Point(324, 34);
            this.SerialPortCombo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SerialPortCombo.Name = "SerialPortCombo";
            this.SerialPortCombo.Size = new System.Drawing.Size(202, 24);
            this.SerialPortCombo.TabIndex = 0;
            // 
            // DeviceTypeLabel
            // 
            this.DeviceTypeLabel.AutoSize = true;
            this.DeviceTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceTypeLabel.Location = new System.Drawing.Point(556, 36);
            this.DeviceTypeLabel.Name = "DeviceTypeLabel";
            this.DeviceTypeLabel.Size = new System.Drawing.Size(110, 18);
            this.DeviceTypeLabel.TabIndex = 10;
            this.DeviceTypeLabel.Text = "Device Type :";
            // 
            // DeviceProfileLabel
            // 
            this.DeviceProfileLabel.AutoSize = true;
            this.DeviceProfileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceProfileLabel.Location = new System.Drawing.Point(942, 36);
            this.DeviceProfileLabel.Name = "DeviceProfileLabel";
            this.DeviceProfileLabel.Size = new System.Drawing.Size(123, 18);
            this.DeviceProfileLabel.TabIndex = 11;
            this.DeviceProfileLabel.Text = "Device Profile :";
            // 
            // DeviceTypeCombo
            // 
            this.DeviceTypeCombo.FormattingEnabled = true;
            this.DeviceTypeCombo.Location = new System.Drawing.Point(711, 34);
            this.DeviceTypeCombo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeviceTypeCombo.Name = "DeviceTypeCombo";
            this.DeviceTypeCombo.Size = new System.Drawing.Size(223, 24);
            this.DeviceTypeCombo.TabIndex = 8;
            this.DeviceTypeCombo.SelectedValueChanged += new System.EventHandler(this.deviceComboValueChanged);
            // 
            // DeviceProfileCombo
            // 
            this.DeviceProfileCombo.FormattingEnabled = true;
            this.DeviceProfileCombo.Location = new System.Drawing.Point(1111, 34);
            this.DeviceProfileCombo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeviceProfileCombo.Name = "DeviceProfileCombo";
            this.DeviceProfileCombo.Size = new System.Drawing.Size(223, 24);
            this.DeviceProfileCombo.TabIndex = 12;
            // 
            // TypeLabel
            // 
            this.TypeLabel.AutoSize = true;
            this.TypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeLabel.Location = new System.Drawing.Point(8, 40);
            this.TypeLabel.Name = "TypeLabel";
            this.TypeLabel.Size = new System.Drawing.Size(55, 22);
            this.TypeLabel.TabIndex = 1;
            this.TypeLabel.Text = "Type";
            // 
            // typeComboBox
            // 
            this.typeComboBox.Location = new System.Drawing.Point(0, 0);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(121, 24);
            this.typeComboBox.TabIndex = 0;
            // 
            // DeviceVersionLabel
            // 
            this.DeviceVersionLabel.AutoSize = true;
            this.DeviceVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceVersionLabel.Location = new System.Drawing.Point(11, 682);
            this.DeviceVersionLabel.Name = "DeviceVersionLabel";
            this.DeviceVersionLabel.Size = new System.Drawing.Size(167, 17);
            this.DeviceVersionLabel.TabIndex = 5;
            this.DeviceVersionLabel.Text = "Device Manager 1.1.2";
            // 
            // copyRightLabel
            // 
            this.copyRightLabel.AutoSize = true;
            this.copyRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyRightLabel.Location = new System.Drawing.Point(1067, 682);
            this.copyRightLabel.Name = "copyRightLabel";
            this.copyRightLabel.Size = new System.Drawing.Size(387, 17);
            this.copyRightLabel.TabIndex = 6;
            this.copyRightLabel.Text = "Copyright 2024 Asset Sence, Inc. All rights reserved";
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabel.Location = new System.Drawing.Point(492, 26);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(410, 50);
            this.headerLabel.TabIndex = 7;
            this.headerLabel.Text = "Device Inventory(SP)";
            // 
            // DeviceInventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1556, 840);
            this.Controls.Add(this.headerLabel);
            this.Controls.Add(this.copyRightLabel);
            this.Controls.Add(this.DeviceVersionLabel);
            this.Controls.Add(this.loginPanel);
            this.Controls.Add(this.devicePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "DeviceInventoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DI-SP";
            this.Load += new System.EventHandler(this.DeviceInventoryForm_Load);
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loader)).EndInit();
            this.devicePanel.ResumeLayout(false);
            this.devicePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       



        #endregion

        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.TextBox PasswordTxt;
        private System.Windows.Forms.TextBox UsernameTxt;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Panel devicePanel;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Button proceedButton;
        private System.Windows.Forms.Label TypeLabel;
        private System.Windows.Forms.TextBox keyTxt;
        private System.Windows.Forms.Label keyLabel;
        private System.Windows.Forms.Label DeviceVersionLabel;
        private System.Windows.Forms.Label copyRightLabel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.TextBox ServerURLTxt;
        private System.Windows.Forms.Label ServerUrlLabel;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox loader;
        private System.Windows.Forms.ComboBox SerialPortCombo;
        private System.Windows.Forms.RadioButton TestRadioButton;
        private System.Windows.Forms.RadioButton SerialPortRadioButton;
        private System.Windows.Forms.Label DeviceTypeLabel;
        private System.Windows.Forms.Label DeviceProfileLabel;
        private System.Windows.Forms.ComboBox DeviceTypeCombo;
        private System.Windows.Forms.ComboBox DeviceProfileCombo;
    }
}


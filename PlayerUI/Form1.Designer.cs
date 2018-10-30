namespace PlayerUI
{
    partial class Form1
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
            this.RoomSelect = new System.Windows.Forms.Panel();
            this.GamePickPanel = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ConnectFourPanel = new System.Windows.Forms.Panel();
            this.SendBox = new System.Windows.Forms.TextBox();
            this.ChatMessageList = new System.Windows.Forms.Label();
            this.NameEnterPanel = new System.Windows.Forms.Panel();
            this.GameSelectPanel = new System.Windows.Forms.Panel();
            this.RoomSelectPanel2 = new System.Windows.Forms.Panel();
            this.RoomListBox2 = new System.Windows.Forms.ListBox();
            this.ConnectFourSelectButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.SendUserNameButton = new System.Windows.Forms.Button();
            this.GamePickPanel2 = new System.Windows.Forms.Panel();
            this.RoomSelect.SuspendLayout();
            this.GamePickPanel.SuspendLayout();
            this.ConnectFourPanel.SuspendLayout();
            this.NameEnterPanel.SuspendLayout();
            this.GameSelectPanel.SuspendLayout();
            this.RoomSelectPanel2.SuspendLayout();
            this.GamePickPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // RoomSelect
            // 
            this.RoomSelect.Controls.Add(this.GamePickPanel);
            this.RoomSelect.Controls.Add(this.listBox1);
            this.RoomSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoomSelect.Location = new System.Drawing.Point(0, 0);
            this.RoomSelect.Name = "RoomSelect";
            this.RoomSelect.Size = new System.Drawing.Size(898, 600);
            this.RoomSelect.TabIndex = 0;
            // 
            // GamePickPanel
            // 
            this.GamePickPanel.Controls.Add(this.ConnectFourPanel);
            this.GamePickPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GamePickPanel.Location = new System.Drawing.Point(0, 0);
            this.GamePickPanel.Name = "GamePickPanel";
            this.GamePickPanel.Size = new System.Drawing.Size(898, 600);
            this.GamePickPanel.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(898, 600);
            this.listBox1.TabIndex = 0;
            // 
            // ConnectFourPanel
            // 
            this.ConnectFourPanel.Controls.Add(this.GamePickPanel2);
            this.ConnectFourPanel.Controls.Add(this.SendBox);
            this.ConnectFourPanel.Controls.Add(this.ChatMessageList);
            this.ConnectFourPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectFourPanel.Location = new System.Drawing.Point(0, 0);
            this.ConnectFourPanel.Name = "ConnectFourPanel";
            this.ConnectFourPanel.Size = new System.Drawing.Size(898, 600);
            this.ConnectFourPanel.TabIndex = 6;
            // 
            // SendBox
            // 
            this.SendBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.SendBox.Location = new System.Drawing.Point(644, 559);
            this.SendBox.Name = "SendBox";
            this.SendBox.Size = new System.Drawing.Size(254, 38);
            this.SendBox.TabIndex = 1;
            // 
            // ChatMessageList
            // 
            this.ChatMessageList.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChatMessageList.Location = new System.Drawing.Point(644, 0);
            this.ChatMessageList.Name = "ChatMessageList";
            this.ChatMessageList.Size = new System.Drawing.Size(254, 556);
            this.ChatMessageList.TabIndex = 0;
            // 
            // NameEnterPanel
            // 
            this.NameEnterPanel.Controls.Add(this.GameSelectPanel);
            this.NameEnterPanel.Controls.Add(this.SendUserNameButton);
            this.NameEnterPanel.Controls.Add(this.UserNameTextBox);
            this.NameEnterPanel.Location = new System.Drawing.Point(0, 0);
            this.NameEnterPanel.Name = "NameEnterPanel";
            this.NameEnterPanel.Size = new System.Drawing.Size(898, 600);
            this.NameEnterPanel.TabIndex = 6;
            // 
            // GameSelectPanel
            // 
            this.GameSelectPanel.Controls.Add(this.RoomSelectPanel2);
            this.GameSelectPanel.Controls.Add(this.button3);
            this.GameSelectPanel.Controls.Add(this.button2);
            this.GameSelectPanel.Controls.Add(this.ConnectFourSelectButton);
            this.GameSelectPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameSelectPanel.Location = new System.Drawing.Point(0, 0);
            this.GameSelectPanel.Name = "GameSelectPanel";
            this.GameSelectPanel.Size = new System.Drawing.Size(898, 600);
            this.GameSelectPanel.TabIndex = 2;
            // 
            // RoomSelectPanel2
            // 
            this.RoomSelectPanel2.Controls.Add(this.RoomListBox2);
            this.RoomSelectPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoomSelectPanel2.Location = new System.Drawing.Point(0, 0);
            this.RoomSelectPanel2.Name = "RoomSelectPanel2";
            this.RoomSelectPanel2.Size = new System.Drawing.Size(898, 600);
            this.RoomSelectPanel2.TabIndex = 3;
            // 
            // RoomListBox2
            // 
            this.RoomListBox2.FormattingEnabled = true;
            this.RoomListBox2.Location = new System.Drawing.Point(22, 25);
            this.RoomListBox2.Name = "RoomListBox2";
            this.RoomListBox2.Size = new System.Drawing.Size(851, 524);
            this.RoomListBox2.TabIndex = 0;
            // 
            // ConnectFourSelectButton
            // 
            this.ConnectFourSelectButton.Location = new System.Drawing.Point(22, 25);
            this.ConnectFourSelectButton.Name = "ConnectFourSelectButton";
            this.ConnectFourSelectButton.Size = new System.Drawing.Size(250, 200);
            this.ConnectFourSelectButton.TabIndex = 0;
            this.ConnectFourSelectButton.Text = "button1";
            this.ConnectFourSelectButton.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(316, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(250, 200);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(623, 25);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(250, 200);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.UserNameTextBox.Location = new System.Drawing.Point(198, 297);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(396, 38);
            this.UserNameTextBox.TabIndex = 0;
            // 
            // SendUserNameButton
            // 
            this.SendUserNameButton.Location = new System.Drawing.Point(608, 297);
            this.SendUserNameButton.Name = "SendUserNameButton";
            this.SendUserNameButton.Size = new System.Drawing.Size(122, 37);
            this.SendUserNameButton.TabIndex = 1;
            this.SendUserNameButton.Text = "Login";
            this.SendUserNameButton.UseVisualStyleBackColor = true;
            // 
            // GamePickPanel2
            // 
            this.GamePickPanel2.Controls.Add(this.NameEnterPanel);
            this.GamePickPanel2.Location = new System.Drawing.Point(0, 0);
            this.GamePickPanel2.Name = "GamePickPanel2";
            this.GamePickPanel2.Size = new System.Drawing.Size(898, 600);
            this.GamePickPanel2.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 600);
            this.Controls.Add(this.RoomSelect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.RoomSelect.ResumeLayout(false);
            this.GamePickPanel.ResumeLayout(false);
            this.ConnectFourPanel.ResumeLayout(false);
            this.ConnectFourPanel.PerformLayout();
            this.NameEnterPanel.ResumeLayout(false);
            this.NameEnterPanel.PerformLayout();
            this.GameSelectPanel.ResumeLayout(false);
            this.RoomSelectPanel2.ResumeLayout(false);
            this.GamePickPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel RoomSelect;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel GamePickPanel;
        private System.Windows.Forms.Panel ConnectFourPanel;
        private System.Windows.Forms.TextBox SendBox;
        private System.Windows.Forms.Label ChatMessageList;
        private System.Windows.Forms.Panel GamePickPanel2;
        private System.Windows.Forms.Panel NameEnterPanel;
        private System.Windows.Forms.Button SendUserNameButton;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.Panel GameSelectPanel;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button ConnectFourSelectButton;
        private System.Windows.Forms.Panel RoomSelectPanel2;
        private System.Windows.Forms.ListBox RoomListBox2;
    }
}


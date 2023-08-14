namespace ClientConnectFour
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
            this.BTN_Login = new System.Windows.Forms.Button();
            this.INPUT_Login = new System.Windows.Forms.TextBox();
            this.LBL_Login = new System.Windows.Forms.Label();
            this.BTN_Register_Forward = new System.Windows.Forms.Button();
            this.BTN_Logout = new System.Windows.Forms.Button();
            this.PIC_Board = new System.Windows.Forms.PictureBox();
            this.BTN_StartGame = new System.Windows.Forms.Button();
            this.COMBO_Games = new System.Windows.Forms.ComboBox();
            this.LBL_GamesTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_Board)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN_Login
            // 
            this.BTN_Login.Location = new System.Drawing.Point(177, 224);
            this.BTN_Login.Name = "BTN_Login";
            this.BTN_Login.Size = new System.Drawing.Size(100, 23);
            this.BTN_Login.TabIndex = 0;
            this.BTN_Login.Text = "Login";
            this.BTN_Login.UseVisualStyleBackColor = true;
            this.BTN_Login.Click += new System.EventHandler(this.BTN_Login_Click);
            // 
            // INPUT_Login
            // 
            this.INPUT_Login.Location = new System.Drawing.Point(177, 198);
            this.INPUT_Login.Name = "INPUT_Login";
            this.INPUT_Login.Size = new System.Drawing.Size(100, 20);
            this.INPUT_Login.TabIndex = 1;
            // 
            // LBL_Login
            // 
            this.LBL_Login.AutoSize = true;
            this.LBL_Login.Location = new System.Drawing.Point(185, 182);
            this.LBL_Login.Name = "LBL_Login";
            this.LBL_Login.Size = new System.Drawing.Size(83, 13);
            this.LBL_Login.TabIndex = 2;
            this.LBL_Login.Text = "Enter ID to login";
            // 
            // BTN_Register_Forward
            // 
            this.BTN_Register_Forward.Location = new System.Drawing.Point(300, 416);
            this.BTN_Register_Forward.Name = "BTN_Register_Forward";
            this.BTN_Register_Forward.Size = new System.Drawing.Size(140, 23);
            this.BTN_Register_Forward.TabIndex = 3;
            this.BTN_Register_Forward.Text = "Forward to register form";
            this.BTN_Register_Forward.UseVisualStyleBackColor = true;
            this.BTN_Register_Forward.Click += new System.EventHandler(this.BTN_Register_Forward_Click);
            // 
            // BTN_Logout
            // 
            this.BTN_Logout.Location = new System.Drawing.Point(12, 416);
            this.BTN_Logout.Name = "BTN_Logout";
            this.BTN_Logout.Size = new System.Drawing.Size(75, 23);
            this.BTN_Logout.TabIndex = 4;
            this.BTN_Logout.Text = "Logout";
            this.BTN_Logout.UseVisualStyleBackColor = true;
            this.BTN_Logout.Visible = false;
            this.BTN_Logout.Click += new System.EventHandler(this.BTN_Logout_Click);
            // 
            // PIC_Board
            // 
            this.PIC_Board.Enabled = false;
            this.PIC_Board.Location = new System.Drawing.Point(6, 6);
            this.PIC_Board.Name = "PIC_Board";
            this.PIC_Board.Size = new System.Drawing.Size(440, 440);
            this.PIC_Board.TabIndex = 5;
            this.PIC_Board.TabStop = false;
            this.PIC_Board.Visible = false;
            // 
            // BTN_StartGame
            // 
            this.BTN_StartGame.Location = new System.Drawing.Point(177, 198);
            this.BTN_StartGame.Name = "BTN_StartGame";
            this.BTN_StartGame.Size = new System.Drawing.Size(103, 35);
            this.BTN_StartGame.TabIndex = 6;
            this.BTN_StartGame.Text = "Start Game";
            this.BTN_StartGame.UseVisualStyleBackColor = true;
            this.BTN_StartGame.Visible = false;
            this.BTN_StartGame.Click += new System.EventHandler(this.BTN_StartGame_Click);
            // 
            // COMBO_Games
            // 
            this.COMBO_Games.FormattingEnabled = true;
            this.COMBO_Games.Location = new System.Drawing.Point(174, 423);
            this.COMBO_Games.Name = "COMBO_Games";
            this.COMBO_Games.Size = new System.Drawing.Size(121, 21);
            this.COMBO_Games.TabIndex = 7;
            this.COMBO_Games.Visible = false;
            this.COMBO_Games.SelectedIndexChanged += new System.EventHandler(this.COMBO_Games_SelectedIndexChanged);
            // 
            // LBL_GamesTitle
            // 
            this.LBL_GamesTitle.AutoSize = true;
            this.LBL_GamesTitle.Location = new System.Drawing.Point(174, 404);
            this.LBL_GamesTitle.Name = "LBL_GamesTitle";
            this.LBL_GamesTitle.Size = new System.Drawing.Size(112, 13);
            this.LBL_GamesTitle.TabIndex = 8;
            this.LBL_GamesTitle.Text = "Select a game to view";
            this.LBL_GamesTitle.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 451);
            this.Controls.Add(this.LBL_GamesTitle);
            this.Controls.Add(this.COMBO_Games);
            this.Controls.Add(this.BTN_Logout);
            this.Controls.Add(this.BTN_Register_Forward);
            this.Controls.Add(this.LBL_Login);
            this.Controls.Add(this.INPUT_Login);
            this.Controls.Add(this.BTN_Login);
            this.Controls.Add(this.BTN_StartGame);
            this.Controls.Add(this.PIC_Board);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PIC_Board)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_Login;
        private System.Windows.Forms.TextBox INPUT_Login;
        private System.Windows.Forms.Label LBL_Login;
        private System.Windows.Forms.Button BTN_Register_Forward;
        private System.Windows.Forms.Button BTN_Logout;
        private System.Windows.Forms.PictureBox PIC_Board;
        private System.Windows.Forms.Button BTN_StartGame;
        private System.Windows.Forms.ComboBox COMBO_Games;
        private System.Windows.Forms.Label LBL_GamesTitle;
    }
}


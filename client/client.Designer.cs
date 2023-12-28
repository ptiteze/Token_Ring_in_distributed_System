namespace client
{
    partial class client
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_send = new Button();
            txt_send = new TextBox();
            view_mess = new ListView();
            btn_passnode = new Button();
            SuspendLayout();
            // 
            // btn_send
            // 
            btn_send.Location = new Point(636, 380);
            btn_send.Name = "btn_send";
            btn_send.Size = new Size(121, 30);
            btn_send.TabIndex = 5;
            btn_send.Text = "send";
            btn_send.UseVisualStyleBackColor = true;
            btn_send.Click += btn_send_Click;
            // 
            // txt_send
            // 
            txt_send.Location = new Point(29, 399);
            txt_send.Name = "txt_send";
            txt_send.Size = new Size(573, 31);
            txt_send.TabIndex = 4;
            txt_send.KeyDown += txt_send_KeyDown;
            // 
            // view_mess
            // 
            view_mess.Location = new Point(29, 20);
            view_mess.Name = "view_mess";
            view_mess.Size = new Size(743, 354);
            view_mess.TabIndex = 3;
            view_mess.UseCompatibleStateImageBehavior = false;
            view_mess.View = View.List;
            // 
            // btn_passnode
            // 
            btn_passnode.Location = new Point(636, 416);
            btn_passnode.Name = "btn_passnode";
            btn_passnode.Size = new Size(112, 34);
            btn_passnode.TabIndex = 6;
            btn_passnode.Text = "passnode";
            btn_passnode.UseVisualStyleBackColor = true;
            btn_passnode.Click += button1_Click;
            // 
            // client
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_passnode);
            Controls.Add(btn_send);
            Controls.Add(txt_send);
            Controls.Add(view_mess);
            Name = "client";
            Text = "client";
            FormClosed += client_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_send;
        private TextBox txt_send;
        private ListView view_mess;
        private Button btn_passnode;
    }
}
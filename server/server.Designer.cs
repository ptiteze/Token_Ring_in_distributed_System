namespace server
{
    partial class server
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
            view_mess = new ListView();
            txt_send = new TextBox();
            btn_send = new Button();
            SuspendLayout();
            // 
            // view_mess
            // 
            view_mess.Location = new Point(12, 12);
            view_mess.Name = "view_mess";
            view_mess.Size = new Size(743, 354);
            view_mess.TabIndex = 0;
            view_mess.UseCompatibleStateImageBehavior = false;
            view_mess.View = View.List;
            // 
            // txt_send
            // 
            txt_send.Location = new Point(12, 391);
            txt_send.Name = "txt_send";
            txt_send.Size = new Size(573, 31);
            txt_send.TabIndex = 1;
            txt_send.KeyDown += txt_send_KeyDown;
            // 
            // btn_send
            // 
            btn_send.Location = new Point(619, 392);
            btn_send.Name = "btn_send";
            btn_send.Size = new Size(121, 30);
            btn_send.TabIndex = 2;
            btn_send.Text = "send";
            btn_send.UseVisualStyleBackColor = true;
            btn_send.Click += btn_send_Click;
            // 
            // server
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(819, 462);
            Controls.Add(btn_send);
            Controls.Add(txt_send);
            Controls.Add(view_mess);
            Name = "server";
            Text = "server";
            FormClosed += server_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView view_mess;
        private TextBox txt_send;
        private Button btn_send;
    }
}
namespace AcquireCPUID
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btAcquire = new System.Windows.Forms.Button();
            this.tbCUPID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btAcquire
            // 
            this.btAcquire.BackColor = System.Drawing.Color.LightGray;
            this.btAcquire.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btAcquire.Location = new System.Drawing.Point(49, 78);
            this.btAcquire.Name = "btAcquire";
            this.btAcquire.Size = new System.Drawing.Size(142, 21);
            this.btAcquire.TabIndex = 0;
            this.btAcquire.Text = "获取";
            this.btAcquire.UseVisualStyleBackColor = false;
            this.btAcquire.Click += new System.EventHandler(this.btAcquire_Click);
            // 
            // tbCUPID
            // 
            this.tbCUPID.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCUPID.Location = new System.Drawing.Point(49, 32);
            this.tbCUPID.Name = "tbCUPID";
            this.tbCUPID.Size = new System.Drawing.Size(142, 23);
            this.tbCUPID.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 143);
            this.Controls.Add(this.tbCUPID);
            this.Controls.Add(this.btAcquire);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "获取系统码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btAcquire;
        private System.Windows.Forms.TextBox tbCUPID;
    }
}


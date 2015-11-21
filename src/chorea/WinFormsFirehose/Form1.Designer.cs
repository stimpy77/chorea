namespace WinFormsFirehose
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
            this.lstMessages = new System.Windows.Forms.ListBox();
            this.cmdAddFirehose = new System.Windows.Forms.Button();
            this.cmdRemoveFirehose = new System.Windows.Forms.Button();
            this.lblFirehoseCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstMessages
            // 
            this.lstMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMessages.FormattingEnabled = true;
            this.lstMessages.Location = new System.Drawing.Point(12, 12);
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(583, 238);
            this.lstMessages.TabIndex = 0;
            // 
            // cmdAddFirehose
            // 
            this.cmdAddFirehose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdAddFirehose.Location = new System.Drawing.Point(12, 271);
            this.cmdAddFirehose.Name = "cmdAddFirehose";
            this.cmdAddFirehose.Size = new System.Drawing.Size(88, 23);
            this.cmdAddFirehose.TabIndex = 1;
            this.cmdAddFirehose.Text = "Add firehose";
            this.cmdAddFirehose.UseVisualStyleBackColor = true;
            this.cmdAddFirehose.Click += new System.EventHandler(this.cmdAddFirehose_Click);
            // 
            // cmdRemoveFirehose
            // 
            this.cmdRemoveFirehose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRemoveFirehose.Location = new System.Drawing.Point(106, 271);
            this.cmdRemoveFirehose.Name = "cmdRemoveFirehose";
            this.cmdRemoveFirehose.Size = new System.Drawing.Size(115, 23);
            this.cmdRemoveFirehose.TabIndex = 2;
            this.cmdRemoveFirehose.Text = "Remove firehose";
            this.cmdRemoveFirehose.UseVisualStyleBackColor = true;
            this.cmdRemoveFirehose.Click += new System.EventHandler(this.cmdRemoveFirehose_Click);
            // 
            // lblFirehoseCount
            // 
            this.lblFirehoseCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFirehoseCount.AutoSize = true;
            this.lblFirehoseCount.Location = new System.Drawing.Point(500, 276);
            this.lblFirehoseCount.Name = "lblFirehoseCount";
            this.lblFirehoseCount.Size = new System.Drawing.Size(95, 13);
            this.lblFirehoseCount.TabIndex = 3;
            this.lblFirehoseCount.Text = "Firehouse count: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 306);
            this.Controls.Add(this.lblFirehoseCount);
            this.Controls.Add(this.cmdRemoveFirehose);
            this.Controls.Add(this.cmdAddFirehose);
            this.Controls.Add(this.lstMessages);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstMessages;
        private System.Windows.Forms.Button cmdAddFirehose;
        private System.Windows.Forms.Button cmdRemoveFirehose;
        private System.Windows.Forms.Label lblFirehoseCount;
    }
}


namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class BackgroundControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundBox = new System.Windows.Forms.GroupBox();
            this.backgroundListBox = new System.Windows.Forms.ListBox();
            this.backgroundDescription = new System.Windows.Forms.Label();
            this.backgroundBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundBox
            // 
            this.backgroundBox.Controls.Add(this.backgroundDescription);
            this.backgroundBox.Controls.Add(this.backgroundListBox);
            this.backgroundBox.Location = new System.Drawing.Point(4, 4);
            this.backgroundBox.Name = "backgroundBox";
            this.backgroundBox.Size = new System.Drawing.Size(944, 188);
            this.backgroundBox.TabIndex = 0;
            this.backgroundBox.TabStop = false;
            this.backgroundBox.Text = "Background";
            // 
            // backgroundListBox
            // 
            this.backgroundListBox.FormattingEnabled = true;
            this.backgroundListBox.Location = new System.Drawing.Point(7, 20);
            this.backgroundListBox.Name = "backgroundListBox";
            this.backgroundListBox.Size = new System.Drawing.Size(120, 160);
            this.backgroundListBox.TabIndex = 0;
            this.backgroundListBox.SelectedIndexChanged += new System.EventHandler(this.backgroundListBox_SelectedIndexChanged);
            // 
            // backgroundDescription
            // 
            this.backgroundDescription.AutoSize = true;
            this.backgroundDescription.Location = new System.Drawing.Point(134, 20);
            this.backgroundDescription.MaximumSize = new System.Drawing.Size(804, 160);
            this.backgroundDescription.Name = "backgroundDescription";
            this.backgroundDescription.Size = new System.Drawing.Size(118, 13);
            this.backgroundDescription.TabIndex = 1;
            this.backgroundDescription.Text = "background description";
            // 
            // BackgroundControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.backgroundBox);
            this.Name = "BackgroundControl";
            this.Size = new System.Drawing.Size(960, 200);
            this.backgroundBox.ResumeLayout(false);
            this.backgroundBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox backgroundBox;
        private System.Windows.Forms.Label backgroundDescription;
        private System.Windows.Forms.ListBox backgroundListBox;
    }
}

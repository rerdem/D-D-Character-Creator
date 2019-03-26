namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class ExportControl
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
            this.exportBox = new System.Windows.Forms.GroupBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.exportBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // exportBox
            // 
            this.exportBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exportBox.Controls.Add(this.exportButton);
            this.exportBox.Location = new System.Drawing.Point(4, 4);
            this.exportBox.Name = "exportBox";
            this.exportBox.Size = new System.Drawing.Size(925, 88);
            this.exportBox.TabIndex = 0;
            this.exportBox.TabStop = false;
            this.exportBox.Text = "Export";
            // 
            // exportButton
            // 
            this.exportButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.exportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportButton.Location = new System.Drawing.Point(308, 19);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(308, 62);
            this.exportButton.TabIndex = 0;
            this.exportButton.Text = "EXPORT";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // ExportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.exportBox);
            this.Name = "ExportControl";
            this.Size = new System.Drawing.Size(937, 100);
            this.exportBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox exportBox;
        private System.Windows.Forms.Button exportButton;
    }
}

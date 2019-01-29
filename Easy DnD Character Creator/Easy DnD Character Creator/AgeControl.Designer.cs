namespace Easy_DnD_Character_Creator
{
    partial class AgeControl
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
            this.ageBox = new System.Windows.Forms.GroupBox();
            this.ageLabel = new System.Windows.Forms.Label();
            this.ageValue = new System.Windows.Forms.NumericUpDown();
            this.ageBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ageValue)).BeginInit();
            this.SuspendLayout();
            // 
            // ageBox
            // 
            this.ageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ageBox.Controls.Add(this.ageValue);
            this.ageBox.Controls.Add(this.ageLabel);
            this.ageBox.Location = new System.Drawing.Point(4, 4);
            this.ageBox.Name = "ageBox";
            this.ageBox.Size = new System.Drawing.Size(944, 88);
            this.ageBox.TabIndex = 0;
            this.ageBox.TabStop = false;
            this.ageBox.Text = "Age";
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Location = new System.Drawing.Point(7, 20);
            this.ageLabel.MaximumSize = new System.Drawing.Size(650, 56);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(103, 13);
            this.ageLabel.TabIndex = 0;
            this.ageLabel.Text = "race age description";
            // 
            // ageValue
            // 
            this.ageValue.Location = new System.Drawing.Point(663, 38);
            this.ageValue.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.ageValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ageValue.Name = "ageValue";
            this.ageValue.Size = new System.Drawing.Size(250, 20);
            this.ageValue.TabIndex = 1;
            this.ageValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // AgeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ageBox);
            this.Name = "AgeControl";
            this.Size = new System.Drawing.Size(950, 100);
            this.ageBox.ResumeLayout(false);
            this.ageBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ageValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ageBox;
        private System.Windows.Forms.NumericUpDown ageValue;
        private System.Windows.Forms.Label ageLabel;
    }
}

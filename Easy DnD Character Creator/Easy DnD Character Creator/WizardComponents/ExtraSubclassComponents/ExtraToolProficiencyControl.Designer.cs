namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    partial class ExtraToolProficiencyControl
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
            this.toolBox = new System.Windows.Forms.GroupBox();
            this.toolComboBox = new System.Windows.Forms.ComboBox();
            this.introLabel = new System.Windows.Forms.Label();
            this.toolBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBox
            // 
            this.toolBox.Controls.Add(this.toolComboBox);
            this.toolBox.Controls.Add(this.introLabel);
            this.toolBox.Location = new System.Drawing.Point(4, 4);
            this.toolBox.Name = "toolBox";
            this.toolBox.Size = new System.Drawing.Size(890, 58);
            this.toolBox.TabIndex = 0;
            this.toolBox.TabStop = false;
            this.toolBox.Text = "Additional Tool Proficiency";
            // 
            // toolComboBox
            // 
            this.toolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolComboBox.FormattingEnabled = true;
            this.toolComboBox.Location = new System.Drawing.Point(229, 23);
            this.toolComboBox.Name = "toolComboBox";
            this.toolComboBox.Size = new System.Drawing.Size(200, 21);
            this.toolComboBox.TabIndex = 1;
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(6, 26);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(217, 13);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = "Please choose an additional tool proficiency:";
            // 
            // ExtraToolProficiencyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolBox);
            this.Name = "ExtraToolProficiencyControl";
            this.Size = new System.Drawing.Size(900, 70);
            this.toolBox.ResumeLayout(false);
            this.toolBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox toolBox;
        private System.Windows.Forms.ComboBox toolComboBox;
        private System.Windows.Forms.Label introLabel;
    }
}

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    partial class CompanionControl
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
            this.companionComboBox = new System.Windows.Forms.ComboBox();
            this.introLabel = new System.Windows.Forms.Label();
            this.companionBox = new System.Windows.Forms.GroupBox();
            this.bookLabel = new System.Windows.Forms.Label();
            this.companionBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // companionComboBox
            // 
            this.companionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.companionComboBox.FormattingEnabled = true;
            this.companionComboBox.Location = new System.Drawing.Point(229, 23);
            this.companionComboBox.Name = "companionComboBox";
            this.companionComboBox.Size = new System.Drawing.Size(200, 21);
            this.companionComboBox.TabIndex = 1;
            this.companionComboBox.SelectedIndexChanged += new System.EventHandler(this.companionComboBox_SelectedIndexChanged);
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(6, 26);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(210, 13);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = "Please choose a beast as your companion:";
            // 
            // companionBox
            // 
            this.companionBox.Controls.Add(this.bookLabel);
            this.companionBox.Controls.Add(this.companionComboBox);
            this.companionBox.Controls.Add(this.introLabel);
            this.companionBox.Location = new System.Drawing.Point(5, 6);
            this.companionBox.Name = "companionBox";
            this.companionBox.Size = new System.Drawing.Size(890, 58);
            this.companionBox.TabIndex = 1;
            this.companionBox.TabStop = false;
            this.companionBox.Text = "Beast Companion";
            // 
            // bookLabel
            // 
            this.bookLabel.AutoSize = true;
            this.bookLabel.Location = new System.Drawing.Point(460, 26);
            this.bookLabel.Name = "bookLabel";
            this.bookLabel.Size = new System.Drawing.Size(237, 13);
            this.bookLabel.TabIndex = 2;
            this.bookLabel.Text = "(Ask your Dungeon Master for detailed statistics.)";
            // 
            // CompanionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.companionBox);
            this.Name = "CompanionControl";
            this.Size = new System.Drawing.Size(900, 70);
            this.companionBox.ResumeLayout(false);
            this.companionBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox companionComboBox;
        private System.Windows.Forms.Label introLabel;
        private System.Windows.Forms.GroupBox companionBox;
        private System.Windows.Forms.Label bookLabel;
    }
}

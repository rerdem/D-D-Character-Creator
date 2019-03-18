namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    partial class DraconicAncestryControl
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
            this.ancestryBox = new System.Windows.Forms.GroupBox();
            this.ancestryComboBox = new System.Windows.Forms.ComboBox();
            this.introLabel = new System.Windows.Forms.Label();
            this.ancestryBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ancestryBox
            // 
            this.ancestryBox.Controls.Add(this.ancestryComboBox);
            this.ancestryBox.Controls.Add(this.introLabel);
            this.ancestryBox.Location = new System.Drawing.Point(4, 4);
            this.ancestryBox.Name = "ancestryBox";
            this.ancestryBox.Size = new System.Drawing.Size(890, 58);
            this.ancestryBox.TabIndex = 0;
            this.ancestryBox.TabStop = false;
            this.ancestryBox.Text = "Draconic Ancestry";
            // 
            // ancestryComboBox
            // 
            this.ancestryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ancestryComboBox.FormattingEnabled = true;
            this.ancestryComboBox.Location = new System.Drawing.Point(357, 25);
            this.ancestryComboBox.Name = "ancestryComboBox";
            this.ancestryComboBox.Size = new System.Drawing.Size(200, 21);
            this.ancestryComboBox.TabIndex = 3;
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(6, 20);
            this.introLabel.MaximumSize = new System.Drawing.Size(350, 26);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(329, 26);
            this.introLabel.TabIndex = 2;
            this.introLabel.Text = "You choose one type of dragon as your ancestor. The damage type associated with e" +
    "ach dragon is used by features you gain later.";
            // 
            // DraconicAncestryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ancestryBox);
            this.Name = "DraconicAncestryControl";
            this.Size = new System.Drawing.Size(900, 70);
            this.ancestryBox.ResumeLayout(false);
            this.ancestryBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ancestryBox;
        private System.Windows.Forms.ComboBox ancestryComboBox;
        private System.Windows.Forms.Label introLabel;
    }
}

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    partial class TotemFeatureControl
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
            this.totemFeatureBox = new System.Windows.Forms.GroupBox();
            this.totemDescriptionLabel = new System.Windows.Forms.Label();
            this.totemList = new System.Windows.Forms.ListBox();
            this.featureIntroLabel = new System.Windows.Forms.Label();
            this.totemFeatureBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // totemFeatureBox
            // 
            this.totemFeatureBox.Controls.Add(this.totemDescriptionLabel);
            this.totemFeatureBox.Controls.Add(this.totemList);
            this.totemFeatureBox.Controls.Add(this.featureIntroLabel);
            this.totemFeatureBox.Location = new System.Drawing.Point(4, 4);
            this.totemFeatureBox.Name = "totemFeatureBox";
            this.totemFeatureBox.Size = new System.Drawing.Size(850, 138);
            this.totemFeatureBox.TabIndex = 0;
            this.totemFeatureBox.TabStop = false;
            this.totemFeatureBox.Text = "Totem Feature";
            // 
            // totemDescriptionLabel
            // 
            this.totemDescriptionLabel.AutoSize = true;
            this.totemDescriptionLabel.Location = new System.Drawing.Point(136, 50);
            this.totemDescriptionLabel.MaximumSize = new System.Drawing.Size(700, 80);
            this.totemDescriptionLabel.Name = "totemDescriptionLabel";
            this.totemDescriptionLabel.Size = new System.Drawing.Size(98, 13);
            this.totemDescriptionLabel.TabIndex = 2;
            this.totemDescriptionLabel.Text = "No totem available.";
            // 
            // totemList
            // 
            this.totemList.FormattingEnabled = true;
            this.totemList.Location = new System.Drawing.Point(10, 37);
            this.totemList.Name = "totemList";
            this.totemList.Size = new System.Drawing.Size(120, 95);
            this.totemList.TabIndex = 1;
            this.totemList.SelectedIndexChanged += new System.EventHandler(this.featureList_SelectedIndexChanged);
            // 
            // featureIntroLabel
            // 
            this.featureIntroLabel.AutoSize = true;
            this.featureIntroLabel.Location = new System.Drawing.Point(7, 20);
            this.featureIntroLabel.MaximumSize = new System.Drawing.Size(840, 14);
            this.featureIntroLabel.Name = "featureIntroLabel";
            this.featureIntroLabel.Size = new System.Drawing.Size(83, 13);
            this.featureIntroLabel.TabIndex = 0;
            this.featureIntroLabel.Text = "feature intro text";
            // 
            // TotemFeatureControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.totemFeatureBox);
            this.Name = "TotemFeatureControl";
            this.Size = new System.Drawing.Size(860, 150);
            this.totemFeatureBox.ResumeLayout(false);
            this.totemFeatureBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox totemFeatureBox;
        private System.Windows.Forms.Label totemDescriptionLabel;
        private System.Windows.Forms.ListBox totemList;
        private System.Windows.Forms.Label featureIntroLabel;
    }
}

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraSubclassComponents
{
    partial class HunterFeatureControl
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
            this.hunterFeatureBox = new System.Windows.Forms.GroupBox();
            this.optionDescriptionLabel = new System.Windows.Forms.Label();
            this.optionList = new System.Windows.Forms.ListBox();
            this.featureIntroLabel = new System.Windows.Forms.Label();
            this.hunterFeatureBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // hunterFeatureBox
            // 
            this.hunterFeatureBox.Controls.Add(this.optionDescriptionLabel);
            this.hunterFeatureBox.Controls.Add(this.optionList);
            this.hunterFeatureBox.Controls.Add(this.featureIntroLabel);
            this.hunterFeatureBox.Location = new System.Drawing.Point(5, 6);
            this.hunterFeatureBox.Name = "hunterFeatureBox";
            this.hunterFeatureBox.Size = new System.Drawing.Size(850, 138);
            this.hunterFeatureBox.TabIndex = 1;
            this.hunterFeatureBox.TabStop = false;
            this.hunterFeatureBox.Text = "Hunter Feature";
            // 
            // optionDescriptionLabel
            // 
            this.optionDescriptionLabel.AutoSize = true;
            this.optionDescriptionLabel.Location = new System.Drawing.Point(136, 50);
            this.optionDescriptionLabel.MaximumSize = new System.Drawing.Size(700, 80);
            this.optionDescriptionLabel.Name = "optionDescriptionLabel";
            this.optionDescriptionLabel.Size = new System.Drawing.Size(101, 13);
            this.optionDescriptionLabel.TabIndex = 2;
            this.optionDescriptionLabel.Text = "No option available.";
            // 
            // optionList
            // 
            this.optionList.FormattingEnabled = true;
            this.optionList.Location = new System.Drawing.Point(10, 37);
            this.optionList.Name = "optionList";
            this.optionList.Size = new System.Drawing.Size(120, 95);
            this.optionList.TabIndex = 1;
            this.optionList.SelectedIndexChanged += new System.EventHandler(this.optionList_SelectedIndexChanged);
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
            // HunterFeatureControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hunterFeatureBox);
            this.Name = "HunterFeatureControl";
            this.Size = new System.Drawing.Size(860, 150);
            this.hunterFeatureBox.ResumeLayout(false);
            this.hunterFeatureBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox hunterFeatureBox;
        private System.Windows.Forms.Label optionDescriptionLabel;
        private System.Windows.Forms.ListBox optionList;
        private System.Windows.Forms.Label featureIntroLabel;
    }
}

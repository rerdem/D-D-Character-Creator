namespace Easy_DnD_Character_Creator.WizardComponents.ExtraClassComponents
{
    partial class MetamagicControl
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
            this.metamagicBox = new System.Windows.Forms.GroupBox();
            this.metamagicDescriptionLabel = new System.Windows.Forms.Label();
            this.metamagicListBox = new System.Windows.Forms.ListBox();
            this.metamagicIntroLabel = new System.Windows.Forms.Label();
            this.metamagicBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // metamagicBox
            // 
            this.metamagicBox.Controls.Add(this.metamagicDescriptionLabel);
            this.metamagicBox.Controls.Add(this.metamagicListBox);
            this.metamagicBox.Controls.Add(this.metamagicIntroLabel);
            this.metamagicBox.Location = new System.Drawing.Point(4, 4);
            this.metamagicBox.Name = "metamagicBox";
            this.metamagicBox.Size = new System.Drawing.Size(890, 188);
            this.metamagicBox.TabIndex = 0;
            this.metamagicBox.TabStop = false;
            this.metamagicBox.Text = "Metamagic";
            // 
            // metamagicDescriptionLabel
            // 
            this.metamagicDescriptionLabel.AutoSize = true;
            this.metamagicDescriptionLabel.Location = new System.Drawing.Point(134, 36);
            this.metamagicDescriptionLabel.MaximumSize = new System.Drawing.Size(735, 128);
            this.metamagicDescriptionLabel.Name = "metamagicDescriptionLabel";
            this.metamagicDescriptionLabel.Size = new System.Drawing.Size(123, 13);
            this.metamagicDescriptionLabel.TabIndex = 2;
            this.metamagicDescriptionLabel.Text = "No metamagic available.";
            // 
            // metamagicListBox
            // 
            this.metamagicListBox.FormattingEnabled = true;
            this.metamagicListBox.Location = new System.Drawing.Point(7, 37);
            this.metamagicListBox.Name = "metamagicListBox";
            this.metamagicListBox.Size = new System.Drawing.Size(120, 147);
            this.metamagicListBox.TabIndex = 1;
            this.metamagicListBox.SelectedIndexChanged += new System.EventHandler(this.metamagicListBox_SelectedIndexChanged);
            // 
            // metamagicIntroLabel
            // 
            this.metamagicIntroLabel.AutoSize = true;
            this.metamagicIntroLabel.Location = new System.Drawing.Point(7, 20);
            this.metamagicIntroLabel.MaximumSize = new System.Drawing.Size(900, 14);
            this.metamagicIntroLabel.Name = "metamagicIntroLabel";
            this.metamagicIntroLabel.Size = new System.Drawing.Size(47, 13);
            this.metamagicIntroLabel.TabIndex = 0;
            this.metamagicIntroLabel.Text = "intro text";
            // 
            // MetamagicControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metamagicBox);
            this.Name = "MetamagicControl";
            this.Size = new System.Drawing.Size(900, 200);
            this.metamagicBox.ResumeLayout(false);
            this.metamagicBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox metamagicBox;
        private System.Windows.Forms.Label metamagicDescriptionLabel;
        private System.Windows.Forms.ListBox metamagicListBox;
        private System.Windows.Forms.Label metamagicIntroLabel;
    }
}

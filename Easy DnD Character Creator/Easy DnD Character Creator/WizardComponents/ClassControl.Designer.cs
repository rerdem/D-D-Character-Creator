namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class ClassControl
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
            this.classBox = new System.Windows.Forms.GroupBox();
            this.extraChoiceLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.extraChoiceLabel = new System.Windows.Forms.Label();
            this.extraChoiceBox = new System.Windows.Forms.ListBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.subclassListBox = new System.Windows.Forms.ListBox();
            this.classListBox = new System.Windows.Forms.ListBox();
            this.classBox.SuspendLayout();
            this.extraChoiceLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // classBox
            // 
            this.classBox.Controls.Add(this.extraChoiceLayout);
            this.classBox.Controls.Add(this.descriptionLabel);
            this.classBox.Controls.Add(this.subclassListBox);
            this.classBox.Controls.Add(this.classListBox);
            this.classBox.Location = new System.Drawing.Point(4, 4);
            this.classBox.Name = "classBox";
            this.classBox.Size = new System.Drawing.Size(925, 188);
            this.classBox.TabIndex = 0;
            this.classBox.TabStop = false;
            this.classBox.Text = "Class/Subclass";
            // 
            // extraChoiceLayout
            // 
            this.extraChoiceLayout.Controls.Add(this.extraChoiceLabel);
            this.extraChoiceLayout.Controls.Add(this.extraChoiceBox);
            this.extraChoiceLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.extraChoiceLayout.Location = new System.Drawing.Point(779, 19);
            this.extraChoiceLayout.Name = "extraChoiceLayout";
            this.extraChoiceLayout.Size = new System.Drawing.Size(140, 163);
            this.extraChoiceLayout.TabIndex = 3;
            this.extraChoiceLayout.Visible = false;
            // 
            // extraChoiceLabel
            // 
            this.extraChoiceLabel.Location = new System.Drawing.Point(3, 3);
            this.extraChoiceLabel.Margin = new System.Windows.Forms.Padding(3);
            this.extraChoiceLabel.Name = "extraChoiceLabel";
            this.extraChoiceLabel.Size = new System.Drawing.Size(131, 39);
            this.extraChoiceLabel.TabIndex = 0;
            this.extraChoiceLabel.Text = "Please choose 1 of the following tools to be proficient in:";
            // 
            // extraChoiceBox
            // 
            this.extraChoiceBox.FormattingEnabled = true;
            this.extraChoiceBox.Location = new System.Drawing.Point(3, 48);
            this.extraChoiceBox.Name = "extraChoiceBox";
            this.extraChoiceBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.extraChoiceBox.Size = new System.Drawing.Size(131, 108);
            this.extraChoiceBox.TabIndex = 1;
            this.extraChoiceBox.SelectedIndexChanged += new System.EventHandler(this.extraChoiceBox_SelectedIndexChanged);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(258, 19);
            this.descriptionLabel.MaximumSize = new System.Drawing.Size(650, 160);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(131, 13);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "class/subclass description";
            // 
            // subclassListBox
            // 
            this.subclassListBox.FormattingEnabled = true;
            this.subclassListBox.Location = new System.Drawing.Point(132, 19);
            this.subclassListBox.Name = "subclassListBox";
            this.subclassListBox.Size = new System.Drawing.Size(120, 160);
            this.subclassListBox.TabIndex = 1;
            this.subclassListBox.SelectedIndexChanged += new System.EventHandler(this.subclassListBox_SelectedIndexChanged);
            // 
            // classListBox
            // 
            this.classListBox.FormattingEnabled = true;
            this.classListBox.Location = new System.Drawing.Point(6, 19);
            this.classListBox.Name = "classListBox";
            this.classListBox.Size = new System.Drawing.Size(120, 160);
            this.classListBox.TabIndex = 0;
            this.classListBox.SelectedIndexChanged += new System.EventHandler(this.classListBox_SelectedIndexChanged);
            // 
            // ClassControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.classBox);
            this.Name = "ClassControl";
            this.Size = new System.Drawing.Size(937, 200);
            this.classBox.ResumeLayout(false);
            this.classBox.PerformLayout();
            this.extraChoiceLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox classBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.ListBox subclassListBox;
        private System.Windows.Forms.ListBox classListBox;
        private System.Windows.Forms.FlowLayoutPanel extraChoiceLayout;
        private System.Windows.Forms.Label extraChoiceLabel;
        private System.Windows.Forms.ListBox extraChoiceBox;
    }
}

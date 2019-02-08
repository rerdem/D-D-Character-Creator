namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class SkillControl
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
            this.skillBox = new System.Windows.Forms.GroupBox();
            this.skillLayout = new System.Windows.Forms.TableLayoutPanel();
            this.extraSkillBox = new System.Windows.Forms.ListBox();
            this.extraSkillLabel = new System.Windows.Forms.Label();
            this.extraSkillLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.tutorialLabel = new System.Windows.Forms.Label();
            this.skillBox.SuspendLayout();
            this.extraSkillLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // skillBox
            // 
            this.skillBox.Controls.Add(this.tutorialLabel);
            this.skillBox.Controls.Add(this.extraSkillLayout);
            this.skillBox.Controls.Add(this.skillLayout);
            this.skillBox.Location = new System.Drawing.Point(3, 3);
            this.skillBox.Name = "skillBox";
            this.skillBox.Size = new System.Drawing.Size(954, 194);
            this.skillBox.TabIndex = 0;
            this.skillBox.TabStop = false;
            this.skillBox.Text = "Skills";
            // 
            // skillLayout
            // 
            this.skillLayout.ColumnCount = 5;
            this.skillLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.skillLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.skillLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.skillLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.skillLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.skillLayout.Location = new System.Drawing.Point(6, 70);
            this.skillLayout.Name = "skillLayout";
            this.skillLayout.RowCount = 4;
            this.skillLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.skillLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.skillLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.skillLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.skillLayout.Size = new System.Drawing.Size(789, 118);
            this.skillLayout.TabIndex = 0;
            // 
            // extraSkillBox
            // 
            this.extraSkillBox.FormattingEnabled = true;
            this.extraSkillBox.Location = new System.Drawing.Point(3, 22);
            this.extraSkillBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.extraSkillBox.Name = "extraSkillBox";
            this.extraSkillBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.extraSkillBox.Size = new System.Drawing.Size(144, 121);
            this.extraSkillBox.TabIndex = 1;
            this.extraSkillBox.SelectedIndexChanged += new System.EventHandler(this.extraSkillBox_SelectedIndexChanged);
            this.extraSkillBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.extraSkillBox_MouseMove);
            // 
            // extraSkillLabel
            // 
            this.extraSkillLabel.AutoSize = true;
            this.extraSkillLabel.Location = new System.Drawing.Point(3, 3);
            this.extraSkillLabel.Margin = new System.Windows.Forms.Padding(3);
            this.extraSkillLabel.MaximumSize = new System.Drawing.Size(144, 0);
            this.extraSkillLabel.Name = "extraSkillLabel";
            this.extraSkillLabel.Size = new System.Drawing.Size(75, 13);
            this.extraSkillLabel.TabIndex = 2;
            this.extraSkillLabel.Text = "extraSkillLabel";
            // 
            // extraSkillLayout
            // 
            this.extraSkillLayout.Controls.Add(this.extraSkillLabel);
            this.extraSkillLayout.Controls.Add(this.extraSkillBox);
            this.extraSkillLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.extraSkillLayout.Location = new System.Drawing.Point(798, 20);
            this.extraSkillLayout.Name = "extraSkillLayout";
            this.extraSkillLayout.Size = new System.Drawing.Size(150, 168);
            this.extraSkillLayout.TabIndex = 3;
            this.extraSkillLayout.Visible = false;
            // 
            // tutorialLabel
            // 
            this.tutorialLabel.AutoSize = true;
            this.tutorialLabel.Location = new System.Drawing.Point(1, 20);
            this.tutorialLabel.MaximumSize = new System.Drawing.Size(777, 47);
            this.tutorialLabel.Name = "tutorialLabel";
            this.tutorialLabel.Size = new System.Drawing.Size(90, 13);
            this.tutorialLabel.TabIndex = 4;
            this.tutorialLabel.Text = "actual tutorial text";
            // 
            // SkillControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.skillBox);
            this.Name = "SkillControl";
            this.Size = new System.Drawing.Size(960, 200);
            this.skillBox.ResumeLayout(false);
            this.skillBox.PerformLayout();
            this.extraSkillLayout.ResumeLayout(false);
            this.extraSkillLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox skillBox;
        private System.Windows.Forms.FlowLayoutPanel extraSkillLayout;
        private System.Windows.Forms.Label extraSkillLabel;
        private System.Windows.Forms.ListBox extraSkillBox;
        private System.Windows.Forms.TableLayoutPanel skillLayout;
        private System.Windows.Forms.Label tutorialLabel;
    }
}

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraClassComponents
{
    partial class ExtraClassSkillControl
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
            this.extraSkillBox = new System.Windows.Forms.GroupBox();
            this.extraSkillLayout = new System.Windows.Forms.TableLayoutPanel();
            this.extraSkillLabel = new System.Windows.Forms.Label();
            this.extraSkillBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // extraSkillBox
            // 
            this.extraSkillBox.Controls.Add(this.extraSkillLayout);
            this.extraSkillBox.Controls.Add(this.extraSkillLabel);
            this.extraSkillBox.Location = new System.Drawing.Point(4, 4);
            this.extraSkillBox.Name = "extraSkillBox";
            this.extraSkillBox.Size = new System.Drawing.Size(890, 188);
            this.extraSkillBox.TabIndex = 0;
            this.extraSkillBox.TabStop = false;
            this.extraSkillBox.Text = "Extra Skills";
            // 
            // extraSkillLayout
            // 
            this.extraSkillLayout.ColumnCount = 5;
            this.extraSkillLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.extraSkillLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.extraSkillLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.extraSkillLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.extraSkillLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.extraSkillLayout.Location = new System.Drawing.Point(7, 61);
            this.extraSkillLayout.Name = "extraSkillLayout";
            this.extraSkillLayout.RowCount = 4;
            this.extraSkillLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.extraSkillLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.extraSkillLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.extraSkillLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.extraSkillLayout.Size = new System.Drawing.Size(877, 121);
            this.extraSkillLayout.TabIndex = 1;
            // 
            // extraSkillLabel
            // 
            this.extraSkillLabel.AutoSize = true;
            this.extraSkillLabel.Location = new System.Drawing.Point(7, 20);
            this.extraSkillLabel.Name = "extraSkillLabel";
            this.extraSkillLabel.Size = new System.Drawing.Size(93, 13);
            this.extraSkillLabel.TabIndex = 0;
            this.extraSkillLabel.Text = "extra skill intro text";
            // 
            // ExtraClassSkillControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.extraSkillBox);
            this.Name = "ExtraClassSkillControl";
            this.Size = new System.Drawing.Size(900, 200);
            this.extraSkillBox.ResumeLayout(false);
            this.extraSkillBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox extraSkillBox;
        private System.Windows.Forms.TableLayoutPanel extraSkillLayout;
        private System.Windows.Forms.Label extraSkillLabel;
    }
}

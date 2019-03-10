namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class LanguageControl
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
            this.standardLanguageBox = new System.Windows.Forms.GroupBox();
            this.standardLanguageLayout = new System.Windows.Forms.TableLayoutPanel();
            this.classLanguageBox = new System.Windows.Forms.GroupBox();
            this.classLanguageLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.exoticLanguageBox = new System.Windows.Forms.GroupBox();
            this.exoticLanguageLayout = new System.Windows.Forms.TableLayoutPanel();
            this.standardLanguageBox.SuspendLayout();
            this.classLanguageBox.SuspendLayout();
            this.exoticLanguageBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // standardLanguageBox
            // 
            this.standardLanguageBox.Controls.Add(this.standardLanguageLayout);
            this.standardLanguageBox.Location = new System.Drawing.Point(4, 4);
            this.standardLanguageBox.Name = "standardLanguageBox";
            this.standardLanguageBox.Size = new System.Drawing.Size(370, 138);
            this.standardLanguageBox.TabIndex = 0;
            this.standardLanguageBox.TabStop = false;
            this.standardLanguageBox.Text = "Standard Languages";
            // 
            // standardLanguageLayout
            // 
            this.standardLanguageLayout.ColumnCount = 2;
            this.standardLanguageLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.standardLanguageLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.standardLanguageLayout.Location = new System.Drawing.Point(7, 20);
            this.standardLanguageLayout.Name = "standardLanguageLayout";
            this.standardLanguageLayout.RowCount = 4;
            this.standardLanguageLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.standardLanguageLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.standardLanguageLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.standardLanguageLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.standardLanguageLayout.Size = new System.Drawing.Size(357, 112);
            this.standardLanguageLayout.TabIndex = 0;
            // 
            // classLanguageBox
            // 
            this.classLanguageBox.Controls.Add(this.classLanguageLayout);
            this.classLanguageBox.Location = new System.Drawing.Point(757, 4);
            this.classLanguageBox.Name = "classLanguageBox";
            this.classLanguageBox.Size = new System.Drawing.Size(176, 138);
            this.classLanguageBox.TabIndex = 1;
            this.classLanguageBox.TabStop = false;
            this.classLanguageBox.Text = "Class-specific Languages";
            // 
            // classLanguageLayout
            // 
            this.classLanguageLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.classLanguageLayout.Location = new System.Drawing.Point(7, 20);
            this.classLanguageLayout.Name = "classLanguageLayout";
            this.classLanguageLayout.Size = new System.Drawing.Size(163, 112);
            this.classLanguageLayout.TabIndex = 0;
            // 
            // exoticLanguageBox
            // 
            this.exoticLanguageBox.Controls.Add(this.exoticLanguageLayout);
            this.exoticLanguageBox.Location = new System.Drawing.Point(380, 4);
            this.exoticLanguageBox.Name = "exoticLanguageBox";
            this.exoticLanguageBox.Size = new System.Drawing.Size(370, 138);
            this.exoticLanguageBox.TabIndex = 2;
            this.exoticLanguageBox.TabStop = false;
            this.exoticLanguageBox.Text = "Exotic Languages";
            // 
            // exoticLanguageLayout
            // 
            this.exoticLanguageLayout.ColumnCount = 2;
            this.exoticLanguageLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.exoticLanguageLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.exoticLanguageLayout.Location = new System.Drawing.Point(7, 20);
            this.exoticLanguageLayout.Name = "exoticLanguageLayout";
            this.exoticLanguageLayout.RowCount = 4;
            this.exoticLanguageLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.exoticLanguageLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.exoticLanguageLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.exoticLanguageLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.exoticLanguageLayout.Size = new System.Drawing.Size(357, 112);
            this.exoticLanguageLayout.TabIndex = 0;
            // 
            // LanguageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.exoticLanguageBox);
            this.Controls.Add(this.classLanguageBox);
            this.Controls.Add(this.standardLanguageBox);
            this.Name = "LanguageControl";
            this.Size = new System.Drawing.Size(937, 150);
            this.standardLanguageBox.ResumeLayout(false);
            this.classLanguageBox.ResumeLayout(false);
            this.exoticLanguageBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox standardLanguageBox;
        private System.Windows.Forms.TableLayoutPanel standardLanguageLayout;
        private System.Windows.Forms.GroupBox classLanguageBox;
        private System.Windows.Forms.FlowLayoutPanel classLanguageLayout;
        private System.Windows.Forms.GroupBox exoticLanguageBox;
        private System.Windows.Forms.TableLayoutPanel exoticLanguageLayout;
    }
}

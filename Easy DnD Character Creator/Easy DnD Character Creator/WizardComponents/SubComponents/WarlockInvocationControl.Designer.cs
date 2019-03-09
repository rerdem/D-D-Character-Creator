namespace Easy_DnD_Character_Creator.WizardComponents.SubComponents
{
    partial class WarlockInvocationControl
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
            this.invocationSpellIntroLabel = new System.Windows.Forms.Label();
            this.invocationBox = new System.Windows.Forms.GroupBox();
            this.invocationSpellLayout = new System.Windows.Forms.TableLayoutPanel();
            this.invocationSpellDescriptionLabel = new System.Windows.Forms.Label();
            this.invocationSpellListBox = new System.Windows.Forms.ListBox();
            this.invocationLayout = new System.Windows.Forms.TableLayoutPanel();
            this.invocationListBox = new System.Windows.Forms.ListBox();
            this.invocationIntroLabel = new System.Windows.Forms.Label();
            this.invocationDescriptionLabel = new System.Windows.Forms.Label();
            this.invocationBox.SuspendLayout();
            this.invocationSpellLayout.SuspendLayout();
            this.invocationLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // invocationSpellIntroLabel
            // 
            this.invocationSpellIntroLabel.AutoSize = true;
            this.invocationSpellLayout.SetColumnSpan(this.invocationSpellIntroLabel, 2);
            this.invocationSpellIntroLabel.Location = new System.Drawing.Point(3, 3);
            this.invocationSpellIntroLabel.Margin = new System.Windows.Forms.Padding(3);
            this.invocationSpellIntroLabel.Name = "invocationSpellIntroLabel";
            this.invocationSpellIntroLabel.Size = new System.Drawing.Size(150, 13);
            this.invocationSpellIntroLabel.TabIndex = 0;
            this.invocationSpellIntroLabel.Text = "Choose your invocation spells:";
            // 
            // invocationBox
            // 
            this.invocationBox.Controls.Add(this.invocationSpellLayout);
            this.invocationBox.Controls.Add(this.invocationLayout);
            this.invocationBox.Location = new System.Drawing.Point(3, 3);
            this.invocationBox.Name = "invocationBox";
            this.invocationBox.Size = new System.Drawing.Size(887, 188);
            this.invocationBox.TabIndex = 1;
            this.invocationBox.TabStop = false;
            this.invocationBox.Text = "Eldritch Invocations";
            // 
            // invocationSpellLayout
            // 
            this.invocationSpellLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.invocationSpellLayout.ColumnCount = 2;
            this.invocationSpellLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.invocationSpellLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.invocationSpellLayout.Controls.Add(this.invocationSpellIntroLabel, 0, 0);
            this.invocationSpellLayout.Controls.Add(this.invocationSpellDescriptionLabel, 1, 1);
            this.invocationSpellLayout.Controls.Add(this.invocationSpellListBox, 0, 1);
            this.invocationSpellLayout.Location = new System.Drawing.Point(452, 20);
            this.invocationSpellLayout.Name = "invocationSpellLayout";
            this.invocationSpellLayout.RowCount = 2;
            this.invocationSpellLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.invocationSpellLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.invocationSpellLayout.Size = new System.Drawing.Size(429, 162);
            this.invocationSpellLayout.TabIndex = 3;
            // 
            // invocationSpellDescriptionLabel
            // 
            this.invocationSpellDescriptionLabel.AutoSize = true;
            this.invocationSpellDescriptionLabel.Location = new System.Drawing.Point(132, 32);
            this.invocationSpellDescriptionLabel.Margin = new System.Windows.Forms.Padding(6);
            this.invocationSpellDescriptionLabel.Name = "invocationSpellDescriptionLabel";
            this.invocationSpellDescriptionLabel.Size = new System.Drawing.Size(150, 13);
            this.invocationSpellDescriptionLabel.TabIndex = 1;
            this.invocationSpellDescriptionLabel.Text = "No invocation spells available.";
            // 
            // invocationSpellListBox
            // 
            this.invocationSpellListBox.FormattingEnabled = true;
            this.invocationSpellListBox.Location = new System.Drawing.Point(3, 29);
            this.invocationSpellListBox.Name = "invocationSpellListBox";
            this.invocationSpellListBox.Size = new System.Drawing.Size(120, 121);
            this.invocationSpellListBox.TabIndex = 2;
            // 
            // invocationLayout
            // 
            this.invocationLayout.ColumnCount = 2;
            this.invocationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.invocationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.invocationLayout.Controls.Add(this.invocationListBox, 0, 1);
            this.invocationLayout.Controls.Add(this.invocationIntroLabel, 0, 0);
            this.invocationLayout.Controls.Add(this.invocationDescriptionLabel, 1, 1);
            this.invocationLayout.Location = new System.Drawing.Point(7, 20);
            this.invocationLayout.Name = "invocationLayout";
            this.invocationLayout.RowCount = 2;
            this.invocationLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.invocationLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.invocationLayout.Size = new System.Drawing.Size(439, 162);
            this.invocationLayout.TabIndex = 2;
            // 
            // invocationListBox
            // 
            this.invocationListBox.FormattingEnabled = true;
            this.invocationListBox.Location = new System.Drawing.Point(3, 29);
            this.invocationListBox.Name = "invocationListBox";
            this.invocationListBox.Size = new System.Drawing.Size(120, 121);
            this.invocationListBox.TabIndex = 1;
            // 
            // invocationIntroLabel
            // 
            this.invocationIntroLabel.AutoSize = true;
            this.invocationLayout.SetColumnSpan(this.invocationIntroLabel, 2);
            this.invocationIntroLabel.Location = new System.Drawing.Point(3, 3);
            this.invocationIntroLabel.Margin = new System.Windows.Forms.Padding(3);
            this.invocationIntroLabel.Name = "invocationIntroLabel";
            this.invocationIntroLabel.Size = new System.Drawing.Size(163, 13);
            this.invocationIntroLabel.TabIndex = 0;
            this.invocationIntroLabel.Text = "Choose your eldritch invocations:";
            // 
            // invocationDescriptionLabel
            // 
            this.invocationDescriptionLabel.AutoSize = true;
            this.invocationDescriptionLabel.Location = new System.Drawing.Point(132, 32);
            this.invocationDescriptionLabel.Margin = new System.Windows.Forms.Padding(6);
            this.invocationDescriptionLabel.Name = "invocationDescriptionLabel";
            this.invocationDescriptionLabel.Size = new System.Drawing.Size(110, 13);
            this.invocationDescriptionLabel.TabIndex = 2;
            this.invocationDescriptionLabel.Text = "invocation description";
            // 
            // WarlockInvocationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.invocationBox);
            this.Name = "WarlockInvocationControl";
            this.Size = new System.Drawing.Size(899, 200);
            this.invocationBox.ResumeLayout(false);
            this.invocationSpellLayout.ResumeLayout(false);
            this.invocationSpellLayout.PerformLayout();
            this.invocationLayout.ResumeLayout(false);
            this.invocationLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label invocationSpellIntroLabel;
        private System.Windows.Forms.TableLayoutPanel invocationSpellLayout;
        private System.Windows.Forms.Label invocationSpellDescriptionLabel;
        private System.Windows.Forms.ListBox invocationSpellListBox;
        private System.Windows.Forms.GroupBox invocationBox;
        private System.Windows.Forms.TableLayoutPanel invocationLayout;
        private System.Windows.Forms.ListBox invocationListBox;
        private System.Windows.Forms.Label invocationIntroLabel;
        private System.Windows.Forms.Label invocationDescriptionLabel;
    }
}

namespace Easy_DnD_Character_Creator.WizardComponents.ExtraClassComponents
{
    partial class WarlockPactControl
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
            this.pactBox = new System.Windows.Forms.GroupBox();
            this.pactSpellLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pactSpellIntroLabel = new System.Windows.Forms.Label();
            this.pactSpellDescriptionLabel = new System.Windows.Forms.Label();
            this.pactSpellListBox = new System.Windows.Forms.ListBox();
            this.pactLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pactListBox = new System.Windows.Forms.ListBox();
            this.pactIntroLabel = new System.Windows.Forms.Label();
            this.pactDescriptionLabel = new System.Windows.Forms.Label();
            this.pactBox.SuspendLayout();
            this.pactSpellLayout.SuspendLayout();
            this.pactLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // pactBox
            // 
            this.pactBox.Controls.Add(this.pactSpellLayout);
            this.pactBox.Controls.Add(this.pactLayout);
            this.pactBox.Location = new System.Drawing.Point(4, 4);
            this.pactBox.Name = "pactBox";
            this.pactBox.Size = new System.Drawing.Size(855, 288);
            this.pactBox.TabIndex = 0;
            this.pactBox.TabStop = false;
            this.pactBox.Text = "Pact";
            // 
            // pactSpellLayout
            // 
            this.pactSpellLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pactSpellLayout.ColumnCount = 2;
            this.pactSpellLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.pactSpellLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pactSpellLayout.Controls.Add(this.pactSpellIntroLabel, 0, 0);
            this.pactSpellLayout.Controls.Add(this.pactSpellDescriptionLabel, 1, 1);
            this.pactSpellLayout.Controls.Add(this.pactSpellListBox, 0, 1);
            this.pactSpellLayout.Location = new System.Drawing.Point(437, 19);
            this.pactSpellLayout.Name = "pactSpellLayout";
            this.pactSpellLayout.RowCount = 2;
            this.pactSpellLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.pactSpellLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pactSpellLayout.Size = new System.Drawing.Size(415, 262);
            this.pactSpellLayout.TabIndex = 3;
            // 
            // pactSpellIntroLabel
            // 
            this.pactSpellIntroLabel.AutoSize = true;
            this.pactSpellLayout.SetColumnSpan(this.pactSpellIntroLabel, 2);
            this.pactSpellIntroLabel.Location = new System.Drawing.Point(3, 3);
            this.pactSpellIntroLabel.Margin = new System.Windows.Forms.Padding(3);
            this.pactSpellIntroLabel.Name = "pactSpellIntroLabel";
            this.pactSpellIntroLabel.Size = new System.Drawing.Size(122, 13);
            this.pactSpellIntroLabel.TabIndex = 0;
            this.pactSpellIntroLabel.Text = "Choose your pact spells:";
            // 
            // pactSpellDescriptionLabel
            // 
            this.pactSpellDescriptionLabel.AutoSize = true;
            this.pactSpellDescriptionLabel.Location = new System.Drawing.Point(132, 32);
            this.pactSpellDescriptionLabel.Margin = new System.Windows.Forms.Padding(6);
            this.pactSpellDescriptionLabel.Name = "pactSpellDescriptionLabel";
            this.pactSpellDescriptionLabel.Size = new System.Drawing.Size(122, 13);
            this.pactSpellDescriptionLabel.TabIndex = 1;
            this.pactSpellDescriptionLabel.Text = "No pact spells available.";
            // 
            // pactSpellListBox
            // 
            this.pactSpellListBox.FormattingEnabled = true;
            this.pactSpellListBox.Location = new System.Drawing.Point(3, 29);
            this.pactSpellListBox.Name = "pactSpellListBox";
            this.pactSpellListBox.Size = new System.Drawing.Size(120, 225);
            this.pactSpellListBox.TabIndex = 2;
            this.pactSpellListBox.SelectedIndexChanged += new System.EventHandler(this.pactSpellListBox_SelectedIndexChanged);
            // 
            // pactLayout
            // 
            this.pactLayout.ColumnCount = 2;
            this.pactLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.pactLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pactLayout.Controls.Add(this.pactListBox, 0, 1);
            this.pactLayout.Controls.Add(this.pactIntroLabel, 0, 0);
            this.pactLayout.Controls.Add(this.pactDescriptionLabel, 1, 1);
            this.pactLayout.Location = new System.Drawing.Point(7, 20);
            this.pactLayout.Name = "pactLayout";
            this.pactLayout.RowCount = 2;
            this.pactLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.pactLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pactLayout.Size = new System.Drawing.Size(424, 262);
            this.pactLayout.TabIndex = 2;
            // 
            // pactListBox
            // 
            this.pactListBox.FormattingEnabled = true;
            this.pactListBox.Location = new System.Drawing.Point(3, 29);
            this.pactListBox.Name = "pactListBox";
            this.pactListBox.Size = new System.Drawing.Size(120, 225);
            this.pactListBox.TabIndex = 1;
            this.pactListBox.SelectedIndexChanged += new System.EventHandler(this.pactListBox_SelectedIndexChanged);
            // 
            // pactIntroLabel
            // 
            this.pactIntroLabel.AutoSize = true;
            this.pactLayout.SetColumnSpan(this.pactIntroLabel, 2);
            this.pactIntroLabel.Location = new System.Drawing.Point(3, 3);
            this.pactIntroLabel.Margin = new System.Windows.Forms.Padding(3);
            this.pactIntroLabel.Name = "pactIntroLabel";
            this.pactIntroLabel.Size = new System.Drawing.Size(171, 13);
            this.pactIntroLabel.TabIndex = 0;
            this.pactIntroLabel.Text = "Choose your pact with your patron:";
            // 
            // pactDescriptionLabel
            // 
            this.pactDescriptionLabel.AutoSize = true;
            this.pactDescriptionLabel.Location = new System.Drawing.Point(132, 32);
            this.pactDescriptionLabel.Margin = new System.Windows.Forms.Padding(6);
            this.pactDescriptionLabel.Name = "pactDescriptionLabel";
            this.pactDescriptionLabel.Size = new System.Drawing.Size(82, 13);
            this.pactDescriptionLabel.TabIndex = 2;
            this.pactDescriptionLabel.Text = "pact description";
            // 
            // WarlockPactControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pactBox);
            this.Name = "WarlockPactControl";
            this.Size = new System.Drawing.Size(865, 300);
            this.pactBox.ResumeLayout(false);
            this.pactSpellLayout.ResumeLayout(false);
            this.pactSpellLayout.PerformLayout();
            this.pactLayout.ResumeLayout(false);
            this.pactLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox pactBox;
        private System.Windows.Forms.TableLayoutPanel pactSpellLayout;
        private System.Windows.Forms.Label pactSpellIntroLabel;
        private System.Windows.Forms.Label pactSpellDescriptionLabel;
        private System.Windows.Forms.ListBox pactSpellListBox;
        private System.Windows.Forms.TableLayoutPanel pactLayout;
        private System.Windows.Forms.ListBox pactListBox;
        private System.Windows.Forms.Label pactIntroLabel;
        private System.Windows.Forms.Label pactDescriptionLabel;
    }
}

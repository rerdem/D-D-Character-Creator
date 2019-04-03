namespace Easy_DnD_Character_Creator.WizardComponents.ExtraClassComponents
{
    partial class WarlockControl
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
            this.warlockBox = new System.Windows.Forms.GroupBox();
            this.warlockLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.pactBox = new System.Windows.Forms.GroupBox();
            this.pactSpellLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pactSpellIntroLabel = new System.Windows.Forms.Label();
            this.pactSpellDescriptionLabel = new System.Windows.Forms.Label();
            this.pactSpellListBox = new System.Windows.Forms.ListBox();
            this.pactLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pactListBox = new System.Windows.Forms.ListBox();
            this.pactIntroLabel = new System.Windows.Forms.Label();
            this.pactDescriptionLabel = new System.Windows.Forms.Label();
            this.invocationBox = new System.Windows.Forms.GroupBox();
            this.invocationSpellLayout = new System.Windows.Forms.TableLayoutPanel();
            this.invocationSpellIntroLabel = new System.Windows.Forms.Label();
            this.invocationSpellDescriptionLabel = new System.Windows.Forms.Label();
            this.invocationSpellListBox = new System.Windows.Forms.ListBox();
            this.invocationLayout = new System.Windows.Forms.TableLayoutPanel();
            this.invocationListBox = new System.Windows.Forms.ListBox();
            this.invocationIntroLabel = new System.Windows.Forms.Label();
            this.invocationDescriptionLabel = new System.Windows.Forms.Label();
            this.warlockBox.SuspendLayout();
            this.warlockLayout.SuspendLayout();
            this.pactBox.SuspendLayout();
            this.pactSpellLayout.SuspendLayout();
            this.pactLayout.SuspendLayout();
            this.invocationBox.SuspendLayout();
            this.invocationSpellLayout.SuspendLayout();
            this.invocationLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // warlockBox
            // 
            this.warlockBox.AutoSize = true;
            this.warlockBox.Controls.Add(this.warlockLayout);
            this.warlockBox.Location = new System.Drawing.Point(4, 4);
            this.warlockBox.Name = "warlockBox";
            this.warlockBox.Size = new System.Drawing.Size(890, 627);
            this.warlockBox.TabIndex = 0;
            this.warlockBox.TabStop = false;
            this.warlockBox.Text = "Warlock Choices";
            // 
            // warlockLayout
            // 
            this.warlockLayout.AutoSize = true;
            this.warlockLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.warlockLayout.Controls.Add(this.pactBox);
            this.warlockLayout.Controls.Add(this.invocationBox);
            this.warlockLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.warlockLayout.Location = new System.Drawing.Point(7, 20);
            this.warlockLayout.Name = "warlockLayout";
            this.warlockLayout.Size = new System.Drawing.Size(861, 588);
            this.warlockLayout.TabIndex = 0;
            this.warlockLayout.WrapContents = false;
            // 
            // pactBox
            // 
            this.pactBox.Controls.Add(this.pactSpellLayout);
            this.pactBox.Controls.Add(this.pactLayout);
            this.pactBox.Location = new System.Drawing.Point(3, 3);
            this.pactBox.Name = "pactBox";
            this.pactBox.Size = new System.Drawing.Size(855, 288);
            this.pactBox.TabIndex = 1;
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
            // invocationBox
            // 
            this.invocationBox.Controls.Add(this.invocationSpellLayout);
            this.invocationBox.Controls.Add(this.invocationLayout);
            this.invocationBox.Location = new System.Drawing.Point(3, 297);
            this.invocationBox.Name = "invocationBox";
            this.invocationBox.Size = new System.Drawing.Size(855, 288);
            this.invocationBox.TabIndex = 2;
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
            this.invocationSpellLayout.Location = new System.Drawing.Point(434, 20);
            this.invocationSpellLayout.Name = "invocationSpellLayout";
            this.invocationSpellLayout.RowCount = 2;
            this.invocationSpellLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.invocationSpellLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.invocationSpellLayout.Size = new System.Drawing.Size(415, 262);
            this.invocationSpellLayout.TabIndex = 3;
            // 
            // invocationSpellIntroLabel
            // 
            this.invocationSpellIntroLabel.AutoSize = true;
            this.invocationSpellLayout.SetColumnSpan(this.invocationSpellIntroLabel, 2);
            this.invocationSpellIntroLabel.Location = new System.Drawing.Point(3, 3);
            this.invocationSpellIntroLabel.Margin = new System.Windows.Forms.Padding(3);
            this.invocationSpellIntroLabel.Name = "invocationSpellIntroLabel";
            this.invocationSpellIntroLabel.Size = new System.Drawing.Size(235, 13);
            this.invocationSpellIntroLabel.TabIndex = 0;
            this.invocationSpellIntroLabel.Text = "Choose your 2 \"Book of Ancient Secrets\" spells:";
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
            this.invocationSpellListBox.Size = new System.Drawing.Size(120, 225);
            this.invocationSpellListBox.TabIndex = 2;
            this.invocationSpellListBox.SelectedIndexChanged += new System.EventHandler(this.invocationSpellListBox_SelectedIndexChanged);
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
            this.invocationLayout.Size = new System.Drawing.Size(425, 262);
            this.invocationLayout.TabIndex = 2;
            // 
            // invocationListBox
            // 
            this.invocationListBox.FormattingEnabled = true;
            this.invocationListBox.Location = new System.Drawing.Point(3, 29);
            this.invocationListBox.Name = "invocationListBox";
            this.invocationListBox.Size = new System.Drawing.Size(120, 225);
            this.invocationListBox.TabIndex = 1;
            this.invocationListBox.SelectedIndexChanged += new System.EventHandler(this.invocationListBox_SelectedIndexChanged);
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
            // WarlockControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.warlockBox);
            this.Name = "WarlockControl";
            this.Size = new System.Drawing.Size(900, 634);
            this.warlockBox.ResumeLayout(false);
            this.warlockBox.PerformLayout();
            this.warlockLayout.ResumeLayout(false);
            this.pactBox.ResumeLayout(false);
            this.pactSpellLayout.ResumeLayout(false);
            this.pactSpellLayout.PerformLayout();
            this.pactLayout.ResumeLayout(false);
            this.pactLayout.PerformLayout();
            this.invocationBox.ResumeLayout(false);
            this.invocationSpellLayout.ResumeLayout(false);
            this.invocationSpellLayout.PerformLayout();
            this.invocationLayout.ResumeLayout(false);
            this.invocationLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox warlockBox;
        private System.Windows.Forms.FlowLayoutPanel warlockLayout;
        private System.Windows.Forms.GroupBox pactBox;
        private System.Windows.Forms.TableLayoutPanel pactSpellLayout;
        private System.Windows.Forms.Label pactSpellIntroLabel;
        private System.Windows.Forms.Label pactSpellDescriptionLabel;
        private System.Windows.Forms.ListBox pactSpellListBox;
        private System.Windows.Forms.TableLayoutPanel pactLayout;
        private System.Windows.Forms.ListBox pactListBox;
        private System.Windows.Forms.Label pactIntroLabel;
        private System.Windows.Forms.Label pactDescriptionLabel;
        private System.Windows.Forms.GroupBox invocationBox;
        private System.Windows.Forms.TableLayoutPanel invocationSpellLayout;
        private System.Windows.Forms.Label invocationSpellIntroLabel;
        private System.Windows.Forms.Label invocationSpellDescriptionLabel;
        private System.Windows.Forms.ListBox invocationSpellListBox;
        private System.Windows.Forms.TableLayoutPanel invocationLayout;
        private System.Windows.Forms.ListBox invocationListBox;
        private System.Windows.Forms.Label invocationIntroLabel;
        private System.Windows.Forms.Label invocationDescriptionLabel;
    }
}

namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class AlignmentControl
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
            this.alignmentBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.generalAlignmentLabel = new System.Windows.Forms.Label();
            this.raceAlignmentLabel = new System.Windows.Forms.Label();
            this.lawBox = new System.Windows.Forms.ListBox();
            this.moralityBox = new System.Windows.Forms.ListBox();
            this.chosenAlignmentLabel = new System.Windows.Forms.Label();
            this.alignmentBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // alignmentBox
            // 
            this.alignmentBox.Controls.Add(this.tableLayoutPanel1);
            this.alignmentBox.Location = new System.Drawing.Point(4, 4);
            this.alignmentBox.Name = "alignmentBox";
            this.alignmentBox.Size = new System.Drawing.Size(944, 188);
            this.alignmentBox.TabIndex = 0;
            this.alignmentBox.TabStop = false;
            this.alignmentBox.Text = "Alignment";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.40773F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.72961F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.86266F));
            this.tableLayoutPanel1.Controls.Add(this.generalAlignmentLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.raceAlignmentLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lawBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.moralityBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.chosenAlignmentLabel, 2, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(932, 163);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // generalAlignmentLabel
            // 
            this.generalAlignmentLabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.generalAlignmentLabel, 3);
            this.generalAlignmentLabel.Location = new System.Drawing.Point(3, 0);
            this.generalAlignmentLabel.Name = "generalAlignmentLabel";
            this.generalAlignmentLabel.Size = new System.Drawing.Size(149, 13);
            this.generalAlignmentLabel.TabIndex = 0;
            this.generalAlignmentLabel.Text = "General alignment description.";
            // 
            // raceAlignmentLabel
            // 
            this.raceAlignmentLabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.raceAlignmentLabel, 3);
            this.raceAlignmentLabel.Location = new System.Drawing.Point(3, 40);
            this.raceAlignmentLabel.Name = "raceAlignmentLabel";
            this.raceAlignmentLabel.Size = new System.Drawing.Size(135, 13);
            this.raceAlignmentLabel.TabIndex = 1;
            this.raceAlignmentLabel.Text = "Race alignment description";
            // 
            // lawBox
            // 
            this.lawBox.FormattingEnabled = true;
            this.lawBox.Location = new System.Drawing.Point(3, 83);
            this.lawBox.Name = "lawBox";
            this.lawBox.Size = new System.Drawing.Size(90, 69);
            this.lawBox.TabIndex = 2;
            this.lawBox.SelectedIndexChanged += new System.EventHandler(this.lawBox_SelectedIndexChanged);
            // 
            // moralityBox
            // 
            this.moralityBox.FormattingEnabled = true;
            this.moralityBox.Location = new System.Drawing.Point(100, 83);
            this.moralityBox.Name = "moralityBox";
            this.moralityBox.Size = new System.Drawing.Size(90, 69);
            this.moralityBox.TabIndex = 3;
            this.moralityBox.SelectedIndexChanged += new System.EventHandler(this.moralityBox_SelectedIndexChanged);
            // 
            // chosenAlignmentLabel
            // 
            this.chosenAlignmentLabel.AutoSize = true;
            this.chosenAlignmentLabel.Location = new System.Drawing.Point(199, 80);
            this.chosenAlignmentLabel.Name = "chosenAlignmentLabel";
            this.chosenAlignmentLabel.Size = new System.Drawing.Size(145, 13);
            this.chosenAlignmentLabel.TabIndex = 4;
            this.chosenAlignmentLabel.Text = "Chosen alignment description";
            // 
            // AlignmentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.alignmentBox);
            this.Name = "AlignmentControl";
            this.Size = new System.Drawing.Size(950, 200);
            this.alignmentBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox alignmentBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label generalAlignmentLabel;
        private System.Windows.Forms.Label raceAlignmentLabel;
        private System.Windows.Forms.ListBox lawBox;
        private System.Windows.Forms.ListBox moralityBox;
        private System.Windows.Forms.Label chosenAlignmentLabel;
    }
}

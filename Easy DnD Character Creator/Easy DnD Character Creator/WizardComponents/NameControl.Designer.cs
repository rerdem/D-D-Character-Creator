namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class NameControl
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
            this.nameBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.characterNameLabel = new System.Windows.Forms.Label();
            this.playerNameLabel = new System.Windows.Forms.Label();
            this.characterNameBox = new System.Windows.Forms.TextBox();
            this.playerNameBox = new System.Windows.Forms.TextBox();
            this.randomNameButton = new System.Windows.Forms.Button();
            this.sexLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.maleButton = new System.Windows.Forms.RadioButton();
            this.femaleButton = new System.Windows.Forms.RadioButton();
            this.nameBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.sexLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameBox
            // 
            this.nameBox.Controls.Add(this.tableLayoutPanel1);
            this.nameBox.Location = new System.Drawing.Point(4, 4);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(925, 111);
            this.nameBox.TabIndex = 0;
            this.nameBox.TabStop = false;
            this.nameBox.Text = "Name";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel1.Controls.Add(this.characterNameLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.playerNameLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.characterNameBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.playerNameBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.randomNameButton, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.sexLayout, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(912, 85);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // characterNameLabel
            // 
            this.characterNameLabel.AutoSize = true;
            this.characterNameLabel.Location = new System.Drawing.Point(3, 3);
            this.characterNameLabel.Margin = new System.Windows.Forms.Padding(3);
            this.characterNameLabel.Name = "characterNameLabel";
            this.characterNameLabel.Size = new System.Drawing.Size(87, 13);
            this.characterNameLabel.TabIndex = 0;
            this.characterNameLabel.Text = "Character Name:";
            // 
            // playerNameLabel
            // 
            this.playerNameLabel.AutoSize = true;
            this.playerNameLabel.Location = new System.Drawing.Point(3, 59);
            this.playerNameLabel.Margin = new System.Windows.Forms.Padding(3);
            this.playerNameLabel.Name = "playerNameLabel";
            this.playerNameLabel.Size = new System.Drawing.Size(70, 13);
            this.playerNameLabel.TabIndex = 1;
            this.playerNameLabel.Text = "Player Name:";
            // 
            // characterNameBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.characterNameBox, 3);
            this.characterNameBox.Location = new System.Drawing.Point(133, 3);
            this.characterNameBox.Name = "characterNameBox";
            this.characterNameBox.Size = new System.Drawing.Size(776, 20);
            this.characterNameBox.TabIndex = 3;
            this.characterNameBox.TextChanged += new System.EventHandler(this.characterNameBox_TextChanged);
            // 
            // playerNameBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.playerNameBox, 3);
            this.playerNameBox.Location = new System.Drawing.Point(133, 59);
            this.playerNameBox.Name = "playerNameBox";
            this.playerNameBox.Size = new System.Drawing.Size(776, 20);
            this.playerNameBox.TabIndex = 4;
            this.playerNameBox.TextChanged += new System.EventHandler(this.playerNameBox_TextChanged);
            // 
            // randomNameButton
            // 
            this.randomNameButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.randomNameButton.Location = new System.Drawing.Point(706, 31);
            this.randomNameButton.Name = "randomNameButton";
            this.randomNameButton.Size = new System.Drawing.Size(150, 22);
            this.randomNameButton.TabIndex = 5;
            this.randomNameButton.Text = "Random Name";
            this.randomNameButton.UseVisualStyleBackColor = true;
            this.randomNameButton.Click += new System.EventHandler(this.randomNameButton_Click);
            // 
            // sexLayout
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.sexLayout, 2);
            this.sexLayout.Controls.Add(this.maleButton);
            this.sexLayout.Controls.Add(this.femaleButton);
            this.sexLayout.Location = new System.Drawing.Point(133, 31);
            this.sexLayout.Name = "sexLayout";
            this.sexLayout.Size = new System.Drawing.Size(514, 22);
            this.sexLayout.TabIndex = 6;
            // 
            // maleButton
            // 
            this.maleButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.maleButton.AutoSize = true;
            this.maleButton.Location = new System.Drawing.Point(3, 3);
            this.maleButton.Name = "maleButton";
            this.maleButton.Size = new System.Drawing.Size(48, 17);
            this.maleButton.TabIndex = 0;
            this.maleButton.TabStop = true;
            this.maleButton.Text = "Male";
            this.maleButton.UseVisualStyleBackColor = true;
            // 
            // femaleButton
            // 
            this.femaleButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.femaleButton.AutoSize = true;
            this.femaleButton.Checked = true;
            this.femaleButton.Location = new System.Drawing.Point(57, 3);
            this.femaleButton.Name = "femaleButton";
            this.femaleButton.Size = new System.Drawing.Size(59, 17);
            this.femaleButton.TabIndex = 1;
            this.femaleButton.TabStop = true;
            this.femaleButton.Text = "Female";
            this.femaleButton.UseVisualStyleBackColor = true;
            // 
            // NameControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nameBox);
            this.Name = "NameControl";
            this.Size = new System.Drawing.Size(937, 123);
            this.nameBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.sexLayout.ResumeLayout(false);
            this.sexLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox nameBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label characterNameLabel;
        private System.Windows.Forms.Label playerNameLabel;
        private System.Windows.Forms.TextBox characterNameBox;
        private System.Windows.Forms.TextBox playerNameBox;
        private System.Windows.Forms.Button randomNameButton;
        private System.Windows.Forms.FlowLayoutPanel sexLayout;
        private System.Windows.Forms.RadioButton maleButton;
        private System.Windows.Forms.RadioButton femaleButton;
    }
}

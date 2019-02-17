namespace Easy_DnD_Character_Creator.WizardComponents
{
    partial class SpellControl
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
            this.spellBox = new System.Windows.Forms.GroupBox();
            this.cantripLayout = new System.Windows.Forms.TableLayoutPanel();
            this.spellLayout = new System.Windows.Forms.TableLayoutPanel();
            this.cantripLabel = new System.Windows.Forms.Label();
            this.availableCantrips = new System.Windows.Forms.ListBox();
            this.chosenCantrips = new System.Windows.Forms.ListBox();
            this.cantripAddButton = new System.Windows.Forms.Button();
            this.cantripRemoveButton = new System.Windows.Forms.Button();
            this.cantripDescriptionLabel = new System.Windows.Forms.Label();
            this.spellLabel = new System.Windows.Forms.Label();
            this.availableSpells = new System.Windows.Forms.ListBox();
            this.chosenSpells = new System.Windows.Forms.ListBox();
            this.spellAddButton = new System.Windows.Forms.Button();
            this.spellRemoveButton = new System.Windows.Forms.Button();
            this.spellDescriptionLabel = new System.Windows.Forms.Label();
            this.spellBox.SuspendLayout();
            this.cantripLayout.SuspendLayout();
            this.spellLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // spellBox
            // 
            this.spellBox.Controls.Add(this.spellLayout);
            this.spellBox.Controls.Add(this.cantripLayout);
            this.spellBox.Location = new System.Drawing.Point(4, 4);
            this.spellBox.Name = "spellBox";
            this.spellBox.Size = new System.Drawing.Size(953, 493);
            this.spellBox.TabIndex = 0;
            this.spellBox.TabStop = false;
            this.spellBox.Text = "Spells";
            // 
            // cantripLayout
            // 
            this.cantripLayout.ColumnCount = 3;
            this.cantripLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.cantripLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.cantripLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.cantripLayout.Controls.Add(this.cantripLabel, 0, 0);
            this.cantripLayout.Controls.Add(this.availableCantrips, 0, 1);
            this.cantripLayout.Controls.Add(this.chosenCantrips, 2, 1);
            this.cantripLayout.Controls.Add(this.cantripAddButton, 1, 1);
            this.cantripLayout.Controls.Add(this.cantripRemoveButton, 1, 2);
            this.cantripLayout.Controls.Add(this.cantripDescriptionLabel, 0, 3);
            this.cantripLayout.Location = new System.Drawing.Point(7, 20);
            this.cantripLayout.Name = "cantripLayout";
            this.cantripLayout.RowCount = 4;
            this.cantripLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.cantripLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.cantripLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.cantripLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.cantripLayout.Size = new System.Drawing.Size(940, 230);
            this.cantripLayout.TabIndex = 0;
            // 
            // spellLayout
            // 
            this.spellLayout.ColumnCount = 3;
            this.spellLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.spellLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.spellLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.spellLayout.Controls.Add(this.spellLabel, 0, 0);
            this.spellLayout.Controls.Add(this.availableSpells, 0, 1);
            this.spellLayout.Controls.Add(this.chosenSpells, 2, 1);
            this.spellLayout.Controls.Add(this.spellAddButton, 1, 1);
            this.spellLayout.Controls.Add(this.spellRemoveButton, 1, 2);
            this.spellLayout.Controls.Add(this.spellDescriptionLabel, 0, 3);
            this.spellLayout.Location = new System.Drawing.Point(7, 257);
            this.spellLayout.Name = "spellLayout";
            this.spellLayout.RowCount = 4;
            this.spellLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.spellLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.spellLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.spellLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.spellLayout.Size = new System.Drawing.Size(940, 230);
            this.spellLayout.TabIndex = 1;
            // 
            // cantripLabel
            // 
            this.cantripLabel.AutoSize = true;
            this.cantripLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantripLabel.Location = new System.Drawing.Point(3, 3);
            this.cantripLabel.Margin = new System.Windows.Forms.Padding(3);
            this.cantripLabel.Name = "cantripLabel";
            this.cantripLabel.Size = new System.Drawing.Size(57, 13);
            this.cantripLabel.TabIndex = 0;
            this.cantripLabel.Text = "Cantrips:";
            // 
            // availableCantrips
            // 
            this.availableCantrips.FormattingEnabled = true;
            this.availableCantrips.Location = new System.Drawing.Point(3, 29);
            this.availableCantrips.MultiColumn = true;
            this.availableCantrips.Name = "availableCantrips";
            this.cantripLayout.SetRowSpan(this.availableCantrips, 2);
            this.availableCantrips.Size = new System.Drawing.Size(370, 121);
            this.availableCantrips.TabIndex = 1;
            // 
            // chosenCantrips
            // 
            this.chosenCantrips.FormattingEnabled = true;
            this.chosenCantrips.Location = new System.Drawing.Point(567, 29);
            this.chosenCantrips.MultiColumn = true;
            this.chosenCantrips.Name = "chosenCantrips";
            this.cantripLayout.SetRowSpan(this.chosenCantrips, 2);
            this.chosenCantrips.Size = new System.Drawing.Size(370, 121);
            this.chosenCantrips.TabIndex = 2;
            // 
            // cantripAddButton
            // 
            this.cantripAddButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cantripAddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantripAddButton.Location = new System.Drawing.Point(440, 29);
            this.cantripAddButton.Name = "cantripAddButton";
            this.cantripAddButton.Size = new System.Drawing.Size(60, 60);
            this.cantripAddButton.TabIndex = 3;
            this.cantripAddButton.Text = ">";
            this.cantripAddButton.UseVisualStyleBackColor = true;
            // 
            // cantripRemoveButton
            // 
            this.cantripRemoveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cantripRemoveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantripRemoveButton.Location = new System.Drawing.Point(440, 96);
            this.cantripRemoveButton.Name = "cantripRemoveButton";
            this.cantripRemoveButton.Size = new System.Drawing.Size(60, 60);
            this.cantripRemoveButton.TabIndex = 4;
            this.cantripRemoveButton.Text = "<";
            this.cantripRemoveButton.UseVisualStyleBackColor = true;
            // 
            // cantripDescriptionLabel
            // 
            this.cantripDescriptionLabel.AutoSize = true;
            this.cantripLayout.SetColumnSpan(this.cantripDescriptionLabel, 3);
            this.cantripDescriptionLabel.Location = new System.Drawing.Point(3, 163);
            this.cantripDescriptionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.cantripDescriptionLabel.MaximumSize = new System.Drawing.Size(934, 64);
            this.cantripDescriptionLabel.Name = "cantripDescriptionLabel";
            this.cantripDescriptionLabel.Size = new System.Drawing.Size(93, 13);
            this.cantripDescriptionLabel.TabIndex = 5;
            this.cantripDescriptionLabel.Text = "cantrip description";
            // 
            // spellLabel
            // 
            this.spellLabel.AutoSize = true;
            this.spellLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spellLabel.Location = new System.Drawing.Point(3, 3);
            this.spellLabel.Margin = new System.Windows.Forms.Padding(3);
            this.spellLabel.Name = "spellLabel";
            this.spellLabel.Size = new System.Drawing.Size(45, 13);
            this.spellLabel.TabIndex = 0;
            this.spellLabel.Text = "Spells:";
            // 
            // availableSpells
            // 
            this.availableSpells.FormattingEnabled = true;
            this.availableSpells.Location = new System.Drawing.Point(3, 29);
            this.availableSpells.MultiColumn = true;
            this.availableSpells.Name = "availableSpells";
            this.spellLayout.SetRowSpan(this.availableSpells, 2);
            this.availableSpells.Size = new System.Drawing.Size(370, 121);
            this.availableSpells.TabIndex = 1;
            // 
            // chosenSpells
            // 
            this.chosenSpells.FormattingEnabled = true;
            this.chosenSpells.Location = new System.Drawing.Point(567, 29);
            this.chosenSpells.MultiColumn = true;
            this.chosenSpells.Name = "chosenSpells";
            this.spellLayout.SetRowSpan(this.chosenSpells, 2);
            this.chosenSpells.Size = new System.Drawing.Size(370, 121);
            this.chosenSpells.TabIndex = 2;
            // 
            // spellAddButton
            // 
            this.spellAddButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.spellAddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spellAddButton.Location = new System.Drawing.Point(440, 29);
            this.spellAddButton.Name = "spellAddButton";
            this.spellAddButton.Size = new System.Drawing.Size(60, 60);
            this.spellAddButton.TabIndex = 3;
            this.spellAddButton.Text = ">";
            this.spellAddButton.UseVisualStyleBackColor = true;
            // 
            // spellRemoveButton
            // 
            this.spellRemoveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.spellRemoveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spellRemoveButton.Location = new System.Drawing.Point(440, 96);
            this.spellRemoveButton.Name = "spellRemoveButton";
            this.spellRemoveButton.Size = new System.Drawing.Size(60, 60);
            this.spellRemoveButton.TabIndex = 4;
            this.spellRemoveButton.Text = "<";
            this.spellRemoveButton.UseVisualStyleBackColor = true;
            // 
            // spellDescriptionLabel
            // 
            this.spellDescriptionLabel.AutoSize = true;
            this.spellDescriptionLabel.Location = new System.Drawing.Point(3, 163);
            this.spellDescriptionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.spellDescriptionLabel.MaximumSize = new System.Drawing.Size(934, 64);
            this.spellDescriptionLabel.Name = "spellDescriptionLabel";
            this.spellDescriptionLabel.Size = new System.Drawing.Size(82, 13);
            this.spellDescriptionLabel.TabIndex = 5;
            this.spellDescriptionLabel.Text = "spell description";
            // 
            // SpellControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spellBox);
            this.Name = "SpellControl";
            this.Size = new System.Drawing.Size(960, 500);
            this.spellBox.ResumeLayout(false);
            this.cantripLayout.ResumeLayout(false);
            this.cantripLayout.PerformLayout();
            this.spellLayout.ResumeLayout(false);
            this.spellLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox spellBox;
        private System.Windows.Forms.TableLayoutPanel spellLayout;
        private System.Windows.Forms.TableLayoutPanel cantripLayout;
        private System.Windows.Forms.Label cantripLabel;
        private System.Windows.Forms.ListBox availableCantrips;
        private System.Windows.Forms.ListBox chosenCantrips;
        private System.Windows.Forms.Button cantripAddButton;
        private System.Windows.Forms.Button cantripRemoveButton;
        private System.Windows.Forms.Label cantripDescriptionLabel;
        private System.Windows.Forms.Label spellLabel;
        private System.Windows.Forms.ListBox availableSpells;
        private System.Windows.Forms.ListBox chosenSpells;
        private System.Windows.Forms.Button spellAddButton;
        private System.Windows.Forms.Button spellRemoveButton;
        private System.Windows.Forms.Label spellDescriptionLabel;
    }
}

namespace Wmp2iTunes
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tvSource = new System.Windows.Forms.TreeView();
            this.tvTarget = new System.Windows.Forms.TreeView();
            this.lblSource = new System.Windows.Forms.Label();
            this.cbSource = new System.Windows.Forms.ComboBox();
            this.cbTarget = new System.Windows.Forms.ComboBox();
            this.lblTarget = new System.Windows.Forms.Label();
            this.cbxReCreateTarget = new System.Windows.Forms.CheckBox();
            this.cbxDeleteOrphanTarget = new System.Windows.Forms.CheckBox();
            this.btnSync = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tvSource
            // 
            this.tvSource.CheckBoxes = true;
            this.tvSource.Location = new System.Drawing.Point(31, 97);
            this.tvSource.Name = "tvSource";
            this.tvSource.Size = new System.Drawing.Size(663, 922);
            this.tvSource.TabIndex = 0;
            this.tvSource.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvSource_AfterCheck);
            // 
            // tvTarget
            // 
            this.tvTarget.CheckBoxes = true;
            this.tvTarget.Location = new System.Drawing.Point(1067, 97);
            this.tvTarget.Name = "tvTarget";
            this.tvTarget.Size = new System.Drawing.Size(663, 922);
            this.tvTarget.TabIndex = 3;
            this.tvTarget.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvTarget_AfterCheck);
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(31, 17);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(66, 25);
            this.lblSource.TabIndex = 4;
            this.lblSource.Text = "Source";
            // 
            // cbSource
            // 
            this.cbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSource.FormattingEnabled = true;
            this.cbSource.Items.AddRange(new object[] {
            "-- Select Source --",
            "Windows Media Player (WMP)"});
            this.cbSource.Location = new System.Drawing.Point(31, 48);
            this.cbSource.Name = "cbSource";
            this.cbSource.Size = new System.Drawing.Size(663, 33);
            this.cbSource.TabIndex = 5;
            this.cbSource.SelectedIndexChanged += new System.EventHandler(this.cbSource_SelectedIndexChanged);
            // 
            // cbTarget
            // 
            this.cbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTarget.FormattingEnabled = true;
            this.cbTarget.Items.AddRange(new object[] {
            "-- Select Target --",
            "iTunes"});
            this.cbTarget.Location = new System.Drawing.Point(1067, 48);
            this.cbTarget.Name = "cbTarget";
            this.cbTarget.Size = new System.Drawing.Size(663, 33);
            this.cbTarget.TabIndex = 7;
            this.cbTarget.SelectedIndexChanged += new System.EventHandler(this.cbTarget_SelectedIndexChanged);
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.Location = new System.Drawing.Point(1067, 17);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(60, 25);
            this.lblTarget.TabIndex = 6;
            this.lblTarget.Text = "Target";
            // 
            // cbxReCreateTarget
            // 
            this.cbxReCreateTarget.AutoSize = true;
            this.cbxReCreateTarget.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbxReCreateTarget.Checked = true;
            this.cbxReCreateTarget.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxReCreateTarget.Enabled = false;
            this.cbxReCreateTarget.Location = new System.Drawing.Point(749, 202);
            this.cbxReCreateTarget.Name = "cbxReCreateTarget";
            this.cbxReCreateTarget.Size = new System.Drawing.Size(219, 54);
            this.cbxReCreateTarget.TabIndex = 8;
            this.cbxReCreateTarget.Text = "If Target playlist exists, \r\nDelete and Re-Create it";
            this.cbxReCreateTarget.UseVisualStyleBackColor = true;
            // 
            // cbxDeleteOrphanTarget
            // 
            this.cbxDeleteOrphanTarget.AutoSize = true;
            this.cbxDeleteOrphanTarget.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbxDeleteOrphanTarget.Enabled = false;
            this.cbxDeleteOrphanTarget.Location = new System.Drawing.Point(749, 319);
            this.cbxDeleteOrphanTarget.Name = "cbxDeleteOrphanTarget";
            this.cbxDeleteOrphanTarget.Size = new System.Drawing.Size(250, 79);
            this.cbxDeleteOrphanTarget.TabIndex = 9;
            this.cbxDeleteOrphanTarget.Text = "If Target has a playlist \r\nwhich Source doesn\'t, \r\nDelete the playlist in Target\r" +
    "\n";
            this.cbxDeleteOrphanTarget.UseVisualStyleBackColor = true;
            // 
            // btnSync
            // 
            this.btnSync.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSync.Location = new System.Drawing.Point(797, 952);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(171, 67);
            this.btnSync.TabIndex = 10;
            this.btnSync.Text = "Synchronize";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1767, 1055);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.cbxDeleteOrphanTarget);
            this.Controls.Add(this.cbxReCreateTarget);
            this.Controls.Add(this.cbTarget);
            this.Controls.Add(this.lblTarget);
            this.Controls.Add(this.cbSource);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.tvTarget);
            this.Controls.Add(this.tvSource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Media Library Synchronizer (MLS)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeView tvSource;
        private TreeView tvTarget;
        private Label lblSource;
        private ComboBox cbSource;
        private ComboBox cbTarget;
        private Label lblTarget;
        private CheckBox cbxReCreateTarget;
        private CheckBox cbxDeleteOrphanTarget;
        private Button btnSync;
    }
}
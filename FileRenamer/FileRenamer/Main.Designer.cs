namespace FileManager
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.SettingButton = new System.Windows.Forms.Button();
            this.SearchWords = new MaterialSkin.Controls.MaterialTextBox2();
            this.LoadProcessBar = new MaterialSkin.Controls.MaterialProgressBar();
            this.OpenFolderButton = new System.Windows.Forms.Button();
            this.FileListView = new System.Windows.Forms.ListView();
            this.CheckAllList = new System.Windows.Forms.ColumnHeader();
            this.FileNameList = new System.Windows.Forms.ColumnHeader();
            this.FileTypeList = new System.Windows.Forms.ColumnHeader();
            this.MakeDateList = new System.Windows.Forms.ColumnHeader();
            this.ModifedDateList = new System.Windows.Forms.ColumnHeader();
            this.FileSizeList = new System.Windows.Forms.ColumnHeader();
            this.WorkPathLabel = new System.Windows.Forms.Label();
            this.LabelPath = new System.Windows.Forms.Label();
            this.WorkSpacePanel = new System.Windows.Forms.Panel();
            this.StatusLabel2 = new System.Windows.Forms.Label();
            this.IsEmptyWorkSpaceLabel = new System.Windows.Forms.Label();
            this.StatusLabel1 = new System.Windows.Forms.Label();
            this.OpenExplorer = new System.Windows.Forms.Button();
            this.ReNameButton = new System.Windows.Forms.Button();
            this.DateChangeButton = new System.Windows.Forms.Button();
            this.DatePicker = new System.Windows.Forms.DateTimePicker();
            this.LabelSetDate = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateCheckToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeSetMasked = new System.Windows.Forms.MaskedTextBox();
            ContainExtensionSwitch = new MaterialSkin.Controls.MaterialSwitch();
            this.WorkSpacePanel.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingButton
            // 
            this.SettingButton.BackColor = System.Drawing.Color.Silver;
            this.SettingButton.FlatAppearance.BorderSize = 0;
            this.SettingButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.SettingButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.SettingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingButton.Location = new System.Drawing.Point(558, 138);
            this.SettingButton.Name = "SettingButton";
            this.SettingButton.Size = new System.Drawing.Size(40, 40);
            this.SettingButton.TabIndex = 5;
            this.SettingButton.UseVisualStyleBackColor = false;
            this.SettingButton.Click += new System.EventHandler(this.SettingButton_Click);
            // 
            // SearchWords
            // 
            this.SearchWords.AnimateReadOnly = false;
            this.SearchWords.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SearchWords.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.SearchWords.Depth = 0;
            this.SearchWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SearchWords.HideSelection = true;
            this.SearchWords.Hint = "검색어 입력";
            this.SearchWords.LeadingIcon = null;
            this.SearchWords.Location = new System.Drawing.Point(558, 84);
            this.SearchWords.MaxLength = 32767;
            this.SearchWords.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchWords.Name = "SearchWords";
            this.SearchWords.PasswordChar = '\0';
            this.SearchWords.PrefixSuffixText = null;
            this.SearchWords.ReadOnly = false;
            this.SearchWords.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SearchWords.SelectedText = "";
            this.SearchWords.SelectionLength = 0;
            this.SearchWords.SelectionStart = 0;
            this.SearchWords.ShortcutsEnabled = true;
            this.SearchWords.Size = new System.Drawing.Size(200, 48);
            this.SearchWords.TabIndex = 4;
            this.SearchWords.TabStop = false;
            this.SearchWords.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SearchWords.TrailingIcon = null;
            this.SearchWords.UseSystemPasswordChar = false;
            this.SearchWords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchWords_KeyDown);
            this.SearchWords.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchWords_KeyPress);
            this.SearchWords.TextChanged += new System.EventHandler(this.SearchWords_TextChanged);
            // 
            // LoadProcessBar
            // 
            this.LoadProcessBar.Depth = 0;
            this.LoadProcessBar.Location = new System.Drawing.Point(5, 432);
            this.LoadProcessBar.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoadProcessBar.Name = "LoadProcessBar";
            this.LoadProcessBar.Size = new System.Drawing.Size(544, 5);
            this.LoadProcessBar.Step = 1;
            this.LoadProcessBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.LoadProcessBar.TabIndex = 3;
            // 
            // OpenFolderButton
            // 
            this.OpenFolderButton.BackColor = System.Drawing.Color.AntiqueWhite;
            this.OpenFolderButton.FlatAppearance.BorderSize = 0;
            this.OpenFolderButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.OpenFolderButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.NavajoWhite;
            this.OpenFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenFolderButton.Location = new System.Drawing.Point(558, 39);
            this.OpenFolderButton.Name = "OpenFolderButton";
            this.OpenFolderButton.Size = new System.Drawing.Size(40, 40);
            this.OpenFolderButton.TabIndex = 1;
            this.OpenFolderButton.UseVisualStyleBackColor = false;
            this.OpenFolderButton.Click += new System.EventHandler(this.OpenFolderButton_Click);
            // 
            // FileListView
            // 
            this.FileListView.AllowColumnReorder = true;
            this.FileListView.CheckBoxes = true;
            this.FileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CheckAllList,
            this.FileNameList,
            this.FileTypeList,
            this.MakeDateList,
            this.ModifedDateList,
            this.FileSizeList});
            this.FileListView.FullRowSelect = true;
            this.FileListView.GridLines = true;
            this.FileListView.LabelWrap = false;
            this.FileListView.Location = new System.Drawing.Point(5, 20);
            this.FileListView.Name = "FileListView";
            this.FileListView.Size = new System.Drawing.Size(544, 400);
            this.FileListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.FileListView.TabIndex = 6;
            this.FileListView.UseCompatibleStateImageBehavior = false;
            this.FileListView.View = System.Windows.Forms.View.Details;
            this.FileListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.FileListView_ColumnClick);
            this.FileListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileListView_KeyDown);
            this.FileListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FileListView_MouseDown);
            // 
            // CheckAllList
            // 
            this.CheckAllList.Text = "*";
            this.CheckAllList.Width = 20;
            // 
            // FileNameList
            // 
            this.FileNameList.Text = "파일 이름 ";
            this.FileNameList.Width = 100;
            // 
            // FileTypeList
            // 
            this.FileTypeList.Text = "파일 유형 ";
            this.FileTypeList.Width = 80;
            // 
            // MakeDateList
            // 
            this.MakeDateList.Text = "만든 날짜 ";
            this.MakeDateList.Width = 100;
            // 
            // ModifedDateList
            // 
            this.ModifedDateList.Text = "수정한 날짜 ";
            this.ModifedDateList.Width = 100;
            // 
            // FileSizeList
            // 
            this.FileSizeList.Text = "파일 크기 ";
            this.FileSizeList.Width = 80;
            // 
            // WorkPathLabel
            // 
            this.WorkPathLabel.AutoSize = true;
            this.WorkPathLabel.BackColor = System.Drawing.Color.Transparent;
            this.WorkPathLabel.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.WorkPathLabel.Location = new System.Drawing.Point(115, 0);
            this.WorkPathLabel.Name = "WorkPathLabel";
            this.WorkPathLabel.Size = new System.Drawing.Size(0, 19);
            this.WorkPathLabel.TabIndex = 7;
            // 
            // LabelPath
            // 
            this.LabelPath.AutoSize = true;
            this.LabelPath.BackColor = System.Drawing.Color.Transparent;
            this.LabelPath.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelPath.Location = new System.Drawing.Point(5, 0);
            this.LabelPath.Name = "LabelPath";
            this.LabelPath.Size = new System.Drawing.Size(111, 19);
            this.LabelPath.TabIndex = 8;
            this.LabelPath.Text = "현재 작업 경로 :";
            // 
            // WorkSpacePanel
            // 
            this.WorkSpacePanel.Controls.Add(this.LabelPath);
            this.WorkSpacePanel.Controls.Add(this.StatusLabel2);
            this.WorkSpacePanel.Controls.Add(this.IsEmptyWorkSpaceLabel);
            this.WorkSpacePanel.Controls.Add(this.StatusLabel1);
            this.WorkSpacePanel.Controls.Add(this.WorkPathLabel);
            this.WorkSpacePanel.Controls.Add(this.FileListView);
            this.WorkSpacePanel.Controls.Add(this.LoadProcessBar);
            this.WorkSpacePanel.Location = new System.Drawing.Point(0, 30);
            this.WorkSpacePanel.Name = "WorkSpacePanel";
            this.WorkSpacePanel.Size = new System.Drawing.Size(552, 464);
            this.WorkSpacePanel.TabIndex = 9;
            // 
            // StatusLabel2
            // 
            this.StatusLabel2.AutoSize = true;
            this.StatusLabel2.Location = new System.Drawing.Point(85, 442);
            this.StatusLabel2.Name = "StatusLabel2";
            this.StatusLabel2.Size = new System.Drawing.Size(0, 15);
            this.StatusLabel2.TabIndex = 17;
            // 
            // IsEmptyWorkSpaceLabel
            // 
            this.IsEmptyWorkSpaceLabel.AutoSize = true;
            this.IsEmptyWorkSpaceLabel.BackColor = System.Drawing.Color.Transparent;
            this.IsEmptyWorkSpaceLabel.Location = new System.Drawing.Point(164, 185);
            this.IsEmptyWorkSpaceLabel.Name = "IsEmptyWorkSpaceLabel";
            this.IsEmptyWorkSpaceLabel.Size = new System.Drawing.Size(194, 30);
            this.IsEmptyWorkSpaceLabel.TabIndex = 16;
            this.IsEmptyWorkSpaceLabel.Text = "현재 설정된 작업 경로가 없습니다.\r\n새 작업 경로을 설정해주세요.";
            this.IsEmptyWorkSpaceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.AutoSize = true;
            this.StatusLabel1.Location = new System.Drawing.Point(5, 442);
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(54, 15);
            this.StatusLabel1.TabIndex = 9;
            this.StatusLabel1.Text = "0개 항목";
            // 
            // OpenExplorer
            // 
            this.OpenExplorer.BackColor = System.Drawing.Color.AntiqueWhite;
            this.OpenExplorer.FlatAppearance.BorderSize = 0;
            this.OpenExplorer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.OpenExplorer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.NavajoWhite;
            this.OpenExplorer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenExplorer.Location = new System.Drawing.Point(605, 39);
            this.OpenExplorer.Name = "OpenExplorer";
            this.OpenExplorer.Size = new System.Drawing.Size(40, 40);
            this.OpenExplorer.TabIndex = 1;
            this.OpenExplorer.UseVisualStyleBackColor = false;
            this.OpenExplorer.Click += new System.EventHandler(this.OpenExplorer_Click);
            // 
            // ReNameButton
            // 
            this.ReNameButton.Location = new System.Drawing.Point(558, 350);
            this.ReNameButton.Name = "ReNameButton";
            this.ReNameButton.Size = new System.Drawing.Size(180, 40);
            this.ReNameButton.TabIndex = 10;
            this.ReNameButton.Text = "선택한 파일 이름 일괄 변경";
            this.ReNameButton.UseVisualStyleBackColor = true;
            this.ReNameButton.Click += new System.EventHandler(this.ReNameButton_Click);
            // 
            // DateChangeButton
            // 
            this.DateChangeButton.Location = new System.Drawing.Point(558, 290);
            this.DateChangeButton.Name = "DateChangeButton";
            this.DateChangeButton.Size = new System.Drawing.Size(180, 40);
            this.DateChangeButton.TabIndex = 11;
            this.DateChangeButton.Text = "선택한 파일 시간 일괄 변경";
            this.DateChangeButton.UseVisualStyleBackColor = true;
            // 
            // DatePicker
            // 
            this.DatePicker.CustomFormat = "";
            this.DatePicker.Location = new System.Drawing.Point(558, 219);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Size = new System.Drawing.Size(180, 23);
            this.DatePicker.TabIndex = 12;
            this.DatePicker.Value = new System.DateTime(2023, 2, 22, 20, 18, 5, 0);
            // 
            // LabelSetDate
            // 
            this.LabelSetDate.AutoSize = true;
            this.LabelSetDate.Location = new System.Drawing.Point(558, 195);
            this.LabelSetDate.Name = "LabelSetDate";
            this.LabelSetDate.Size = new System.Drawing.Size(59, 15);
            this.LabelSetDate.TabIndex = 13;
            this.LabelSetDate.Text = "변경할 날짜 선택";
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileStripMenu,
            this.SettingStripMenu});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(780, 24);
            this.MenuStrip.TabIndex = 16;
            // 
            // FileStripMenu
            // 
            this.FileStripMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FileStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UpdateCheckToolStrip});
            this.FileStripMenu.Name = "FileStripMenu";
            this.FileStripMenu.Size = new System.Drawing.Size(43, 20);
            this.FileStripMenu.Text = "파일";
            // 
            // UpdateCheckToolStrip
            // 
            this.UpdateCheckToolStrip.Name = "UpdateCheckToolStrip";
            this.UpdateCheckToolStrip.Size = new System.Drawing.Size(150, 22);
            this.UpdateCheckToolStrip.Text = "업데이트 확인";
            // 
            // SettingStripMenu
            // 
            this.SettingStripMenu.Name = "SettingStripMenu";
            this.SettingStripMenu.Size = new System.Drawing.Size(43, 20);
            this.SettingStripMenu.Text = "설정";
            // 
            // TimeSetMasked
            // 
            this.TimeSetMasked.Location = new System.Drawing.Point(678, 250);
            this.TimeSetMasked.Mask = "90시90분";
            this.TimeSetMasked.Name = "TimeSetMasked";
            this.TimeSetMasked.Size = new System.Drawing.Size(60, 23);
            this.TimeSetMasked.TabIndex = 17;
            this.TimeSetMasked.ValidatingType = typeof(System.DateTime);
            // 
            // ContainExtensionSwitch
            // 
            ContainExtensionSwitch.AutoSize = true;
            ContainExtensionSwitch.Depth = 0;
            ContainExtensionSwitch.Location = new Point(570, 420);
            ContainExtensionSwitch.Margin = new Padding(0);
            ContainExtensionSwitch.MouseLocation = new Point(-1, -1);
            ContainExtensionSwitch.MouseState = MaterialSkin.MouseState.HOVER;
            ContainExtensionSwitch.Name = "ContainExtensionSwitch";
            ContainExtensionSwitch.Ripple = true;
            ContainExtensionSwitch.Size = new Size(150, 37);
            ContainExtensionSwitch.TabIndex = 0;
            ContainExtensionSwitch.Text = "확장자 포함 변경";
            ContainExtensionSwitch.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(780, 500);
            this.Controls.Add(this.TimeSetMasked);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.LabelSetDate);
            this.Controls.Add(this.DatePicker);
            this.Controls.Add(this.DateChangeButton);
            this.Controls.Add(this.ReNameButton);
            this.Controls.Add(this.OpenExplorer);
            this.Controls.Add(this.OpenFolderButton);
            this.Controls.Add(this.SettingButton);
            this.Controls.Add(this.SearchWords);
            this.Controls.Add(this.WorkSpacePanel);
            this.Controls.Add(this.ContainExtensionSwitch);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(766, 489);
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "파일 관리자";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.WorkSpacePanel.ResumeLayout(false);
            this.WorkSpacePanel.PerformLayout();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button SettingButton;
        private MaterialSkin.Controls.MaterialTextBox2 SearchWords;
        private MaterialSkin.Controls.MaterialProgressBar LoadProcessBar;
        private MaterialSkin.Controls.MaterialSwitch ContainExtensionSwitch;
        private Button OpenFolderButton;
        private ListView FileListView;
        private Label WorkPathLabel;
        private Label LabelPath;
        private Panel WorkSpacePanel;
        private Label StatusLabel1;
        private Button OpenExplorer;
        private Button ReNameButton;
        private Button DateChangeButton;
        private DateTimePicker DatePicker;
        private Label LabelSetDate;
        private Label IsEmptyWorkSpaceLabel;
        private ColumnHeader FileNameList;
        private ColumnHeader FileTypeList;
        private ColumnHeader MakeDateList;
        private ColumnHeader ModifedDateList;
        private ColumnHeader FileSizeList;
        private Label StatusLabel2;
        private ColumnHeader CheckAllList;
        private MenuStrip MenuStrip;
        private ToolStripMenuItem FileStripMenu;
        private MaskedTextBox TimeSetMasked;
        private ToolStripMenuItem UpdateCheckToolStrip;
        private ToolStripMenuItem SettingStripMenu;
    }
}
namespace FileRenamer
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
            SettingButton = new Button();
            SearchWords = new MaterialSkin.Controls.MaterialTextBox2();
            LoadProcessBar = new MaterialSkin.Controls.MaterialProgressBar();
            OpenFolderButton = new Button();
            FileListView = new ListView();
            CheckAllList = new ColumnHeader();
            FileNameList = new ColumnHeader();
            FileTypeList = new ColumnHeader();
            MakeDateList = new ColumnHeader();
            ModifedDateList = new ColumnHeader();
            FileSizeList = new ColumnHeader();
            WorkPathLabel = new Label();
            LabelPath = new Label();
            WorkSpacePanel = new Panel();
            StatusLabel2 = new Label();
            IsEmptyWorkSpaceLabel = new Label();
            StatusLabel1 = new Label();
            OpenExplorer = new Button();
            ReNameButton = new Button();
            DateChangeButton = new Button();
            DatePicker = new DateTimePicker();
            LabelSetDate = new Label();
            MenuStrip = new MenuStrip();
            FileStripMenu = new ToolStripMenuItem();
            UpdateCheckToolStrip = new ToolStripMenuItem();
            SettingStripMenu = new ToolStripMenuItem();
            TimeSetMasked = new MaskedTextBox();
            ContainExtensionSwitch = new MaterialSkin.Controls.MaterialSwitch();
            WorkSpacePanel.SuspendLayout();
            MenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // SettingButton
            // 
            SettingButton.BackColor = Color.Silver;
            SettingButton.FlatAppearance.BorderSize = 0;
            SettingButton.FlatAppearance.MouseDownBackColor = Color.Gray;
            SettingButton.FlatAppearance.MouseOverBackColor = Color.DarkGray;
            SettingButton.FlatStyle = FlatStyle.Flat;
            SettingButton.Location = new Point(558, 138);
            SettingButton.Name = "SettingButton";
            SettingButton.Size = new Size(40, 40);
            SettingButton.TabIndex = 5;
            SettingButton.UseVisualStyleBackColor = false;
            SettingButton.Click += SettingButton_Click;
            // 
            // SearchWords
            // 
            SearchWords.AnimateReadOnly = false;
            SearchWords.BackgroundImageLayout = ImageLayout.None;
            SearchWords.CharacterCasing = CharacterCasing.Normal;
            SearchWords.Depth = 0;
            SearchWords.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            SearchWords.HideSelection = true;
            SearchWords.Hint = "검색어 입력";
            SearchWords.LeadingIcon = null;
            SearchWords.Location = new Point(558, 84);
            SearchWords.MaxLength = 32767;
            SearchWords.MouseState = MaterialSkin.MouseState.OUT;
            SearchWords.Name = "SearchWords";
            SearchWords.PasswordChar = '\0';
            SearchWords.PrefixSuffixText = null;
            SearchWords.ReadOnly = false;
            SearchWords.RightToLeft = RightToLeft.No;
            SearchWords.SelectedText = "";
            SearchWords.SelectionLength = 0;
            SearchWords.SelectionStart = 0;
            SearchWords.ShortcutsEnabled = true;
            SearchWords.Size = new Size(200, 48);
            SearchWords.TabIndex = 4;
            SearchWords.TabStop = false;
            SearchWords.TextAlign = HorizontalAlignment.Left;
            SearchWords.TrailingIcon = null;
            SearchWords.UseSystemPasswordChar = false;
            SearchWords.KeyDown += SearchWords_KeyDown;
            SearchWords.KeyPress += SearchWords_KeyPress;
            SearchWords.TextChanged += SearchWords_TextChanged;
            // 
            // LoadProcessBar
            // 
            LoadProcessBar.Depth = 0;
            LoadProcessBar.Location = new Point(5, 432);
            LoadProcessBar.MouseState = MaterialSkin.MouseState.HOVER;
            LoadProcessBar.Name = "LoadProcessBar";
            LoadProcessBar.Size = new Size(544, 5);
            LoadProcessBar.Step = 1;
            LoadProcessBar.Style = ProgressBarStyle.Continuous;
            LoadProcessBar.TabIndex = 3;
            // 
            // OpenFolderButton
            // 
            OpenFolderButton.BackColor = Color.AntiqueWhite;
            OpenFolderButton.FlatAppearance.BorderSize = 0;
            OpenFolderButton.FlatAppearance.MouseDownBackColor = Color.LightGray;
            OpenFolderButton.FlatAppearance.MouseOverBackColor = Color.NavajoWhite;
            OpenFolderButton.FlatStyle = FlatStyle.Flat;
            OpenFolderButton.Location = new Point(558, 39);
            OpenFolderButton.Name = "OpenFolderButton";
            OpenFolderButton.Size = new Size(40, 40);
            OpenFolderButton.TabIndex = 1;
            OpenFolderButton.UseVisualStyleBackColor = false;
            OpenFolderButton.Click += OpenFolderButton_Click;
            // 
            // FileListView
            // 
            FileListView.AllowColumnReorder = true;
            FileListView.AllowDrop = true;
            FileListView.CheckBoxes = true;
            FileListView.Columns.AddRange(new ColumnHeader[] { CheckAllList, FileNameList, FileTypeList, MakeDateList, ModifedDateList, FileSizeList });
            FileListView.FullRowSelect = true;
            FileListView.GridLines = true;
            FileListView.LabelWrap = false;
            FileListView.Location = new Point(5, 20);
            FileListView.Name = "FileListView";
            FileListView.Size = new Size(544, 400);
            FileListView.Sorting = SortOrder.Ascending;
            FileListView.TabIndex = 6;
            FileListView.UseCompatibleStateImageBehavior = false;
            FileListView.View = View.Details;
            FileListView.ColumnClick += FileListView_ColumnClick;
            FileListView.DragDrop += FileListView_DragDrop;
            FileListView.DragEnter += FileListView_DragEnter;
            FileListView.KeyDown += FileListView_KeyDown;
            FileListView.MouseDown += FileListView_MouseDown;
            // 
            // CheckAllList
            // 
            CheckAllList.Text = "*";
            CheckAllList.Width = 20;
            // 
            // FileNameList
            // 
            FileNameList.Text = "파일 이름 ";
            FileNameList.Width = 100;
            // 
            // FileTypeList
            // 
            FileTypeList.Text = "파일 유형 ";
            FileTypeList.Width = 80;
            // 
            // MakeDateList
            // 
            MakeDateList.Text = "만든 날짜 ";
            MakeDateList.Width = 100;
            // 
            // ModifedDateList
            // 
            ModifedDateList.Text = "수정한 날짜 ";
            ModifedDateList.Width = 100;
            // 
            // FileSizeList
            // 
            FileSizeList.Text = "파일 크기 ";
            FileSizeList.Width = 80;
            // 
            // WorkPathLabel
            // 
            WorkPathLabel.AutoSize = true;
            WorkPathLabel.BackColor = Color.Transparent;
            WorkPathLabel.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            WorkPathLabel.Location = new Point(115, 0);
            WorkPathLabel.Name = "WorkPathLabel";
            WorkPathLabel.Size = new Size(0, 19);
            WorkPathLabel.TabIndex = 7;
            // 
            // LabelPath
            // 
            LabelPath.AutoSize = true;
            LabelPath.BackColor = Color.Transparent;
            LabelPath.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            LabelPath.Location = new Point(5, 0);
            LabelPath.Name = "LabelPath";
            LabelPath.Size = new Size(111, 19);
            LabelPath.TabIndex = 8;
            LabelPath.Text = "현재 작업 경로 :";
            // 
            // WorkSpacePanel
            // 
            WorkSpacePanel.Controls.Add(LabelPath);
            WorkSpacePanel.Controls.Add(StatusLabel2);
            WorkSpacePanel.Controls.Add(IsEmptyWorkSpaceLabel);
            WorkSpacePanel.Controls.Add(StatusLabel1);
            WorkSpacePanel.Controls.Add(WorkPathLabel);
            WorkSpacePanel.Controls.Add(FileListView);
            WorkSpacePanel.Controls.Add(LoadProcessBar);
            WorkSpacePanel.Location = new Point(0, 30);
            WorkSpacePanel.Name = "WorkSpacePanel";
            WorkSpacePanel.Size = new Size(552, 464);
            WorkSpacePanel.TabIndex = 9;
            // 
            // StatusLabel2
            // 
            StatusLabel2.AutoSize = true;
            StatusLabel2.Location = new Point(85, 442);
            StatusLabel2.Name = "StatusLabel2";
            StatusLabel2.Size = new Size(0, 15);
            StatusLabel2.TabIndex = 17;
            // 
            // IsEmptyWorkSpaceLabel
            // 
            IsEmptyWorkSpaceLabel.AutoSize = true;
            IsEmptyWorkSpaceLabel.BackColor = Color.Transparent;
            IsEmptyWorkSpaceLabel.Location = new Point(164, 185);
            IsEmptyWorkSpaceLabel.Name = "IsEmptyWorkSpaceLabel";
            IsEmptyWorkSpaceLabel.Size = new Size(194, 30);
            IsEmptyWorkSpaceLabel.TabIndex = 16;
            IsEmptyWorkSpaceLabel.Text = "현재 설정된 작업 경로가 없습니다.\r\n새 작업 경로을 설정해주세요.";
            IsEmptyWorkSpaceLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // StatusLabel1
            // 
            StatusLabel1.AutoSize = true;
            StatusLabel1.Location = new Point(5, 442);
            StatusLabel1.Name = "StatusLabel1";
            StatusLabel1.Size = new Size(54, 15);
            StatusLabel1.TabIndex = 9;
            StatusLabel1.Text = "0개 항목";
            // 
            // OpenExplorer
            // 
            OpenExplorer.BackColor = Color.AntiqueWhite;
            OpenExplorer.FlatAppearance.BorderSize = 0;
            OpenExplorer.FlatAppearance.MouseDownBackColor = Color.LightGray;
            OpenExplorer.FlatAppearance.MouseOverBackColor = Color.NavajoWhite;
            OpenExplorer.FlatStyle = FlatStyle.Flat;
            OpenExplorer.Location = new Point(605, 39);
            OpenExplorer.Name = "OpenExplorer";
            OpenExplorer.Size = new Size(40, 40);
            OpenExplorer.TabIndex = 1;
            OpenExplorer.UseVisualStyleBackColor = false;
            OpenExplorer.Click += OpenExplorer_Click;
            // 
            // ReNameButton
            // 
            ReNameButton.Location = new Point(558, 350);
            ReNameButton.Name = "ReNameButton";
            ReNameButton.Size = new Size(180, 40);
            ReNameButton.TabIndex = 10;
            ReNameButton.Text = "선택한 파일 이름 일괄 변경";
            ReNameButton.UseVisualStyleBackColor = true;
            ReNameButton.Click += ReNameButton_Click;
            // 
            // DateChangeButton
            // 
            DateChangeButton.Location = new Point(558, 290);
            DateChangeButton.Name = "DateChangeButton";
            DateChangeButton.Size = new Size(180, 40);
            DateChangeButton.TabIndex = 11;
            DateChangeButton.Text = "선택한 파일 시간 일괄 변경";
            DateChangeButton.UseVisualStyleBackColor = true;
            DateChangeButton.Click += DateChangeButton_Click;
            // 
            // DatePicker
            // 
            DatePicker.CustomFormat = "";
            DatePicker.Location = new Point(558, 219);
            DatePicker.Name = "DatePicker";
            DatePicker.Size = new Size(180, 23);
            DatePicker.TabIndex = 12;
            DatePicker.Value = new DateTime(2023, 2, 22, 20, 20, 0, 0);
            // 
            // LabelSetDate
            // 
            LabelSetDate.AutoSize = true;
            LabelSetDate.Location = new Point(558, 195);
            LabelSetDate.Name = "LabelSetDate";
            LabelSetDate.Size = new Size(99, 15);
            LabelSetDate.TabIndex = 13;
            LabelSetDate.Text = "변경할 날짜 선택";
            // 
            // MenuStrip
            // 
            MenuStrip.BackColor = SystemColors.Control;
            MenuStrip.Items.AddRange(new ToolStripItem[] { FileStripMenu, SettingStripMenu });
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Size = new Size(780, 24);
            MenuStrip.TabIndex = 16;
            // 
            // FileStripMenu
            // 
            FileStripMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
            FileStripMenu.DropDownItems.AddRange(new ToolStripItem[] { UpdateCheckToolStrip });
            FileStripMenu.Name = "FileStripMenu";
            FileStripMenu.Size = new Size(43, 20);
            FileStripMenu.Text = "파일";
            // 
            // UpdateCheckToolStrip
            // 
            UpdateCheckToolStrip.Name = "UpdateCheckToolStrip";
            UpdateCheckToolStrip.Size = new Size(150, 22);
            UpdateCheckToolStrip.Text = "업데이트 확인";
            // 
            // SettingStripMenu
            // 
            SettingStripMenu.Name = "SettingStripMenu";
            SettingStripMenu.Size = new Size(43, 20);
            SettingStripMenu.Text = "설정";
            // 
            // TimeSetMasked
            // 
            TimeSetMasked.Location = new Point(678, 250);
            TimeSetMasked.Mask = "90시00분";
            TimeSetMasked.Name = "TimeSetMasked";
            TimeSetMasked.Size = new Size(60, 23);
            TimeSetMasked.TabIndex = 17;
            TimeSetMasked.ValidatingType = typeof(DateTime);
            TimeSetMasked.KeyPress += TimeSetMasked_KeyPress;
            TimeSetMasked.Validating += TimeSetMasked_Validating;
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
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(780, 500);
            Controls.Add(TimeSetMasked);
            Controls.Add(MenuStrip);
            Controls.Add(LabelSetDate);
            Controls.Add(DatePicker);
            Controls.Add(DateChangeButton);
            Controls.Add(ReNameButton);
            Controls.Add(OpenExplorer);
            Controls.Add(OpenFolderButton);
            Controls.Add(SettingButton);
            Controls.Add(SearchWords);
            Controls.Add(WorkSpacePanel);
            Controls.Add(ContainExtensionSwitch);
            ForeColor = SystemColors.WindowText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = MenuStrip;
            MinimumSize = new Size(766, 489);
            Name = "Main";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "파일 관리자";
            FormClosing += Main_FormClosing;
            Load += Main_Load;
            Resize += Main_Resize;
            WorkSpacePanel.ResumeLayout(false);
            WorkSpacePanel.PerformLayout();
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
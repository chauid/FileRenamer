using FindImage;
using MaterialSkin;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

// TODO LIST v0.9
// Rename.cs 특수문자 예외 처리 
// 정규식 오타 예외 처리
// 정규식 유형 자동 완성
// 복수선택 이름 변경시 확장자 포함 변경 선택 기능 추가
// 특정 단어 앞, 또는 뒤에 문자열 추가
// Ctrl + Z 기능 추가 

namespace FileManager
{
    /// <summary>
    /// <para>이름 정규식 유형 : {추가, 삭제, 대체, 새이름 정의, 숫자증분}</para>
    /// <para>정규식 구분자 : "{}"</para>
    /// </summary>
    public enum RegularType
    {
        /// <summary>
        /// 문자열 추가. 정규식 활용:{Append:"추가할 문자열", 추가할 위치(int), 인덱싱순서(Boolean)}
        /// </summary>
        Append = 0,

        /// <summary>
        /// 문자열 삭제. 정규식 활용:{Delete:삭제할 범위(int), 삭제할 위치(int), 인덱싱순서(Boolean)}
        /// </summary>
        Delete = 1,

        /// <summary>
        /// 문자열 대체. 정규식 활용:{Replace:"찾을 문자열", "대체할 문자열"}
        /// </summary>
        Replace = 2,

        /// <summary>
        /// 새로운 문자열 추가. 정규식 활용 : {NewNameSet:"새로운 문자열"}
        /// </summary>
        NewNameSet = 3,

        /// <summary>
        /// 숫자 자동 증가. 정규식 활용 : {AutoIncrement:시작값(int), 증분값(int)}
        /// </summary>
        AutoIncrement = 4
    }
    public partial class Main : Form
    {
        public const string Version = "0.9";

        /// <summary>
        /// ListView 우클릭 메뉴 
        /// </summary>
        private ContextMenuStrip? FileListMenu;

        /// <summary>
        /// 항목별 아이템 리스트{파일명, 유형, 만든 날짜, 수정한 날짜, 크기} 
        /// </summary>
        private List<ListViewItem> FileItemInfo = new();

        /// <summary>
        /// 현재 폴더 경로 
        /// </summary>
        private string? FolderPath;

        /// <summary>
        /// 불러온 파일 이름 리스트(string) 
        /// </summary>
        private string[]? FileList;

        /// <summary>
        /// 새 폴더 이름 
        /// </summary>
        private string NewFileName = "직박구리";

        /// <summary>
        /// 이름 바꾸기 상태  
        /// </summary>
        private bool RenameState;

        /// <summary>
        /// FileListView 선택 개수 Update 
        /// </summary>
        private System.Windows.Forms.Timer? CheckedBoxTimer;

        private BackgroundWorker Worker;
        private int ProgressCounter;
        private MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;

        #region Main 
        public Main()
        {
            InitializeComponent();
            MainUILayout(1);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue500, Primary.BlueGrey800, Primary.BlueGrey800, Accent.DeepPurple700, TextShade.BLACK);
            Worker = new();
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;
            Worker.DoWork += Worker_DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            CheckedBoxTimer = new();
            CheckedBoxTimer.Interval = 34; // 30FPS 
            CheckedBoxTimer.Tick += CheckedBoxTimer_Tick;
            CheckedBoxTimer.Enabled = true;
            FileListView.Select();

            // 종료 이벤트 
            foreach (Control controls in this.Controls)
            {
                controls.KeyDown += KeyDown_Close;
            }
        }
        private void Main_Load(object sender, EventArgs e) // 초기 화면 설정 
        {
            ToolTip toolTip = new();
            toolTip.AutoPopDelay = 3000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;

            /* FileListView */
            FileListView.Columns[1].Text += '▽';
            FileListView.ListViewItemSorter = new ListViewItemComparer(1, "asc"); // cols:1 = 파일명 

            /* OpenFolderButton */
            OpenFolderButton.Image = ImageFinder.GetIamgeFile("AddFolder.png", 25, 25);
            toolTip.SetToolTip(OpenFolderButton, "작업 경로 설정하기");

            /* SettingButton */
            SettingButton.Image = ImageFinder.GetIamgeFile("Setting.png", 25, 25);
            toolTip.SetToolTip(SettingButton, "설정");

            /* SearchWords */
            SearchWords.TrailingIcon = ImageFinder.GetIamgeFile("Search.png", 32, 32);

            /* OpenExplorer */
            OpenExplorer.Image = ImageFinder.GetIamgeFile("OpenFolder.png", 25, 25);
            toolTip.SetToolTip(OpenExplorer, "파일 탐색기로 열기");

            /* IsEmptyWorkSpaceLabel */
            if (WorkPathLabel.Text == string.Empty) IsEmptyWorkSpaceLabel.Show();
            else IsEmptyWorkSpaceLabel.Hide();
        }
        private void Main_Resize(object sender, EventArgs e) { MainUILayout(1); } // 화면 크기 조절 

        private void CheckedBoxTimer_Tick(object? sender, EventArgs e)
        {
            if (FileListView.CheckedItems.Count > 0)
            {
                StatusLabel2.Location = new Point(StatusLabel1.Location.X + StatusLabel1.Width, StatusLabel1.Location.Y);
                StatusLabel2.Text = string.Format("{0}개 선택함", FileListView.CheckedItems.Count);
            }
            else StatusLabel2.Text = string.Empty;
        }

        /// <summary>
        /// 메뉴화면 UI 그리기 
        /// </summary>
        /// <param name="Selection">0:WorkSpaceUI만 그리기, 1:모두 다시 그리기</param>
        private void MainUILayout(int Selection) // 0: WorkSpaceUI, 1: 모두 재설정 
        {
            if (Selection > -1)
            {
                /* WorkSpace UI */
                WorkSpacePanel.Size = new Size(ClientSize.Width - 220, ClientSize.Height - 25);
                FileListView.Size = new Size(WorkSpacePanel.Width - 10, WorkSpacePanel.Height - 60);
                IsEmptyWorkSpaceLabel.Location = new Point(FileListView.Width / 2 - IsEmptyWorkSpaceLabel.Width / 2 + 5, FileListView.Height / 2 - IsEmptyWorkSpaceLabel.Height / 2 + 20);
                LoadProcessBar.Location = new Point(5, FileListView.Location.Y + FileListView.Height + 5);
                LoadProcessBar.Size = new Size(FileListView.Width, 5);
                StatusLabel1.Location = new Point(5, LoadProcessBar.Location.Y + 10);
                StatusLabel2.Location = new Point(StatusLabel1.Location.X + StatusLabel1.Width + 5, LoadProcessBar.Location.Y + 10);
            }
            if (Selection > 0)
            {
                /* 외부 UI */
                OpenFolderButton.Location = new Point(WorkSpacePanel.Width + 5, 40);
                OpenExplorer.Location = new Point(OpenFolderButton.Location.X + OpenFolderButton.Width + 5, 40);
                SearchWords.Location = new Point(WorkSpacePanel.Width + 5, 85);
                SettingButton.Location = new Point(WorkSpacePanel.Width + 5, 140);
                LabelSetDate.Location = new Point(WorkSpacePanel.Width + 5, 200);
                DatePicker.Location = new Point(WorkSpacePanel.Width + 5, 220);
                TimeSetMasked.Location = new Point(DatePicker.Location.X + DatePicker.Width - TimeSetMasked.Width, 250);
                DateChangeButton.Location = new Point(WorkSpacePanel.Width + 5, 290);
                ReNameButton.Location = new Point(WorkSpacePanel.Width + 5, 350);
            }
        }
        private void KeyDown_Close(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || (ModifierKeys == Keys.Control && e.KeyCode == Keys.W)) { this.Close(); return; }
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e) // 종료 -> BackgroundWorker 종료 
        {
            if (Worker != null) if (Worker.IsBusy) Worker.CancelAsync();
            if (MessageBox.Show("프로그램을 종료하시겠습니까?", "종료", MessageBoxButtons.OKCancel) == DialogResult.OK) e.Cancel = false;
            else e.Cancel = true;
        }
        #endregion

        #region BackgroundWorker 
        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            string FolderPath;
            string[] SearchFile;
            int FileListCount = 0;
            if (e.Argument is not null) FolderPath = e.Argument.ToString()!;
            else FolderPath = @"C:\";
            DirectoryInfo SelectDirectory = new(FolderPath);
            SearchFile = Directory.GetFiles(SelectDirectory.ToString());
            for (int i = 0; i < SearchFile.Length; i++)
            {
                FileInfo files = new(SearchFile[i]);
                if (!files.Attributes.HasFlag(FileAttributes.Hidden)) FileListCount++;
            }
            FileList = new string[FileListCount];
            FileListCount = 0;
            for (int i = 0; i < SearchFile.Length; i++)
            {
                FileInfo files = new(SearchFile[i]);
                if (!files.Attributes.HasFlag(FileAttributes.Hidden)) FileList[FileListCount++] = SearchFile[i];
            }
            Console.WriteLine("Files : {0}", FileList.Length);
            FileItemInfo.Clear();
            double ProgressPercentage;
            for (int i = 0; i < FileList.Length; i++)
            {
                FileInfo FilesInfo = new(FileList[i]);
                if (!FilesInfo.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    long FileSize = FilesInfo.Length;
                    if (FileSize >= 1024) FileSize /= 1024; // kilobyte 
                    else if (FileSize == 0) FileSize = 0;
                    else FileSize = 1;
                    string[] ItemInfo = { "", FilesInfo.Name, FilesInfo.Extension, FilesInfo.CreationTime.ToString(), FilesInfo.LastWriteTime.ToString(), string.Format($"{FileSize:#,####0}KB") };
                    ListViewItem item = new ListViewItem(ItemInfo);
                    FileItemInfo.Add(item);
                    ProgressPercentage = ++ProgressCounter / (double)FileList.Length * 100;
                    Worker.ReportProgress((int)ProgressPercentage);
                }
            }
            if (Worker.CancellationPending == true) { e.Cancel = true; return; }
        }
        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            LoadProcessBar.Value = e.ProgressPercentage;
            StatusLabel1.Text = string.Format("불러오는 중... {0}%", e.ProgressPercentage);
        }
        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (FileList != null) StatusLabel1.Text = string.Format("{0}개 항목", FileList.Length);
            if (FileItemInfo != null) FileListView.Items.AddRange(FileItemInfo.ToArray());
            ProgressCounter = 0;
        }
        #endregion

        #region FileListView 
        private void FileListView_ColumnClick(object sender, ColumnClickEventArgs e) // FileListView Column 클릭(재정렬) 
        {
            if (e.Column == 0)
            {
                bool IsSelectAll = true;
                foreach (ListViewItem item in FileListView.Items) { if (!item.Checked) { IsSelectAll = false; } }
                if (IsSelectAll) foreach (ListViewItem item in FileListView.Items) { item.Checked = false; }
                else foreach (ListViewItem item in FileListView.Items) { item.Checked = true; }
            }
            else
            {
                if (FileListView.Columns[e.Column].Text.Contains('▽'))
                {
                    FileListView.ListViewItemSorter = new ListViewItemComparer(e.Column, "desc");
                    Console.WriteLine("Desc");
                    foreach (ColumnHeader Columns in FileListView.Columns)
                    {
                        if (Columns.Index != 0)
                        {
                            Columns.Text = Columns.Text.Replace("▽", "");
                            Columns.Text = Columns.Text.Replace("△", "");
                        }
                    }
                    FileListView.Columns[e.Column].Text += "△";
                }
                else
                {
                    FileListView.ListViewItemSorter = new ListViewItemComparer(e.Column, "asc");
                    Console.WriteLine("Asc");
                    foreach (ColumnHeader Columns in FileListView.Columns)
                    {
                        if (Columns.Index != 0)
                        {
                            Columns.Text = Columns.Text.Replace("▽", "");
                            Columns.Text = Columns.Text.Replace("△", "");
                        }
                    }
                    FileListView.Columns[e.Column].Text += "▽";
                }
            }
        }
        private void FileListView_KeyDown(object sender, KeyEventArgs e) // FileListView 키보드 이벤트 
        {
            if (e.KeyCode == Keys.Escape || (ModifierKeys == Keys.Control && e.KeyCode == Keys.W)) { this.Close(); return; }
            if (ModifierKeys == Keys.Control && e.KeyCode == Keys.A) { foreach (ListViewItem item in FileListView.Items) item.Selected = true; return; }
            if (ModifierKeys == Keys.Control && e.KeyCode == Keys.N) { New_File(); return; }
            if (ModifierKeys == Keys.Control && e.KeyCode == Keys.O) { OpenFolder(); return; }
            if (e.KeyCode == Keys.Enter && FileListView.SelectedItems.Count > 0) { Process_Start(); return; }
            if (e.KeyCode == Keys.Delete && FileListView.SelectedItems.Count > 0) { Delete_File(); return; }
            if (e.KeyCode == Keys.F2) { Rename_File(); return; }
            if (e.KeyCode == Keys.F5) { Refresh_FileList(); return; }
        }
        private void FileListView_MouseDown(object sender, MouseEventArgs e) // FileListView 클릭 이벤트 
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && FileListView.SelectedItems.Count == 1)
            {
                Console.WriteLine("Open File Application");
                Process_Start();
                return;
            }
            // DoubleClick, Left : 파일 실행 
            // None Select, Right : ContextMenu(새 파일(&N), 전체 선택(&A), 새로고침(&E), |, 새 작업영역 설정(&S), 파일 탐색기에서 열기)
            // 1 Select, Right : ContextMenu(파일 실행(&O), 전체 선택(&A), 새로고침(&E), |,이름 바꾸기(&M), 삭제(&D), |, 새 작업영역 설정(&S), 파일 탐색기에서 열기)
            // Above 2 Select, Right : ContextMenu(파일 실행(&O), 전체 선택(&A), 새로고침(&E), |, 삭제(&D), |, 새 작업영역 설정(&S), 파일 탐색기에서 열기)
            FileListMenu = new();
            if (e.Button == MouseButtons.Right && FolderPath != null)
            {
                if (FileListView.SelectedItems.Count == 0)
                {
                    Console.WriteLine("None Select, Right");
                    FileListMenu.Items.Add("새 파일(&N)", null, New_File_Event);
                    FileListMenu.Items.Add("전체 선택(&A)", null, Select_All_Event);
                    FileListMenu.Items.Add("새로 고침(&E)", null, Refresh_FileList_Event);
                }
                if (FileListView.SelectedItems.Count == 1)
                {
                    Console.WriteLine("1 Select, Right");
                    FileListMenu.Items.Add("열기(&O)", null, Process_Start_Event);
                    FileListMenu.Items.Add("전체 선택(&A)", null, Select_All_Event);
                    FileListMenu.Items.Add("새로 고침(&E)", null, Refresh_FileList_Event);
                    FileListMenu.Items.Add(new ToolStripSeparator());
                    FileListMenu.Items.Add("이름 바꾸기(&M)", null, Rename_File_Event);
                    FileListMenu.Items.Add("삭제(&D)", null, Delete_File_Event);
                }
                if (FileListView.SelectedItems.Count > 1)
                {
                    Console.WriteLine("Above 2 Select, Right");
                    FileListMenu.Items.Add("열기(&O)", null, Process_Start_Event);
                    FileListMenu.Items.Add("전체 선택(&A)", null, Select_All_Event);
                    FileListMenu.Items.Add("새로 고침(&E)", null, Refresh_FileList_Event);
                    FileListMenu.Items.Add(new ToolStripSeparator());
                    FileListMenu.Items.Add("삭제(&D)", null, Delete_File_Event);
                }
                FileListMenu.Items.Add(new ToolStripSeparator());
                FileListMenu.Items.Add("새 작업영역 설정(&S)", null, New_WorkSpace_Event);
                FileListMenu.Items.Add("파일 탐색기에서 열기", null, Open_Explorer_Event);
                FileListMenu.Show(WorkSpacePanel, new Point(FileListView.Location.X + e.X, FileListView.Location.Y + e.Y));
            }
        }
        private void Process_Start_Event(object? sender, EventArgs e) { Process_Start(); } // 파일 실행(&O)Strip 
        private void Process_Start() // 파일 실행 
        {
            using Process FileOpen = new();
            FileOpen.StartInfo.FileName = "explorer";
            if (FolderPath != null)
            {
                bool ErrorState = false;
                string OpenFileName, ErrorMessage;
                ErrorMessage = "선택한 파일이 존재하지 않습니다.\n새로 고침 후 다시 시도해 주세요.\n\n";
                foreach (ListViewItem items in FileListView.SelectedItems)
                {
                    if (FolderPath.Length == 3) OpenFileName = FolderPath + items.SubItems[1].Text;
                    else OpenFileName = FolderPath + '\\' + items.SubItems[1].Text;
                    if (File.Exists(OpenFileName))
                    {
                        FileOpen.StartInfo.Arguments = OpenFileName;
                        FileOpen.Start();
                    }
                    else
                    {
                        ErrorMessage += OpenFileName + '\n';
                        ErrorState = true;
                    }
                }
                if (ErrorState) MessageBox.Show(ErrorMessage, "파일 없음");
            }
        }
        private void New_File_Event(object? sender, EventArgs e) { New_File(); } // 새 파일(&N)Strip 
        private void New_File() // 새 파일 
        {
            if (FolderPath != null)
            {
                Console.WriteLine(FolderPath);
                string NewFileFull = FolderPath + "\\" + NewFileName;
                int FileNumber = 1;
                if (File.Exists(NewFileFull)) FileNumber++;
                while (File.Exists(NewFileFull + '(' + FileNumber.ToString() + ')')) FileNumber++;
                if (FileNumber == 1)
                {
                    File.Create(NewFileFull).Close();
                    FileInfo NewFileInfo = new(NewFileFull);
                    long FileSize = NewFileInfo.Length;
                    if (FileSize >= 1024) FileSize /= 1024; // kilobyte 
                    else if (FileSize == 0) FileSize = 0;
                    else FileSize = 1;
                    string[] ItemInfo = { "", NewFileInfo.Name, NewFileInfo.Extension, NewFileInfo.CreationTime.ToString(), NewFileInfo.LastWriteTime.ToString(), string.Format($"{FileSize:#,####0}KB") };
                    ListViewItem NewItem = new(ItemInfo);
                    FileListView.Items.Add(NewItem);
                    FileItemInfo.Add(NewItem);
                }
                else
                {
                    NewFileFull += '(' + FileNumber.ToString() + ')';
                    File.Create(NewFileFull).Close();
                    FileInfo NewFileInfo = new(NewFileFull);
                    long FileSize = NewFileInfo.Length;
                    if (FileSize >= 1024) FileSize /= 1024; // kilobyte 
                    else if (FileSize == 0) FileSize = 0;
                    else FileSize = 1;
                    string[] ItemInfo = { "", NewFileInfo.Name, NewFileInfo.Extension, NewFileInfo.CreationTime.ToString(), NewFileInfo.LastWriteTime.ToString(), string.Format($"{FileSize:#,####0}KB") };
                    ListViewItem NewItem = new(ItemInfo);
                    FileListView.Items.Add(NewItem);
                    FileItemInfo.Add(NewItem);
                }
                if (FileList != null) StatusLabel1.Text = string.Format("{0}개 항목", FileItemInfo.Count);
            }
        }
        private void Select_All_Event(object? sender, EventArgs e) // 전체 선택(&A)Strip 
        {
            foreach (ListViewItem item in FileListView.Items) item.Selected = true;
        }
        private void Refresh_FileList_Event(object? sender, EventArgs e) { Refresh_FileList(); } // 새로고침(&E)Strip 
        private void Refresh_FileList()
        {
            if (FolderPath != null)
            {
                if (!Worker.IsBusy)
                {
                    FileListView.Items.Clear();
                    Worker.RunWorkerAsync(FolderPath);
                }
                else
                {
                    Worker.CancelAsync();
                    FileListView.Items.Clear();
                }
                StatusLabel2.Text = string.Empty;
            }
        }
        private void Rename_File_Event(object? sender, EventArgs e) { Rename_File(); } // 이름 바꾸기(&M)Strip 

        /// <summary>
        /// 이름 바꾸기 - 단일선택 
        /// </summary>
        private void Rename_File()
        {
            if (FileListView.SelectedItems.Count > 0)
            {
                TextBox RenamerBox = new();
                RenamerBox.Text = FileListView.SelectedItems[0].SubItems[1].Text;
                RenamerBox.Location = FileListView.SelectedItems[0].SubItems[1].Bounds.Location;
                RenamerBox.Size = TextRenderer.MeasureText(RenamerBox.Text + "  ", new Font("맑은 고딕", 9F));
                RenamerBox.KeyDown += RenamerBox_KeyDown;
                RenamerBox.KeyPress += RenamerBox_KeyPress;
                RenamerBox.Leave += RenamerBox_Leave;
                FileListView.Controls.Add(RenamerBox);
                RenamerBox.BringToFront();
                RenamerBox.Select();
                RenameState = true;
            }
        }

        /// <summary>
        /// 이름 바꾸기 - 복수선택 
        /// </summary>
        /// <param name="Regular"><term>정규식 입력 예시</term> {RegularType:params}</param>
        private void Rename_File(string Regular)
        {
            // 정규식 분류 
            // {} 대분류 => 요소 분류 
            // : 중분류 => 정규식 유형, 매개변수 분류
            // , 소분류 => 매개변수 간 분류
            string RenameText = string.Empty;
            List<string> Components = new();
            string Tokens = string.Empty;
            string[] Params;
            bool ReadState = false, SkipParen = false;
            for (int i = 0; i < Regular.Length; i++)
            {
                if (Regular[i] == '\"')
                {
                    if (SkipParen) SkipParen = false;
                    else SkipParen = true;
                }
                if(!SkipParen)
                {
                    if (Regular[i] == '{') { ReadState = true; continue; }
                    if (Regular[i] == '}')
                    {
                        Components.Add(Tokens);
                        Tokens = string.Empty;
                        ReadState = false;
                        continue;
                    }
                }
                if (ReadState) Tokens += Regular[i];
            }
            foreach (string component in Components)
            {
                if (component.Split(':').First() == RegularType.Append.ToString())
                {
                    Tokens = component.Split(':').Last();
                    Params = Tokens.Split(",");
                    string AppendStr = Params[0].Trim(); // 추가할 문자열
                    int AppendIndex = int.Parse(Params[1].Trim()); // 추가할 위치
                    bool Sequence = bool.Parse(Params[2].Trim()); // 인덱싱 순서
                    Console.WriteLine("Append : {0}, {1}, {2}", AppendStr, AppendIndex, Sequence);
                }
                if (component.Split(':').First() == RegularType.Delete.ToString())
                {
                    Console.WriteLine("Delete");
                }
                if (component.Split(':').First() == RegularType.Replace.ToString())
                {
                    Console.WriteLine("Replace");
                }
                if (component.Split(':').First() == RegularType.NewNameSet.ToString())
                {
                    Console.WriteLine("NewNameSet");
                }
            }
            Console.WriteLine("\n변경대상 파일");
            foreach(ListViewItem items in FileListView.CheckedItems)
            {
                Console.WriteLine(items.SubItems[1].Text);
            }
        }
        private void RenamerBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { RenameState = true; FileListView.Select(); return; }
            if (e.KeyCode == Keys.Escape) { RenameState = false; FileListView.Select(); return; }
        }
        private void RenamerBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Escape) e.Handled = true;

            // 파일명 유효성 검사 KeyCode 
            // /,?, |, \, <, >, ", *, :
            if (e.KeyChar == '/' || e.KeyChar == '?' || e.KeyChar == '|' || e.KeyChar == '\\' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '"' || e.KeyChar == '*' || e.KeyChar == ':' || e.KeyChar == '<')
            {
                Console.WriteLine("Invalid KeyCode");
                e.KeyChar = Convert.ToChar(0);
            }
        }
        private void RenamerBox_Leave(object? sender, EventArgs e)
        {
            if (RenameState)
            {
                if (sender is TextBox RenamerBox)
                {
                    if (FolderPath != null && RenamerBox.Text != string.Empty)
                    {
                        string sourceName, destName;
                        if (FolderPath.Length == 3)
                        {
                            sourceName = FolderPath + FileListView.SelectedItems[0].SubItems[1].Text;
                            destName = FolderPath + RenamerBox.Text;
                        }
                        else
                        {
                            sourceName = FolderPath + '\\' + FileListView.SelectedItems[0].SubItems[1].Text;
                            destName = FolderPath + '\\' + RenamerBox.Text;
                        }
                        if (File.Exists(sourceName))
                        {
                            if (File.Exists(destName) && sourceName != destName)
                            {
                                int FileNumber = 1;
                                if (File.Exists(destName)) FileNumber++;
                                while (File.Exists(destName + '(' + FileNumber + ')')) FileNumber++;
                                if (MessageBox.Show(string.Format("이 위치에 이미 {0} 파일이 있습니다. {1}로 변경하시겠습니까?", destName, destName + '(' + FileNumber + ')'), "파일 중복", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                {
                                    FileItemInfo[FileItemInfo.IndexOf(FileListView.SelectedItems[0])].Text = RenamerBox.Text + '(' + FileNumber + ')';
                                    FileListView.SelectedItems[0].SubItems[1].Text = RenamerBox.Text + '(' + FileNumber + ')';
                                    if (FileListView.SelectedItems[0].SubItems[1].Text.Contains('.')) FileListView.SelectedItems[0].SubItems[2].Text = '.' + RenamerBox.Text.Split('.').Last();
                                    File.Move(sourceName, destName + '(' + FileNumber + ')', true);
                                }
                            }
                            else
                            {
                                FileItemInfo[FileItemInfo.IndexOf(FileListView.SelectedItems[0])].Text = RenamerBox.Text;
                                FileListView.SelectedItems[0].SubItems[1].Text = RenamerBox.Text;
                                if (FileListView.SelectedItems[0].SubItems[1].Text.Contains('.')) FileListView.SelectedItems[0].SubItems[2].Text = '.' + RenamerBox.Text.Split('.').Last();
                                File.Move(sourceName, destName, true);
                            }
                        }
                        else Refresh_FileList();
                    }
                }
            }
            FileListView.Controls.RemoveAt(0);
        }
        private void Delete_File_Event(object? sender, EventArgs e) { Delete_File(); } // 삭제(&D)Strip 
        private void Delete_File() // 삭제 
        {
            if (FolderPath != null)
            {
                bool ErrorState = false;
                string DeleteFileName, ErrorMessage, DeleteFiles = string.Empty;
                List<ListViewItem> DeleteFileList = new();
                ErrorMessage = "선택한 파일이 존재하지 않습니다.\n새로 고침 후 다시 시도해 주세요.\n\n";
                foreach (ListViewItem items in FileListView.SelectedItems)
                {
                    if (FolderPath.Length == 3) DeleteFileName = FolderPath + items.SubItems[1].Text;
                    else DeleteFileName = FolderPath + '\\' + items.SubItems[1].Text;
                    if (File.Exists(DeleteFileName)) DeleteFileList.Add(items);
                    else
                    {
                        ErrorMessage += DeleteFileName + '\n';
                        ErrorState = true;
                    }
                }
                if (ErrorState) MessageBox.Show(ErrorMessage, "파일 없음");
                else
                {
                    string DeleteMessage = string.Empty;
                    int DeleteCount = 0; // 삭제할 파일 개수 
                    foreach (ListViewItem delfile in DeleteFileList)
                    {
                        if (DeleteCount++ < 10) DeleteMessage += delfile.SubItems[1].Text + ' ' + delfile.SubItems[5].Text + '\n';
                    }
                    if (DeleteCount > 10) DeleteMessage += "  . . .\n";
                    DeleteMessage += string.Format("\n위에 파일을 포함한 총 {0}개의 파일을 완전히 삭제하시겠습니까?", DeleteCount);
                    if (MessageBox.Show(DeleteMessage, "파일 삭제", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        foreach (ListViewItem delfile in DeleteFileList)
                        {
                            if (FolderPath.Length == 3) File.Delete(FolderPath + delfile.SubItems[1].Text);
                            else File.Delete(FolderPath + '\\' + delfile.SubItems[1].Text);
                            FileListView.Items.Remove(delfile);
                            FileItemInfo.Remove(delfile);
                        }
                    }
                }
                if (FileList != null) StatusLabel1.Text = string.Format("{0}개 항목", FileItemInfo.Count);
            }
        }
        private void New_WorkSpace_Event(object? sender, EventArgs e) // 새 작업영역(&S)Strip 
        {
            OpenFolder();
        }
        private void Open_Explorer_Event(object? sender, EventArgs e) // 파일 탐색기에서 열기Strip 
        {
            string OpenFileFullName;
            if (FolderPath != null)
            {
                if (FileListView.SelectedItems.Count > 0)
                {
                    if (FolderPath.Length == 3) OpenFileFullName = "/select, " + FolderPath + FileListView.SelectedItems[0].SubItems[1].Text;
                    else OpenFileFullName = "/select, " + FolderPath + '\\' + FileListView.SelectedItems[0].SubItems[1].Text;
                    Console.WriteLine(OpenFileFullName);
                    if (FolderPath != null) Process.Start("explorer.exe", OpenFileFullName);
                    else StatusLabel1.Text = "현재 설정된 작업 경로 없습니다.";
                }
                else
                {
                    if (FolderPath != null) Process.Start("explorer.exe", FolderPath);
                    else StatusLabel1.Text = "현재 설정된 작업 경로 없습니다.";
                }
            }
        }
        #endregion

        #region Rename Event
        private void ReNameButton_Click(object sender, EventArgs e) // 이름 바꾸기 폼 열기 
        {
            if (FileListView.CheckedItems.Count == 0) { MessageBox.Show("선택한 파일이 없습니다.", "대상 없음", MessageBoxButtons.OK); return; }
            Rename rename = new();
            rename.StartPosition = FormStartPosition.CenterParent;
            rename.ExcuteRename += Rename_Renamed;
            rename.ShowDialog();
        }
        private void Rename_Renamed(string? Regular) // 이름 바꾸기 폼 이벤트 리스너 
        {
            if (Regular != null) if(Regular.Trim() != string.Empty) Rename_File(Regular);
        }
        #endregion

        #region External UI
        private void SettingButton_Click(object sender, EventArgs e) // 설정 버튼 
        {
            FileListView.Select();
            Setting SettingForm = new();
            SettingForm.StartPosition = FormStartPosition.CenterParent;
            SettingForm.ShowDialog();
        }

        private void SearchWords_KeyDown(object sender, KeyEventArgs e) // 검색어 감지 
        {
            if (e.KeyCode == Keys.Enter) FileListView.Focus();
        }
        private void SearchWords_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) e.Handled = true; } // Enter 소리 무음 처리 
        private void OpenFolderButton_Click(object sender, EventArgs e) { OpenFolder(); } // 폴더 열기Click 
        private void OpenFolder() // 폴더 열기 
        {
            FileListView.Select();
            FolderBrowserDialog OpenFolder = new();
            DialogResult = OpenFolder.ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                FolderPath = OpenFolder.SelectedPath;
                if (!Worker.IsBusy)
                {
                    IsEmptyWorkSpaceLabel.Hide();
                    WorkPathLabel.Text = OpenFolder.SelectedPath;
                    FileListView.Items.Clear();
                    Worker.RunWorkerAsync(FolderPath);
                }
                else
                {
                    Worker.CancelAsync();
                    FileListView.Items.Clear();
                    Worker.RunWorkerAsync(FolderPath);
                }
            }
            StatusLabel2.Text = string.Empty;
        }
        private void OpenExplorer_Click(object sender, EventArgs e) // 탐색기로 열기 
        {
            FileListView.Select();
            if (FolderPath != null) Process.Start("explorer.exe", FolderPath);
            else StatusLabel1.Text = "현재 설정된 작업 경로 없습니다.";
        }
        private void SearchWords_TextChanged(object sender, EventArgs e) // 검색어 입력 
        {
            if (Worker.IsBusy) return;
            int SearchItemSize = 0;
            if (FileItemInfo != null)
            {
                FileListView.Items.Clear();
                foreach (ListViewItem items in FileItemInfo) if (items.SubItems[1].Text.Contains(SearchWords.Text)) SearchItemSize++;
                if (SearchItemSize > 0)
                {
                    ListViewItem[] ResultItems = new ListViewItem[SearchItemSize];
                    int resultindex = 0;
                    foreach (ListViewItem items in FileItemInfo) if (items.SubItems[1].Text.Contains(SearchWords.Text)) ResultItems[resultindex++] = items;
                    FileListView.Items.AddRange(ResultItems);
                    StatusLabel1.Text = string.Format("{0}개 항목", SearchItemSize);
                    IsEmptyWorkSpaceLabel.Hide();
                }
                else
                {
                    StatusLabel1.Text = "0개 항목";
                    IsEmptyWorkSpaceLabel.Text = "일치하는 항목이 없습니다.";
                    IsEmptyWorkSpaceLabel.Show();
                }
                MainUILayout(0);
            }
        }
        #endregion
    }

    /// <summary>
    /// ListView 정렬 
    /// </summary>
    class ListViewItemComparer : IComparer
    {
        private int col;
        public string sort = "asc";
        public ListViewItemComparer()
        {
            col = 0;
        }
        public ListViewItemComparer(int column, string sort)
        {
            col = column;
            this.sort = sort;
        }
#pragma warning disable CS8767 // 매개 변수 형식에서 참조 형식의 null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않음(null 허용 여부 특성 때문일 수 있음)
        public int Compare(object x, object y)
#pragma warning restore CS8767 // 매개 변수 형식에서 참조 형식의 null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않음(null 허용 여부 특성 때문일 수 있음)
        {
            if (sort == "asc")
            {
                if (col == 5)
                {
                    string sx, sy;
                    sx = ((ListViewItem)x).SubItems[col].Text.Replace(",", "");
                    sy = ((ListViewItem)y).SubItems[col].Text.Replace(",", "");
                    double dx, dy;
                    dx = Double.Parse(sx[..^2]);
                    dy = Double.Parse(sy[..^2]);
                    return dx.CompareTo(dy);
                }
                else return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
            else
            {
                if (col == 5)
                {
                    string sx, sy;
                    sx = ((ListViewItem)x).SubItems[col].Text.Replace(",", "");
                    sy = ((ListViewItem)y).SubItems[col].Text.Replace(",", "");
                    double dx, dy;
                    dx = Double.Parse(sx[..^2]);
                    dy = Double.Parse(sy[..^2]); // sy.Substring(0, sy.Length - 2)
                    return dy.CompareTo(dx);
                }
                return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
            }
        }
    }
}
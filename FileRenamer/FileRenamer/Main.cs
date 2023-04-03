using FindImage;
using MaterialSkin;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

// TODO LIST v0.9
// ���Խ� ��Ÿ ���� ó��
// ���Խ� ���� �ڵ� �ϼ�
// �������� �̸� ����� Ȯ���� ���� ���� ���� ��� �߰�
// Ctrl + Z ��� �߰� 

namespace FileRenamer
{
    /// <summary>
    /// <para>�̸� ���Խ� ���� : {�߰�, ����, ��ü, ���̸� ����, ��������}</para>
    /// <para>���Խ� ������ : "{}"</para>
    /// </summary>
    public enum RegularType
    {
        /// <summary>
        /// ���ڿ� �߰�. ���Խ� Ȱ��:{Append:"�߰��� ���ڿ�", �߰��� ��ġ(int), �ε��̼���(Boolean)}
        /// </summary>
        Append = 0,

        /// <summary>
        /// ���ڿ� ����. ���Խ� Ȱ��:{Delete:������ ����(int), ������ ��ġ(int), �ε��̼���(Boolean)}
        /// </summary>
        Delete = 1,

        /// <summary>
        /// ���ڿ� ��ü. ���Խ� Ȱ��:{Replace:"ã�� ���ڿ�", "��ü�� ���ڿ�"}
        /// </summary>
        Replace = 2,

        /// <summary>
        /// ���ο� ���ڿ� �߰�. ���Խ� Ȱ�� : {NewNameSet:"���ο� ���ڿ�"}
        /// </summary>
        NewNameSet = 3,

        /// <summary>
        /// ���� �ڵ� ����. ���Խ� Ȱ�� : {AutoIncrement:���۰�(int), ���а�(int)}
        /// </summary>
        AutoIncrement = 4
    }
    public partial class Main : Form
    {
        public const string Version = "0.9";

        /// <summary>
        /// ListView ��Ŭ�� �޴� 
        /// </summary>
        private ContextMenuStrip? FileListMenu;

        /// <summary>
        /// �׸� ������ ����Ʈ{���ϸ�, ����, ���� ��¥, ������ ��¥, ũ��} 
        /// </summary>
        private List<ListViewItem> FileItemInfo = new();

        /// <summary>
        /// �̸� ���� ��� �׸� ����Ʈ(������ �׸�)
        /// </summary>
        private List<string> SelectedRenameList = new();

        /// <summary>
        /// ���� ���� ��� 
        /// </summary>
        private string? FolderPath;

        /// <summary>
        /// �ҷ��� ���� �̸� ����Ʈ(string) 
        /// </summary>
        private string[]? FileList;

        /// <summary>
        /// �� ���� �̸� 
        /// </summary>
        private string NewFileName = "���ڱ���";

        /// <summary>
        /// �̸� �ٲٱ� ����  
        /// </summary>
        private bool RenameState;

        /// <summary>
        /// FileListView ���� ���� Update Timer
        /// </summary>
        private System.Windows.Forms.Timer? CheckedBoxTimer;

        /// <summary>
        /// FileListView ���� ����
        /// </summary>
        private int CheckedBoxCount;

        private BackgroundWorker FileLoadWorker;
        private BackgroundWorker FileRenameWorker;
        private int ProgressCounter;
        private MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;

        #region Main 
        public Main()
        {
            InitializeComponent();
            MainUILayout(1);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue500, Primary.BlueGrey800, Primary.BlueGrey800, Accent.DeepPurple700, TextShade.BLACK);

            // Load Background
            FileLoadWorker = new();
            FileLoadWorker.WorkerReportsProgress = true;
            FileLoadWorker.WorkerSupportsCancellation = true;
            FileLoadWorker.DoWork += Worker_FileLoad;
            FileLoadWorker.ProgressChanged += Worker_FileLoadProgress;
            FileLoadWorker.RunWorkerCompleted += Worker_FileLoadCompleted;

            // Rename Background
            FileRenameWorker = new();
            FileRenameWorker.WorkerReportsProgress = true;
            FileRenameWorker.WorkerSupportsCancellation = true;
            FileRenameWorker.DoWork += Worker_FileRename;
            FileRenameWorker.ProgressChanged += Worker_FileRenameProgress;
            FileRenameWorker.RunWorkerCompleted += Worker_FileRenameCompleted;

            // Timer
            CheckedBoxTimer = new();
            CheckedBoxTimer.Interval = 34; // 30FPS 
            CheckedBoxTimer.Tick += CheckedBoxTimer_Tick;
            CheckedBoxTimer.Enabled = true;
            FileListView.Select();

            // ���� �̺�Ʈ 
            foreach (Control controls in this.Controls)
            {
                controls.KeyDown += KeyDown_Close;
            }


            string input = "{aa:\"Hi my bi\\\"g jaji\"}";
            string pattern = "\"(.*)\""; // ū ����ǥ�� ���� ���ڿ� ����
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            if (match.Success)
            {
                string result = match.Groups[1].Value;
                result = result.Replace("\\\"", "\""); // ���� ����ǥ�� ū ����ǥ�� ����
                Console.WriteLine(result);
            }

        }
        private void Main_Load(object sender, EventArgs e) // �ʱ� ȭ�� ���� 
        {
            ToolTip toolTip = new();
            toolTip.AutoPopDelay = 3000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;

            /* FileListView */
            FileListView.Columns[1].Text += '��';
            FileListView.ListViewItemSorter = new ListViewItemComparer(1, "asc"); // cols:1 = ���ϸ� 

            /* OpenFolderButton */
            OpenFolderButton.Image = ImageFinder.GetIamgeFile("AddFolder.png", 25, 25);
            toolTip.SetToolTip(OpenFolderButton, "�۾� ��� �����ϱ�");

            /* SettingButton */
            SettingButton.Image = ImageFinder.GetIamgeFile("Setting.png", 25, 25);
            toolTip.SetToolTip(SettingButton, "����");

            /* SearchWords */
            SearchWords.TrailingIcon = ImageFinder.GetIamgeFile("Search.png", 32, 32);

            /* OpenExplorer */
            OpenExplorer.Image = ImageFinder.GetIamgeFile("OpenFolder.png", 25, 25);
            toolTip.SetToolTip(OpenExplorer, "���� Ž����� ����");

            /* IsEmptyWorkSpaceLabel */
            if (WorkPathLabel.Text == string.Empty) IsEmptyWorkSpaceLabel.Show();
            else IsEmptyWorkSpaceLabel.Hide();

            /* ContainExtensionSwitch */
            toolTip.SetToolTip(ContainExtensionSwitch, "�̸� ���� ��, ���ϸ� Ȯ���� ���Կ� ���� �����Դϴ�.");
        }
        private void Main_Resize(object sender, EventArgs e) { MainUILayout(1); } // ȭ�� ũ�� ���� 

        private void CheckedBoxTimer_Tick(object? sender, EventArgs e) // ListView ���� Tick 
        {
            if (CheckedBoxCount != FileListView.CheckedItems.Count)
            {
                if (FileListView.CheckedItems.Count > 0)
                {
                    StatusLabel2.Location = new Point(StatusLabel1.Location.X + StatusLabel1.Width, StatusLabel1.Location.Y);
                    StatusLabel2.Text = string.Format("{0}�� ������", FileListView.CheckedItems.Count);
                    if (SelectedRenameList != null)
                    {
                        SelectedRenameList.Clear();
                        foreach (ListViewItem selectedItems in FileListView.CheckedItems) SelectedRenameList.Add(selectedItems.SubItems[1].Text);
                    }
                }
                else StatusLabel2.Text = string.Empty;
                CheckedBoxCount = FileListView.CheckedItems.Count;
            }
        }

        /// <summary>
        /// �޴�ȭ�� UI �׸��� 
        /// </summary>
        /// <param name="Selection">0:WorkSpaceUI�� �׸���, 1:��� �ٽ� �׸���</param>
        private void MainUILayout(int Selection) // 0: WorkSpaceUI, 1: ��� �缳�� 
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
                /* �ܺ� UI */
                OpenFolderButton.Location = new Point(WorkSpacePanel.Width + 5, 40);
                OpenExplorer.Location = new Point(OpenFolderButton.Location.X + OpenFolderButton.Width + 5, 40);
                SearchWords.Location = new Point(WorkSpacePanel.Width + 5, 85);
                SettingButton.Location = new Point(WorkSpacePanel.Width + 5, 140);
                LabelSetDate.Location = new Point(WorkSpacePanel.Width + 5, 200);
                DatePicker.Location = new Point(WorkSpacePanel.Width + 5, 220);
                TimeSetMasked.Location = new Point(DatePicker.Location.X + DatePicker.Width - TimeSetMasked.Width, 250);
                DateChangeButton.Location = new Point(WorkSpacePanel.Width + 5, 290);
                ReNameButton.Location = new Point(WorkSpacePanel.Width + 5, 350);
                ContainExtensionSwitch.Location = new Point(WorkSpacePanel.Width + 10, 410);
            }
        }
        private void KeyDown_Close(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || (ModifierKeys == Keys.Control && e.KeyCode == Keys.W)) { this.Close(); return; }
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e) // ���� -> BackgroundWorker ���� 
        {
            if (FileLoadWorker != null) if (FileLoadWorker.IsBusy) FileLoadWorker.CancelAsync();
            if (MessageBox.Show("���α׷��� �����Ͻðڽ��ϱ�?", "����", MessageBoxButtons.OKCancel) == DialogResult.OK) e.Cancel = false;
            else e.Cancel = true;
        }
        #endregion

        #region BackgroundWorker
        private void Worker_FileLoad(object? sender, DoWorkEventArgs e) // File Load
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
                if (FileLoadWorker.CancellationPending == true) { e.Cancel = true; return; }
                FileInfo FilesInfo = new(FileList[i]);
                if (!FilesInfo.Attributes.HasFlag(FileAttributes.Hidden))
                {
                    long FileSize = FilesInfo.Length;
                    if (FileSize >= 1024) FileSize /= 1024; // kilobyte 
                    else if (FileSize == 0) FileSize = 0;
                    else FileSize = 1;
                    string[] ItemInfo = { "", FilesInfo.Name, FilesInfo.Extension, FilesInfo.CreationTime.ToString(), FilesInfo.LastWriteTime.ToString(), string.Format($"{FileSize:#,####0}KB") };
                    ListViewItem item = new(ItemInfo);
                    FileItemInfo.Add(item);
                    ProgressPercentage = ++ProgressCounter / (double)FileList.Length * 100;
                    FileLoadWorker.ReportProgress((int)ProgressPercentage);
                }
            }
        }
        private void Worker_FileLoadProgress(object? sender, ProgressChangedEventArgs e)
        {
            LoadProcessBar.Value = e.ProgressPercentage;
            StatusLabel1.Text = string.Format("�ҷ����� ��... {0}%", e.ProgressPercentage);
        }
        private void Worker_FileLoadCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (FileList != null) StatusLabel1.Text = string.Format("{0}�� �׸�", FileList.Length);
            if (FileItemInfo != null) FileListView.Items.AddRange(FileItemInfo.ToArray());
            ProgressCounter = 0;
        }
        private void Worker_FileRename(object? sender, DoWorkEventArgs e) // File Rename
        {
            if (e.Argument is string Regular)
            {
                //SelectedRenameList // �̸� ���� ����Ʈ
                // ���Խ� �з� 
                // {} ��з� => ��� �з� 
                // : �ߺз� => ���Խ� ����, �Ű����� �з�
                // , �Һз� => �Ű����� �� �з�
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
                    if (!SkipParen)
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
                int Repetition = 0; // ��� �׸� ���� �ݺ� ��
                foreach (string FileFullName in SelectedRenameList) // ������ ��� �׸� ���Ͽ�
                {
                    if (FileLoadWorker.CancellationPending == true) { e.Cancel = true; return; }
                    if (FolderPath is string FilePath)
                    {
                        string FileNameNoExt = FileFullName, FileExt = string.Empty, ResultName = string.Empty;
                        if (!ContainExtensionSwitch.Checked && FileFullName.Contains('.')) // Ȯ���� ������, '.' ����
                        {
                            FileExt = '.' + FileFullName.Split('.').Last();
                            FileNameNoExt = FileFullName.Remove(FileFullName.Length - FileExt.Length);
                        }
                        // (Ȯ���� ����, '.'����), (Ȯ���� ����, '.'������) = (Ȯ���� ������, '.'������)
                        foreach (string component in Components) // ��� �з�
                        {
                            if (component.Split(':').First() == RegularType.Append.ToString())
                            {
                                Tokens = component.Split(':').Last(); // ���Խ� ����, �Ű����� �з�
                                Params = Tokens.Split(','); // �Ű����� �� �з�
                                string AppendStr = Params[0].Trim().Trim('"'); // �߰��� ���ڿ�
                                int AppendIndex = int.Parse(Params[1].Trim()); // �߰��� ��ġ
                                bool Sequence = bool.Parse(Params[2].Trim()); // �ε��� ����

                                if (AppendIndex > FileNameNoExt.Length || AppendIndex < 0) // �ε��� ���� ����ó��
                                {
                                    MessageBox.Show(string.Format($"���� ���ϸ� ���� �ε������� �߸��Ǿ����ϴ�.\n{FileFullName}"), "���� �ߴ�!", MessageBoxButtons.OK);
                                    e.Cancel = true; return;
                                }
                                if (!ContainExtensionSwitch.Checked && FileFullName.Contains('.')) // Ȯ���� ������, '.'����
                                {
                                    if (Sequence) ResultName += FileNameNoExt[..AppendIndex] + AppendStr + FileNameNoExt[AppendIndex..] + FileExt;
                                    else ResultName += FileNameNoExt[..^AppendIndex] + AppendStr + FileNameNoExt[^AppendIndex..] + FileExt;
                                }
                                else // (Ȯ���� ����, '.'����), (Ȯ���� ����, '.'������) = (Ȯ���� ������, '.'������)
                                {
                                    if (Sequence) ResultName += FileNameNoExt[..AppendIndex] + AppendStr + FileNameNoExt[AppendIndex..];
                                    else ResultName += FileNameNoExt[..^AppendIndex] + AppendStr + FileNameNoExt[^AppendIndex..];
                                    // ���� [..^AppendIndex] = Substring(0, targetFile.Length - AppendIndex)
                                }
                            }
                            if (component.Split(':').First() == RegularType.Delete.ToString())
                            {
                                Tokens = component.Split(':').Last();// ���Խ� ����, �Ű����� �з�
                                Params = Tokens.Split(','); // �Ű����� �� �з�
                                int DeleteRange = int.Parse(Params[0].Trim()); // ������ ����
                                int DeleteIndex = int.Parse(Params[1].Trim()); // ������ ��ġ
                                bool Sequence = bool.Parse(Params[2].Trim()); // �ε��� ����

                                if (DeleteIndex + DeleteRange > FileNameNoExt.Length || DeleteIndex < 0) // �ε��� ���� ����ó��
                                {
                                    MessageBox.Show(string.Format($"���� ���ϸ� ���� �ε������� �߸��Ǿ����ϴ�.\n{FileFullName}"), "���� �ߴ�!", MessageBoxButtons.OK);
                                    e.Cancel = true; return;
                                }
                                if (!ContainExtensionSwitch.Checked && FileFullName.Contains('.')) // Ȯ���� ������, '.'����
                                {
                                    if (Sequence) ResultName += FileNameNoExt.Remove(DeleteIndex, DeleteRange) + FileExt;
                                    else ResultName += FileNameNoExt.Remove(FileNameNoExt.Length - DeleteIndex - DeleteRange, DeleteRange) + FileExt;
                                }
                                else // (Ȯ���� ����, '.'����), (Ȯ���� ����, '.'������) = (Ȯ���� ������, '.'������)
                                {
                                    if (Sequence) ResultName += FileNameNoExt.Remove(DeleteIndex, DeleteRange);
                                    else ResultName += FileNameNoExt.Remove(FileNameNoExt.Length - DeleteIndex - DeleteRange, DeleteRange);
                                }
                            }
                            if (component.Split(':').First() == RegularType.Replace.ToString())
                            {
                                Tokens = component.Split(':').Last(); // ���Խ� ����, �Ű����� �з�
                                Params = Tokens.Split(','); // �Ű����� �� �з�
                                string SearchStr = Params[0].Trim().Trim('"'); // ã�� ���ڿ�
                                string ReplaceStr = Params[1].Trim().Trim('"'); // ��ü�� ���ڿ�

                                if (!ContainExtensionSwitch.Checked && FileFullName.Contains('.')) // Ȯ���� ������, '.'����
                                {
                                    if(FileNameNoExt.Contains(SearchStr)) ResultName += FileNameNoExt.Replace(SearchStr, ReplaceStr) + FileExt;
                                }
                                else // (Ȯ���� ����, '.'����), (Ȯ���� ����, '.'������) = (Ȯ���� ������, '.'������)
                                {
                                    if (FileNameNoExt.Contains(SearchStr)) ResultName += FileNameNoExt.Replace(SearchStr, ReplaceStr);
                                }
                            }
                            if (component.Split(':').First() == RegularType.NewNameSet.ToString())
                            {
                                // �Ű����� �� �з� ����
                                string NewStr = component.Split(':').Last().Trim().Trim('"'); // ���ο� ���ڿ�
                                ResultName += NewStr;
                            }
                            if(component.Split(':').First() == RegularType.AutoIncrement.ToString())
                            {
                                Tokens = component.Split(':').Last(); // ���Խ� ����, �Ű����� �з�
                                Params = Tokens.Split(','); // �Ű����� �� �з�
                                int StartNumber = int.Parse(Params[0].Trim());
                                int Increment = int.Parse(Params[1].Trim());
                                int CurrentNumber = StartNumber + Increment * Repetition++;
                                ResultName += CurrentNumber.ToString();
                                Console.WriteLine($"StartNumber:{StartNumber}, Increment;{Increment}, Repetition:{Repetition}");
                            }
                        }

                        /* �̸����� ���� */
                        string SourceName = string.Empty; int FileNumber = 1;
                        if (ResultName.Length > 0)
                        {
                            if (FilePath.Length == 3)
                            {
                                SourceName = FilePath + FileFullName;
                                ResultName = FilePath + ResultName;
                            }
                            else
                            {
                                SourceName = FilePath + '\\' + FileFullName;
                                ResultName = FilePath + '\\' + ResultName;
                            }
                        }
                        else
                        {
                            MessageBox.Show("�ƹ��͵� ������� �ʾҽ��ϴ�.", "���� ���!", MessageBoxButtons.OK);
                            e.Cancel = true; return;
                        }
                        Console.WriteLine($"From:{SourceName}, To:{ResultName}");
                        if (File.Exists(ResultName)) FileNumber++;
                        if (FileNumber > 1) // �ߺ� �߻�
                        {
                            if (!ContainExtensionSwitch.Checked && FileFullName.Contains('.')) // Ȯ���� ������, '.'����
                            {
                                while (File.Exists(ResultName.Remove(ResultName.Length - FileExt.Length) + '(' + FileNumber.ToString() + ')' + FileExt)) FileNumber++; // ���� �ߺ� �˻�
                                Console.WriteLine("���� ���� ���ϸ� : {0}", ResultName.Remove(ResultName.Length - FileExt.Length) + '(' + FileNumber.ToString() + ')' + FileExt);
                                File.Move(SourceName, ResultName.Remove(ResultName.Length - FileExt.Length) + '(' + FileNumber.ToString() + ')' + FileExt, false);
                            }
                            else // (Ȯ���� ����, '.'����), (Ȯ���� ����, '.'������) = (Ȯ���� ������, '.'������)
                            {
                                while (File.Exists(ResultName + '(' + FileNumber.ToString() + ')')) FileNumber++; // ���� �ߺ� �˻�
                                Console.WriteLine("���� ���� ���ϸ� : {0}", ResultName + '(' + FileNumber.ToString() + ')');
                                File.Move(SourceName, ResultName + '(' + FileNumber.ToString() + ')', false);
                            }
                        }
                        else File.Move(SourceName, ResultName, false);
                    }
                }
            }
        }
        private void Worker_FileRenameProgress(object? sender, ProgressChangedEventArgs e)
        {
            LoadProcessBar.Value = e.ProgressPercentage;
            StatusLabel1.Text = string.Format("�̸� ���� ��... {0}%", e.ProgressPercentage);
        }
        private void Worker_FileRenameCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            ProgressCounter = 0;
            Refresh_FileList();
        }
        #endregion

        #region FileListView 
        private void FileListView_ColumnClick(object sender, ColumnClickEventArgs e) // FileListView Column Ŭ��(������) 
        {
            if (e.Column == 0)
            {
                bool IsSelectAll = true;
                foreach (ListViewItem item in FileListView.Items) { if (!item.Checked) IsSelectAll = false; }
                if (IsSelectAll) foreach (ListViewItem item in FileListView.Items) { item.Checked = false; }
                else foreach (ListViewItem item in FileListView.Items) { item.Checked = true; }
            }
            else
            {
                if (FileListView.Columns[e.Column].Text.Contains('��'))
                {
                    FileListView.ListViewItemSorter = new ListViewItemComparer(e.Column, "desc");
                    Console.WriteLine("Desc");
                    foreach (ColumnHeader Columns in FileListView.Columns)
                    {
                        if (Columns.Index != 0)
                        {
                            Columns.Text = Columns.Text.Replace("��", "");
                            Columns.Text = Columns.Text.Replace("��", "");
                        }
                    }
                    FileListView.Columns[e.Column].Text += "��";
                }
                else
                {
                    FileListView.ListViewItemSorter = new ListViewItemComparer(e.Column, "asc");
                    Console.WriteLine("Asc");
                    foreach (ColumnHeader Columns in FileListView.Columns)
                    {
                        if (Columns.Index != 0)
                        {
                            Columns.Text = Columns.Text.Replace("��", "");
                            Columns.Text = Columns.Text.Replace("��", "");
                        }
                    }
                    FileListView.Columns[e.Column].Text += "��";
                }
            }
        }
        private void FileListView_KeyDown(object sender, KeyEventArgs e) // FileListView Ű���� �̺�Ʈ 
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
        private void FileListView_MouseDown(object sender, MouseEventArgs e) // FileListView Ŭ�� �̺�Ʈ 
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && FileListView.SelectedItems.Count == 1)
            {
                Console.WriteLine("Open File Application");
                Process_Start();
                return;
            }
            // DoubleClick, Left : ���� ���� 
            // None Select, Right : ContextMenu(�� ����(&N), ��ü ����(&A), ���ΰ�ħ(&E), |, �� �۾����� ����(&S), ���� Ž���⿡�� ����)
            // 1 Select, Right : ContextMenu(���� ����(&O), ��ü ����(&A), ���ΰ�ħ(&E), |,�̸� �ٲٱ�(&M), ����(&D), |, �� �۾����� ����(&S), ���� Ž���⿡�� ����)
            // Above 2 Select, Right : ContextMenu(���� ����(&O), ��ü ����(&A), ���ΰ�ħ(&E), |, ����(&D), |, �� �۾����� ����(&S), ���� Ž���⿡�� ����)
            FileListMenu = new();
            if (e.Button == MouseButtons.Right && FolderPath != null)
            {
                if (FileListView.SelectedItems.Count == 0)
                {
                    Console.WriteLine("None Select, Right");
                    FileListMenu.Items.Add("�� ����(&N)", null, New_File_Event);
                    FileListMenu.Items.Add("��ü ����(&A)", null, Select_All_Event);
                    FileListMenu.Items.Add("���� ��ħ(&E)", null, Refresh_FileList_Event);
                }
                if (FileListView.SelectedItems.Count == 1)
                {
                    Console.WriteLine("1 Select, Right");
                    FileListMenu.Items.Add("����(&O)", null, Process_Start_Event);
                    FileListMenu.Items.Add("��ü ����(&A)", null, Select_All_Event);
                    FileListMenu.Items.Add("���� ��ħ(&E)", null, Refresh_FileList_Event);
                    FileListMenu.Items.Add(new ToolStripSeparator());
                    FileListMenu.Items.Add("�̸� �ٲٱ�(&M)", null, Rename_File_Event);
                    FileListMenu.Items.Add("����(&D)", null, Delete_File_Event);
                }
                if (FileListView.SelectedItems.Count > 1)
                {
                    Console.WriteLine("Above 2 Select, Right");
                    FileListMenu.Items.Add("����(&O)", null, Process_Start_Event);
                    FileListMenu.Items.Add("��ü ����(&A)", null, Select_All_Event);
                    FileListMenu.Items.Add("���� ��ħ(&E)", null, Refresh_FileList_Event);
                    FileListMenu.Items.Add(new ToolStripSeparator());
                    FileListMenu.Items.Add("����(&D)", null, Delete_File_Event);
                }
                FileListMenu.Items.Add(new ToolStripSeparator());
                FileListMenu.Items.Add("�� �۾����� ����(&S)", null, New_WorkSpace_Event);
                FileListMenu.Items.Add("���� Ž���⿡�� ����", null, Open_Explorer_Event);
                FileListMenu.Show(WorkSpacePanel, new Point(FileListView.Location.X + e.X, FileListView.Location.Y + e.Y));
            }
        }
        private void Process_Start_Event(object? sender, EventArgs e) { Process_Start(); } // ���� ����(&O)Strip 
        private void Process_Start() // ���� ���� 
        {
            using Process FileOpen = new();
            FileOpen.StartInfo.FileName = "explorer";
            if (FolderPath != null)
            {
                bool ErrorState = false;
                string OpenFileName, ErrorMessage;
                ErrorMessage = "������ ������ �������� �ʽ��ϴ�.\n���� ��ħ �� �ٽ� �õ��� �ּ���.\n\n";
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
                if (ErrorState) MessageBox.Show(ErrorMessage, "���� ����");
            }
        }
        private void New_File_Event(object? sender, EventArgs e) { New_File(); } // �� ����(&N)Strip 
        private void New_File() // �� ���� 
        {
            if (FolderPath != null)
            {
                string NewFileFull = FolderPath + '\\' + NewFileName;
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
                if (FileList != null) StatusLabel1.Text = string.Format("{0}�� �׸�", FileItemInfo.Count);
            }
        }
        private void Select_All_Event(object? sender, EventArgs e) // ��ü ����(&A)Strip 
        {
            foreach (ListViewItem item in FileListView.Items) item.Selected = true;
        }
        private void Refresh_FileList_Event(object? sender, EventArgs e) { Refresh_FileList(); } // ���ΰ�ħ(&E)Strip 
        private void Refresh_FileList()
        {
            if (FolderPath != null)
            {
                if (!FileLoadWorker.IsBusy)
                {
                    FileListView.Items.Clear();
                    FileLoadWorker.RunWorkerAsync(FolderPath);
                }
                else
                {
                    FileListView.Items.Clear();
                    FileLoadWorker.CancelAsync();
                }
                StatusLabel2.Text = string.Empty;
            }
        }
        private void Rename_File_Event(object? sender, EventArgs e) { Rename_File(); } // �̸� �ٲٱ�(&M)Strip 

        /// <summary>
        /// �̸� �ٲٱ� - ���ϼ��� 
        /// </summary>
        private void Rename_File()
        {
            if (FileListView.SelectedItems.Count > 0)
            {
                TextBox RenamerBox = new()
                {
                    Text = FileListView.SelectedItems[0].SubItems[1].Text,
                    Location = FileListView.SelectedItems[0].SubItems[1].Bounds.Location
                };
                if (TextRenderer.MeasureText(RenamerBox.Text + "  ", new Font("���� ���", 9F)).Width <= 200) RenamerBox.Size = TextRenderer.MeasureText(RenamerBox.Text + "  ", new Font("���� ���", 9F));
                else RenamerBox.Size = new Size(200, RenamerBox.Height);
                RenamerBox.KeyDown += RenamerBox_KeyDown;
                RenamerBox.KeyPress += RenamerBox_KeyPress;
                RenamerBox.TextChanged += RenamerBox_TextChanged;
                RenamerBox.Leave += RenamerBox_Leave;
                FileListView.Controls.Add(RenamerBox);
                RenamerBox.BringToFront();
                RenamerBox.Select();
                RenameState = true;
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

            // ���ϸ� ��ȿ�� �˻� KeyCode 
            // /,?, |, \, <, >, ", *, :
            if (e.KeyChar == '/' || e.KeyChar == '?' || e.KeyChar == '|' || e.KeyChar == '\\' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '"' || e.KeyChar == '*' || e.KeyChar == ':' || e.KeyChar == '<')
            {
                Console.WriteLine("Invalid KeyCode");
                e.KeyChar = Convert.ToChar(0);
            }
        }
        private void RenamerBox_TextChanged(object? sender, EventArgs e)
        {
            if (sender is TextBox RenamerBox)
            {
                if (TextRenderer.MeasureText(RenamerBox.Text + "  ", new Font("���� ���", 9F)).Width <= 200) RenamerBox.Size = TextRenderer.MeasureText(RenamerBox.Text + "  ", new Font("���� ���", 9F));
                else RenamerBox.Size = new Size(200, RenamerBox.Height);
            }
        }
        private void RenamerBox_Leave(object? sender, EventArgs e)
        {
            if (RenameState)
            {
                if (sender is TextBox RenamerBox)
                {
                    while (RenamerBox.Text.Last() == '.') RenamerBox.Text = RenamerBox.Text.Remove(RenamerBox.Text.Length - 1); // ������ '.' ����
                    while (RenamerBox.Text.First() == ' ') RenamerBox.Text = RenamerBox.Text.Remove(0, 1); // ó�� ' ' ����
                    while (RenamerBox.Text.Last() == ' ') RenamerBox.Text = RenamerBox.Text.Remove(RenamerBox.Text.Length - 1); // ������ ' ' ����
                    if (FolderPath != null && RenamerBox.Text != string.Empty)
                    {
                        string SourceName, ResultName;
                        if (FolderPath.Length == 3)
                        {
                            SourceName = FolderPath + FileListView.SelectedItems[0].SubItems[1].Text;
                            ResultName = FolderPath + RenamerBox.Text;
                        }
                        else
                        {
                            SourceName = FolderPath + '\\' + FileListView.SelectedItems[0].SubItems[1].Text;
                            ResultName = FolderPath + '\\' + RenamerBox.Text;
                        }
                        if (SourceName == ResultName) { FileListView.Controls.RemoveAt(0); return; } // ���� �� �̸� == ���� �� �̸� => return
                        string FileNameNoExt = RenamerBox.Text, FileExt = string.Empty;
                        if (RenamerBox.Text.Contains('.'))
                        {
                            FileExt = '.' + RenamerBox.Text.Split('.').Last();
                            FileNameNoExt = RenamerBox.Text.Remove(RenamerBox.Text.Length - FileExt.Length);
                        }
                        if (File.Exists(SourceName))
                        {
                            int FileNumber = 1;
                            if (File.Exists(ResultName)) FileNumber++; // ���� �ߺ�
                            if (FileNumber > 1)
                            {
                                if (ResultName.Contains('.'))
                                {
                                    while (File.Exists(ResultName.Remove(ResultName.Length - FileExt.Length) + '(' + FileNumber.ToString() + ')' + FileExt)) FileNumber++;
                                    if (MessageBox.Show(string.Format("�� ��ġ�� �̹� {0} ������ �ֽ��ϴ�. {1}�� �����Ͻðڽ��ϱ�?", RenamerBox.Text, FileNameNoExt + '(' + FileNumber.ToString() + ')' + FileExt), "���� �ߺ�", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                    {
                                        FileItemInfo[FileItemInfo.IndexOf(FileListView.SelectedItems[0])].Text = FileNameNoExt + '(' + FileNumber + ')' + FileExt;
                                        FileListView.SelectedItems[0].SubItems[1].Text = FileNameNoExt + '(' + FileNumber + ')' + FileExt;
                                        FileListView.SelectedItems[0].SubItems[2].Text = FileExt;
                                        File.Move(SourceName, ResultName.Remove(ResultName.Length - FileExt.Length) + '(' + FileNumber.ToString() + ')' + FileExt, false);
                                    }
                                }
                                else
                                {
                                    while (File.Exists(ResultName + '(' + FileNumber.ToString() + ')')) FileNumber++;
                                    if (MessageBox.Show(string.Format("�� ��ġ�� �̹� {0} ������ �ֽ��ϴ�. {1}�� �����Ͻðڽ��ϱ�?", RenamerBox.Text, ResultName + '(' + FileNumber.ToString() + ')'), "���� �ߺ�", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                    {
                                        FileItemInfo[FileItemInfo.IndexOf(FileListView.SelectedItems[0])].Text = FileNameNoExt + '(' + FileNumber.ToString() + ')';
                                        FileListView.SelectedItems[0].SubItems[1].Text = FileNameNoExt + '(' + FileNumber.ToString() + ')';
                                        FileListView.SelectedItems[0].SubItems[2].Text = string.Empty;
                                        File.Move(SourceName, ResultName + '(' + FileNumber.ToString() + ')', false);
                                    }
                                }
                            }
                            else
                            {
                                FileItemInfo[FileItemInfo.IndexOf(FileListView.SelectedItems[0])].Text = RenamerBox.Text;
                                FileListView.SelectedItems[0].SubItems[1].Text = RenamerBox.Text;
                                if (FileListView.SelectedItems[0].SubItems[1].Text.Contains('.')) FileListView.SelectedItems[0].SubItems[2].Text = FileExt;
                                else FileListView.SelectedItems[0].SubItems[2].Text = string.Empty;
                                File.Move(SourceName, ResultName, false);
                            }
                        }
                        else Refresh_FileList();
                    }
                }
            }
            FileListView.Controls.RemoveAt(0);
        }
        private void Delete_File_Event(object? sender, EventArgs e) { Delete_File(); } // ����(&D)Strip 
        private void Delete_File() // ���� 
        {
            if (FolderPath != null)
            {
                bool ErrorState = false;
                string DeleteFileName, ErrorMessage, DeleteFiles = string.Empty;
                List<ListViewItem> DeleteFileList = new();
                ErrorMessage = "������ ������ �������� �ʽ��ϴ�.\n���� ��ħ �� �ٽ� �õ��� �ּ���.\n\n";
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
                if (ErrorState) MessageBox.Show(ErrorMessage, "���� ����");
                else
                {
                    string DeleteMessage = string.Empty;
                    int DeleteCount = 0; // ������ ���� ���� 
                    foreach (ListViewItem delfile in DeleteFileList)
                    {
                        if (DeleteCount++ < 10) DeleteMessage += delfile.SubItems[1].Text + ' ' + delfile.SubItems[5].Text + '\n';
                    }
                    if (DeleteCount > 10) DeleteMessage += "  . . .\n";
                    DeleteMessage += string.Format("\n���� ������ ������ �� {0}���� ������ ������ �����Ͻðڽ��ϱ�?", DeleteCount);
                    if (MessageBox.Show(DeleteMessage, "���� ����", MessageBoxButtons.OKCancel) == DialogResult.OK)
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
                if (FileList != null) StatusLabel1.Text = string.Format("{0}�� �׸�", FileItemInfo.Count);
            }
        }
        private void New_WorkSpace_Event(object? sender, EventArgs e) // �� �۾�����(&S)Strip 
        {
            OpenFolder();
        }
        private void Open_Explorer_Event(object? sender, EventArgs e) // ���� Ž���⿡�� ����Strip 
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
                    else StatusLabel1.Text = "���� ������ �۾� ��� �����ϴ�.";
                }
                else
                {
                    if (FolderPath != null) Process.Start("explorer.exe", FolderPath);
                    else StatusLabel1.Text = "���� ������ �۾� ��� �����ϴ�.";
                }
            }
        }
        #endregion

        #region Rename Event
        private void ReNameButton_Click(object sender, EventArgs e) // �̸� �ٲٱ� �� ���� 
        {
            if (FileListView.CheckedItems.Count == 0) { MessageBox.Show("������ ������ �����ϴ�.", "��� ����", MessageBoxButtons.OK); FileListView.Select(); return; }
            Rename rename = new();
            rename.StartPosition = FormStartPosition.CenterParent;
            rename.ExcuteRename += Rename_Renamed;
            rename.ShowDialog();
        }
        private void Rename_Renamed(string? Regular) // �̸� �ٲٱ� �� �̺�Ʈ ������ 
        {
            if (Regular != null)
            {
                if (Regular.Trim() != string.Empty)
                {
                    if (!FileRenameWorker.IsBusy) FileRenameWorker.RunWorkerAsync(Regular);
                    else FileLoadWorker.CancelAsync();
                    StatusLabel2.Text = string.Empty;
                }
            }
        }
        #endregion

        #region External UI
        private void SettingButton_Click(object sender, EventArgs e) // ���� ��ư 
        {
            FileListView.Select();
            Setting SettingForm = new();
            SettingForm.StartPosition = FormStartPosition.CenterParent;
            SettingForm.ShowDialog();
        }

        private void SearchWords_KeyDown(object sender, KeyEventArgs e) // �˻��� ���� 
        {
            if (e.KeyCode == Keys.Enter) FileListView.Focus();
        }
        private void SearchWords_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == (char)Keys.Enter) e.Handled = true; } // Enter �Ҹ� ���� ó�� 
        private void OpenFolderButton_Click(object sender, EventArgs e) { OpenFolder(); } // ���� ����Click 
        private void OpenFolder() // ���� ���� 
        {
            FileListView.Select();
            FolderBrowserDialog OpenFolder = new();
            DialogResult = OpenFolder.ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                FolderPath = OpenFolder.SelectedPath;
                if (!FileLoadWorker.IsBusy)
                {
                    IsEmptyWorkSpaceLabel.Hide();
                    WorkPathLabel.Text = OpenFolder.SelectedPath;
                    FileListView.Items.Clear();
                    FileLoadWorker.RunWorkerAsync(FolderPath);
                }
                else
                {
                    FileLoadWorker.CancelAsync();
                    FileListView.Items.Clear();
                    FileItemInfo.Clear();
                    this.OpenFolder();
                }
            }
            StatusLabel2.Text = string.Empty;
        }
        private void OpenExplorer_Click(object sender, EventArgs e) // Ž����� ���� 
        {
            FileListView.Select();
            if (FolderPath != null) Process.Start("explorer.exe", FolderPath);
            else StatusLabel1.Text = "���� ������ �۾� ��� �����ϴ�.";
        }
        private void SearchWords_TextChanged(object sender, EventArgs e) // �˻��� �Է� 
        {
            if (FileLoadWorker.IsBusy) return;
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
                    StatusLabel1.Text = string.Format("{0}�� �׸�", SearchItemSize);
                    IsEmptyWorkSpaceLabel.Hide();
                }
                else
                {
                    StatusLabel1.Text = "0�� �׸�";
                    IsEmptyWorkSpaceLabel.Text = "��ġ�ϴ� �׸��� �����ϴ�.";
                    IsEmptyWorkSpaceLabel.Show();
                }
                MainUILayout(0);
            }
        }
        #endregion

        private void FileListView_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("Drag");
        }
    }
}
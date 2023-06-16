using MaterialSkin;
using System.ComponentModel;
using System.Diagnostics;

namespace FileRenamer
{
    partial class Main
    {
        /// <summary>
        /// 현재 버전
        /// </summary>
        private const string Version = "0.9-beta";

        /// <summary>
        /// ListView 빈공간 우클릭 메뉴
        /// </summary>
        private readonly ContextMenuStrip FileListMenuNoneSelect = new();

        /// <summary>
        /// ListView 단일 파일 우클릭 메뉴
        /// </summary>
        private readonly ContextMenuStrip FileListMenuSingleSelect = new();

        /// <summary>
        /// ListView 다중 파일 우클릭 메뉴
        /// </summary>
        private readonly ContextMenuStrip FileListMenuMultiSelect = new();

        /// <summary>
        /// 항목별 아이템 리스트{파일명, 유형, 만든 날짜, 수정한 날짜, 크기}
        /// </summary>
        private readonly List<ListViewItem> FileItemInfo = [];

        /// <summary>
        /// 이름 변경 대상 항목 리스트(선택한 항목)
        /// </summary>
        private readonly List<string> SelectedRenameList = [];

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
        private string DefaultFileName = "직박구리";

        /// <summary>
        /// 만든 날짜, 수정한 날짜, 액세스 날짜 일괄 변경 선택
        /// </summary>
        private bool IsDateTimeAllChange;

        /// <summary>
        /// 이름 바꾸기 상태  
        /// </summary>
        private bool RenameState;

        /// <summary>
        /// FileListView 선택 개수 Update Timer
        /// </summary>
        private readonly System.Windows.Forms.Timer CheckedBoxTimer = new();

        /// <summary>
        /// FileListView 선택 개수
        /// </summary>
        private int CheckedBoxCount;

        private int ProgressCounter;
        private readonly BackgroundWorker FileLoadWorker = new();
        private readonly BackgroundWorker FileRenameWorker = new();
        private readonly MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;

        /// <summary>
        /// 메뉴화면 UI 그리기 
        /// </summary>
        /// <param name="Selection">0:WorkSpaceUI만 그리기, 1:모두 다시 그리기</param>
        private void MainUILayout(int Selection) // 0: WorkSpaceUI, 1: 모두 재설정 
        {
            if (Selection == 0)
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
            if (Selection == 1)
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
                ContainExtensionSwitch.Location = new Point(WorkSpacePanel.Width + 10, 410);
            }
        }

        /// <summary>
        /// 파일(프로세스) 실행
        /// </summary>
        private void Process_Start()
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

        /// <summary>
        /// 새 파일 생성
        /// </summary>
        private void New_File() 
        {
            if (FolderPath != null)
            {
                string NewFileFull = FolderPath + '\\' + DefaultFileName;
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
                    string[] ItemInfo = ["", NewFileInfo.Name, NewFileInfo.Extension, NewFileInfo.CreationTime.ToString(), NewFileInfo.LastWriteTime.ToString(), string.Format($"{FileSize:#,####0}KB")];
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
                    string[] ItemInfo = ["", NewFileInfo.Name, NewFileInfo.Extension, NewFileInfo.CreationTime.ToString(), NewFileInfo.LastWriteTime.ToString(), string.Format($"{FileSize:#,####0}KB")];
                    ListViewItem NewItem = new(ItemInfo);
                    FileListView.Items.Add(NewItem);
                    FileItemInfo.Add(NewItem);
                }
                if (FileList != null) StatusLabel1.Text = string.Format("{0}개 항목", FileItemInfo.Count);
            }
        }

        /// <summary>
        /// ListView 파일 새로고침
        /// </summary>
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

        /// <summary>
        /// 이름 바꾸기 - 단일선택 
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
                if (TextRenderer.MeasureText(RenamerBox.Text + "  ", new Font("맑은 고딕", 9F)).Width <= 200) RenamerBox.Size = TextRenderer.MeasureText(RenamerBox.Text + "  ", new Font("맑은 고딕", 9F));
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

        /// <summary>
        /// 현재 ListView 선택한 항목 삭제
        /// </summary>
        private void Delete_File()
        {
            if (FolderPath != null)
            {
                bool ErrorState = false;
                List<ListViewItem> DeleteFileList = [];
                string ErrorMessage = "선택한 파일이 존재하지 않습니다.\n새로 고침 후 다시 시도해 주세요.\n\n";
                foreach (ListViewItem items in FileListView.SelectedItems)
                {
                    string DeleteFileName;
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

        /// <summary>
        /// 폴더 열기
        /// </summary>
        /// <param name="NewFolderPath"></param>
        private void OpenFolder(string NewFolderPath = "")
        {
            if (NewFolderPath == string.Empty)
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
            else
            {
                FolderPath = NewFolderPath;
                if (!FileLoadWorker.IsBusy)
                {
                    IsEmptyWorkSpaceLabel.Hide();
                    WorkPathLabel.Text = NewFolderPath;
                    FileListView.Items.Clear();
                    FileLoadWorker.RunWorkerAsync(FolderPath);
                }
                else
                {
                    FileLoadWorker.CancelAsync();
                    FileListView.Items.Clear();
                    FileItemInfo.Clear();
                    this.OpenFolder(FolderPath);
                }
            }
        }
    }
}

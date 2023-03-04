using MaterialSkin;

namespace FileManager
{
    /// <summary>
    /// 이름 바꾸기 이벤트 핸들러 
    /// </summary>
    /// <param name="Pattern">이름 변경 패턴</param>
    /// <param name="Regular">이름 변경 표현식</param>
    public delegate void RenameEventArgs(PatternMethod Pattern, string Regular);

    /// <summary>
    /// 이름 패턴 방식 
    /// </summary>
    public enum PatternMethod
    {
        /// <summary>
        /// 기존 파일명 + 새로운 이름 규칙 
        /// </summary>
        Append = 0,

        /// <summary>
        /// 기존 파일명에서 지정한 단어에 대해 대체 
        /// </summary>
        Replace = 1,

        /// <summary>
        /// 기존 파일명에서 특정 단어 또는 입력한 숫자만큼 삭제 
        /// </summary>
        Delete = 2,

        /// <summary>
        /// 새로운 이름 규칙의 파일명으로 대체 
        /// </summary>
        NewPattern = 3
    }

    public partial class Rename : Form
    {
        private PatternMethod CurrentPattern;
        public event RenameEventArgs? Renamed;
        #region Rename
        public Rename()
        {
            InitializeComponent();
            DetailOptionPanel.Select();
        }
        #endregion

        private void ChangeButton_Click(object sender, EventArgs e) // 변경 
        {
            Renamed?.Invoke(CurrentPattern, "d");
        }
        private void CloseButton_Click(object sender, EventArgs e) // 취소 
        {
            Close();
        }

        private void AppendRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AppendRadioButton.Checked)
            {
                CurrentPattern = PatternMethod.Append;
                Console.WriteLine(CurrentPattern);
            }
        }
        private void DeleteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DeleteRadioButton.Checked)
            {
                CurrentPattern = PatternMethod.Delete;
                Console.WriteLine(CurrentPattern);
            }
        }
        private void ReplaceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ReplaceRadioButton.Checked)
            {
                CurrentPattern = PatternMethod.Replace;
                Console.WriteLine(CurrentPattern);
            }
        }
        private void NewPatternRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (NewPatternRadioButton.Checked)
            {
                CurrentPattern = PatternMethod.NewPattern;
                Console.WriteLine(CurrentPattern);
            }
        }
    }
}

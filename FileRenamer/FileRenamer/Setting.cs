namespace FileRenamer
{
    /// <summary>
    /// 설정 이벤트 핸들러
    /// </summary>
    /// <param name="Filename">기본 파일 이름</param>
    /// <param name="Isdatetime">일괄 변경 선택</param>
    public delegate void SettingEventArgs(string? Filename, bool Isdatetime);

    public partial class Setting : Form
    {
        public event SettingEventArgs? ApplySetting;

        /// <summary>
        /// 기본 생성 파일 이름 ex)직박구리
        /// </summary>
        public string? DefaultFileName { get; set; }

        /// <summary>
        /// 만든 날짜, 수정한 날짜, 액세스 날짜 일괄 변경 선택
        /// </summary>
        public bool IsDateTimeAllChange { get; set; }

        public Setting()
        {
            InitializeComponent();

            // 종료 이벤트
            foreach (Control controls in this.Controls)
            {
                controls.KeyDown += KeyDown_SettingForm;
            }
        }
        private void Setting_Load(object sender, EventArgs e)
        {
            DefaultFileNameLabel.Text += DefaultFileName;
            BatchDateTimeCheckBox.Checked = IsDateTimeAllChange;
        }

        #region OKCancel
        private void ApplyButton_Click(object sender, EventArgs e) // 적용
        {
            ApplySetting?.Invoke(DefaultFileName, IsDateTimeAllChange);
            Close();
        }
        private void CloseButton_Click(object sender, EventArgs e) { Close(); } // 취소 
        #endregion

        private void KeyDown_SettingForm(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }
        private void InputFileNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
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
        private void BatchDateTimeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IsDateTimeAllChange = BatchDateTimeCheckBox.Checked;
        }
        private void InputFileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            DefaultFileName = InputFileNameTextBox.Text;
        }
    }
}

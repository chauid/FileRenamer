using System.Text.RegularExpressions;

namespace FileRenamer
{
    /// <summary>
    /// <para>이름 변경 표현식 유형 : {추가, 삭제, 대체, 새이름 정의, 숫자증분}</para>
    /// <para>이름 변경 표현식 구분자 : "{}"</para>
    /// </summary>
    public enum NamePatternType
    {
        /// <summary>
        /// 문자열 추가. 이름 변경 표현식 활용:{Append:"추가할 문자열", 추가할 위치(int), 인덱싱순서(Boolean)}
        /// </summary>
        Append = 0,

        /// <summary>
        /// 문자열 삭제. 이름 변경 표현식 활용:{Delete:삭제할 범위(int), 삭제할 위치(int), 인덱싱순서(Boolean)}
        /// </summary>
        Delete = 1,

        /// <summary>
        /// 문자열 대체. 이름 변경 표현식 활용:{Replace:"찾을 문자열", "대체할 문자열"}
        /// </summary>
        Replace = 2,

        /// <summary>
        /// 새로운 문자열 추가. 이름 변경 표현식 활용 : {NewNameSet:"새로운 문자열"}
        /// </summary>
        NewNameSet = 3,

        /// <summary>
        /// 숫자 자동 증가. 이름 변경 표현식 활용 : {AutoIncrement:시작값(int), 증분값(int)}
        /// </summary>
        Increment = 4
    }

    /// <summary>
    /// 이름 변경 표현식 클래스
    /// </summary>
    /// <param name="pattern">이름 변경 표현식</param>
    internal partial class RenamePattern(string pattern)
    {
        /// <summary>
        /// 입력 이름 변경 표현식
        /// </summary>
        private string pattern = pattern;

        /// <summary>
        /// 토큰순 이름 변경 표현식 유형
        /// </summary>
        public List<NamePatternType> PatternTypes { get; private set; } = [];

        /// <summary>
        /// 토큰순 이름 변경표현식 옵션값
        /// </summary>
        public List<string> PatternOptions { get; private set; } = [];

        /// <summary>
        /// 토큰 길이: {key:value} 개수
        /// </summary>
        public int Length { get; private set; } = 0;

        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string ErrorMessage { get; private set; } = string.Empty;

        /// <summary>
        /// <para>
        /// 생성자 기준 pattern 검증.
        /// </para>
        ///  영향받는 멤버 변수: PatternTypes, PatternOptions, Length, ErrorMessage
        /// </summary>
        public bool ValidatePattern()
        {
            if (String.IsNullOrWhiteSpace(pattern)) { ErrorMessage = "구문 오류: 빈 문자열"; return false; }
            return IsValidPattern(pattern);
        }

        /// <summary>
        /// <para>
        /// 입력값 기준 pattern 검증. 입력값으로 pattern값 변경.
        /// </para>
        /// 영향받는 멤버 변수: PatternTypes, PatternOptions, Length, ErrorMessage
        /// </summary>
        public bool ValidatePattern(string newPattern)
        {
            if (String.IsNullOrWhiteSpace(newPattern)) { ErrorMessage = "구문 오류: 빈 문자열"; return false; }
            if (IsValidPattern(newPattern)) { pattern = newPattern; return true; }
            return false;
        }

        /// <summary>
        /// 이름 변경 표현식 유효성 검증
        /// <para>{} => token 분류</para>
        /// <para>: => {이름 변경 표현식 유형 : 옵션값} 분류</para>
        /// <para>, => 옵션값 분류</para>
        /// <para>example: {tokenType:"option1",option2..}{tokenType:..}..</para>
        /// </summary>
        /// <param name="newPattern">입력 이름 변경 표현식</param>
        private bool IsValidPattern(string newPattern)
        {
            int openParen = 0;
            int closeParen = 0;
            for (int i = 0; i < newPattern.Length; i++)
            {
                if (newPattern[i] == '{') { openParen++; }
                if (newPattern[i] == '}') { closeParen++; }
            }
            if (openParen != closeParen) { ErrorMessage = "구문 오류: 괄호 불일치"; return false; }
            if (openParen == 0) { ErrorMessage = "이름 표현식을 찾을 수 없음"; return false; }
            Length = openParen;

            bool isReadToken = false;
            bool ignoreParen = false;
            string token = String.Empty;
            for (int i = 0; i < newPattern.Length; i++)
            {
                if (newPattern[i] == '\"') ignoreParen = !ignoreParen;
                if (ignoreParen) { token += newPattern[i]; continue; }

                if (newPattern[i] == '{') { isReadToken = true; continue; }
                if (newPattern[i] == '}') isReadToken = false;
                if (isReadToken) token += newPattern[i];
                else
                {
                    string[] split = token.Split(':');
                    if (split.Length != 2) { ErrorMessage = "key: value 쌍이 일치하지 않음."; return false; }
                    string[] optionSplit = split[1].Split(',');
                    try
                    {
                        switch (split[0])
                        {
                            case "Append":
                                if (optionSplit.Length != 3) { ErrorMessage = "구문 오류: Append"; ClearProperties(); return false; }
                                int quotes = optionSplit[0].Count(ch => (ch == '\"')); // -----------------------정규식으로 수정해야 함
                                Console.WriteLine(quotes);
                                int.Parse(optionSplit[1].Trim()); // 추가할 위치
                                bool.Parse(optionSplit[2].Trim()); // 인덱싱 순서
                                PatternTypes.Add(NamePatternType.Append);
                                break;
                            case "Delete":
                                if (optionSplit.Length != 3) { ErrorMessage = "구문 오류: Delete"; ClearProperties(); return false; }
                                int.Parse(optionSplit[0].Trim()); // 삭제할 범위
                                int.Parse(optionSplit[1].Trim()); // 삭제할 위치
                                bool.Parse(optionSplit[2].Trim()); // 인덱싱 순서
                                PatternTypes.Add(NamePatternType.Delete);
                                break;
                            case "Replace":
                                if (optionSplit.Length != 2) { ErrorMessage = "구문 오류: Replace"; ClearProperties(); return false; }
                                PatternTypes.Add(NamePatternType.Replace);
                                break;
                            case "NewNameSet":
                                if (optionSplit.Length != 1) { ErrorMessage = "구문 오류: NewNameSet"; ClearProperties(); return false; }
                                PatternTypes.Add(NamePatternType.NewNameSet);
                                break;
                            case "Increment":
                                if (optionSplit.Length != 2) { ErrorMessage = "구문 오류: Increment"; ClearProperties(); return false; }
                                int.Parse(optionSplit[0].Trim()); // 시작 번호
                                int.Parse(optionSplit[1].Trim()); // 증가량
                                PatternTypes.Add(NamePatternType.Increment);
                                break;
                            default:
                                ErrorMessage = "유효하지 않은 이름 표현식 유형";
                                return false;
                        }
                    }
                    catch (Exception)
                    {
                        ErrorMessage = "형변환 오류";
                        ClearProperties();
                        return false;
                    }
                    PatternOptions.Add(split[1]);
                    token = String.Empty;
                }
            }
            return true;
        }

        /// <summary>
        /// 멤버 변수 초기화
        /// <para>pattern, ErrorMessage 초기화X</para>
        /// </summary>
        private void ClearProperties()
        {
            PatternTypes.Clear();
            PatternOptions.Clear();
            Length = 0;
        }
    }
}

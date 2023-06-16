using System.Collections;

namespace FileRenamer
{
    /// <summary>
    /// ListView 정렬 
    /// </summary>
    internal class ListViewItemComparer : IComparer
    {
        private readonly int col;
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
        public int Compare(object? x, object? y)
        {
            if (x == null || y == null) return 0;
            if (sort == "asc")
            {
                if (col == 5) // 파일 크기 col = 5
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
                if (col == 5) // 파일 크기 col = 5
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

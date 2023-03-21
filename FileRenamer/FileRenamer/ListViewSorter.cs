using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileRenamer
{
    /// <summary>
    /// ListView 정렬 
    /// </summary>
    internal class ListViewItemComparer : IComparer
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

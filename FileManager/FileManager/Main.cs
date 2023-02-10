using System.Data;
using System.Data.SQLite;
namespace FileManager
{
    public partial class Main : Form
    {
        public Main()
        {
            DirectoryInfo currentPath = new DirectoryInfo(Application.StartupPath);
            Console.WriteLine(currentPath);
            SQLiteConnection.CreateFile("E:/a.sqlite");
            SQLiteConnection connection = new SQLiteConnection("Data Source=E:/a.sqlite;Version=3;");
            connection.Open();
            string query = "CREATE TABLE test(name varchar(20), code int);";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            int result = command.ExecuteNonQuery();

            query = "INSERT INTO test VALUES('¾È³ç', 30)";
            command = new SQLiteCommand(query, connection);
            result = command.ExecuteNonQuery();

            string sql = "select * from test";
            SQLiteCommand cmd = new SQLiteCommand(sql, connection);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                Console.WriteLine(string.Format("{0}, {1}", rdr["name"], rdr["code"]));
            }
            Label label= new Label();
            label.Location = new Point(1,1);
            label.Text = "dasd";
            this.Controls.Add(label);
            InitializeComponent();
        }
    }
}
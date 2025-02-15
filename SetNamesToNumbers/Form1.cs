using System.IO;

namespace SetNamesToNumbers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                label2.Text = "Status: Path is empty";
                return;
            }

            if (!Directory.Exists(textBox1.Text))
            {
                label2.Text = "Status: Directory is not found";
                return;
            }

            var directory = new DirectoryInfo(textBox1.Text);
            var files = directory.GetFiles()
                .Where(x => x.Name.Contains(".jpg"))
                .OrderBy(x => x.LastWriteTime)
                .ToList();

            var filesCount = files.Count;
            var counter = 1;

            for (int i = 0; i < filesCount; i++)
            {
                label2.Text = $"Status: In progress ({counter}/{filesCount})";
                files[i].MoveTo($"{directory.FullName}\\Screenshots\\{counter++}.jpg");
            }

            label2.Text = $"Status: Done";
        }
    }
}

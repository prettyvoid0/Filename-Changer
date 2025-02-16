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

            if (checkBox1.Checked)
            {
                var copyDirectoryName = directory.FullName + $"_copy";
                Directory.CreateDirectory(copyDirectoryName);
                foreach (var file in files)
                {
                    file.CopyTo($"{copyDirectoryName}\\{file.Name}");
                }
            }

            var tempDirectoryPath = directory.FullName + "_temp";
            Directory.CreateDirectory(tempDirectoryPath);
            foreach (var file in files)
            {
                file.MoveTo($"{tempDirectoryPath}\\{file.Name}");
            }

            foreach (var file in files)
            {
                label2.Text = $"Status: In progress ({counter}/{filesCount})";
                file.MoveTo($"{directory.FullName}\\{counter++}.jpg");
            }

            Directory.Delete(tempDirectoryPath);
            label2.Text = $"Status: Done";
        }
    }
}

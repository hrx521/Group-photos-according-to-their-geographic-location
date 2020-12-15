using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern void FreeConsole();
        public Form1()
        {
            InitializeComponent();
            //textBoxSource.Text = @"F:\baiduyundownload\吕强文丰园村\03加乐园村";
            //textBoxDest.Text = @"F:\baiduyundownload\吕强文丰园村\整理后";
            textBoxTolerence.Text = "300";
            comboBoxGpsDataSource.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;

            AllocConsole();   //开启控制台
        }

        private void buttonSource_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxGpsDataSource.SelectedIndex == 0)
                {

                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "TXT文件(*.TXT)|*.txt|CVS文件(*.CSV)|*.csv|所有文件|*.*";
                    ofd.ValidateNames = true;
                    ofd.CheckPathExists = true;
                    ofd.CheckFileExists = true;

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        textBoxSource.Text = ofd.FileName;
                    }
                }
                else
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    dialog.Description = "请选择图片文件源路径";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string foldPath = dialog.SelectedPath;
                        DirectoryInfo theFolder = new DirectoryInfo(foldPath);
                        FileInfo[] dirInfo = theFolder.GetFiles();
                        if (dirInfo.Count() == 0)
                            return;
                        textBoxSource.Text = theFolder.FullName;
                        ////遍历文件夹
                        //foreach (FileInfo file in dirInfo)
                        //{
                        //    MessageBox.Show(file.ToString());
                        //}

                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void buttonTarg_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择目标路径";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string foldPath = dialog.SelectedPath;
                    DirectoryInfo theFolder = new DirectoryInfo(foldPath);
                    FileInfo[] dirInfo = theFolder.GetFiles();
                    //if (dirInfo.Count() == 0)
                    //    return;
                    textBoxDest.Text = theFolder.FullName;
                    ////遍历文件夹
                    //foreach (FileInfo file in dirInfo)
                    //{
                    //    MessageBox.Show(file.ToString());
                    //}

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async void buttonExcuteGroup_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBoxProcess.Visible = true;
                buttonExcuteGroup.Enabled = false;
                labelProcess.Text = "开始挑拣。。。";
                var choseService = new ChoseService(textBoxSource.Text, textBoxDest.Text, Convert.ToInt32(textBoxTolerence.Text.Trim()), (comboBox1.SelectedIndex == 0 ? true : false));
                await choseService.ExcuteGroup();
                pictureBoxProcess.Visible = false;
                labelProcess.Text = "挑拣完成！";
                buttonExcuteGroup.Enabled = true;
            }
            catch (Exception ex) 
            {
                pictureBoxProcess.Visible = false;
                MessageBox.Show(ex.Message);
                labelProcess.Text = "";
                buttonExcuteGroup.Enabled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

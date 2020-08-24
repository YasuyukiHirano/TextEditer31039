using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditer31039
{
    public partial class Form1 : Form
    {
        //現在編集中のファイル名
        private string fileName = "";   //Camel形式(先頭文字が小文字) ⇔ Pascal形式(先頭文字が大文字(メソッド名など))

        public Form1()
        {
            InitializeComponent();
        }

        //ファイルプルダウンメニュー
        //新規作成メニュー
        private void NewNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Text = "";
            this.fileName = "";
        }

        //開くメニュー
        private void OpenOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //開くダイアログを表示
            if (ofdFileOpen.ShowDialog() == DialogResult.OK)
            {
                //StreamReaderクラスを使用してファイル読込
                using (StreamReader sr = new StreamReader(ofdFileOpen.FileName, Encoding.GetEncoding("utf-8"), false))
                {
                    rtTextArea.Text = sr.ReadToEnd();
                    this.fileName = ofdFileOpen.FileName;   //現在開いているファイル名を設定
                }
            }
        }

        //上書き保存メニュー
        private void SaveSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.fileName != "" ? FileSave(fileName) : SaveNameAToolStripMenuItem_Click(sender, e);

            if (this.fileName != "")
            {
                FileSave(fileName);
            }
            else
            {
                SaveNameAToolStripMenuItem_Click(sender, e);
            }
        }

        //名前を付けて保存メニュー
        private void SaveNameAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //[名前を付けて保存]ダイアログを表示
            if(sfdFileSave.ShowDialog() == DialogResult.OK)
            {
                FileSave(sfdFileSave.FileName);
                /*using (StreamWriter sw = new StreamWriter(sfdFileSave.FileName,false,Encoding.GetEncoding("utf-8")))
                {
                    sw.WriteLine(rtTextArea.Text);
                }*/
            }
        }

        //ファイル名を指定してデータを保存
        private void FileSave(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(rtTextArea.Text);

            }
        }

        //終了メニュー
        private void ExitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アプリケーション終了
            Application.Exit();
        }


        //編集プルダウンメニュー
        //元に戻すメニュー
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.CanUndo == true)
            {
                rtTextArea.Undo();
            }
        }
    }
}

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
            if(rtTextArea.Modified)
            {
                Message(sender, e);             
            }
            rtTextArea.Text = "";
            this.fileName = "";           
        }

        //開くメニュー
        private void OpenOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtTextArea.Modified)
            {
                Message(sender, e);
            }
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
            if (rtTextArea.Modified)
            {
                Message(sender, e);
            }
            //アプリケーション終了
            Application.Exit();
        }



        //編集プルダウンメニュー
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initButton();
        }
        //元に戻すメニュー
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Undo();        
        }

        //やり直しメニュー
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Redo();
        }

        //切り取りメニュー
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Cut();
        }

        //コピーメニュー
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Copy();           
        }

        //貼り付けメニュー
        private void PeastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.Paste();
        }

        //削除メニュー
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtTextArea.SelectedText = "";
        }

        //色メニュー
        private void ColerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cdColor.ShowDialog() == DialogResult.OK)
            {
                rtTextArea.ForeColor = cdColor.Color;
            }              
        }

        //フォントメニュー
        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fdFont.ShowDialog() == DialogResult.OK)
            {
                rtTextArea.Font = fdFont.Font;
            }
        }

        //マスクするメソッド
        void initButton()   //初期状態では変更ボタンはマスク
        {
            if (rtTextArea.Text == "")
            {
                UndoToolStripMenuItem.Enabled = false;  //元に戻す
                RedoToolStripMenuItem.Enabled = false;  //やり直し
                CutToolStripMenuItem.Enabled = false;   //切り取り
                CopyToolStripMenuItem.Enabled = false;  //コピー
                DeleteToolStripMenuItem.Enabled = false;    //削除
            }
            else
            {
                UndoToolStripMenuItem.Enabled = true;  //元に戻す
                RedoToolStripMenuItem.Enabled = true;  //やり直し
                CutToolStripMenuItem.Enabled = true;   //切り取り
                CopyToolStripMenuItem.Enabled = true;  //コピー
                DeleteToolStripMenuItem.Enabled = true;    //削除
            }


        }

        //未保存で警告を表示
        private void Message(object sender , EventArgs e)
        {
            DialogResult result = MessageBox.Show("ファイルを保存しますか？",
                "質問",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            //何が選択されたか調べる
            if (result == DialogResult.Yes)
            {
                //「はい」が選択された時
                SaveSToolStripMenuItem_Click(sender, e);           
            }
            else if (result == DialogResult.No)
            {
                //「いいえ」が選択された時
                rtTextArea.Text = "";
                fileName = "";
            }
            else if (result == DialogResult.Cancel)
            {
                //「キャンセル」が選択された時
            }
        }

        //フォームを閉じるときに確認
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rtTextArea.Modified)
            {
                Message(sender, e);
            }
            Application.Exit();
        }
    }
}

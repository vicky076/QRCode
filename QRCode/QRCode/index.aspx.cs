using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.IO;
using ZXing;
using System.Collections;

namespace QRCode
{
    public partial class index : System.Web.UI.Page
    {
        string QRUrl = "~/QRImage/temp";
        protected void Page_Load(object sender, EventArgs e)
        {
            labMsg.Text = "";
        }

        //產生條碼
        protected void btnCode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()).Equals(true))
            {
                labMsg.Text = "請輸入文字！";
                return;
            }

            if (string.IsNullOrEmpty(radType.SelectedValue).Equals(true))
            {
                labMsg.Text = "請選擇樣式！";
                return;
            }

            if (string.IsNullOrEmpty(txtSize.Text.Trim()).Equals(true))
            {
                labMsg.Text = "請輸入Size！";
                return;
            }

            CreateQRCode();
        }

        //解析
        protected void btnDeCode_Click(object sender, EventArgs e)
        {
            if (pictureBox2.ImageUrl == "")
            {
                labMsg.Text = "請上傳條碼圖檔！";
                return;
            }

            //顯示在畫面中
            pictureBox2.ImageUrl = "~/QRImage/" + txtpictureBox2.Text.Trim();

            System.Drawing.Bitmap bitmap = null;
            //宣告 QRCode Reader 物件
            ZXing.IBarcodeReader reader = new ZXing.BarcodeReader();

            //讀取要解碼的圖片
            FileStream fs = new FileStream(Server.MapPath("~/QRImage/" + txtpictureBox2.Text), FileMode.Open);
            Byte[] data = new Byte[fs.Length];
            // 把檔案讀取到位元組陣列
            fs.Read(data, 0, data.Length);
            fs.Close();
            // 實例化一個記憶體資料流 MemoryStream，將位元組陣列放入
            MemoryStream ms = new MemoryStream(data);
            // 將記憶體資料流的資料放到 BitMap的物件中
            bitmap = (Bitmap)Image.FromStream(ms);

            //將圖片顯示於 PictureBox 中
            //pictureBox2.ImageUrl = ("~/temp.jpg");
            //進行解碼的動作
            ZXing.Result result = reader.Decode(bitmap);

            //如果有成功解讀，則顯示文字
            if (result != null)
                txtDesCode.Text = result.Text;
            else
                labDesMsg.Text = "解讀失敗！";

            txtDesCode.Focus();
        }

        //上傳
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string FilePath, FileName, EName;
            int FileSize;

            if (fupImport.HasFile)
            {
                FileName = Path.GetFileName(fupImport.PostedFile.FileName);
                EName = Path.GetExtension(FileName);
                if (EName.ToLower() == ".jpeg" || EName.ToLower() == ".jpg" || EName.ToLower() == ".png" || EName.ToLower() == ".gif")
                {
                    FileSize = fupImport.PostedFile.ContentLength;
                    if (FileSize > 10000000)
                    {
                        labMsg.Text = "檔案大小超過限制(約10MB)。";
                        return;
                    }

                    //先判斷檔案是否存在，若存在就先刪除再存檔
                    FilePath = Server.MapPath("~/QRImage/");
                    if (System.IO.File.Exists(FilePath + EName))
                        System.IO.File.Delete(FilePath + EName);

                    try
                    {
                        fupImport.PostedFile.SaveAs(FilePath + FileName);
                    }
                    catch (Exception ex)
                    {
                        labMsg.Text = "上傳檔案發生錯誤。" + ex.Message + "|路徑：" + QRUrl;
                        return;
                    }
                }
                else
                {
                    labMsg.Text = "副檔名格式不符(.jpg)";
                    return;
                }
            }
            else
            {
                labMsg.Text = "未選擇任何檔案";
                return;
            }
            pictureBox2.ImageUrl = "~/QRImage/" + FileName;
            txtpictureBox2.Text = FileName;
            txtDesCode.Text = "";
        }

        //上傳取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            fupImport.Style.Clear();
        }

        //QRCode
        public void CreateQRCode()
        {
            System.Drawing.Bitmap bitmap = null;
            //要轉成QRCode 的內容
            string content = textBox1.Text;
            //QRCode的設定
            BarcodeWriter bw = new BarcodeWriter();
            switch (radType.SelectedValue)
            {
                case "QR":
                    bw.Format = BarcodeFormat.QR_CODE;
                    bw.Options.Width = Convert.ToInt32(txtSize.Text.Trim());
                    bw.Options.Height = Convert.ToInt32(txtSize.Text.Trim());
                    break;
                case "128":
                    bw.Format = BarcodeFormat.CODE_128;
                    bw.Options.Width = 100;
                    bw.Options.Height = Convert.ToInt32(txtSize.Text.Trim());
                    break;
                case "13":
                    bw.Format = BarcodeFormat.EAN_13;
                    bw.Options.Width = 100;
                    bw.Options.Height = Convert.ToInt32(txtSize.Text.Trim());
                    break;
                default:
                    break;
            }
            //將要編碼的文字產生出QRCode的圖檔
            bitmap = bw.Write(content);

            //儲存圖片
            switch (ddlStype.SelectedValue)
            {
                case ".jpg":
                    bitmap.Save(Server.MapPath(QRUrl + ddlStype.SelectedValue), System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case ".png":
                    bitmap.Save(Server.MapPath(QRUrl + ddlStype.SelectedValue), System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case ".gif":
                    bitmap.Save(Server.MapPath(QRUrl + ddlStype.SelectedValue), System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                default:
                    break;
            }

            //顯示在畫面中
            pictureBox1.ImageUrl = QRUrl + ddlStype.SelectedValue;
        }
    }
}
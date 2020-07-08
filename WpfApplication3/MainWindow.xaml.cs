using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using BarcodeLib;
using DataMatrix.net;
using Microsoft.Win32;
using ThoughtWorks.QRCode.Codec;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Barcode _linearEncoder;
        Bitmap _bitmap;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded +=new RoutedEventHandler(MainWindow_Loaded);
        }

        private void MainWindow_Loaded (object sender, RoutedEventArgs e)
        {
            this.ChildWindow1.Show();
            this.Cbo_L_E_Type.ItemsSource = Enum.GetNames(typeof(TYPE));
            this.Cbo_L_LPosition.ItemsSource = Enum.GetNames(typeof(LabelPositions));
            this.Cbo_L_Rotate.ItemsSource = Enum.GetNames(typeof(RotateFlipType));
            this.Cbo_L_Alignment.ItemsSource = Enum.GetNames(typeof(AlignmentPositions));

            this.Cbo_QR_Scale.ItemsSource = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            this.Cbo_QR_Mode.ItemsSource = Enum.GetNames(typeof(QRCodeEncoder.ENCODE_MODE));
            this.Cbo_QR_Version.ItemsSource = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40" };
            this.Cbo_QR_ErrorC.ItemsSource = Enum.GetNames(typeof(QRCodeEncoder.ERROR_CORRECTION));

            this.Cbo_D_Size.ItemsSource = Enum.GetNames(typeof(DmtxSymbolSize));
            this.Cbo_D_Scheme.ItemsSource = Enum.GetNames(typeof(DmtxScheme));
            this.Cbo_D_Module.ItemsSource = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            this.Cbo_D_Margin.ItemsSource = new string[] { "1", "2", "3", "4", "5" };

            this.Cbo_L_E_Type.SelectedItem = TYPE.CODE128.ToString();
            this.Cbo_L_LPosition.SelectedItem = LabelPositions.BOTTOMCENTER.ToString();
            this.Cbo_L_Rotate.SelectedItem = RotateFlipType.RotateNoneFlipNone.ToString();
            this.Cbo_L_Alignment.SelectedItem = AlignmentPositions.CENTER.ToString();
            this.Cbo_QR_Scale.SelectedItem = "4";
            this.Cbo_QR_Mode.SelectedItem = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC.ToString();
            this.Cbo_QR_Version.SelectedItem = "6";
            this.Cbo_QR_ErrorC.SelectedItem = QRCodeEncoder.ERROR_CORRECTION.M.ToString();
            this.Cbo_D_Size.SelectedItem = DmtxSymbolSize.DmtxSymbol104x104.ToString();
            this.Cbo_D_Scheme.SelectedItem = DmtxScheme.DmtxSchemeAscii.ToString();
            this.Cbo_D_Module.SelectedItem = "4";
            this.Cbo_D_Margin.SelectedItem = "2";
            this.Rdo_Linear.IsChecked = true;
            this.Txt_InputData.Text = "03800035";
        }

        private void Btn_Generate_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.Txt_InputData.Text))
                return;

            this.GenerateBarcode(this.Txt_InputData.Text);
        }

        private void GenerateBarcode(string inputData)
        {
            if (this.Rdo_Linear.IsChecked.GetValueOrDefault())
                try
                {
                    TYPE type = (TYPE)Enum.Parse(typeof(TYPE), this.Cbo_L_E_Type.SelectedItem.ToString());

                    if (type == TYPE.UNSPECIFIED)
                        return;

                    _linearEncoder = new Barcode();
                    this.TextBox1.Text = _linearEncoder.Country_Assigning_Manufacturer_Code;

                    if (!String.IsNullOrEmpty(this.Txt_label.Text))
                        _linearEncoder.AlternateLabel = this.Txt_label.Text;

                    _linearEncoder.BackColor = System.Drawing.Color.White;
                    _linearEncoder.ForeColor = System.Drawing.Color.Black;
                    _linearEncoder.IncludeLabel = this.CheckBox1.IsChecked.GetValueOrDefault();
                    //_linearEncoder.LabelPosition = EnumUtility.Convert<LabelPositions>(this.Cbo_L_LPosition.SelectedItem.ToString());
                    _linearEncoder.RotateFlipType = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), this.Cbo_L_Rotate.SelectedItem.ToString());
                    _linearEncoder.EncodedType = (TYPE)Enum.Parse(typeof(TYPE), this.Cbo_L_E_Type.SelectedItem.ToString());
                    //_linearEncoder.Alignment = EnumUtility.Convert<AlignmentPositions>(this.Cbo_L_Alignment.SelectedItem.ToString());
                    Image image = _linearEncoder.Encode(type, inputData);
                    _bitmap = new Bitmap(image);
                    this.Image.Width = _bitmap.Width;
                    this.Image.Height = _bitmap.Height;
                    this.Image.Source = ConvertImage(_bitmap);
                    this.ChildWindow2.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }

            if (this.Rdo_QR.IsChecked.GetValueOrDefault())
                try
                {
                    var qrEncoder = new QRCodeEncoder();
                    qrEncoder.QRCodeBackgroundColor = System.Drawing.Color.White;
                    qrEncoder.QRCodeForegroundColor = System.Drawing.Color.Black;
                    qrEncoder.QRCodeScale = Convert.ToInt32(Cbo_QR_Scale.SelectedItem);
                    qrEncoder.QRCodeVersion = Convert.ToInt32(Cbo_QR_Version.SelectedItem);
                    qrEncoder.QRCodeEncodeMode = (QRCodeEncoder.ENCODE_MODE)Enum.Parse(typeof(QRCodeEncoder.ENCODE_MODE),
                        this.Cbo_QR_Mode.SelectedItem.ToString());
                    qrEncoder.QRCodeErrorCorrect = (QRCodeEncoder.ERROR_CORRECTION)Enum.Parse(typeof(QRCodeEncoder.ERROR_CORRECTION),
                        this.Cbo_QR_ErrorC.SelectedItem.ToString());
                    var image = qrEncoder.Encode(inputData);
                    _bitmap = new Bitmap(image);
                    this.Image.Width = _bitmap.Width;
                    this.Image.Height = _bitmap.Height;
                    this.Image.Source = ConvertImage(_bitmap);
                    this.ChildWindow2.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }

            if (this.Rdo_DataM.IsChecked.GetValueOrDefault())
                try
                {
                    var dataEncoder = new DmtxImageEncoder();
                    var dataEncoderOptions = new DmtxImageEncoderOptions();
                    dataEncoderOptions.SizeIdx = (DmtxSymbolSize)Enum.Parse(typeof(DmtxSymbolSize),
                        this.Cbo_D_Size.SelectedItem.ToString());
                    dataEncoderOptions.Scheme = (DmtxScheme)Enum.Parse(typeof(DmtxScheme),
                        this.Cbo_D_Scheme.SelectedItem.ToString());
                    dataEncoderOptions.ModuleSize = Convert.ToInt32(Cbo_D_Module.SelectedItem);
                    dataEncoderOptions.MarginSize = Convert.ToInt32(this.Cbo_D_Margin.SelectedItem);
                    dataEncoderOptions.ForeColor = System.Drawing.Color.Black;
                    dataEncoderOptions.BackColor = System.Drawing.Color.White;
                    Image image = dataEncoder.EncodeImage(inputData, dataEncoderOptions);
                    _bitmap = new Bitmap(image);
                    this.Image.Width = _bitmap.Width;
                    this.Image.Height = _bitmap.Height;
                    this.Image.Source = ConvertImage(_bitmap);
                    this.ChildWindow2.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
        }

        private BitmapImage ConvertImage(object value)
        {
            using (var ms = new MemoryStream())
            {
                var image = (System.Drawing.Image)value;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                image.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            ///Comprobar
            ///https://online-barcode-reader.inliteresearch.com/
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "BMP (*.bmp)|*.bmp|GIF (*.gif)|*.gif|JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|TIFF (*.tif)|*.tif";
            dialog.FilterIndex = 4;
            dialog.AddExtension = true;

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                SaveTypes savetype1 = SaveTypes.UNSPECIFIED;
                ImageFormat savetype2 = ImageFormat.Bmp;

                switch (dialog.FilterIndex)
                {
                    case 1: savetype1 = SaveTypes.BMP;
                        savetype2 = ImageFormat.Bmp;
                        break;
                    case 2: savetype1 = SaveTypes.GIF;
                        savetype2 = ImageFormat.Gif;
                        break;
                    case 3: savetype1 = SaveTypes.JPG;
                        savetype2 = ImageFormat.Jpeg;
                        break;
                    case 4: savetype1 = SaveTypes.PNG;
                        savetype2 = ImageFormat.Png;
                        break;
                    case 5: savetype1 = SaveTypes.TIFF;
                        savetype2 = ImageFormat.Tiff;
                        break;
                    default: break;
                }

                if (_linearEncoder != null)
                    _linearEncoder.SaveImage(dialog.FileName, savetype1);
                else
                    if (_bitmap != null)
                        _bitmap.Save(dialog.FileName, savetype2);
            }
        }

        private void Btn_ToXML_Click(object sender, RoutedEventArgs e)
        {
            if (_linearEncoder == null)
                return;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "XML Files|*.xml";

            if (dialog.ShowDialog().GetValueOrDefault())
                using (StreamWriter streamWriter = new StreamWriter(dialog.FileName))
                    streamWriter.Write(_linearEncoder.XML);
        }

        //Recuperar un código de barra de un archivo tipo XML
        //private void btnLoadXML_Click(object sender, EventArgs e)
        //{
        //    using (OpenFileDialog ofd = new OpenFileDialog())
        //    {
        //        ofd.Multiselect = false;
        //        if (ofd.ShowDialog() == DialogResult.OK)
        //        {
        //            using (BarcodeLib.BarcodeXML XML = new BarcodeLib.BarcodeXML())
        //            {
        //                XML.ReadXml(ofd.FileName);

        //                //load image from xml
        //                this.barcode.Width = XML.Barcode[0].ImageWidth;
        //                this.barcode.Height = XML.Barcode[0].ImageHeight;
        //                this.barcode.BackgroundImage = BarcodeLib.Barcode.GetImageFromXML(XML);

        //                //populate the screen
        //                this.txtData.Text = XML.Barcode[0].RawData;
        //                this.chkGenerateLabel.Checked = XML.Barcode[0].IncludeLabel;

        //                switch (XML.Barcode[0].Type)
        //                {
        //                    case "UCC12":
        //                    case "UPCA":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("UPC-A");
        //                        break;
        //                    case "UCC13":
        //                    case "EAN13":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("EAN-13");
        //                        break;
        //                    case "Interleaved2of5":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Interleaved 2 of 5");
        //                        break;
        //                    case "Industrial2of5":
        //                    case "Standard2of5":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Standard 2 of 5");
        //                        break;
        //                    case "LOGMARS":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("LOGMARS");
        //                        break;
        //                    case "CODE39":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 39");
        //                        break;
        //                    case "CODE39Extended":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 39 Extended");
        //                        break;
        //                    case "CODE39_Mod43":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 39 Mod 43");
        //                        break;
        //                    case "Codabar":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Codabar");
        //                        break;
        //                    case "PostNet":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("PostNet");
        //                        break;
        //                    case "ISBN":
        //                    case "BOOKLAND":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Bookland/ISBN");
        //                        break;
        //                    case "JAN13":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("JAN-13");
        //                        break;
        //                    case "UPC_SUPPLEMENTAL_2DIGIT":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("UPC 2 Digit Ext.");
        //                        break;
        //                    case "MSI_Mod10":
        //                    case "MSI_2Mod10":
        //                    case "MSI_Mod11":
        //                    case "MSI_Mod11_Mod10":
        //                    case "Modified_Plessey":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("MSI");
        //                        break;
        //                    case "UPC_SUPPLEMENTAL_5DIGIT":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("UPC 5 Digit Ext.");
        //                        break;
        //                    case "UPCE":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("UPC-E");
        //                        break;
        //                    case "EAN8":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("EAN-8");
        //                        break;
        //                    case "USD8":
        //                    case "CODE11":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 11");
        //                        break;
        //                    case "CODE128":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 128");
        //                        break;
        //                    case "CODE128A":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 128-A");
        //                        break;
        //                    case "CODE128B":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 128-B");
        //                        break;
        //                    case "CODE128C":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 128-C");
        //                        break;
        //                    case "ITF14":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("ITF-14");
        //                        break;
        //                    case "CODE93":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Code 93");
        //                        break;
        //                    case "FIM":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("FIM");
        //                        break;
        //                    case "Pharmacode":
        //                        this.cbEncodeType.SelectedIndex = this.cbEncodeType.FindString("Pharmacode");
        //                        break;

        //                    default: throw new Exception("ELOADXML-1: Unsupported encoding type in XML.");
        //                }

        //                this.txtEncoded.Text = XML.Barcode[0].EncodedValue;
        //                this.btnForeColor.BackColor = ColorTranslator.FromHtml(XML.Barcode[0].Forecolor);
        //                this.btnBackColor.BackColor = ColorTranslator.FromHtml(XML.Barcode[0].Backcolor); ;
        //                this.txtWidth.Text = XML.Barcode[0].ImageWidth.ToString();
        //                this.txtHeight.Text = XML.Barcode[0].ImageHeight.ToString();

        //                //populate the local object
        //                btnEncode_Click(sender, e);

        //                //reposition the barcode image to the middle
        //                barcode.Location = new Point((this.barcode.Location.X + this.barcode.Width / 2) - barcode.Width / 2, (this.barcode.Location.Y + this.barcode.Height / 2) - barcode.Height / 2);
        //            }
        //        }
        //    }
        //}
    }
}

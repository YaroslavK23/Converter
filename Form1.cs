using System.Globalization;

namespace Convert_Color_Picture_to_Grayscale
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK) 
            {
                try
                {
                    pictureBoxOpenFile.Image = new Bitmap(openFileDialog.FileName);
                }
                catch
                {
                    MessageBox.Show("The selected file cannot be opened" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            if (pictureBoxOpenFile.Image != null) 
            {
                Bitmap inputImage = new Bitmap(pictureBoxOpenFile.Image);
                Bitmap outputImage = new Bitmap(inputImage.Width,inputImage.Height);

                for (int y = 0; y < inputImage.Height; y++)
                {
                    for (int x = 0; x < inputImage.Width; x++)
                    {
                        UInt32 inputPixel = (UInt32)(inputImage.GetPixel(x, y).ToArgb());

                        float R = (float)((inputPixel & 0x00FF0000) >> 16);
                        float G = (float)((inputPixel & 0x0000FF00) >> 8);
                        float B = (float)(inputPixel & 0x000000FF);

                        R = G = B = (R + G + B) / 3.0f;

                        UInt32 outputPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | (UInt32)B;

                        outputImage.SetPixel(x, y, Color.FromArgb((int)outputPixel));
                    }
                }
                pictureBoxSaveFile.Image = outputImage;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (pictureBoxSaveFile.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Save image as..";
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.CheckPathExists = true;
                saveFileDialog.Filter = "Image File (*.BMP)|*.BMP|Image File (*.JPG)|*.JPG|Image File (*.GIF)|*.GIF|" +
                    "Image File (*.PNG)|*.PNG|All files (*.*)|*.*";
                saveFileDialog.ShowHelp = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBoxSaveFile.Image.Save(saveFileDialog.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("The selected file cannot be saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        
    }
}
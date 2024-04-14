namespace DZ_Lab3_IMG
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {

        private PictureBox originalPictureBox = new PictureBox();
        private PictureBox grayScalePictureBox = new PictureBox();
        private PictureBox thresholdPictureBox = new PictureBox();
        private PictureBox negativePictureBox = new PictureBox();
        private PictureBox greenScalePictureBox = new PictureBox();
        private Button loadButton = new Button();
        private Button processButton = new Button();
        private OpenFileDialog openFileDialog = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Width = 1000;  
            this.Height = 700;
            this.Text = "Image Processing";

            int picBoxWidth = 480;  
            int picBoxHeight = 450; 

            originalPictureBox.SetBounds(10, 10, picBoxWidth, picBoxHeight);
            originalPictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            int processedPicBoxWidth = picBoxWidth / 2;
            int processedPicBoxHeight = picBoxHeight / 2;

            grayScalePictureBox.SetBounds(500, 10, processedPicBoxWidth, processedPicBoxHeight);
            grayScalePictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            thresholdPictureBox.SetBounds(500 + processedPicBoxWidth, 10, processedPicBoxWidth, processedPicBoxHeight);
            thresholdPictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            negativePictureBox.SetBounds(500, 10 + processedPicBoxHeight, processedPicBoxWidth, processedPicBoxHeight);
            negativePictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            greenScalePictureBox.SetBounds(500 + processedPicBoxWidth, 10 + processedPicBoxHeight, processedPicBoxWidth, processedPicBoxHeight);
            greenScalePictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            loadButton.SetBounds(10, 460, 160, 30);
            loadButton.Text = "Load Image";
            loadButton.Click += LoadButton_Click;

            processButton.SetBounds(180, 460, 160, 30);
            processButton.Text = "Process Images";
            processButton.Click += ProcessButton_Click;

            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg";
            openFileDialog.FilterIndex = 1;

            this.Controls.Add(originalPictureBox);
            this.Controls.Add(grayScalePictureBox);
            this.Controls.Add(thresholdPictureBox);
            this.Controls.Add(negativePictureBox);
            this.Controls.Add(greenScalePictureBox);
            this.Controls.Add(loadButton);
            this.Controls.Add(processButton);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                var originalImage = Image.FromFile(filename);
                originalPictureBox.Image = originalImage;
            }
        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            if (originalPictureBox.Image != null)
            {
                ProcessImage(originalPictureBox.Image);
            }
            else
            {
                MessageBox.Show("Please load an image first.");
            }
        }

        private void ProcessImage(Image image)
        {
            Bitmap originalBitmap = new Bitmap(image);
            Bitmap grayScale = new Bitmap(originalBitmap);
            Bitmap threshold = new Bitmap(originalBitmap);
            Bitmap negative = new Bitmap(originalBitmap);
            Bitmap greenScale = new Bitmap(originalBitmap);

            Parallel.Invoke(
                () => ApplyGrayScale(grayScale),
                () => ApplyThreshold(threshold),
                () => ApplyGreenScale(greenScale),
                () => ApplyNegative(negative)
            );

            this.Invoke(new Action(() =>
            {
                grayScalePictureBox.Image = grayScale;
                thresholdPictureBox.Image = threshold;
                negativePictureBox.Image = negative;
                greenScalePictureBox.Image = greenScale;

            }));
        }

        private Bitmap ApplyGrayScale(Bitmap image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color originalColor = image.GetPixel(i, j);
                    int grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);
                    image.SetPixel(i, j, newColor);
                }
            }
            return image;
        }

        private Bitmap ApplyThreshold(Bitmap image)
        {
            int thresholdValue = 110; // choose threshold value
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color originalColor = image.GetPixel(i, j);
                    int intensity = (int)((originalColor.R + originalColor.G + originalColor.B) / 3);
                    Color newColor = intensity >= thresholdValue ? Color.White : Color.Black;
                    image.SetPixel(i, j, newColor);
                }
            }
            return image;
        }

        private Bitmap ApplyNegative(Bitmap image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color originalColor = image.GetPixel(i, j);
                    Color newColor = Color.FromArgb(255 - originalColor.R, 255 - originalColor.G, 255 - originalColor.B);
                    image.SetPixel(i, j, newColor);
                }
            }
            return image;
        }

        /*
        private Bitmap ApplyGreenScale(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;
            Bitmap greenScaleImage = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color originalColor = image.GetPixel(x, y);
                    Color greenScaleColor = Color.FromArgb(0, originalColor.G, 0);
                    greenScaleImage.SetPixel(x, y, greenScaleColor);
                }
            }

            return greenScaleImage;
        }
        */
        private void ApplyGreenScale(Bitmap image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color originalColor = image.GetPixel(i, j);

                    int green = originalColor.G;
                    Color greenColor = Color.FromArgb(0, green, 0);
                    image.SetPixel(i, j, greenColor);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
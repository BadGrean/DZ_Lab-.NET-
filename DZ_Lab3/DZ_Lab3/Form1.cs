using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;

namespace DZ_Lab3
{
    public partial class Form1 : Form
    {
        public int[,] matrix1;
        public int[,] matrix2;


        int rows1 = 200;  // Specify matrix size here
        int cols1 = 200;
        int rows2 = 200;  // Specify matrix size here
        int cols2 = 200;

        public int[,] matrix_seq;
        public int[,] matrix_thread;


        private Button btnGenerate;
        private Button btnMultiply;
        private RichTextBox txtMatrix1;
        private RichTextBox txtMatrix2;
        private RichTextBox txtResult;
        private RichTextBox txtResultThreaded;
        private TextBox txtTimeElapsed;
        private TextBox txtTimeElapsedThreaded;
        private Label lblNonThreadedResult;
        private Label lblThreadedResult;
        private RichTextBox txtResultParallel;
        private TextBox txtTimeElapsedParallel;
        private Label lblParallelResult;

        public Form1()
        {
            InitializeComponent();
            SetupForm();
        }
        private void MultiplyMatricesThreaded(int[,] m1, int[,] m2, int[,] result, int startRow, int endRow)
        {
            int m1Cols = m1.GetLength(1);
            int m2Cols = m2.GetLength(1);

            for (int i = startRow; i < endRow; i++)
            {
                for (int j = 0; j < m2Cols; j++)
                {
                    int temp = 0;
                    for (int k = 0; k < m1Cols; k++)
                    {
                        temp += m1[i, k] * m2[k, j];
                    }
                    result[i, j] = temp;
                }
            }
        }

        private int[,] MultiplyMatricesUsingThreads(int[,] m1, int[,] m2, int TC)
        {
            int rowCount = m1.GetLength(0);
            int[,] result = new int[rowCount, m2.GetLength(1)];
            int rowsPerThread = rowCount / TC;
            int remainingRows = rowCount % TC;
            int completedThreads = 0;
            ManualResetEvent doneEvent = new ManualResetEvent(false);

            int startRow = 0;
            for (int i = 0; i < TC; i++)
            {
                int endRow = startRow + rowsPerThread + (i < remainingRows ? 1 : 0);
                ThreadPool.QueueUserWorkItem((stateInfo) =>
                {
                    int[] range = (int[])stateInfo;
                    MultiplyMatricesThreaded(m1, m2, result, range[0], range[1]);
                    if (Interlocked.Increment(ref completedThreads) == TC)
                    {
                        doneEvent.Set();
                    }
                }, new int[] { startRow, endRow });

                startRow = endRow;
            }

            doneEvent.WaitOne();
            return result;
        }

        private int[,] MultiplyMatricesParallel(int[,] m1, int[,] m2)
        {
            int m1Rows = m1.GetLength(0);
            int m2Cols = m2.GetLength(1);
            int m1Cols = m1.GetLength(1);
            int[,] result = new int[m1Rows, m2Cols];

            ParallelOptions options = new()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };

            Parallel.ForEach(Enumerable.Range(0, m1Rows), options, (int i) =>
            {
                for (int j = 0; j < m2Cols; j++)
                {
                    int temp = 0;
                    for (int k = 0; k < m1Cols; k++)
                    {
                        temp += m1[i, k] * m2[k, j];
                    }
                    result[i, j] = temp;
                }
            });

            return result;
        }
        private int[,] GenerateMatrix(int rows, int cols)
        {
            Random rnd = new Random();
            int[,] matrix = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rnd.Next(0, 40);  //number size
                }
            }
            return matrix;
        }

        private string MatrixToString(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            string result = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result += matrix[i, j].ToString().PadLeft(6);
                }
                result += Environment.NewLine;
            }
            return result;
        }

        private void SetupForm()
        {
            btnGenerate = new Button
            {
                Text = "Generate Matrices",
                Location = new System.Drawing.Point(50, 20),
                Size = new System.Drawing.Size(200, 30)
            };
            btnGenerate.Click += BtnGenerate_Click;
            Controls.Add(btnGenerate);

            txtMatrix1 = new RichTextBox
            {
                Font = new Font("Courier New", 10, FontStyle.Regular),
                Multiline = true,
                Location = new System.Drawing.Point(50, 60),
                Size = new System.Drawing.Size(300, 200),
                ScrollBars = RichTextBoxScrollBars.Both,
                WordWrap = false
            };
            Controls.Add(txtMatrix1);

            txtMatrix2 = new RichTextBox
            {
                Font = new Font("Courier New", 10, FontStyle.Regular),
                Multiline = true,
                Location = new System.Drawing.Point(370, 60),
                Size = new System.Drawing.Size(300, 200),
                ScrollBars = RichTextBoxScrollBars.Both,
                WordWrap = false
            };
            Controls.Add(txtMatrix2);

            btnMultiply = new Button
            {
                Text = "Multiply Matrices",
                Location = new System.Drawing.Point(50, 270),
                Size = new System.Drawing.Size(200, 30)
            };
            btnMultiply.Click += BtnMultiply_Click;
            Controls.Add(btnMultiply);

            txtResult = new RichTextBox
            {
                Font = new Font("Courier New", 10, FontStyle.Regular),
                Multiline = true,
                Location = new System.Drawing.Point(50, 320),
                Size = new System.Drawing.Size(300, 200),
                ScrollBars = RichTextBoxScrollBars.Both,
                WordWrap = false
            };
            Controls.Add(txtResult);

            txtResultThreaded = new RichTextBox
            {
                Font = new Font("Courier New", 10, FontStyle.Regular),
                Multiline = true,
                Location = new System.Drawing.Point(370, 320),
                Size = new System.Drawing.Size(300, 200),
                ScrollBars = RichTextBoxScrollBars.Both,
                WordWrap = false
            };
            Controls.Add(txtResultThreaded);

            txtTimeElapsed = new TextBox
            {
                Font = new Font("Courier New", 8, FontStyle.Regular),
                Multiline = false,
                Location = new System.Drawing.Point(50, 530),
                Size = new System.Drawing.Size(100, 20),  
                ReadOnly = true  
            };
            Controls.Add(txtTimeElapsed);


            txtTimeElapsedThreaded = new TextBox
            {
                Font = new Font("Courier New", 8, FontStyle.Regular),
                Multiline = false,
                Location = new System.Drawing.Point(370, 530), 
                Size = new System.Drawing.Size(100, 20),
                ReadOnly = true
            };
            Controls.Add(txtTimeElapsedThreaded);

            lblNonThreadedResult = new Label
            {
                Text = "Non-Threaded Result",
                Location = new System.Drawing.Point(50, 300),  
                Size = new System.Drawing.Size(120, 20)
            };
            Controls.Add(lblNonThreadedResult);

            lblThreadedResult = new Label
            {
                Text = "Threaded Result",
                Location = new System.Drawing.Point(370, 300),  
                Size = new System.Drawing.Size(120, 20)
            };
            Controls.Add(lblThreadedResult);
            txtResultParallel = new RichTextBox
            {
                Font = new Font("Courier New", 10, FontStyle.Regular),
                Multiline = true,
                Location = new Point(690, 320), 
                Size = new Size(300, 200),
                ScrollBars = RichTextBoxScrollBars.Both,
                WordWrap = false
            };
            Controls.Add(txtResultParallel);

            txtTimeElapsedParallel = new TextBox
            {
                Font = new Font("Courier New", 8, FontStyle.Regular),
                Multiline = false,
                Location = new Point(690, 530),
                Size = new Size(100, 20),
                ReadOnly = true
            };
            Controls.Add(txtTimeElapsedParallel);

            lblParallelResult = new Label
            {
                Text = "Parallel Result",
                Location = new Point(690, 300),
                Size = new Size(120, 20)
            };
            Controls.Add(lblParallelResult);
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            matrix1 = GenerateMatrix(rows1, cols1);
            matrix2 = GenerateMatrix(rows2, cols2);
            txtMatrix1.Text = MatrixToString(matrix1);
            txtMatrix2.Text = MatrixToString(matrix2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialization code here
        }

        private void BtnMultiply_Click(object sender, EventArgs e)
        {
            if (matrix1 == null || matrix2 == null)
            {
                txtResult.Text = "Please generate the matrices first.";
                txtResultThreaded.Text = "Please generate the matrices first.";
                return;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();
            matrix_seq = MultiplyMatricesUsingThreads(matrix1, matrix2, 1);
            watch.Stop();
            //txtResult.Text = MatrixToString(matrix_seq);
            txtTimeElapsed.Text = $"Time: {watch.ElapsedMilliseconds} ms"; // Display non-threaded time

            // Threaded multiplication timing
            watch.Restart();
            matrix_thread = MultiplyMatricesUsingThreads(matrix1, matrix2, 12);
            watch.Stop();
            //txtResultThreaded.Text = MatrixToString(matrix_thread);
            txtTimeElapsedThreaded.Text = $"Time: {watch.ElapsedMilliseconds} ms"; // Display threaded time

            var watchParallel = System.Diagnostics.Stopwatch.StartNew();
            int[,] matrix_parallel = MultiplyMatricesParallel(matrix1, matrix2);
            watchParallel.Stop();
            //txtResultParallel.Text = MatrixToString(matrix_parallel);
            txtTimeElapsedParallel.Text = $"Time: {watchParallel.ElapsedMilliseconds} ms"; // Display parallel time
        }
    }
    
}

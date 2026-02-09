using System;
using System.Drawing;
using System.Windows.Forms;

class SimpleGUI : Form
{
    ProgressBar loadingBar;
    Label infoLabel;
    Button exitButton;
    Button gameButton;

    public SimpleGUI()
    {
        this.Text = "Simple GUI";
        this.Size = new Size(500, 300);

        // 로딩바
        loadingBar = new ProgressBar();
        loadingBar.Size = new Size(300, 30);
        loadingBar.Location = new Point(100, 50);
        this.Controls.Add(loadingBar);

        // 텍스트
        infoLabel = new Label();
        infoLabel.Text = "Loading...";
        infoLabel.Size = new Size(400, 50);
        infoLabel.Location = new Point(50, 100);
        infoLabel.TextAlign = ContentAlignment.MiddleCenter;
        this.Controls.Add(infoLabel);

        // 버튼
        exitButton = new Button();
        exitButton.Text = "Exit";
        exitButton.Size = new Size(100, 40);
        exitButton.Location = new Point(100, 200);
        exitButton.Click += (s, e) => Application.Exit();
        exitButton.Visible = false;
        this.Controls.Add(exitButton);

        gameButton = new Button();
        gameButton.Text = "Game";
        gameButton.Size = new Size(100, 40);
        gameButton.Location = new Point(300, 200);
        gameButton.Click += (s, e) => infoLabel.Text = "준비중";
        gameButton.Visible = false;
        this.Controls.Add(gameButton);

        // 로딩 시작 (WinForms Timer 사용)
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        int progress = 0;
        timer.Interval = 50; // 50ms
        timer.Tick += (s, e) =>
        {
            progress++;
            loadingBar.Value = Math.Min(progress, 100);
            if (progress >= 100)
            {
                timer.Stop();
                infoLabel.Text = "by epsomm";
                loadingBar.Visible = false;
                exitButton.Visible = true;
                gameButton.Visible = true;
            }
        };
        timer.Start();
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new SimpleGUI());
    }
}

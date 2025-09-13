using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace LTLVC_Control_Panel
{
    public partial class Form1 : Form
    {
        Int32 sec = 0;
        Int32 min = 0;
        Int32 hr = 0;
        Int32 day = 0;

        String below10 = ":";
        String below1 = ":";
        String below1_2 = ":";

        String startingtime;
        public Form1()
        {
            InitializeComponent();
            timer3.Start(); // timer to get current date and time
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            toolStripStatusLabel2.Text = Convert.ToString(currentTime);
        }

        private void startStopwatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Start(); // timer to get current elasped time

            DateTime currentTime = DateTime.Now;
            startingtime = Convert.ToString(currentTime);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // time calculation
            sec += 1;
            if (sec == 60)
            {
                sec = 0;
                min += 1;

                if (min == 60)
                {
                    min = 0;
                    hr += 1;

                    if (hr == 24)
                    {
                        day += 1;
                        hr = 0;
                    }
                }
            }
            
            // proper time formatting (part 1)
            if (sec < 10) below10 = ":0";
            else below10 = ":";

            if (min < 10) below1 = ":0";
            else below1 = ":";

            if (hr < 10) below1_2 = ":0";
            else below1_2 = ":";

            // proper time formatting (part 2 + sending to toolstrip)
            if (hr == 0 && day == 0)
                toolStripStatusLabel1.Text = "Elapsed time... " + Convert.ToString(min) + below10 + Convert.ToString(sec);
            else if (hr >= 1 && day == 0)
                toolStripStatusLabel1.Text = "Elapsed time... " + Convert.ToString(hr) + below1 + Convert.ToString(min) + below10 + Convert.ToString(sec);
            else
                toolStripStatusLabel1.Text = "Elapsed time... " + Convert.ToString(day) + below1_2 + Convert.ToString(hr) + below1 + Convert.ToString(min) + below10 + Convert.ToString(sec);
        }
        private void endCompetitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            DateTime currentTime = DateTime.Now;
            string endingTime = Convert.ToString(currentTime);

            DialogResult endresult = MessageBox.Show(

                "Competition complete! Would you like to save the statistics into a text file for later viewing?",
                "Competition complete, elapsed time: " + Convert.ToString(day) + below1_2 + Convert.ToString(hr) + below1 + Convert.ToString(min) + below10 + Convert.ToString(sec),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1
                
                );

            if (endresult == DialogResult.Yes)
            {
                try
                {
                    string[] txtline = { "Elapsed Time: " + Convert.ToString(day) + below1_2 + Convert.ToString(hr) + below1 + Convert.ToString(min) + below10 + Convert.ToString(sec), "Starting Time: " + startingtime, "Ending Time: " + endingTime };
                    string docPath =
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "LTLVC_Results.txt")))
                    {
                        foreach (string line in txtline)
                            outputFile.WriteLine(line);
                    }

                    MessageBox.Show(
                        "Your statistics were saved to your Documents folder.",
                        "Save complete!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "An error occured whilst saving. Your statistics have not been saved.",
                        "Save Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1
                    );
                }
            }
            else { }
        }
    }
}

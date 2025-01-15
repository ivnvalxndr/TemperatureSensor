using System.Net;

namespace TemperatureSensor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const string api = "ca88e8c853d9c9e518c3bfe1f4ff3cae";

            string url = $"https://api.openweathermap.org/data/2.5/weather?q=London&appid={api}";
            try
            {
                using (WebClient client = new WebClient())
                {
                    string html = client.DownloadString(url);
                    label3.Text = html;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
                throw;
            }
            
        }
    }
}

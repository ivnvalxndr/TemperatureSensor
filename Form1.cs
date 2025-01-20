using System.Net;

namespace TemperatureSensor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // �������� ������ �� API (�� ������������)
        /*private void button1_Click(object sender, EventArgs e)
        {
            const string api = "ca88e8c853d9c9e518c3bfe1f4ff3cae";

            string url = $"https://api.openweathermap.org/data/2.5/weather?q=Perm&appid={api}&units=metric";
            try
            {
                using (WebClient client = new WebClient())
                {
                    string response = client.DownloadString(url);

                    // ������ json
                    var json = JObject.Parse(response);
                    string temp = json["main"]["temp"].ToString();
                    label3.Text = temp;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }*/

        private void button2_Click(object sender, EventArgs e)
        {
            string ftpServer = "hidden";  // FTP ������
            string remoteDirectory = "/test/";  // ����� �� �������, ��� ��������� �����
            string remoteFilePattern = "sound*.txt";  // ����� ��� ������ �� FTP
            string localDirectory = @"D:\dt\";  // ����� �� ��������� ������ ��� ���������� ������
            string userName = "hidden";
            string password = "hidden";

            // ��������� ����, ������� ����� �������� ����������
            while (true)
            {
                try
                {
                    // �������� ������ ������ � ����������
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpServer + remoteDirectory);
                    request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;  // ������ �� ��������� ������ ������
                    request.Credentials = new NetworkCredential(userName, password);
                    request.UsePassive = true;  // ��������� ���������� ������ FTP

                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            // ���������, ��� ��� ����� ���������� � "sound" � ������������� �� ".txt"
                            if (line.Contains("sound") && line.EndsWith(".txt"))
                            {
                                string fileName = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
                                string remoteFilePath = ftpServer + remoteDirectory + fileName;  // ������ ���� � ����� �� �������
                                string localFilePath = localDirectory + fileName;  // ���� ��� ���������� �� ��������� ������

                                // ��������� ���� � FTP
                                FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(remoteFilePath);
                                downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                                downloadRequest.Credentials = new NetworkCredential(userName, password);
                                downloadRequest.UsePassive = true;

                                using (FtpWebResponse downloadResponse = (FtpWebResponse)downloadRequest.GetResponse())
                                using (Stream responseStream = downloadResponse.GetResponseStream())
                                using (FileStream localFileStream = new FileStream(localFilePath, FileMode.Create))
                                {
                                    byte[] buffer = new byte[4096];
                                    int bytesRead;
                                    while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        localFileStream.Write(buffer, 0, bytesRead);
                                    }
                                }

                                // ����� ��������� ���������� ������� ���� � �������
                                FtpWebRequest deleteRequest = (FtpWebRequest)WebRequest.Create(remoteFilePath);
                                deleteRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                                deleteRequest.Credentials = new NetworkCredential(userName, password);
                                deleteRequest.UsePassive = true;

                                using (FtpWebResponse deleteResponse = (FtpWebResponse)deleteRequest.GetResponse())
                                {
                                    //MessageBox.Show($"���� {fileName} ������� ����� � FTP-�������.");
                                }

                                // ������ �������� ������������ �����
                                using (StreamReader localReader = new StreamReader(localFilePath))
                                {
                                    string lineFromFile;
                                    while ((lineFromFile = localReader.ReadLine()) != null)
                                    {
                                        MessageBox.Show(lineFromFile);  // ������� ������ �� �����
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("������ ��� �������� �����: " + ex.Message);
                }

                // ����� ����� ��������� ��������� (��������, 5 ������)
                Thread.Sleep(5000);
            }
        }
    }
}


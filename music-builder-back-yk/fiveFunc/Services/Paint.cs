using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.IO;
using SkiaSharp;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Diagnostics;

namespace WebApplication1
{
    public class Paint
    {
        public async Task<string> GetPaint(string keyWord,string name)
        {

            var targetUrl = "http://127.0.0.1:7860/sdapi/v1/txt2img";
            var payload = new
            {
                prompt = trans(keyWord),
                negative_prompt = "",
                steps = 15,
                override_settings = new
                {
                    sd_model_checkpoint = "anything-v5-PrtRE",
                    CLIP_stop_at_last_layers = 1
                },
                width = "512",
                height = "512"
            };

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(180); // ���ó�ʱʱ��
                var jsonContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(targetUrl, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject r = JObject.Parse(jsonResponse);
                    //var r = JsonConvert.DeserializeObject<dynamic>(jsonResponse); // ʹ��dynamic����������JSON
                    //Console.WriteLine(r["images"]);
                    //var imageBytes = Convert.FromBase64String(r[0]["images"][0]);
                    JArray imagesArray = (JArray)r["images"];
                    string base64Image = imagesArray[0].ToString();
                    var imageBytes = Convert.FromBase64String(base64Image);
                    //Console.WriteLine(6666);
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        // ʹ�� SKBitmap �����м���ͼ��
                        SKBitmap bitmap = SKBitmap.Decode(ms);

                        // ȷ������λ�ûص���ʼλ��


                        // ʹ�� SKImage �� SKBitmap ����ͼ��
                        SKImage image = SKImage.FromBitmap(bitmap);

                        // ����ͼ��ΪPNG��ʽ��ָ�����ļ�·��
                        using (var imageDest = new FileStream("C:\\Users\\yuanke\\Desktop\\PaintFile\\"+name+".png", FileMode.Create))
                        {
                            // ����ͼ��ΪPNG������
                            image.Encode(SKEncodedImageFormat.Png, 100).SaveTo(imageDest);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
            return "C:\\Users\\yuanke\\Desktop\\PaintFile\\" + name+".png";
        }

        public string trans (string keyword_zh)
        {
            Process process = new Process();
            string firstCommand = "cd C:\\Users\\yuanke\\Desktop\\trans";
            string secondCommand = "python main.py " + keyword_zh;

            process.StartInfo.FileName = "cmd.exe"; // ָ��Ҫִ�е������г���������cmd.exe
            process.StartInfo.Arguments = $"/c {firstCommand} & {secondCommand}";  // ָ��Ҫִ�е�����������г���ǰĿ¼�µ��ļ�
            process.StartInfo.RedirectStandardOutput = true; // ������׼����ض��򵽳�����
            process.StartInfo.UseShellExecute = false; // ��ʹ��Shellִ�г���
            process.StartInfo.CreateNoWindow = true; // �������µĴ���
                                                     // ��������
                                                     //try
                                                     //{
                                                     // ��������
            process.Start();

            // �ȴ����̽���
            process.WaitForExit();

            // ��ȡ���
            string output = process.StandardOutput.ReadToEnd();
            return output;
        }
    }
}

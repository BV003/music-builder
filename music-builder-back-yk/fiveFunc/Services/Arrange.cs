using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace WebApplication1
{
    public class Arrange
    {
        public string GetArrange(string instrument)
        {
            Process process = new Process();
            string firstCommand = "cd C:\\Users\\yuanke\\Desktop\\arrangement";
            string secondCommand = "python arrangement.py " + instrument;

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

                 // ������ʽƥ���ļ�·��
                 string pattern = @"'(.*?)'";

                // ����������ʽ����
                Regex regex = new Regex(pattern);

                 // ƥ�������ַ����е������ļ�·��
                MatchCollection matches = regex.Matches(output);

            // ��������ƥ�����ӡ


            // Group 1 ��������ƥ������ݣ����ļ�·��
            
            
            // ��ӡ����ʹ���



            // ���ؽű����
             return matches[0].Groups[1].Value;
           // }
           // catch (Exception ex)
           // {
           //     Console.WriteLine("Exception: " + ex.Message);
           //     return "Error: " + ex.Message;
          //  }
        }
    }

}



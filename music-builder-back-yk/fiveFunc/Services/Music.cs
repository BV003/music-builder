using System.Diagnostics;
using System.Text.RegularExpressions;

namespace WebApplication1
{
    public class Music
    {
        public string GetMusic(string description)
        {

            // ����һ���µ�Processʵ��
            Process process = new Process();
            string firstCommand = "cd C:\\Users\\yuanke\\Desktop\\makeMusic";
            string secondCommand = "python makeMusic.py " + trans(description);
            // ����ProcessStartInfo����
            process.StartInfo.FileName = "cmd.exe"; // ָ��Ҫִ�е������г���������cmd.exe
            process.StartInfo.Arguments = $"/c {firstCommand} & {secondCommand}";  // ָ��Ҫִ�е�����������г���ǰĿ¼�µ��ļ�
            process.StartInfo.RedirectStandardOutput = true; // ������׼����ض��򵽳�����
            process.StartInfo.UseShellExecute = false; // ��ʹ��Shellִ�г���
            process.StartInfo.CreateNoWindow = true; // �������µĴ���

            // ��������
            process.Start();

            // ��ȡ���
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit(); // �ȴ����̽���

            string pattern = @"'(.*?)'";

            // ����������ʽ����
            Regex regex = new Regex(pattern);

            // ƥ�������ַ����е������ļ�·��
            MatchCollection matches = regex.Matches(output);

            return matches[1].Groups[1].Value;
        }

        public string trans(string keyword_zh)
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

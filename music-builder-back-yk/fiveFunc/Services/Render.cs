using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
    public class Render
    {
        public string GetRender(string midi, string singer, string lyrics, string wavName)
        {

            // ����һ���µ�Processʵ��
            Process process = new Process();
            string firstCommand = "cd C:\\Users\\yuanke\\Desktop\\OpenUtau-0.1.501.42\\OpenUtau";
            string secondCommand = "dotnet run --" + " " + midi + " " + singer + " " + " C:\\Users\\yuanke\\Desktop\\RenderFile\\" + wavName + " " + lyrics;
            // ����ProcessStartInfo����
            process.StartInfo.FileName = "cmd.exe"; // ָ��Ҫִ�е������г���������cmd.exe
            process.StartInfo.Arguments = $"/c {firstCommand} & {secondCommand}"; ; // ָ��Ҫִ�е�����������г���ǰĿ¼�µ��ļ�
            process.StartInfo.RedirectStandardOutput = true; // ������׼����ض��򵽳�����
            process.StartInfo.UseShellExecute = false; // ��ʹ��Shellִ�г���
            process.StartInfo.CreateNoWindow = true; // �������µĴ���

            // ��������
            process.Start();

            // ��ȡ���
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit(); // �ȴ����̽���
            string local = "C:\\Users\\yuanke\\Desktop\\RenderFile\\" + wavName + ".wav";

            return local;
        }

    }
}

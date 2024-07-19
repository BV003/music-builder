using System.Diagnostics;

namespace WebApplication1
{
    public class Some
    {
        public string GetSome(string wavname)
        {

            // ����һ���µ�Processʵ��
            Process process = new Process();
            string firstCommand = "conda activate some";
            string secondCommand = "cd C:\\Users\\yuanke\\Desktop\\SOME-1.0.0-baseline";
            string thirdCommand = "python infer.py --model C:\\Users\\yuanke\\Desktop\\0119_continuous128_5spk\\0119_continuous256_5spk\\model_ckpt_steps_100000_simplified.ckpt --wav " + wavname;

            process.StartInfo.FileName = "cmd.exe"; // ָ��Ҫִ�е������г���������cmd.exe
            process.StartInfo.Arguments = $"/c {firstCommand} & {secondCommand} & {thirdCommand}";  // ָ��Ҫִ�е�����������г���ǰĿ¼�µ��ļ�
            process.StartInfo.RedirectStandardOutput = true; // ������׼����ض��򵽳�����
            process.StartInfo.UseShellExecute = false; // ��ʹ��Shellִ�г���
            process.StartInfo.CreateNoWindow = true; // �������µĴ���

            // ��������
            process.Start();

            // ��ȡ���
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit(); // �ȴ����̽���

            return "C:\\Users\\yuanke\\Desktop\\SomeFile\\1.mid";
        }
    }
}

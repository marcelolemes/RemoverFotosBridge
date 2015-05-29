using System;
using System.Collections;
using ps = Photoshop;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
namespace getBridgeFiles
{
    class Program
    {


        static void Main(string[] args)
        {
            String arquivoOrigem;
            String valorAlbum;
            String valorFoto;
            String caminhoAlvo;
            List<string> valorCaminho = new List<string>();
            List<string> apoio = new List<string>();
            try
            {
                ArrayList list = new ArrayList();
                list.AddRange(arquivosSelecionados());

                for (int i = 0; i <= list.Count - 1; i++)
                {
                    apoio.Clear();
                    valorCaminho.Clear();
                    valorAlbum = list[i].ToString();           // file name
                    arquivoOrigem = list[i].ToString();
                    apoio.AddRange(arquivoOrigem.Split('/'));
                    arquivoOrigem = "";
                    for (int x = 0; x < apoio.Count; x++)
                    {
                        if (x < apoio.Count - 1)
                        {
                            if (apoio[x].Length <= 1)
                            {
                                if (Directory.Exists(apoio[x] + ":"))
                                {
                                    arquivoOrigem = arquivoOrigem + apoio[x] + ":\\";
                                }
                            }
                            else
                            {
                                arquivoOrigem = arquivoOrigem + apoio[x] + "\\";
                            }
                        }
                        else {
                            arquivoOrigem = arquivoOrigem + apoio[x] ;
                        }
                    }



                    valorCaminho.AddRange(valorAlbum.Split('/'));

                    valorFoto = valorCaminho[valorCaminho.Count - 1];
                    valorCaminho.Remove(valorFoto);
                    valorAlbum = valorCaminho[valorCaminho.Count - 1];
                    valorCaminho.Remove(valorAlbum);
                    caminhoAlvo = "";
                    foreach (String s in valorCaminho)
                    {
                        if (s.Length <= 1)
                        {
                            if (Directory.Exists(s + ":"))
                            {
                                caminhoAlvo = caminhoAlvo + s + ":\\";
                            }
                        }
                        else
                        {
                            caminhoAlvo = caminhoAlvo + s + "\\";
                        }
                    }



                    try
                    {
                        if (!Directory.Exists(caminhoAlvo + "Removidas\\" + valorAlbum))
                        {
                            Directory.CreateDirectory(caminhoAlvo + "Removidas\\" + valorAlbum);
                        }

                        
                        File.Move(arquivoOrigem, caminhoAlvo + "Removidas\\" + valorAlbum + "\\" + valorFoto);


                        

                    }
                    catch {

                        try
                        {
                            if (!Directory.Exists("\\\\"+caminhoAlvo + "Removidas\\" + valorAlbum))
                            {
                                Directory.CreateDirectory("\\\\" + caminhoAlvo + "Removidas\\" + valorAlbum);
                            }


                            File.Move("\\\\" + arquivoOrigem, "\\\\" + caminhoAlvo + "Removidas\\" + valorAlbum + "\\" + valorFoto);


                        


                        }
                        catch { 
                        
                        }
                    
                    }

                }
            }
            catch
            {

                MessageBox.Show("Falha");
            }

        }





        public static ArrayList arquivosSelecionados()
        {
            try
            {
                ps.ApplicationClass app = new ps.ApplicationClass();
                String Code = "var fileList;" +
"if ( BridgeTalk.isRunning( 'bridge' ) ) {" +
"var bt = new BridgeTalk();" +
"bt.target = 'bridge';" +
"bt.body = 'var theFiles = photoshop.getBridgeFileListForAutomateCommand();theFiles.toSource();';" +
"bt.onResult = function( inBT ) { fileList = eval( inBT.body ); }" +
"bt.onError = function( inBT ) { fileList = new Array(); }" +
"bt.send(8);" +
"bt.pump();" +
"var timeOutAt = ( new Date() ).getTime() + 5000;" +
"var currentTime = ( new Date() ).getTime();" +
"while ( ( currentTime < timeOutAt ) && ( undefined == fileList ) ) {" +
"bt.pump();" +
"$.sleep( 100 );" +
"currentTime = ( new Date() ).getTime();" +
"}}" +
"if ( undefined == fileList ) {" +
"fileList = new Array();}" +
"fileList = decodeURI(fileList.toString());";
                String RC = app.DoJavaScript(Code, null, null);
                ArrayList list = new ArrayList();
                list.AddRange(RC.Split(new char[] { ',' }));
                foreach (String s in list)
                {
                }
                return list;
            }
            catch
            {
                MessageBox.Show("Abra o Photoshop");
                return null;
            }

        }
    }
}

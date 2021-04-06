using System;
using System.IO;
using System.Net;
using System.IO.Compression;
using Microsoft.Win32;
using System.Diagnostics;

namespace ValheimBrasil
{
    public class Util
    {
        public string dirgame = "C:/Program Files (x86)/Steam/steamapps/common/valheim";
        public string dirselected = null;
        public static Util util = new Util();
        
        public static void InstallValheimPlus()
        {
            // Baixando o Core
            try
            {
                WebClient webClient = new WebClient();
                Console.WriteLine("\nIniciando o download....");
                webClient.DownloadFile(Program.downloadlink, Program.downloadname);
                Console.WriteLine("\nArquivo baixado com sucesso! Extraindo arquivo para o diretório do jogo...");
                ZipFile.ExtractToDirectory(Program.downloadname, $"{Directory.GetCurrentDirectory()}");
            }
            catch (Exception)
            {
                Console.WriteLine("O download e a extração do núcleo falharam...");
                Console.WriteLine("As vezes isto acontece por que o instalador não tem permissão para apagar os arquivos.");
                Console.WriteLine("Por favor, delete os seguintes arquivos/pastas que estão no diretório do seu jogo:");
                Console.WriteLine("/doorstop_config.ini");
                Console.WriteLine("/winhttp.dll");
                Console.WriteLine("/BepInEx");
                Console.WriteLine("/doorstop_libs");
                Console.WriteLine("/unstripped_corlib");
                Console.WriteLine("Depois de remover eles, por favor, aperte enter para o instalador fechar,\n depois disto abra o instalador e escolha a opção \'instalar\'");
                System.Threading.Thread.Sleep(10000);
                System.Environment.Exit(0);
            }
            
            
            // Limpeza de Desnecessários
            System.Threading.Thread.Sleep(2500);
            Console.WriteLine("Deletando arquivo baixado .zip");
            File.Delete(Program.downloadname);
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }

        public static void FullClean(string dir)
        {
            try
            {
                bool existsdoorstop = System.IO.File.Exists($"{dir}/doorstop_config.ini");
                bool existswinhttp = System.IO.File.Exists($"{dir}/winhttp.dll");
                bool existsbepfolder = System.IO.Directory.Exists($"{dir}/BepInEx");
                bool existsdorstoplibs = System.IO.Directory.Exists($"{dir}/doorstop_libs");
                bool existsunstripped = System.IO.Directory.Exists($"{dir}/unstripped_corlib");
                bool existsstartserversh = System.IO.File.Exists($"{dir}/start_server_bepinex.sh");
                Console.WriteLine("Removendo Arquivos...");
                System.Threading.Thread.Sleep(2000);
                
                //Deleting Files
                if(existsdoorstop)
                    File.Delete("doorstop_config.ini");
                if(existswinhttp)
                    File.Delete("winhttp.dll");
                if(existsstartserversh)
                    File.Delete("start_server_bepinex.sh");
                if (existsbepfolder)
                {
                    string bepinexdir = $"{Directory.GetCurrentDirectory()}/BepInEx";
                    DirectoryInfo directorybep = new DirectoryInfo(bepinexdir);
                    directorybep.Delete(true);
                }
                if (existsdorstoplibs)
                {
                    string dorstopdir = $"{Directory.GetCurrentDirectory()}/doorstop_libs";
                    DirectoryInfo directorydorstop = new DirectoryInfo(dorstopdir);
                    directorydorstop.Delete(true);
                }
                if (existsunstripped)
                {
                    string unstrippeddir = $"{Directory.GetCurrentDirectory()}/unstripped_corlib";
                    DirectoryInfo directoryunstrip = new DirectoryInfo(unstrippeddir);
                    directoryunstrip.Delete(true);
                }
                Console.WriteLine("\nRemoção de Arquivos Concluída!");
                System.Threading.Thread.Sleep(2000);
            }
            catch (System.Exception)
            {
                Console.WriteLine("A remoção de arquivos falhou, reiniciando o instalador...");
                System.Threading.Thread.Sleep(2000);
                Program.MenuInicial();
            }
        }
        
        public static string RegistryDefault()
        {
            string interdirgame = string.Empty;
            try
            {
                RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App 892970");
                if (registryKey != null)
                    interdirgame = registryKey.GetValue("InstallLocation").ToString();
            }
            catch (Exception)
            {
                throw;
            }

            return interdirgame;
        }

        public static bool UsarOuNao(string dirgame)
        {
            Console.WriteLine("O diretório encontrado foi: " + dirgame);
            Console.Write("\nDeseja utilizar o diretório encontrado?\n[S/N]\n> ");
            string usarounao = Console.ReadLine();
            
            if (usarounao.ToLower() == "s")
            {
                Console.WriteLine("Okay, usando diretório encontrado.");
                System.Threading.Thread.Sleep(2000);
                util.dirselected = dirgame;
                return true;
            } 
            else if (usarounao.ToLower() == "n")
            {
                Console.WriteLine("Okay, seguindo para seleção manual.");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                return false;
            }
            else
            {
                Console.WriteLine("Resposta inválida, por favor, digite uma resposta válida.");
                UsarOuNao(dirgame);
            }

            return false;
        }
        
        public static bool ProsseguirOuNao()
        {
            string usarounao = Console.ReadLine();
            
            if (usarounao.ToLower() == "s")
            {
                Console.WriteLine("\nOkay, prosseguindo...");
                System.Threading.Thread.Sleep(2000);
                return true;
            } 
            else if (usarounao.ToLower() == "n")
            {
                Console.WriteLine("\nOkay, voltando...");
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                return false;
            }
            else
            {
                Console.WriteLine("Resposta inválida, por favor, digite uma resposta válida.");
                ProsseguirOuNao();
            }

            return false;
        }
        

        public static bool TestDefaultDirectory()
        {
            //Menu
            Menus.Menu();
            //Procurando Diretório Padrão
            Console.WriteLine("Procurando Diretório...");
            string dirgame = RegistryDefault();
            bool exists = System.IO.Directory.Exists(dirgame);

            if (exists)
            {
                bool usou = UsarOuNao(dirgame);
                if (usou)
                {
                    //Procurando Valheim Exe
                    Console.WriteLine("Procurando por Valheim.exe");
                    Directory.SetCurrentDirectory(dirgame);
                    System.Threading.Thread.Sleep(2000);
                    Util.SearchingValheimExe(dirgame);
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                }
                else
                {
                    exists = false;
                    return exists;
                }
            }
            else
            {
                Console.WriteLine("O diretório não foi encontrado, continuando...");
                System.Threading.Thread.Sleep(8000);
                Console.Clear();
            }

            return exists;
        }
        
        public static void SearchingDirectory(string dir)
        {
            bool exists = System.IO.Directory.Exists(dir);
            try
            {
                Directory.SetCurrentDirectory(dir);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("\nDiretório não encontrado: " + $"'{dir}'");
                System.Threading.Thread.Sleep(2500);
                Console.Clear();
                SelectDirectory();
            }

            if (exists)
            {
                Console.WriteLine("\nDiretório encontrado, procurando valheim.exe");
                SearchingValheimExe(dir);
                System.Threading.Thread.Sleep(2000);
            }
        }

        public static void SearchingValheimExe(string dir)
        {
            bool existsexe = System.IO.File.Exists($"{dir}/valheim.exe");
            if (existsexe)
            {
                Console.WriteLine("Valheim.Exe foi encontrado, continuando a instalação...");
                System.Threading.Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Valheim.Exe não foi encontrado, reiniciando...");
                System.Threading.Thread.Sleep(4000);
                Console.Clear();
                SelectDirectory();
            }
        }
        
        public static void SearchingBepInExInstall(string dir)
        {
            //Console.WriteLine("SearchingBepInEx Dir Recebido: " + dir);
            bool existsbepinex = false;
            bool existsdoorstop = System.IO.File.Exists($"{dir}/doorstop_config.ini");
            bool existswinhttp = System.IO.File.Exists($"{dir}/winhttp.dll");
            bool existsbepfolder = System.IO.Directory.Exists($"{dir}/BepInEx");
            bool existsdorstoplibs = System.IO.Directory.Exists($"{dir}/doorstop_libs");
            bool existsunstripped = System.IO.Directory.Exists($"{dir}/unstripped_corlib");
            bool existsstartserversh = System.IO.File.Exists($"{dir}/start_server_bepinex.sh");
            
            if (existsdoorstop || existsbepfolder || existsdorstoplibs || existswinhttp || existsunstripped)
            {
                //Console.WriteLine("existsunstripped" + existsunstripped);
                //Console.WriteLine("existswinhttp" + existswinhttp);
                //Console.WriteLine("existsbepfolder" + existsbepfolder);
                //Console.WriteLine("existsdorstoplibs" + existsdorstoplibs);
                //Console.WriteLine("existsunstripped" + existsunstripped);
                existsbepinex = true;
            }

            if (existsbepinex)
            {
                Console.WriteLine("\nOs seguintes arquivos/diretórios: BepInEx, doorstop_libs, unstripped_corlib, doorstop_config.ini ou winhttp.dll foram encontrados na pasta selecionada.");
                Console.WriteLine("Você quer remover esses arquivos? Tudo será deletado.");
                Console.WriteLine("[S/N]");
                Console.Write("> ");
                string opcselec = Console.ReadLine().ToLower();

                if (opcselec == "s")
                {
                    Console.WriteLine("\nRemovendo Arquivos...");
                    System.Threading.Thread.Sleep(2000);
                    
                    //Deleting Files
                    if(existsdoorstop)
                        File.Delete("doorstop_config.ini");
                    if(existswinhttp)
                        File.Delete("winhttp.dll");
                    if(existsstartserversh)
                        File.Delete("start_server_bepinex.sh");
                    if(existsbepfolder)
                    {
                        string bepinexdir = $"{Directory.GetCurrentDirectory()}/BepInEx";
                        DirectoryInfo directorybep = new DirectoryInfo(bepinexdir);
                        directorybep.Delete(true);
                    }
                    if (existsdorstoplibs)
                    {
                        string dorstopdir = $"{Directory.GetCurrentDirectory()}/doorstop_libs";
                        DirectoryInfo directorydorstop = new DirectoryInfo(dorstopdir);
                        directorydorstop.Delete(true);
                    }
                    if (existsunstripped)
                    {
                        string unstrippeddir = $"{Directory.GetCurrentDirectory()}/unstripped_corlib";
                        DirectoryInfo directoryunstrip = new DirectoryInfo(unstrippeddir);
                        directoryunstrip.Delete(true);
                    }
                    Console.WriteLine("Remoção de arquivo concluída com sucesso!");
                    System.Threading.Thread.Sleep(2000);
                }
                else if (opcselec == "n")
                {
                    Console.WriteLine("Ok, se você quiser instalar o mod você deve apagar arquivos de instalações anteriores ou selecionar a opção \"Atualizar Mods\".");
                    System.Threading.Thread.Sleep(8000);
                    System.Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Opção Inválida, reiniciando...");
                    Console.Clear();
                    SearchingBepInExInstall(dir);
                }
                
                System.Threading.Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Nenhuma instalação anterior foi encontrada, continuando...");
                System.Threading.Thread.Sleep(2000);
            }
        }

        public static string SelectDirectory()
        {
            try
            {
                Menus.Menu();
                Console.WriteLine("Primeiro, escolha o diretório");
                Console.WriteLine("Normalmente ele fica em: C:/Program Files (x86)/Steam/steamapps/common/valheim");
                Console.WriteLine("Precisamos que você escreva o diretório completo, sem erros.");
                Console.WriteLine("Em qual diretório está seu jogo?\n");
                util.dirselected = Console.ReadLine();
                SearchingDirectory(util.dirselected);
                Console.Clear();
                return util.dirselected;
            }
            catch (Exception)
            {
                Console.WriteLine("Diretório Inválido...");
                System.Threading.Thread.Sleep(2000);
                System.Environment.Exit(0);
                throw;
            }
        }

        public static string CurrentVersion()
        {
            try
            {
                FileVersionInfo InstalledVBrasilVersionInfo = FileVersionInfo.GetVersionInfo(Util.util.dirselected + "\\BepInEx\\plugins\\VBrasil.dll");
                return InstalledVBrasilVersionInfo.FileVersion;
            }
            catch
            {
                Program.newestVersion = "Falha/Inexistente";
                return Program.newestVersion;
            }
        }

        public static string RepositoryVersion()
        {
            WebClient client = new WebClient();
            
            client.Headers.Add("User-Agent: VBrasil Installer");
            
            try
            {
                Console.Clear();
                Console.WriteLine("Aguarde... Obtendo versões.");
                string reply = client.DownloadString(Program.ApiRepository);
                Program.newestVersion = reply.Split(new[] { "," }, StringSplitOptions.None)[0].Trim().Replace("\"", "").Replace("[{name:", "");
                Console.WriteLine("Versão obtida com sucesso.");
                System.Threading.Thread.Sleep(1500);
                Console.Clear();
                return Program.newestVersion;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("A mais nova versão não pôde ser determinada.");
                Program.newestVersion = "Falha/Inexistente";
                System.Threading.Thread.Sleep(1500);
                Console.Clear();
                return Program.newestVersion;
            }
        }
        
        public static bool IsNewVersionAvailable()
        {
            //Parse versions for proper version check
            if (System.Version.TryParse(Program.verrepo, out System.Version newVersion))
            {
                if (System.Version.TryParse(Program.veratual, out System.Version currentVersion))
                {
                    if (currentVersion < newVersion)
                    {
                        return true;
                    }
                }
            }
            else //Fallback version check if the version parsing fails
            {
                if (Program.verrepo != Program.veratual)
                {
                    return true;
                }
            }

            return false;
        }
    }
}    
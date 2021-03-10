using System;
using System.ComponentModel;
using System.IO;
using System.Collections;
using System.Net;
using System.Runtime.Remoting.Lifetime;
using System.IO.Compression;
using System.Runtime.CompilerServices;

namespace ValheimBrasil
{
    internal class Program
    {
        public string dirgame = "C:/Program Files (x86)/Steam/steamapps/common/valheim";

        //
        enum opcao
        {
            ValheimPlus = 1,
            Desinstalar,
            Atualizar,
        };

        public static void Menu()
        {
            Console.WriteLine("========== Valheim Brasil ==========\n");
            Console.WriteLine("AVISO: O JOGO PRECISA ESTAR DESLIGADO!");
            Console.WriteLine("Seja muito bem vindo ao instalador do BepInEx + ValheimPlus da nossa comunidade de Valheim Brasil");
            Console.WriteLine("Este aplicativo foi feito para instalar os Mods necessários para entrar no servidor de Roleplay\n");
        }

        
        public static void MenuDeInstalacao()
        {
            Menu();
            Console.WriteLine("Oque você quer fazer?");
            Console.WriteLine("[1] Instalar V+ Brasil Mod");
            Console.WriteLine("[2] Desinstalar V+ Brasil Mod");
            Console.WriteLine("[3] Atualizar V+ Brasil Mod");
            Console.Write("> ");
        }

        public bool TestandoDefault()
        {
            Menu();
            Console.WriteLine("Testando diretório padrão...");
            bool exists = System.IO.Directory.Exists(dirgame);

            if (exists)
            {
                Console.WriteLine("Deritório padrão encontrado! Procurando Valheim.exe");
                Directory.SetCurrentDirectory(dirgame);
                System.Threading.Thread.Sleep(2000);
                ProcurandoValheimExe(dirgame);
                Console.WriteLine("Valheim.exe foi encontrado, continuando...");
                System.Threading.Thread.Sleep(3000);
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Deritório padrão não encontrado, continuando...");
                System.Threading.Thread.Sleep(8000);
                Console.Clear();
            }

            return exists;
        }

        public static void ProcurandoDiretorio(string dir)
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
                Program programa = new Program();
                programa.EscolhaODiretorio();
            }

            if (exists)
            {
                Console.WriteLine("\nDiretório Encontrado, procurando pelo valheim.exe");
                ProcurandoValheimExe(dir);
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
            }
        }

        public static void ProcurandoValheimExe(string dir)
        {
            bool existsexe = System.IO.File.Exists($"{dir}/valheim.exe");
            if (existsexe)
            {
                Console.WriteLine("Valheim.Exe foi encontrado, continuando instalação...");
                System.Threading.Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Valheim.Exe não foi foi encontrado, reiniciando...");
                System.Threading.Thread.Sleep(4000);
                Console.Clear();
                Program programa = new Program();
                programa.EscolhaODiretorio();
            }
        }

        public string EscolhaODiretorio()
        {
            try
            {
                Menu();
                Console.WriteLine("Primeiro, escolha qual é o diretório do jogo");
                Console.WriteLine("Normalmente ele fica em: C:/Program Files (x86)/Steam/steamapps/common/valheim");
                Console.WriteLine("Precisamos que você escreva o diretório completo, sem erros.");
                Console.WriteLine("Em qual diretório está o seu jogo?\n");
                string internodir = Console.ReadLine();
                ProcurandoDiretorio(internodir);
                return internodir;
            }
            catch (System.Exception)
            {
                Console.WriteLine("Diretório inválido...");
                System.Threading.Thread.Sleep(2000);
                System.Environment.Exit(0);
                throw;
            }
        }

        public static void Agradecimentos()
        {
            Console.WriteLine("========== Valheim Brasil ==========\n");
            Console.WriteLine("ValheimPlus foi instaldo com sucesso! Seja bem vindo ao servidor.");
            Console.WriteLine("O instalador irá encerrar em 10 segundos...");
            Console.WriteLine("Discord: https://discord.gg/BjeTBv6pxe");
            Console.WriteLine("\nCriado por: CastBlacKing");
            System.Threading.Thread.Sleep(10000);
        }
        
        public static void Despedida()
        {
            Console.WriteLine("========== Valheim Brasil ==========\n");
            Console.WriteLine("ValheimPlus foi desinstalado com sucesso! Adeus :(");
            Console.WriteLine("O instalador irá encerrar em 10 segundos...");
            Console.WriteLine("Discord: https://discord.gg/BjeTBv6pxe");
            Console.WriteLine("\nCriado por: CastBlacKing");
            System.Threading.Thread.Sleep(10000);
        }
        
        public static void MsgDeAtualizacao()
        {
            Console.WriteLine("========== Valheim Brasil ==========\n");
            Console.WriteLine("ValheimPlus foi ATUALIZADO com sucesso!");
            Console.WriteLine("O instalador irá encerrar em 10 segundos...");
            Console.WriteLine("Discord: https://discord.gg/BjeTBv6pxe");
            Console.WriteLine("\nCriado por: CastBlacKing");
            System.Threading.Thread.Sleep(10000);
        }

        public static void BaixarBrasilMod()
        {
            // Baixando o Core
            try
            {
                WebClient webClient = new WebClient();
                Console.WriteLine("\nIniciando Descarregamento...");
                webClient.DownloadFile("https://github.com/Valheim-Brasil/VPlus-Brasil/releases/latest/download/ValheimBrasil.zip", "ValheimBrasil.zip");
                Console.WriteLine("\nArquivo baixado com sucesso!\nExtraindo arquivo para o diretório do jogo...");
                ZipFile.ExtractToDirectory("ValheimBrasil.zip", $"{Directory.GetCurrentDirectory()}");
            }
            catch (Exception)
            {
                Console.WriteLine("O descarregamento e a extração do core falhou...");
                System.Threading.Thread.Sleep(2000);
                System.Environment.Exit(0);
            }
            
            
            // Limpeza de Desnecessários
            System.Threading.Thread.Sleep(2500);
            Console.WriteLine("Deletando arquivo Core .zip");
            File.Delete("ValheimBrasil.zip");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }

        public static void LimpezaTotal()
        {
            try
            {
                string bepinexdir = $"{Directory.GetCurrentDirectory()}/BepInEx";
                string dorstopdir = $"{Directory.GetCurrentDirectory()}/doorstop_libs";
                string unstrippeddir = $"{Directory.GetCurrentDirectory()}/unstripped_corlib";
                DirectoryInfo directorybep = new DirectoryInfo(bepinexdir);
                DirectoryInfo directorydorstop = new DirectoryInfo(dorstopdir);
                DirectoryInfo directoryunstrip = new DirectoryInfo(unstrippeddir);
                directorybep.Delete(true);
                directorydorstop.Delete(true);
                directoryunstrip.Delete(true);
                File.Delete("doorstop_config.ini");
                File.Delete("winhttp.dll");
                Console.WriteLine("\nDesinstalação concluída com sucesso, obrigado!");
                System.Threading.Thread.Sleep(2000);
            }
            catch (System.Exception)
            {
                Console.WriteLine("A desinstalação falhou, reiniciando instalador...");
                System.Threading.Thread.Sleep(2000);
                MenuDeInstalacao();
            }
        }

        //Função principal
        public static void Main(string[] args)
        {
            //Chamando o programa
            Program programa = new Program();
            //Tetando o padrão
            if(programa.TestandoDefault())
            {
            }
            else
            {
                programa.EscolhaODiretorio();
            }

            //Menu de instalação
            MenuDeInstalacao();
            int index = int.Parse(Console.ReadLine());
            opcao opcaoSelecionada = (opcao)index;
            
            //Selector
            switch (opcaoSelecionada)
            {
             case opcao.ValheimPlus:
                 BaixarBrasilMod();
                 Agradecimentos();
                 break;
             case opcao.Desinstalar:
                 LimpezaTotal();
                 Despedida();
                 break;
             case opcao.Atualizar:
                 // Atualizando o Brasil Mod
                 LimpezaTotal();
                 BaixarBrasilMod();
                 MsgDeAtualizacao();
                 break;
            }
        }


    }
}
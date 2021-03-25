using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.IO.Compression;
using Microsoft.Win32;

namespace ValheimBrasil
{
    // COPYRIGHT 2021 João Pedro Viana Freitas // https://github.com/J-Pster
    // GITHUB REPOSITORY https://github.com/Valheim-Brasil/VBrasil_Instalador
    
    internal class Program
    {
        public static string version = "3.1.1";
        public static string name = "Valheim Brasil";
        public static string installname = "VBrasil";
        public static string repository = "https://github.com/Valheim-Brasil";
        public static string creator = "CastBlacKing";
        public static string creatorgithub = "https://github.com/CastBlacKing";
        public static string downloadlink = "https://github.com/Valheim-Brasil/VBrasil/releases/latest/download/ValheimBrasil.zip";
        public static string downloadname = "ValheimBrasil.zip";
        
        // Update
        public static string Repository = "https://github.com/Valheim-Brasil/VBrasil";
        public static string ApiRepository = "https://api.github.com/repos/Valheim-Brasil/VBrasil/tags";
        public static string newestVersion = "";
        public static string veratual;
        public static string verrepo;
        public static bool haveanewversion;

        enum opcao
        {
            Instalar = 1,
            Desinstalar,
            Atualizar,
        };

        public static void MenuInicial()
        {
            //Menu de instalação
            Menus.InstallMenu();
            int index = int.Parse(Console.ReadLine());
            opcao opcaoSelecionada = (opcao) index;

            //Selector
            switch (opcaoSelecionada)
            {
                case opcao.Instalar:
                    if (veratual != "Falha/Inexistente")
                    {
                        Console.Write($"\nVocê já tem uma versão instalada [{veratual}], deseja prosseguir?\n[S/N]\n> ");
                        bool resposta = Util.ProsseguirOuNao();
                        if (resposta)
                        {
                            Util.SearchingBepInExInstall(Util.util.dirselected);
                            Util.InstallValheimPlus();
                            Menus.FinishThanks();
                        }
                        else
                        {
                            MenuInicial();
                        }
                    }
                    else
                    {
                        Util.SearchingBepInExInstall(Util.util.dirselected);
                        Util.InstallValheimPlus();
                        Menus.FinishThanks();
                    }
                    break;
                case opcao.Desinstalar:
                    Util.FullClean(Util.util.dirselected);
                    Console.Clear();
                    Menus.Goodbye();
                    break;
                case opcao.Atualizar:
                    if (!haveanewversion)
                    {
                        Console.Write("\nVocê não tem uma versão instalada, ou já está na versão mais atual, deseja prosseguir?\n[S/N]\n> ");
                        bool resposta = Util.ProsseguirOuNao();
                        if (resposta)
                        {
                            Util.FullClean(Util.util.dirselected);
                            Util.InstallValheimPlus();
                            Menus.UpdateMessage();
                        }
                        else
                        {
                            MenuInicial();
                        }
                    }
                    else
                    {
                        Util.FullClean(Util.util.dirselected);
                        Util.InstallValheimPlus();
                        Menus.UpdateMessage();
                    }
                    break;
            }
        }

        //Função principal
        public static void Main(string[] args)
        {
            //Chamando o programa
            //Tetando o padrão
            if(Util.TestDefaultDirectory())
            {
            }
            else
            { 
                Util.SelectDirectory();
            }
            
            // Pegando versão atual e versão do repositório
            veratual = Util.CurrentVersion();
            verrepo = Util.RepositoryVersion();
            haveanewversion = Util.IsNewVersionAvailable();

            Console.Clear();
            Console.WriteLine("AVISO, LEIA COM ATENÇÃO!\nCaso escolha instalar usando este instalador ele irá remover qualquer mod ou config que esteja na pasta.\nPor favor, caso você tenha algo de importante nas pastas do BepInEx, salve-o!");
            Console.Write("\nDeseja prosseguir? [S/N]\n> ");
            bool resultado = Util.ProsseguirOuNao();
            
            if (!resultado)
                Environment.Exit(0);
            
            Console.Clear();
            MenuInicial();
        }
    }
}
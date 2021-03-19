using System;
using System.IO;
using System.Net;
using System.IO.Compression;
using Microsoft.Win32;

namespace ValheimBrasil
{
    internal class Program
    {
        public static string version = "3.0.2";
        public static string name = "Valheim Brasil";
        public static string installname = "VBrasil";
        public static string repository = "https://github.com/Valheim-Brasil";
        public static string creator = "CastBlacKing";
        public static string creatorgithub = "https://github.com/CastBlacKing";
        public static string downloadlink = "https://github.com/Valheim-Brasil/VBrasil/releases/latest/download/ValheimBrasil.zip";
        public static string downloadname = "ValheimBrasil.zip";

        enum opcao
        {
            ValheimPlus = 1,
            Desinstalar,
            Atualizar,
        };

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

            //Menu de instalação
            Menus.InstallMenu();
            int index = int.Parse(Console.ReadLine());
            opcao opcaoSelecionada = (opcao)index;
            
            //Selector
            switch (opcaoSelecionada)
            {
                case opcao.ValheimPlus:
                    Util.SearchingBepInExInstall(Util.util.dirselected);
                    Util.InstallValheimPlus();
                    Menus.FinishThanks();
                    break;
                case opcao.Desinstalar:
                    Util.FullClean(Util.util.dirselected);
                    Console.Clear();
                    Menus.Goodbye();
                    break;
                case opcao.Atualizar:
                    Util.FullClean(Util.util.dirselected);
                    Util.InstallValheimPlus();
                    Menus.UpdateMessage(); 
                    break;
            }
        }


    }
}
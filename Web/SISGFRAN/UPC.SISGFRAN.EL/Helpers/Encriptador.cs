using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Helpers
{
    public class Encriptador
    {
        private long RandSeed;
        private int LONG_MATRIZ;
        private int MAX_LONG_CLAVE;
        private string alfabeto;
        //String val;


        public Encriptador()
        {
            RandSeed = 0L;
            LONG_MATRIZ = 6;
            MAX_LONG_CLAVE = 30;
            alfabeto = " ABCDEFGHIJKLMNCOPQRSTUVWXYZ1234567890_$#";

        }

        private long random(long max)
        {
            long x = 0x100000000L;
            RandSeed *= 0x8088405L;
            RandSeed++;
            RandSeed = RandSeed % x;
            double i = (double)RandSeed / 4294967295D;
            long fynal = (long)((double)max * i);
            return fynal;

        }

        private long random1(long max)
        {
            long x = 0x100000000L;
            RandSeed *= 0x8088405L;
            RandSeed++;
            RandSeed = RandSeed % x;
            double i = (double)RandSeed / 4294967295D;
            long fynal = (long)((double)max * i);
            return fynal;
        }

        private String calvaltotsga(int codigo, String contrasenna)
        {
            String salida = "";
            int[,] matriz = new int[LONG_MATRIZ + 1, MAX_LONG_CLAVE + 1];
            if (codigo != 0x2d78539)
            {
                return "XXX";
            }

            int longitud_alfabeto = alfabeto.Length - 1;
            int longitud_contrasenna = contrasenna.Length - 1;
            int semilla = 0;

            for (int i = 1; i <= LONG_MATRIZ; i++)
            {
                for (int j = 1; j <= MAX_LONG_CLAVE; j++)
                {
                    matriz[i, j] = 0;
                }
            }

            for (int i = 0; i <= longitud_contrasenna; i++)
            {
                int numascii = (int)((contrasenna[i]));
                matriz[1, (i + 1)] = numascii;
                semilla += numascii * ((longitud_contrasenna - i) + 1);

                if (numascii % 2 == 1)
                {
                    semilla++;
                }
            }

            RandSeed = semilla;

            for (int i = 2; i <= LONG_MATRIZ - 2; i++)
            {
                for (int j = 1; j <= MAX_LONG_CLAVE; j++)
                {
                    matriz[i, j] = (int)random(256L);
                }
            }

            for (int i = 2; i <= LONG_MATRIZ - 2; i++)
            {
                for (int j = 1; j <= MAX_LONG_CLAVE; j++)
                {
                    if (i == 2)
                    {
                        matriz[(LONG_MATRIZ - 1), j] = matriz[i, j];
                    }
                    else
                    {
                        matriz[(LONG_MATRIZ - 1), j] = matriz[(LONG_MATRIZ - 1), j] ^ matriz[i, j];
                    }

                }
            }

            for (int j = 1; j <= MAX_LONG_CLAVE; j++)
            {
                matriz[(LONG_MATRIZ - 1), j] = matriz[(LONG_MATRIZ - 1), j] ^ matriz[1, j];
                RandSeed = matriz[(LONG_MATRIZ - 1), j];
                matriz[LONG_MATRIZ, j] = (int)random(longitud_alfabeto) + 1;
            }

            for (int j = 1; j <= MAX_LONG_CLAVE; j++)
            {
                salida = salida + alfabeto[matriz[LONG_MATRIZ, j]];
            }
            return salida;
        }

        public static String Encripta(String clave)
        {
            Encriptador encriptar = new Encriptador();
            return encriptar.calvaltotsga(0x2d78539, clave);
        }
    }
}

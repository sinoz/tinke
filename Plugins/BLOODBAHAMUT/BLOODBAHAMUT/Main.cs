﻿/*
 * Copyright (C) 2011  pleoNeX
 *
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <http://www.gnu.org/licenses/>. 
 *
 * Programador: pleoNeX
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using PluginInterface;

namespace BLOODBAHAMUT
{
    public class Main : IGamePlugin
    {
        IPluginHost pluginHost;
        String gameCode;

        public void Initialize(IPluginHost pluginHost, string gameCode)
        {
            this.pluginHost = pluginHost;
            this.gameCode = gameCode;
        }
        public bool IsCompatible()
        {
            if (gameCode == "CYJJ")
                return true;

            return false;
        }

        public Format Get_Format(string fileName, byte[] magic, int id)
        {
            if (fileName.ToUpper().EndsWith(".DPK"))
                return Format.Pack;

            return Format.Unknown;
        }

        public string Pack(sFolder unpacked, string file, int id)
        {
            if (file.ToUpper().EndsWith(".DPK"))
                return DPK.Pack(unpacked, file, id);

            return null;
        }
        public sFolder Unpack(string file, int id)
        {
            if (file.ToUpper().EndsWith(".DPK"))
                return DPK.Unpack(file, pluginHost);

            return new sFolder();
        }

        public void Read(string file, int id)
        {
        }

        public System.Windows.Forms.Control Show_Info(string file, int id)
        {
            return new System.Windows.Forms.Control();
        }
    }
}
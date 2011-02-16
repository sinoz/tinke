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
 * Programa utilizado: SharpDevelop
 * Fecha: 16/02/2011
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PluginInterface;

namespace Nintendo
{
	/// <summary>
	/// Archivo de entrada al módulo.
	/// </summary>
	public class Main : IPlugin
	{
		IPluginHost pluginHost;
		
		public void Inicializar(IPluginHost pluginHost)
		{
			this.pluginHost = pluginHost;
		}
		
		public Formato Get_Formato(string nombre, byte[] magic)
		{
			if (nombre.EndsWith(".nbfp"))
				return Formato.Paleta;
			else if (nombre.EndsWith(".nbfc"))
				return Formato.Imagen;
			else if (nombre.EndsWith(".nbfs"))
				return Formato.Screen;
			
			return Formato.Desconocido;
		}
		
		public Control Show_Info(string archivo)
		{
			if (archivo.ToUpper().EndsWith(".NBFC"))
			{
				new nbfc(pluginHost, archivo).Leer();

				if (pluginHost.Get_NCLR().cabecera.file_size != 0x00)
				{
					if (pluginHost.Get_NSCR().cabecera.file_size != 0x00)
					{
						NCGR nuevo = pluginHost.Get_NCGR();
						nuevo.rahc.tileData = pluginHost.Transformar_NSCR(pluginHost.Get_NSCR(), nuevo.rahc.tileData);
						pluginHost.Set_NCGR(nuevo);
					}
					return new iNCGR(pluginHost);
				}
			}
			
			return new Control();
		}
		
		public void Leer(string archivo)
		{
			if (archivo.ToUpper().EndsWith(".NBFP"))
				new nbfp(pluginHost, archivo).Leer();
			if (archivo.ToUpper().EndsWith(".NBFC"))
				new nbfc(pluginHost, archivo).Leer();
			if (archivo.ToUpper().EndsWith(".NBFS"))
				new nbfs(pluginHost, archivo).Leer();
		}
	}
}
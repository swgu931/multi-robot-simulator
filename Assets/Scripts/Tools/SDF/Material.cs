/*
 * Copyright (c) 2020 LG Electronics Inc.
 *
 * SPDX-License-Identifier: MIT
 */

using System.Xml;
using System;

namespace SDF
{
	public class Color
	{
		public double R = 0.0;
		public double G = 0.0;
		public double B = 0.0;
		public double A = 1.0;

		public void SetByString(string value)
		{
			if (string.IsNullOrEmpty(value))
				return;

			value = value.Trim();

			var tmp = value.Split(' ');

			if (tmp.Length != 4)
				return;

			R = (double)Convert.ChangeType(tmp[0], TypeCode.Double);
			G = (double)Convert.ChangeType(tmp[1], TypeCode.Double);
			B = (double)Convert.ChangeType(tmp[2], TypeCode.Double);
			A = (double)Convert.ChangeType(tmp[3], TypeCode.Double);
		}

		public Color()
		: this(0.0, 0.0, 0.0, 1.0)
		{}

		public Color(double r, double g, double b, double a)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}
	}

	public class Material : Entity
	{
		// <script> : TBD
		// <shader> : TBD
		// <lighting> : TBD
		public Color ambient = null;
		public Color diffuse = null;
		public Color specular = null;
		public Color emissive = null;

		public Material(XmlNode _node)
			: base(_node)
		{
			ParseElements();
		}

		protected override void ParseElements()
		{
			if (root == null)
				return;

			if (IsValidNode("ambient"))
			{
				ambient = new Color();
				ambient.SetByString(GetValue<string>("ambient"));
			}

			if (IsValidNode("diffuse"))
			{
				diffuse = new Color();
				diffuse.SetByString(GetValue<string>("diffuse"));
			}

			if (IsValidNode("specular"))
			{
				specular = new Color();
				specular.SetByString(GetValue<string>("specular"));
			}

			if (IsValidNode("emissive"))
			{
				emissive = new Color();
				emissive.SetByString(GetValue<string>("emissive"));
			}
		}
	}
}
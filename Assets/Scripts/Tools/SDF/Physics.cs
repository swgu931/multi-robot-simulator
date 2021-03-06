/*
 * Copyright (c) 2020 LG Electronics Inc.
 *
 * SPDX-License-Identifier: MIT
 */

using System.Xml;

namespace SDF
{
	public class Physics : Entity
	{
		// <defualt> : attribute will be ignored.
		private double max_step_size = 0.001;
		private double real_time_factor = 1.0;
		private double real_time_update_rate = 1000.0;
		private int max_contacts = 20;

		// <dart> : TBD
		// <simbody> : TBD
		// <bullet> : TBD
		// <ode> - solver
		//       - constraints
		// <PhysX> : TBD - new feature??

		public Physics(XmlNode _node)
			: base(_node)
		{
			ParseElements();
		}

		protected override void ParseElements()
		{
			if (root == null)
				return;

			max_step_size = GetValue<double>("max_step_size");
			real_time_factor = GetValue<double>("real_time_factor");
			real_time_update_rate = GetValue<double>("real_time_update_rate");
			max_contacts = GetValue<int>("max_contacts");

			// Console.WriteLine("[{0}] {1} {2} {3} {4}", GetType().Name,
			// 	isStatic, isSelfCollide, allowAutoDisable, enableWind);
		}
	}
}
/*
 * Copyright (c) 2020 LG Electronics Inc.
 *
 * SPDX-License-Identifier: MIT
 */

using System.Collections.Generic;
using System;

namespace SDF
{
	public class SensorType
	{
		public string name = string.Empty;
		public string type = string.Empty;
		protected Pose<double> pose = new Pose<double>(0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
		protected string relative_to = string.Empty;

		public Pose<double> Pose => pose;

		public void ParsePose(in string poseString, in string relativeTo = "")
		{
			relative_to = (string.IsNullOrEmpty(relativeTo))? string.Empty:relativeTo;

			if (!string.IsNullOrEmpty(poseString))
			{
				// x y z roll pitch yaw
				var poseInfo = poseString.Split(' ');

				pose.Pos.X = Convert.ToDouble(poseInfo[0]);
				pose.Pos.Y = Convert.ToDouble(poseInfo[1]);
				pose.Pos.Z = Convert.ToDouble(poseInfo[2]);
				pose.Rot.Roll = Convert.ToDouble(poseInfo[3]);
				pose.Rot.Pitch = Convert.ToDouble(poseInfo[4]);
				pose.Rot.Yaw = Convert.ToDouble(poseInfo[5]);

				// Console.WriteLine("Pose {0} {1} {2} {3} {4} {5}",
				// 	pose.Pos.X, pose.Pos.Y, pose.Pos.Z,
				// 	pose.Rot.Roll, pose.Rot.Pitch, pose.Rot.Yaw
				// );
			}
		}
	}

	public class Noise
	{
		public string type = "none";
		public double mean = 0;
		public double stddev = 0;
		public double bias_mean = 0;
		public double bias_stddev = 0;
		public double dynamic_bias_stddev = 0;
		public double dynamic_bias_correlation_time = 0;
		public double precision = 0;
	}

	// <altimeter> : TBD

	public class Cameras : SensorType
	{
		public List<Camera> list = new List<Camera>();
	}

	public class Camera : SensorType
	{
		public class Clip
		{
			public Clip() {}

			public Clip(in double near, in double far)
			{
				this.near = 0.1f;
				this.far = 0.1f;
			}

			public double near = 0.1f;
			public double far = 100f;
		}

		public class Noise
		{
			public string type = "gaussian";
			public double mean = 0;
			public double stddev = 0;
		}

		public class Distortion
		{
			public double k1 = 0;
			public double k2 = 0;
			public double k3 = 0;
			public double p1 = 0;
			public double p2 = 0;
			public SDF.Vector2<double> center = new SDF.Vector2<double>(0.0f, 0.0f);
		}

		public class Lens
		{
			public class CustomFunction
			{
				public double c1 = 1;
				public double c2 = 1;
				public double c3 = 0;
				public double f = 1;
				public string fun = "tan";
			}

			public class Intrinsics
			{
				public double fx = 277;
				public double fy = 277;
				public double cx = 160;
				public double cy = 120;
				public double s = 0;
			}

			public string type = "stereographic";
			public bool scale_to_hfov = true;
			public CustomFunction custom_function = new CustomFunction();
			public double cutoff_angle = 1.5707f;
			public int env_texture_size = 256;

			public Intrinsics intrinsics = new Intrinsics();
		}

		public double horizontal_fov = 1.047f;

		public int image_width = 320;
		public int image_height = 240;
		public string image_format = "R8G8B8";

		public Clip clip = new Clip(1, 10);

		public bool save_enabled = false;
		public string save_path = string.Empty;

		public string depth_camera_output = string.Empty;
		public Clip depth_camera_clip = new Clip(1, 100);

		public Noise noise = new Noise();

		public Distortion distortion = new Distortion();

		public Lens lens = new Lens();
	}

	// <contact> : TBD
	// <gps> : TBD

	public class IMU : SensorType
	{
		public class OrientationReferenceFrame
		{
			public string localization = "CUSTOM";
			public SDF.Vector3<double> custom_rpy;
			public string custom_rpy_parent_frame;
			public SDF.Vector3<int> grav_dir_x;
			public string grav_dir_x_parent_frame;
		}

		public OrientationReferenceFrame orientation_reference_frame = new OrientationReferenceFrame();

		public Noise angular_velocity_x = new Noise();
		public Noise angular_velocity_y = new Noise();
		public Noise angular_velocity_z = new Noise();
		public Noise linear_acceleration_x = new Noise();
		public Noise linear_acceleration_y = new Noise();
		public Noise linear_acceleration_z = new Noise();
	}


	// <logical_camera> : TBD

	public class Magnetometer : SensorType
	{
		public Noise x = new Noise();
		public Noise y = new Noise();
		public Noise z = new Noise();
	}

	public class Ray : SensorType
	{
		public class Scan
		{
			public Scan(uint samples = 640)
			{
				this.samples = samples;
			}
			public uint samples;
			public double resolution = 1;
			public double min_angle = 0.0;
			public double max_angle = 0.0;
		}

		public Scan horizontal = new Scan(640);
		public Scan vertical = new Scan(1);

		public double range_min = 0.0;
		public double range_max = 0.0;
		public double range_resolution = 0.0;

		public string noise_type = "gaussian";
		public double noise_mean = 0.0;
		public double noise_stddev = 0.0;
	}

	// <rfidtag> : TBD
	// <rfid> : TBD

	public class Sonar: SensorType
	{
		public string geometry = "cone";
		public double min = 0;
		public double max = 1;
		public double radius = 0.5f;
	}

	// <transceiver> : TBD
	// <force_torque> : TBD
}
// <auto-generated>
//   This file was generated by a tool; you should avoid making direct changes.
//   Consider using 'partial classes' to extend these types
//   Input: wind.proto
// </auto-generated>

#pragma warning disable CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace gazebo.msgs
{

    [global::ProtoBuf.ProtoContract()]
    public partial class Wind : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"linear_velocity")]
        public Vector3d LinearVelocity { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"enable_wind")]
        public bool EnableWind
        {
            get { return __pbn__EnableWind.GetValueOrDefault(); }
            set { __pbn__EnableWind = value; }
        }
        public bool ShouldSerializeEnableWind() => __pbn__EnableWind != null;
        public void ResetEnableWind() => __pbn__EnableWind = null;
        private bool? __pbn__EnableWind;

    }

}

#pragma warning restore CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192

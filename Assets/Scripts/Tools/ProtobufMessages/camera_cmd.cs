// <auto-generated>
//   This file was generated by a tool; you should avoid making direct changes.
//   Consider using 'partial classes' to extend these types
//   Input: camera_cmd.proto
// </auto-generated>

#pragma warning disable CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace gazebo.msgs
{

    [global::ProtoBuf.ProtoContract()]
    public partial class CameraCmd : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"follow_model")]
        [global::System.ComponentModel.DefaultValue("")]
        public string FollowModel
        {
            get { return __pbn__FollowModel ?? ""; }
            set { __pbn__FollowModel = value; }
        }
        public bool ShouldSerializeFollowModel() => __pbn__FollowModel != null;
        public void ResetFollowModel() => __pbn__FollowModel = null;
        private string __pbn__FollowModel;

    }

}

#pragma warning restore CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192